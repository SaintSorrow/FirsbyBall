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
        public SettingsPage()
        {
            InitializeComponent();
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
    }
}