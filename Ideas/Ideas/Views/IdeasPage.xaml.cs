using Ideas.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ideas.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IdeasPage : ContentPage
    {
        public IdeasPage()
        {
            InitializeComponent();
        }

        private async void GoToNewIdea_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddNewIdeaPage());
        }

        private async void IdeasListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            //paasmos como parametro el item seleccionado
            var idea = e.Item as Idea;

            await Navigation.PushAsync(new EditIdeaPage(idea));
        }

        private async void SearchIdeas_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SearchPage());
        }

        private async void Logout_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }
    }
}