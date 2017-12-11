using FrisbyBall.Managers;
using FrisbyBall.Models;
using FrisbyBall.Validations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FrisbyBall.Controlers;

namespace FrisbyBall.Views
{
    /// <summary>
    /// This view is used for registrating new users for the application
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : ContentPage
    {
        UserManager manager;
        private RegistrationPageController registrationPageControler;

        public RegistrationPage()
        {
            InitializeComponent();
            Init();
        }

        /// <summary>
        /// Initializes graphics components and some data
        /// </summary>
        async void Init()
        {
            manager = UserManager.DefaultManager;

            BackgroundColor = Constants.BackgroundColor;
            Lbl_Username.TextColor = Constants.MainTextColor;
            Lbl_Password.TextColor = Constants.MainTextColor;
            Lbl_RepeatPassword.TextColor = Constants.MainTextColor;
            ActivitySpinner.IsVisible = false;
            LogoIcon.HeightRequest = Constants.LoginIconHeight;

            Entry_Username.Completed += (s, e) => Entry_Password.Focus();
            Entry_Password.Completed += (s, e) => Entry_RepeatPassword.Focus();
            Entry_RepeatPassword.Completed += (s, e) => Entry_Email.Focus();
            registrationPageControler = new RegistrationPageController();

        }

        /// <summary>
        /// This code is launched when registration  button is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
         async void RegisterProcedure(object sender, EventArgs e)
        {
            try
            {
                if (registrationPageControler.CheckInputAsync(Entry_Username.Text,Entry_Password.Text,Entry_Email.Text,Entry_RepeatPassword.Text))
                {
                    User user = new User
                    {
                        UserName = Entry_Username.Text,
                        Password = Entry_Password.Text,
                        Email = Entry_Email.Text,
                        MatchCount = 0,
                        Wins = 0,
                        Loses = 0,
                        Image = "astronaut.png"
                    };

                    List<User> userList = await manager.GetUsersAsync();

                    if(registrationPageControler.CheckIfExistsAsync(Constants.userList, user))
                    {
                        await manager.SaveUserAsync(user);
                        await DisplayAlert(Labels.Info, Labels.RegSucc, Labels.Ok);
                        Constants.userList = await manager.GetUsersAsync();
                        await Navigation.PopModalAsync();
                    }

                }
            }
            catch (Exception exc)
            {
                await DisplayAlert(Labels.Exc, exc.Message, Labels.Ok);
            }
        }

      

    }
}