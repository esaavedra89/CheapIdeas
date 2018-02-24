
using Ideas.Helpers;
using Ideas.Services;
using System.Windows.Input;
using Xamarin.Forms;

namespace Ideas.ViewModels
{
    public class LoginViewModel
    {
        private readonly ApiServices _apiServices = new ApiServices();

        public string Username { get; set; }

        public string Password { get; set; }

        public ICommand LoginCommand
        {
            get
            {
                return new Command(async() =>
                {
                    //almacenamos el AccessToken en una variable
                    var accesstoken = await _apiServices.LoginAsync(
                        Username, Password);
                    //almacenamos el Token
                    Settings.AccessToken = accesstoken;
                });
            }
        }

        public LoginViewModel()
        {
            //almacenamos los valores
            Username = Settings.Username;

            Password = Settings.Password;
        }
    }
}
