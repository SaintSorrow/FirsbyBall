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
    /// <summary>
    /// Main page after user log in to the application
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PropertiesPage : ContentPage
    {

        public PropertiesPage()
        {
            InitializeComponent();
            Init();
        }

        /// <summary>
        /// initializes graphical components and data
        /// </summary>
        async void Init()
        {   
            Lbl_Hello.TextColor = Constants.MainTextColor;
            Lbl_User.TextColor = Constants.MainTextColor;
            Lbl_User.Text = "Welcome " + Constants.LocalUser.UserName;
            LogoIcon.HeightRequest = Constants.LoginIconHeight;

            Constants.opponentList = new List<User>();
            foreach (User user in Constants.userList)
            {
                if (user.UserName != Constants.LocalUser.UserName)
                {
                    Constants.opponentList.Add(user);
                }
            }
        }

        /// <summary>
        /// Pushes OpponentListViewPage on top of Navigation stack
        /// view for opponents
        /// </summary>
        /// <param name="e"></param>
        /// <param name="s"></param>
        public async void NewOponentProcedure(object e, EventArgs s)
        {
            await Navigation.PushModalAsync(new OpponentListViewPage(), false);
        }

        /// <summary>
        /// Clears some static variables and Pops view from navigation stack
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void Logout(object sender, EventArgs e)
        {
            Constants.LocalUser = null;
            Constants.opponentList = null;
            Constants.userMatches = null;
            await Navigation.PopModalAsync(false);
        }

        /// <summary>
        /// checks if you have choosen an opponent
        /// pushes MatchPage on top of navigation stack
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Method is launched when physical back button is pressed on the mobile phone
        /// can't go back
        /// </summary>
        /// <returns>
        /// true - can't go back
        /// </returns>
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}