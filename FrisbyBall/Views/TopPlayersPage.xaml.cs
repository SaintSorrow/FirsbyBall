using FrisbyBall.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FrisbyBall.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TopPlayersPage : ContentPage
    {
        private List<User> mostWinBuffer;
        private List<User> bestPercentageBuffer;
        private List<User> tempBuffer;
        private User tempUser = new User();
        private decimal winRate = 0;
        private decimal topWinRate = 0;
        public TopPlayersPage()
        {
            InitializeComponent();
            Init();
        }

        void Init()
        {
            try
            {
                tempBuffer = Constants.userList;
                for (int i = 0; i <= 2; i++)
                {
                    foreach (User user in tempBuffer)
                    {
                        if (user.Wins > tempUser.Wins)
                        {
                            tempUser = user;
                        }
                    }
                    mostWinBuffer.Add(tempUser);
                    tempBuffer.Remove(tempUser);
                    tempUser = null;
                }

                tempBuffer = Constants.userList;
                for (int i = 1; i <= 2; i++)
                {
                    foreach (User user in tempBuffer)
                    {
                        winRate = (user.Wins / (user.Loses + user.Wins)) * 100;
                        if (winRate > topWinRate)
                        {
                            topWinRate = winRate;
                            tempUser = user;
                        }
                        winRate = 0;
                    }
                    bestPercentageBuffer.Add(tempUser);
                    tempBuffer.Remove(tempUser);
                    tempUser = null;
                    topWinRate = 0;
                }

                TopWinsListView.ItemsSource = mostWinBuffer;
                TopPercentageListView.ItemsSource = bestPercentageBuffer;
            }
            catch (Exception exc)
            {
                DisplayAlert(Labels.Exc, exc.Message, Labels.Ok);
            }
        }
    }
}