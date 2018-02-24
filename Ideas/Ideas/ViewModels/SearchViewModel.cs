
using Ideas.Helpers;
using Ideas.Models;
using Ideas.Services;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace Ideas.ViewModels
{
    public class SearchViewModel : INotifyPropertyChanged
    {
        ApiServices _apiServices = new ApiServices();

        public string Keyword { get; set; }

        private List<Idea> _ideas;

        public List<Idea> Ideas
        {
            get
            {
                return _ideas;
            }

            set
            {
                _ideas = value;
                OnPropertyChanged();
            }
        }

        public ICommand SearchCommand
        {

            get
            {
                return new Command(async() => 
                {
                    Ideas = await _apiServices.SearchIdeasAsync(Keyword, Settings.AccessToken);
                });
            }

        }

        //Se adiciona para reconocer los cambios
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
