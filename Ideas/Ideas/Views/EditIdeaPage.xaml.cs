using Ideas.Models;
using Ideas.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ideas.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditIdeaPage : ContentPage
    {
        public EditIdeaPage(Idea idea)
        {
            //Instanciamos
            var editIdeaViewModel = new EditIdeaViewModel();
            //igualamos con el parametro entrante
            editIdeaViewModel.Idea = idea;
            //hacemos BindingContext
            BindingContext = editIdeaViewModel;

            InitializeComponent();


            //var editIdeaViewModel = BindingContext as EditIdeaViewModel;
        }
    }
}