using System;
using Ideas.Views;
using Xamarin.Forms;
using Ideas.Helpers;
using Ideas.ViewModels;

namespace Ideas
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            SetMainPager();

            //MainPage = new NavigationPage(new RegisterPage());
        }
        //creamos tres escenarios por si tiene el Token, clave y usuario o si no tiene nada almacenado en las
        //preferences (Settings)
        private void SetMainPager()
        {
            //si tiene el token lo pasa directo
            if (!string.IsNullOrEmpty(Settings.AccessToken))
            {
                //verificamos el accessTokenExpiration
                if (DateTime.UtcNow.AddHours(1) > Settings.AccessTokenexpiration)
                {
                    var vm = new LoginViewModel();
                    //Ejecutamos el comando con Execute y le pasamos null
                    vm.LoginCommand.Execute(null);
                }

                MainPage = new NavigationPage(new IdeasPage());

            }
            //si tiene clave y usuario  al login
            else if(!string.IsNullOrEmpty(Settings.Username) &&
                !string.IsNullOrEmpty(Settings.Password))
            {
                MainPage = new NavigationPage(new LoginPage());
            }
            //sino lo manda al registro
            else
            {
                MainPage = new NavigationPage(new RegisterPage());
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
