
using Ideas.Helpers;
using Ideas.Models;
using Ideas.Services;
using System.Windows.Input;
using Xamarin.Forms;

namespace Ideas.ViewModels
{
    public class AddNewIdeaViewModel
    {
        ApiServices _apiServices = new ApiServices();

        //creamos las propiedades para crear nuevas Ideas

        public string Title { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        //Necesitamos enviar estas ideas al WS

        public ICommand AddCommand
        {
            get
            {
                return new Command(async() =>
                {
                    //creamos esta variable para almacenar las tres variables
                    //pasarlas en el post
                    var idea = new Idea
                    {
                        Title = Title,
                        Description = Description,
                        Category = Category
                    };
                    //pasamos el idea y el accessToken que tenemos almacenados en 
                    //el Settings
                     await _apiServices.PostIdeaAsync(idea, Settings.AccessToken);
                });
            }
        }
    }
}
