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
    public partial class SettingsPage : TabbedPage
    {
        private Images selectedImage;
        private List<Images> Images;
        private UserManager userManager;
        public SettingsPage()
        {
            InitializeComponent();
            Init();
        }

        /// <summary>
        /// Initializes data and graphical components
        /// </summary>
        void Init()
        {
            try
            {
                userManager = UserManager.DefaultManager;
                Lbl_CurrentEmail.Text = Constants.LocalUser.Email;
                Images = new List<Images>()
                {
                    new Images{ Image = "astronaut.png", ImageName = "Astronaut"},
                    new Images{ Image = "batman.png", ImageName = "Batman"},
                    new Images{ Image = "charlesChaplin.png", ImageName = "Charles Chaplin"},
                    new Images{ Image= "geek.png", ImageName = "Geek"},
                    new Images{ Image = "yoda.png", ImageName = "Yoda"},
                    new Images{ Image = "pirate.png", ImageName = "Pirate"},
                    new Images{ Image = "PlayerIcon.png", ImageName = "Default icon"},
                    new Images{ Image = "predator.png", ImageName = "Predator"},
                    new Images{ Image = "punk.png", ImageName = "Punk"},
                    new Images{ Image = "serialKiller.png", ImageName = "Serial Killer"},
                    new Images{ Image = "soldier.png", ImageName = "Soldier"},
                    new Images{ Image = "StarWarsSoldier.png", ImageName = "Star Wars Soldier"}
               };

                LogoListview.ItemsSource = Images;
            }
            catch (Exception exc)
            {
                DisplayAlert(Labels.Exc, exc.Message, Labels.Ok);
            }
        }

        /// <summary>
        /// Checking if password is correct, if it macthes regex and if both new password entries are equal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ChangePasswordEvent(object sender, EventArgs e)
        {
            try
            {
                if (Entry_CurrentPassword.Text != Constants.LocalUser.Password)
                {
                    DisplayAlert(Labels.Info, Labels.IncorrectPass, Labels.Ok);
                }
                else if (Entry_NewPassword.Text != Entry_RepeatPassword.Text)
                {
                    DisplayAlert(Labels.Info, Labels.PassNotMatch, Labels.Ok);
                }
                else if (!Validation.PasswordPatternMatch(Entry_NewPassword.Text))
                {
                    DisplayAlert(Labels.Info, Labels.PassRegexFail, Labels.Ok);
                }
                else
                {
                    ChangePassword();
                }
            }
            catch (Exception exc)
            {
                DisplayAlert(Labels.Exc, exc.Message, Labels.Ok);
            }
        }

        /// <summary>
        /// Changes password for local user
        /// </summary>
        async void ChangePassword()
        {
            try
            {
                Constants.LocalUser.Password = Entry_NewPassword.Text;
                await userManager.SaveUserAsync(Constants.LocalUser);
                Entry_CurrentPassword.Text = "";
                Entry_NewPassword.Text = "";
                Entry_RepeatPassword.Text = "";
                Constants.userList = await userManager.GetUsersAsync();
                DisplayAlert(Labels.Info, Labels.PassChanged, Labels.Ok);
            }
            catch (Exception exc)
            {
                DisplayAlert(Labels.Exc, exc.Message, Labels.Ok);
            }
        }

        void ChangeEmailEvent(object sender, EventArgs e)
        {
            try
            {
                if (!Validation.EmailPatternMatch(Entry_NewEmail.Text))
                {
                    DisplayAlert(Labels.Info, Labels.EmailNotMatch, Labels.Ok);
                }
                else
                {
                    ChangeEmail();
                }
            }
            catch (Exception exc)
            {
                DisplayAlert(Labels.Exc, exc.Message, Labels.Ok);
            }
        }

        /// <summary>
        /// Changes local user email adress
        /// </summary>
        async void ChangeEmail()
        {
            Constants.LocalUser.Email = Entry_NewEmail.Text;
            await userManager.SaveUserAsync(Constants.LocalUser);
            Constants.userList = await userManager.GetUsersAsync();
            Entry_NewEmail.Text = "";
            DisplayAlert(Labels.Info, Labels.EmailChanged, Labels.Ok);
        }

        /// <summary>
        /// Disables navigation animation when poping view from modal stack
        /// </summary>
        /// <returns>
        /// true - disables gling back
        /// </returns>
        protected override bool OnBackButtonPressed()
        {
            Navigation.PopModalAsync(false);
            return true;
        }

        /// <summary>
        /// Selects tapped logo and deselects if tapped on the same logo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                if (selectedImage == e.Item as Images)
                {
                    ((ListView)sender).SelectedItem = null;
                    selectedImage = null;
                }
                else
                {
                    selectedImage = e.Item as Images;
                }
            }
            catch (Exception exc)
            {
                DisplayAlert(Labels.Exc, exc.Message, Labels.Ok);
            }
        }

        /// <summary>
        /// Changes local users icon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ChooseIconEvent(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception exc)
            {

            }
        }
    }
}