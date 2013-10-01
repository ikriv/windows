using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace IKriv.Windows.Async
{
    public abstract class AsyncCommandBase : IAsyncCommand
    {
        private int _runCount;
        private bool _isRunning;

        protected abstract bool CanExecuteImpl(object parameter);
        protected abstract IAsyncOperation CreateOperation(object parameter);

        
        public bool IsRunning
        {
            get { lock(this) return _isRunning; }
        }

        private void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        private void BeginRun()
        {
            lock (this)
            {
                ++_runCount;
                _isRunning = true;
            }

            RaisePropertyChanged("IsRunning");
        }

        private void EndRun()
        {
            lock (this)
            {
                --_runCount;
                _isRunning = (_runCount > 0);
            }
            RaisePropertyChanged("IsRunning");
        }

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecuteImpl(parameter);
        }

        public event EventHandler CanExecuteChanged;

        void ICommand.Execute(object parameter)
        {
            BeginRun();

            var operation = CreateOperation(parameter);
            var task = new Task<object>(() => operation.Action(parameter));
            task.ContinueWith(finished =>
                                    {
                                        EndRun();

                                        if (finished.IsFaulted)
                                        {
                                            operation.OnFailure(finished.Exception);
                                        }
                                        else
                                        {
                                            operation.OnSuccess(finished.Result);
                                        }
                                    },
                                TaskScheduler.FromCurrentSynchronizationContext());

            task.Start();
        }

        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null) CanExecuteChanged(this, EventArgs.Empty);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class AsyncCommand : AsyncCommandBase
    {
        public Func<IAsyncOperation> Operation { get; set; }
        public Func<bool> CanExecute { get; set; }

        public void Execute()
        {
            ((ICommand)this).Execute(null);
        }

        protected override bool CanExecuteImpl(object ignoredParameter)
        {
            if (CanExecute == null) return true;
            return CanExecute();
        }

        protected override IAsyncOperation CreateOperation(object parameter)
        {
            return Operation();
        }
    }

    public class AsyncCommand<TParam> : AsyncCommandBase
    {
        public Func<TParam, IAsyncOperation> Operation { get; set; }
        public Func<TParam, bool> CanExecute { get; set; }

        public void Execute(TParam parameter)
        {
            ((ICommand)this).Execute(parameter);
        }

        protected override bool CanExecuteImpl(object parameter)
        {
            if (CanExecute == null) return true;
            return CanExecute((TParam)parameter);
        }

        protected override IAsyncOperation CreateOperation(object parameter)
        {
            return Operation((TParam)parameter);
        }
    }
}
