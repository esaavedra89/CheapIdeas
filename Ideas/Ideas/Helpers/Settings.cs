// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;

namespace Ideas.Helpers
{
  /// <summary>
  /// This is the Settings static class that can be used in your Core solution or in any
  /// of your client applications. All settings are laid out the same exact way with getters
  /// and setters. 
  /// </summary>
  /// Aqui almacenamos los Preferences en el dispositivo
  public static class Settings
  {
    private static ISettings AppSettings
    {
      get
      {
        return CrossSettings.Current;
      }
    }

   

        //-----------------------Esto es para Registro y Login


        //este es para almacenar el usuario del registro 
    public static string Username
    {
      get
      {//especificamos en el segundo como Empty
        return AppSettings.GetValueOrDefault<string>("Username", "");
      }
      set
      {
        AppSettings.AddOrUpdateValue<string>("Username", value);
      }
    }
        //Este es para almacenar el password de los uaurios
        public static string Password
        {
            get
            {//especificamos en el segundo como null
                return AppSettings.GetValueOrDefault<string>("Password", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue<string>("Password", value);
            }
        }
        //se debe crear para poder acceder a este desde otra clse
        public static string AccessToken
        {
            get
            {//especificamos en el segundo como null
                return AppSettings.GetValueOrDefault<string>("AccessToken", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue<string>("AccessToken", value);
            }
        }

        /*Creamos propiedad para AccessTokenExpire*/

        public static DateTime AccessTokenexpiration
        {
            get
            {//especificamos en el segundo como un valor de fecha por area  
                return AppSettings.GetValueOrDefault<DateTime>("AccessTokenExpiration", 
                    DateTime.UtcNow);
            }
            set
            {
                AppSettings.AddOrUpdateValue<DateTime>("AccessTokenExpiration", value);
            }
        }

    }
}