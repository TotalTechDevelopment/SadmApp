using System;
namespace SADM.Helpers
{
    public interface ICanExecuteChanged
    {
        event EventHandler CanExecuteChanged;
        void OnCanExecuteChanged();
    }
}
