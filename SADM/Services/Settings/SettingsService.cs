using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SADM.Models;
using Xamarin.Forms;

namespace SADM.Services
{
    public class SettingsService : ISettingsService
    {
        #region [Setting Contants]
        const string APP_TOKEN = "apptoken";
        const string USER = "user";

        const string NAME = "name";
        const string EMAIL = "email";
        const string REMEMBER_ME = "rememberme";
        const string CONTRACT_LIST = "contractlist";
        #endregion

        protected User user;

        public string AppToken => Read<string>(APP_TOKEN);
        public async Task<bool> WriteAppTokenAsync(string token)
        {
            return await WriteAsync(APP_TOKEN, token);
        }


        public User User 
        {
            get
            {
                if(user is null)
                {
                    user = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(Read<string>(USER) ?? string.Empty);
                }
                return user;
            }
        }

        public async Task<bool> WriteUserAsync(User user)
        {
            this.user = user;
            return await WriteAsync(USER, user != null ? Newtonsoft.Json.JsonConvert.SerializeObject(user) : string.Empty);
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name => User.Name;//Read<string>(NAME) ?? "JUAN PEREZ";

        /// <summary>
        /// Gets the email.
        /// </summary>
        /// <value>The email.</value>
        public string Email => Read<string>(EMAIL);

        /// <summary>
        /// Gets the remember me.
        /// </summary>
        /// <value>The remember me.</value>
        public bool? RememberMe => Read<bool?>(REMEMBER_ME);

        /// <summary>
        /// Writes the name async.
        /// </summary>
        /// <returns>The name async.</returns>
        /// <param name="name">Name.</param>
        public async Task<bool> WriteNameAsync(string name)
        {
            user.Name = name;
            return await WriteUserAsync(user);
        }

        /// <summary>
        /// Writes the email async.
        /// </summary>
        /// <returns>The email async.</returns>
        /// <param name="email">Email.</param>
        public async Task<bool> WriteEmailAsync(string email)
        {
            return await WriteAsync(EMAIL, email);
        }

        /// <summary>
        /// Writes the remember me async.
        /// </summary>
        /// <returns>The remember me async.</returns>
        /// <param name="remember">Remember.</param>
        public async Task<bool> WriteRememberMeAsync(bool? remember)
        {
            return await WriteAsync(REMEMBER_ME, remember);
        }

        public async Task<bool> WriteSessionDataAsync(User user, string email, bool? remember)
        {
            return await WriteUserAsync(user) && 
                await WriteRememberMeAsync(remember) && 
                await WriteEmailAsync(remember.GetValueOrDefault() ? email : null);
        }

        /// <summary>
        /// Read the specified key.
        /// </summary>
        /// <returns>The read.</returns>
        /// <param name="key">Key.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public T Read<T>(string key)
        {
            return GetValueOrDefaultInternal(key, default(T));
        }

        /// <summary>
        /// Writes the value async.
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="key">Key.</param>
        /// <param name="value">Value.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public async Task<bool> WriteAsync<T>(string key, T value)
        {
            return await AddOrUpdateValueInternal(key, value);
        }

        /// <summary>
        /// Adds the or update value internal.
        /// </summary>
        /// <returns>The or update value internal.</returns>
        /// <param name="key">Key.</param>
        /// <param name="value">Value.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        protected async Task<bool> AddOrUpdateValueInternal<T>(string key, T value)
        {
            try
            {
                if (value == null)
                {
                    await RemoveAsync(key);
                }
                Application.Current.Properties[key] = value;
                await Application.Current.SavePropertiesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the value or default internal.
        /// </summary>
        /// <returns>The value or default internal.</returns>
        /// <param name="key">Key.</param>
        /// <param name="defaultValue">Default value.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        protected T GetValueOrDefaultInternal<T>(string key, T defaultValue = default(T))
        {
            object value = null;
            if (Application.Current.Properties.ContainsKey(key))
            {
                value = Application.Current.Properties[key];
            }
            return null != value ? (T)value : defaultValue;
        }

        /// <summary>
        /// Removes the async.
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="key">Key.</param>
        protected async Task RemoveAsync(string key)
        {
            if (Application.Current.Properties[key] != null)
            {
                Application.Current.Properties.Remove(key);
                await Application.Current.SavePropertiesAsync();
            }
        }

        public IList<Contract> ReadContractList()
        {
            IList<Contract> result = new List<Contract>();
            var json = Read<string>(CONTRACT_LIST);
            if(!string.IsNullOrEmpty(json))
            {
                result = Newtonsoft.Json.JsonConvert.DeserializeObject<IList<Contract>>(Read<string>(CONTRACT_LIST));
            }
            return result;
        } 

        public Task<bool> WriteContractListAsync(IList<Contract> contractList)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(contractList);
            return WriteAsync(CONTRACT_LIST, json);
        }
    }
}