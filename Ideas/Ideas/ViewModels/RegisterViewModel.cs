
using Ideas.Helpers;
using Ideas.Services;
using System.Windows.Input;
using Xamarin.Forms;

namespace Ideas.ViewModels
{
    public class RegisterViewModel
    {
        ApiServices _apiServices = new ApiServices();

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string Message { get; set; }

        public ICommand RegisterCommand
        {
            get
            {
                return new Command(async() => 
                {
                    //metodo se creó en ApiServices
                    //llamamos a la API
                    var isSuccess = await _apiServices.RegisterAsync(
                        Email, Password, ConfirmPassword);
                    //almacenamos estos valores en las variables (Mediante Nuget)
                    //puedo acceder a las propiedades ya que es Protected
                    //al hacer click en el boton de register, se van a almacenar en Preferences
                    Settings.Username = Email;

                    Settings.Password = Password;

                    if (isSuccess)
                    { 
                        Message = "Registro Exitoso";

                    await Application.Current.MainPage.DisplayAlert(
                        "Excellente", "Registro Exitoso", "ok");
                    }
                    else
                    {
                        Message = "Intente luego";

                        await Application.Current.MainPage.DisplayAlert(
                            "Error", "Por alguna razon no registro", "ok");
                    }
                });
            }
        }
    }
}
