using FrisbyBall.Managers;
using FrisbyBall.Models;
using FrisbyBall.Validations;
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
    public partial class RegistrationPage : ContentPage
    {
        UserManager manager;

        public RegistrationPage()
        {
            InitializeComponent();
            Init();
        }

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
        }

         async void RegisterProcedure(object sender, EventArgs e)
        {
            try
            {
                if (await CheckInputAsync())
                {
                    User user = new User
                    {
                        UserName = Entry_Username.Text,
                        Password = Entry_Password.Text,
                        Email = Entry_Email.Text,
                        MatchCount = 0,
                        Wins = 0,
                        Loses = 0
                    };

                    List<User> userList = await manager.GetUsersAsync();

                    if(await checkIfExistsAsync(userList, user))
                    {
                        await manager.SaveUserAsync(user);
                        await DisplayAlert(Labels.Info, Labels.RegSucc, Labels.Ok);
                        await Navigation.PopModalAsync();
                    }

                }
            }
            catch (Exception exc)
            {
                await DisplayAlert(Labels.Exc, exc.Message, Labels.Ok);
            }
        }

        async Task<bool> CheckInputAsync()
        {
            
            if (Validation.UsernamePatternMatch(Entry_Username.Text) &&
                Validation.PasswordPatternMatch(Entry_Password.Text) &&
                Validation.EmailPatternMatch(Entry_Email.Text)       &&
                Validation.PasswordPatternMatch(Entry_RepeatPassword.Text))
            {
                if (Entry_Password.Text == Entry_RepeatPassword.Text)
                {
                    return true;
                }
                else
                {
                    await DisplayAlert(Labels.Info, Labels.PassNotMatch, Labels.Ok);
                    return false;
                }
            }
            else
            {
                await DisplayAlert(Labels.Info, Labels.NoMatch, Labels.Ok);
                return false;
            }
        }

        async Task<bool> checkIfExistsAsync(List<User> _userList, User _user)
        {
            foreach (User user in _userList)
            {
                if (user.UserName == _user.UserName)
                {
                    await DisplayAlert(Labels.Info, Labels.UserExists, Labels.Ok);
                    return false;
                }
            }

            return true;
        }

    }
}