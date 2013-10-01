using System;

namespace IKriv.Windows.Async
{
    /// <summary>
    /// This is a single instance of outstanding operation
    /// </summary>
    public class AsyncOperation<TParam, TResult> : IAsyncOperation
    {
        public Func<TParam, TResult> Action { get; set; }
        public Action<TResult> Success { get; set; }
        public Action<Exception> Failure { get; set; }

        object IAsyncOperation.Action(object parameter)
        {
            return Action((TParam)parameter);
        }

        public void OnSuccess(object result)
        {
            if (Success != null) Success((TResult) result);
        }

        public void OnFailure(Exception ex)
        {
            if (Failure != null) Failure(ex);
        }
    }

    /// <summary>
    /// This is a single instance of outstanding operation
    /// </summary>
    public class AsyncOperation<T> : IAsyncOperation
    {
        public Func<T> Action { get; set; }
        public Action<T> Success { get; set; }
        public Action<Exception> Failure { get; set; }

        object IAsyncOperation.Action(object ignoredParameter)
        {
            return Action();
        }

        public void OnSuccess(object result)
        {
            if (Success != null) Success((T) result);
        }

        public void OnFailure(Exception ex)
        {
            if (Failure != null) Failure(ex);
        }
    }

    public class AsyncOperation : IAsyncOperation
    {
        public Action Action { get; set; }
        public Action Success { get; set; }
        public Action<Exception> Failure { get; set; }

        object IAsyncOperation.Action(object ignoredPaameter)
        {
            Action();
            return null;
        }

        public void OnSuccess(object result)
        {
            if (Success != null) Success();
        }

        public void OnFailure(Exception ex)
        {
            if (Failure != null) Failure(ex);
        }
    }
}
