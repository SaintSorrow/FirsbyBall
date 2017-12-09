using FrisbyBall.Managers;
using FrisbyBall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FrisbyBall.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StatisticsPage : ContentPage
    {
        private MatchManager matchManager;
        private List<Match> matchList = new List<Match>();
        //private List<Match> userMatches = new List<Match>();

        public StatisticsPage()
        {
            InitializeComponent();
            Init();
        }

        async void Init()
        {
            try
            {
                MyListView.ItemsSource = Constants.userMatches;
            }
            catch (Exception exc)
            {
                await DisplayAlert(Labels.Exc, exc.Message, Labels.Ok);
            }
        }

    }
}
