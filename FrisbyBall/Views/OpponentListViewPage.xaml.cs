using FrisbyBall.Managers;
using FrisbyBall.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FrisbyBall.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OpponentListViewPage : ContentPage
    {
        private User selectedUser;
        private List<Match> matchList = new List<Match>();
        private UserManager manager;
        private MatchManager matchManager;

        public OpponentListViewPage()
        {
            InitializeComponent();

            Init();
        }

        /// <summary>
        /// Initializes data ant managers
        /// </summary>
        async void Init()
        {
            try
            {
                MyListView.ItemsSource = Constants.opponentList;
                manager = UserManager.DefaultManager;
                matchManager = MatchManager.DefaultManager;
                matchList = await matchManager.GetMatchListAsync();

                MyListView.ItemsSource = opponentList;
            }
            catch (Exception exc)
            {
                await DisplayAlert(Labels.Exc, exc.Message, Labels.Ok);
            }
        }

        /// <summary>
        /// selects user when tapped or deselects if same user is tapped
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (selectedUser == e.Item as User)
            {
                ((ListView)sender).SelectedItem = null;
            }
            else
            {
                selectedUser = e.Item as User;
            }
        }

        /// <summary>
        /// player is chosed as an opponent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ChooseOpponent(object sender, EventArgs e)
        {
            try
            {
                if(selectedUser.Id == Constants.LocalUser.Id)
                {
                    DisplayAlert(Labels.Info, Labels.CannotChooseYourself, Labels.Ok);
                }
                else if (selectedUser != null)
                {
                    Constants.opponent = selectedUser;
                    Navigation.PopModalAsync(false);
                }
                else
                {
                    DisplayAlert(Labels.Info, Labels.PlayerNotSelect, Labels.Ok);
                }
            }
            catch (Exception exc)
            {
                DisplayAlert(Labels.Exc, exc.Message, Labels.Ok);
            }
        }

        /// <summary>
        /// Here we can see basic  information of player in DisplayAlert object
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void DisplayPlayerInfo(object sender, EventArgs e)
        {
            try
            {
                if (selectedUser == null)
                {
                    await DisplayAlert(Labels.Failed, Labels.OpNotSelect, Labels.Ok);
                }
                else
                {
                    await DisplayAlert(Labels.Info, $"Username : {selectedUser.UserName}" +
                                                    $"\nEmail: {selectedUser.Email}" +
                                                    $"\nW/L :{selectedUser.Wins}/{selectedUser.Loses}", Labels.Ok);
                }
            }
            catch(Exception exc)
            {
                await DisplayAlert(Labels.Exc, exc.Message, Labels.Ok);
            }
        }

        /// <summary>
        /// Displays selected user match history
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void MatchHistory(object sender, EventArgs e)
        {
            try
            {
                if (selectedUser == null)
                {
                    await DisplayAlert(Labels.Failed, Labels.OpNotSelect, Labels.Ok);
                }
                else
                {
                    var userMatches = matchList.Where(_match => _match.PlayerLost == selectedUser.UserName ||
                                                            _match.PlayerWon == selectedUser.UserName).ToList();
          
                    Constants.userMatches = userMatches;
                    await Navigation.PushModalAsync(new StatisticsPage(), false);
                }
            }
            catch (Exception exc)
            {
                await DisplayAlert(Labels.Exc, exc.Message, Labels.Ok);
            }
        }

        /// <summary>
        /// This one is used when physical back button is pressed on mobile phone. It disables Navigation animation
        /// </summary>
        /// <returns></returns>
        protected override bool OnBackButtonPressed()
        {
            Navigation.PopModalAsync(false);
            return true;
        }
    }
}