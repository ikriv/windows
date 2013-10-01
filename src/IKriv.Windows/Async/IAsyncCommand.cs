using System.ComponentModel;
using System.Windows.Input;

namespace IKriv.Windows.Async
{
    public interface IAsyncCommand : ICommand, INotifyPropertyChanged
    {
        bool IsRunning { get; }
    }
}
