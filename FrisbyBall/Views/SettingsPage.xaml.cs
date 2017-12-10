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
    public partial class SettingsPage : TabbedPage
    {
        private Images selectedImage;
        private List<Images> Images;
        public SettingsPage()
        {
            InitializeComponent();
            Init();
        }

        void Init()
        {
            Images = new List<Images>()
            {
                new Images{ Image = "astronaut.png", ImageName = "Astronaut"},
                new Images{ Image = "batman.png", ImageName = "Batman"},
                new Images{ Image = "charlesChaplin.png", ImageName = "Charles Chaplin"},
                new Images{ Image= "geek.png", ImageName = "Geek"},
                new Images{ Image = "yoda.png", ImageName = "Yoda"},
                new Images{ Image = "pirate.png", ImageName = "Pirate"},
                new Images{ Image = "PlayerIcon.png", ImageName = "Default icon"},
                new Images{ Image = "Predator.png", ImageName = "Predator"},
                new Images{ Image = "punk.png", ImageName = "Punk"},
                new Images{ Image = "serialKiller.png", ImageName = "Serial Killer"},
                new Images{ Image = "soldier.png", ImageName = "Soldier"},
                new Images{ Image = "StarWarsSoldier.png", ImageName = "Star Wars Soldier"}
            };

            LogoListview.ItemsSource = Images;
        }

        void ChangePasswordEvent(object sender, EventArgs e)
        {

        }

        void ChangeEmailEvent(object sender, EventArgs e)
        {

        }

        protected override bool OnBackButtonPressed()
        {
            Navigation.PopModalAsync(false);
            return true;
        }

        void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (selectedImage == e.Item as Images)
            {
                ((ListView)sender).SelectedItem = null;
            }
            else
            {
                selectedImage = e.Item as Images;
                DisplayAlert("Ok", selectedImage.ImageName, "ok");
            }
        }
    }
}