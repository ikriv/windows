using System;

namespace IKriv.Windows.Async
{
    public interface IAsyncOperation
    {
        object Action(object parameter);
        void OnSuccess(object result);
        void OnFailure(Exception ex);
    }
}
