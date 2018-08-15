using System.Threading.Tasks;
using SADM.Models.Requests;
using SADM.Models.Responses;

namespace SADM.Services
{
    /// <summary>
    /// Contract to Sadm API service.
    /// </summary>
    public interface ISadmApiService
    {
        Task<V> CallServiceAsync<U, V>(U request) where U : RequestBase where V : ResponseBase, new();
        Task<T> SimulateSuccessfulAsync<T>() where T : new();
    }
}
