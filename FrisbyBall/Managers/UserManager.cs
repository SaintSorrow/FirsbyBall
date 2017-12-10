using FrisbyBall.Models;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace FrisbyBall.Managers
{
    /// <summary>
    /// manager for working with user from the cloud storage
    /// </summary>
    public partial class UserManager
    {
        private static UserManager defaultInstance = new UserManager();
        MobileServiceClient client;
        IMobileServiceTable<User> userTable;


        private UserManager()
        {
            this.client = new MobileServiceClient("https://frisbyball.azurewebsites.net");

            this.userTable = client.GetTable<User>();
        }

        /// <summary>
        /// return default instance of the UserManager
        /// </summary>
        public static UserManager DefaultManager
        {
            get
            {
                return defaultInstance;
            }
            private set
            {
                defaultInstance = value;
            }
        }

        /// <summary>
        /// returns currentclient instance
        /// </summary>
        public MobileServiceClient CurrentClient
        {
            get { return client; }
        }

        /// <summary>
        /// uses azure cloud storage for retrieving a list of users
        /// </summary>
        /// <returns>
        /// list of users
        /// </returns>
        public async Task<List<User>> GetUsersAsync()
        {
            try
            {
                IEnumerable<User> users = await userTable.ToEnumerableAsync();
                return new List<User>(users);
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"Sync error: {0}", e.Message);
            }
            return null;
        }

        /// <summary>
        /// Inserts new user into the table or updates it if it already exists
        /// </summary>
        /// <param name="user">
        /// user which needs to be inserted or updated
        /// </param>
        /// <returns>
        /// returns void
        /// </returns>
        public async Task SaveUserAsync(User user)
        {
            if (user.Id == null)
            {
                await userTable.InsertAsync(user);
            }
            else
            {
                await userTable.UpdateAsync(user);
            }
        }
    }
}
