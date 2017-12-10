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
    /// Main view of the application, this view is launched when application starts
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private UserManager manager;

        public LoginPage()
        {
            InitializeComponent();
            Init();
        }

        /// <summary>
        /// Graphical components are initialized here and data loaded
        /// </summary>
        async void Init()
        {
            Lbl_Username.TextColor = Constants.MainTextColor;
            Lbl_Password.TextColor = Constants.MainTextColor;
            LogoIcon.HeightRequest = Constants.LoginIconHeight;

            Entry_Username.Completed += (s, e) => Entry_Password.Focus();

            manager = UserManager.DefaultManager;
            Constants.userList = await manager.GetUsersAsync();
        }

        /// <summary>
        /// This methed is launched when registration button is pressed, signs in player or not
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void SignInProcedure(object sender, EventArgs e)
        {
            try
            {
                User user = new User
                {
                    UserName = Entry_Username.Text,
                    Password = Entry_Password.Text
                };

                var LogUser = Constants.userList.FirstOrDefault(_user => user.UserName == _user.UserName
                                                            && user.Password == _user.Password);

                if (LogUser != null)
                {
                    Constants.LocalUser = LogUser;
                    Entry_Password.Text = "";
                    Entry_Username.Text = "";
                    await DisplayAlert(Labels.Login, Labels.LoginSucc, Labels.Ok);
                    await Navigation.PushModalAsync(new PropertiesPage(), false);
                }
                else
                {
                    await DisplayAlert(Labels.Failed, Labels.UserNotExists, Labels.Ok);
                }
            }
            catch (Exception exc)
            {
                await DisplayAlert(Labels.Exc, exc.Message, Labels.Ok);
            }
        }

        /// <summary>
        /// This method is launched when registration  button is launched
        /// RegistrationPage is pushed on top of navigation stack
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="s"></param>
        void RegisterProcedure(object sender, EventArgs s)
        {
            Entry_Username.Text = "";
            Entry_Password.Text = "";
            Navigation.PushModalAsync(new RegistrationPage(), false);
        }
    }
}