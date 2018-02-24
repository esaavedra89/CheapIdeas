using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ideas.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void GoToIdeasPage_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new IdeasPage());
        }
    }
}