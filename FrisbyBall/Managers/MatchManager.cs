using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FrisbyBall.Models;

namespace FrisbyBall.Managers
{
    /// <summary>
    /// Manager for matches, uses azure mobile apps, easy tables
    /// </summary>
    public partial class MatchManager
    {
        static MatchManager defaultInstance = new MatchManager();
        MobileServiceClient client;
        IMobileServiceTable<Models.Match> matchTable;

        private MatchManager()
        {
            this.client = new MobileServiceClient("https://frisbyball.azurewebsites.net");

            this.matchTable = client.GetTable<Models.Match>();
        }

        /// <summary>
        /// return default isntance of the mananger
        /// </summary>
        public static MatchManager DefaultManager
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
        /// return current client
        /// </summary>
        public MobileServiceClient CurrentClient
        {
            get { return client; }
        }

        /// <summary>
        /// Gets match list from azure tables in the cloud
        /// </summary>
        /// <returns>
        /// return a list of matches from azure
        /// </returns>
        public async Task<List<Models.Match>> GetMatchListAsync()
        {
            try
            {
                IEnumerable<Models.Match> matches = await matchTable.ToEnumerableAsync();
                return new List<Models.Match>(matches);
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
        /// saves match if it does not exist or updates it if it exists
        /// </summary>
        /// <param name="match">
        /// match which need to be saved or updated
        /// </param>
        /// <returns>
        /// return void
        /// </returns>
        public async Task SaveMatchAsync(Models.Match match)
        {
            if (match.Id == null)
            {
                await matchTable.InsertAsync(match);
            }
            else
            {
                await matchTable.UpdateAsync(match);
            }
        }
    }
}
