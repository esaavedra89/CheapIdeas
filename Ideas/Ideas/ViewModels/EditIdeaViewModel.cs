
using Ideas.Helpers;
using Ideas.Models;
using Ideas.Services;
using System.Windows.Input;
using Xamarin.Forms;

namespace Ideas.ViewModels
{
    public class EditIdeaViewModel
    {
        ApiServices _apiServices = new ApiServices();

        public Idea Idea { get; set; }

        public ICommand EditCommand
        {
            get
            {
                return new Command(async() => 
                {
                    await _apiServices.PutIdeaAsync(Idea, Settings.AccessToken);
                });
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                return new Command(async() =>
                {
                    await _apiServices.DeleteIdeaAsync(Idea.Id, Settings.AccessToken); 
                });
            }
        }
    }
}
