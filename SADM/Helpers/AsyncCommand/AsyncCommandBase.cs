using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SADM.Helpers
{
    public abstract class AsyncCommandBase : IAsyncCommand
    {
        private readonly ICanExecuteChanged canExecuteChanged;

        protected AsyncCommandBase(Func<object, ICanExecuteChanged> canExecuteChangedFactory)
        {
            canExecuteChanged = canExecuteChangedFactory(this);
        }

        public abstract Task ExecuteAsync(object parameter);

        protected abstract bool CanExecute(object parameter);

        protected void OnCanExecuteChanged()
        {
            canExecuteChanged.OnCanExecuteChanged();
        }

        event EventHandler ICommand.CanExecuteChanged
        {
            add { canExecuteChanged.CanExecuteChanged += value; }
            remove { canExecuteChanged.CanExecuteChanged -= value; }
        }

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute(parameter);
        }

        async void ICommand.Execute(object parameter)
        {
            await ExecuteAsync(parameter);
        }
    }
}
