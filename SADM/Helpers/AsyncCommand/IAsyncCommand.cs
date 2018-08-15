using System.Threading.Tasks;
using System.Windows.Input;

namespace SADM.Helpers
{
    public interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync(object parameter);
    }
}
