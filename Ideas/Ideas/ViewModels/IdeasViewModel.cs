
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
    public class IdeasViewModel : INotifyPropertyChanged
    {
        ApiServices _apiServices = new ApiServices();

        //public string AccessToken { get; set; }

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

        public ICommand GetIdeasCommand
        {
            get
            {
                return new Command(async() => 
                {
                    //obtenemos el AccessToken
                    var accesstoken = Settings.AccessToken;
                    Ideas = await _apiServices.GetIdeasAsync(accesstoken);
                });
            }
            
        }
        
        /*Borramos el AccessToken, Username y Password*/
        public ICommand LogoutCommand
        {
            get
            {
                return new Command(() =>
                {   
                    //obtenemos el AccessToken
                    Settings.AccessToken = "";
                    Settings.Username = "";
                    Settings.Password = "";
                });
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
