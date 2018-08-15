using System.Collections.Generic;
using System.Threading.Tasks;
using SADM.Models;

namespace SADM.Services
{
    /// <summary>
    /// Contract for Settings service.
    /// </summary>
    public interface ISettingsService
    {
        string AppToken { get; }
        Task<bool> WriteAppTokenAsync(string token);
        User User { get; }
        Task<bool> WriteUserAsync(User user);

        /// <summary>
        /// Gets the Name.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; }

        /// <summary>
        /// Writes the Name async.
        /// </summary>
        /// <returns>The name async.</returns>
        /// <param name="name">Name.</param>
        Task<bool> WriteNameAsync(string name);

        /// <summary>
        /// Gets the email.
        /// </summary>
        /// <value>The email.</value>
        string Email { get; }

        /// <summary>
        /// Writes the email async.
        /// </summary>
        /// <returns>The email async.</returns>
        /// <param name="email">Email.</param>
        Task<bool> WriteEmailAsync(string email);

        /// <summary>
        /// Gets the remember me.
        /// </summary>
        /// <value>The remember me.</value>
        bool? RememberMe { get; }

        /// <summary>
        /// Writes the remember me async.
        /// </summary>
        /// <returns>The remember me async.</returns>
        /// <param name="remember">Remember.</param>
        Task<bool> WriteRememberMeAsync(bool? remember);


        Task<bool> WriteSessionDataAsync(User user, string email, bool? remember);

        /// <summary>
        /// Read the specified key.
        /// </summary>
        /// <returns>The read.</returns>
        /// <param name="key">Key.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        T Read<T>(string key);

        /// <summary>
        /// Writes the value async.
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="key">Key.</param>
        /// <param name="value">Value.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        Task<bool> WriteAsync<T>(string key, T value);

        IList<Contract> ReadContractList();
        Task<bool> WriteContractListAsync(IList<Contract> contractList);
    }
}
