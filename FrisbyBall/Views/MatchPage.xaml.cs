using FrisbyBall.Controlers;
using FrisbyBall.Managers;
using FrisbyBall.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FrisbyBall.Views
{
    /// <summary>
    /// A view where two players are playing a match and points are being counted
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MatchPage : ContentPage
    {
        private MatchPageController matchPageController = new MatchPageController();

        public MatchPage()
        {
            InitializeComponent();
            Init();
        }

        /// <summary>
        /// initializing graphical components and data
        /// </summary>
        async void Init()
        {
            try
            {
                Btn_HomeGoal.Text = Constants.LocalUser.UserName + " : " + Constants.homeGoalCount.ToString();
                Btn_AwayGoal.Text = Constants.opponent.UserName + " : " + Constants.awayGoalCount.ToString();
                Btn_HomeGoal.TextColor = Constants.MainTextColor;
                Btn_AwayGoal.TextColor = Constants.MainTextColor;
            }
            catch (Exception exc)
            {
                await DisplayAlert(Labels.Exc, exc.Message, Labels.Ok);
            }
        }

        /// <summary>
        /// this methos is launched then phisical back button is pressed on the phone
        /// you can't go back until there is a winner
        /// </summary>
        /// <returns>
        /// true - can't go back
        /// </returns>
        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        /// <summary>
        /// this method is launched when home player scores a goal 
        /// </summary>
        private void HomeGoalProcedure()
        {
            try
            {
                if (matchPageController.HomeGoalProceudre())
                {
                    DisplayAlert(Labels.Win, Labels.Player + Constants.LocalUser.UserName + Labels.WonGame, Labels.Ok);
                    Navigation.PopModalAsync(false);
                }
                Btn_HomeGoal.Text = Constants.LocalUser.UserName + " : " + Constants.homeGoalCount.ToString();
            }
            catch (Exception exc)
            {
                DisplayAlert(Labels.Exc, exc.Message, Labels.Ok);
            }
        }

        /// <summary>
        /// This method is launched when away player scores a goal
        /// </summary>
        private void AwayGoalProcedure()
        {
            try
            {
                if (matchPageController.AwayGoalProcedure())
                {
                    DisplayAlert(Labels.Win, Labels.Player + Constants.opponent.UserName + Labels.WonGame, Labels.Ok);
                    Navigation.PopModalAsync(false);
                }
                Btn_AwayGoal.Text = Constants.opponent.UserName + " : " + Constants.awayGoalCount.ToString();
            }
            catch (Exception exc)
            {
                DisplayAlert(Labels.Exc, exc.Message, Labels.Ok);
            }
        }
    }
}