using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FrisbyBall.Models;

namespace FrisbyBall.Managers
{
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

        public MobileServiceClient CurrentClient
        {
            get { return client; }
        }

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
