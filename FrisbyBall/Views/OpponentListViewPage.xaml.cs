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
        private List<User> userList;
        private List<User> opponentList = new List<User>();
        private List<Match> matchList = new List<Match>();
        private UserManager manager;
        private MatchManager matchManager;

        public OpponentListViewPage()
        {
            InitializeComponent();

            Init();
        }

        async void Init()
        {
            try
            {
                manager = UserManager.DefaultManager;
                userList = await manager.GetUsersAsync();
                matchManager = MatchManager.DefaultManager;
                matchList = await matchManager.GetMatchListAsync();

                foreach (User user in userList)
                {
                        opponentList.Add(user);
                }

                MyListView.ItemsSource = opponentList;
            }
            catch (Exception exc)
            {
                await DisplayAlert(Labels.Exc, exc.Message, Labels.Ok);
            }
        }

        void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            selectedUser = e.Item as User;
            //Deselect Item
            //((ListView)sender).SelectedItem = null;
        }

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
    }
}