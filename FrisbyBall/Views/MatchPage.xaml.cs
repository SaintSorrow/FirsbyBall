using FrisbyBall.Managers;
using FrisbyBall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FrisbyBall.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MatchPage : ContentPage
    {

        private event Action<User, User> MatchPlayedEvent;

        private List<User> userList;
        private UserManager manager;
        private MatchManager matchManager;
        private int awayGoalCount = 0;
        private int homeGoalCount = 0;

        public MatchPage()
        {
            InitializeComponent();
            Init();
        }

        async void Init()
        {
            try
            {
                manager = UserManager.DefaultManager;
                matchManager = MatchManager.DefaultManager;

                MatchPlayedEvent += new Action<User, User>(MatchPlayedEventHandler);

                Btn_HomeGoal.Text = Constants.LocalUser.UserName + " : " + homeGoalCount.ToString();
                Btn_AwayGoal.Text = Constants.opponent.UserName + " : " + awayGoalCount.ToString();
                Btn_HomeGoal.TextColor = Constants.MainTextColor;
                Btn_AwayGoal.TextColor = Constants.MainTextColor;

                userList = await manager.GetUsersAsync();
            }
            catch (Exception exc)
            {
                await DisplayAlert(Labels.Exc, exc.Message, Labels.Ok);
            }
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private async void MatchPlayedEventHandler(User win, User lost)
        {
            try
            {
                foreach (User user in userList)
                {
                    if (user.UserName == win.UserName)
                    {
                        user.Wins++;
                        await manager.SaveUserAsync(user);
                    }
                    else if (user.UserName == lost.UserName)
                    {
                        user.Loses++;
                        await manager.SaveUserAsync(user);
                    }
                }

                Match match = new Match
                {
                    PlayerWon = win.UserName,
                    PlayerLost = lost.UserName,
                    WinPoints = Constants.GoalLimit,
                    LostPoints = awayGoalCount
                };

                homeGoalCount = 0;
                awayGoalCount = 0;
                Constants.opponent = null;

                await matchManager.SaveMatchAsync(match);
            }
            catch (Exception exc)
            {
                await DisplayAlert(Labels.Exc, exc.Message, Labels.Ok);
            }
        }

        private async void HomeGoalProcedure()
        {
            try
            {
                homeGoalCount++;
                Btn_HomeGoal.Text = Constants.LocalUser.UserName + " : " + homeGoalCount.ToString();
                if (homeGoalCount >= Constants.GoalLimit)
                {
                    MatchPlayedEvent(Constants.LocalUser, Constants.opponent);
                    await DisplayAlert(Labels.Win, Labels.Player + Constants.LocalUser.UserName + Labels.WonGame, Labels.Ok);
                    await Navigation.PopModalAsync(false);
                }
            }
            catch (Exception exc)
            {
                await DisplayAlert(Labels.Exc, exc.Message, Labels.Ok);
            }
        }

        private async void AwayGoalProcedure()
        {
            try
            {
                awayGoalCount++;
                Btn_AwayGoal.Text = Constants.opponent.UserName + " : " + awayGoalCount.ToString();

                if (awayGoalCount >= Constants.GoalLimit)
                {
                    MatchPlayedEvent(Constants.opponent, Constants.LocalUser);
                    await DisplayAlert(Labels.Win, Labels.Player + Constants.opponent.UserName + Labels.WonGame, Labels.Ok);
                    await Navigation.PopModalAsync(false);
                }
            }
            catch (Exception exc)
            {
                await DisplayAlert(Labels.Exc, exc.Message, Labels.Ok);
            }
        }
    }
}