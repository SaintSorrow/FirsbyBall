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
    public partial class PropertiesPage : ContentPage
    {

        public PropertiesPage()
        {
            InitializeComponent();
            Init();
        }

        async void Init()
        {   
            Lbl_Hello.TextColor = Constants.MainTextColor;
            Lbl_User.TextColor = Constants.MainTextColor;
            Lbl_User.Text = "Welcome " + Constants.LocalUser.UserName;
            LogoIcon.HeightRequest = Constants.LoginIconHeight;
        }

        public async void NewOponentProcedure(object e, EventArgs s)
        {
            await Navigation.PushModalAsync(new OpponentListViewPage(), false);
        }

        async void Logout(object sender, EventArgs e)
        {
            Constants.LocalUser = null;
            Constants.GoalLimit = 0;
            await Navigation.PopModalAsync(false);
        }

        async void StartMatchProcedure(object sender, EventArgs e)
        {
            if (Constants.opponent == null)
            {
                await DisplayAlert(Labels.NoOp, Labels.OpNotChosen, Labels.Ok);
            }
            else
            {
                await Navigation.PushModalAsync(new MatchPage(), false);
            }
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}