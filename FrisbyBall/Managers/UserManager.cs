using FrisbyBall.Models;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace FrisbyBall.Managers
{
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

        public MobileServiceClient CurrentClient
        {
            get { return client; }
        }

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
