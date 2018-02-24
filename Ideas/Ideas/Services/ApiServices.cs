
namespace Ideas.Services
{
    using Ideas.Helpers;
    using Ideas.Models;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    public class ApiServices
    {
        public async Task<bool> RegisterAsync(string email, string password, string confirmPassword)
        {
            var client = new HttpClient();

            var model = new RegisterBindingModel
            {
                Email = email,
                Password = password,
                ConfirmPassword = confirmPassword
            };

            var json = JsonConvert.SerializeObject(model);

            HttpContent content = new StringContent(json);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync("http://localhost:54745/api/Account/Register", content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
        //Login
        public async Task<string> LoginAsync(string userName, string password)
        {
            var KeyValues = new List<KeyValuePair<string, string>>
            {
                //se pasan los pares clave/valor
                //obtienes los valores de username y password
                new KeyValuePair<string, string>("username", userName),
                new KeyValuePair<string, string>("password", password),
                new KeyValuePair<string, string>("grant_type", "password")
            };
            //hacemos la peticion
            var request = new HttpRequestMessage(
                HttpMethod.Post, "http://localhost:54745/Token");
            //adicionamos los dos parametros de la peticion
            request.Content = new FormUrlEncodedContent(KeyValues);

            var client = new HttpClient();

            var response = await client.SendAsync(request);
            //obtiene el accessToken
            var jwt = await response.Content.ReadAsStringAsync();
            //obtiene el access token del json    
            JObject jwtDynamic = JsonConvert.DeserializeObject<dynamic>(jwt);
            //obtenemos el access_token de jwtDynamic
            var accessToken = jwtDynamic.Value<string>("access_token");
            //obtenemos accessTokenExpiration
            var accessTokenExpiration = jwtDynamic.Value<DateTime>(".expires");
            //Almacenamos el accessTokenExpiration
            Settings.AccessTokenexpiration = accessTokenExpiration;

            Debug.WriteLine(jwt);

            return accessToken;
        }
        //usamos el accessToken para hacer uso de una Peticion Autenticada a nuestro WebServices
        public async Task<List<Idea>> GetIdeasAsync(string accessToken)
        {
            var client = new HttpClient();
            //Para el Token solicita lo siguiente
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue
                ("Bearer", accessToken);

            var json = await client.GetStringAsync("http://localhost:54745/api/ideas");
            //obtenemos la lista de Ideas del WS
            var ideas = JsonConvert.DeserializeObject<List<Idea>>(json);

            return ideas;

        }
        //implementamos el metodo que se comunicara con nuestro WS
        public async Task PostIdeaAsync(Idea idea, string accessToken)
        {

            var client = new HttpClient();
            //necesita dos parametros el bearer y el accessToken
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", accessToken);

            //adicionamos el content de cuerpo de BodyRequest
            var json = JsonConvert.SerializeObject(idea);
            //creamos el httpContent, metemos el json dentro del content
            HttpContent content = new StringContent(json);
            //definimos el ContentType(en el Postman se hace)
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            //hay que especificar el tipo de peticion y la URL

            var response = await client.PostAsync("http://localhost:54745/api/ideas", content);

        }

        public async Task PutIdeaAsync(Idea idea, string accessToken)
        {
            var client = new HttpClient();
            //enviamos el Token
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", accessToken);
            //serializamos la idea en un Json
            var json = JsonConvert.SerializeObject(idea);
            //metemos en un contenedor el json
            HttpContent content = new StringContent(json);
            //especificamos el tipo de httcontent
            content.Headers.ContentType = new MediaTypeHeaderValue(
                "application/json");
            //enviamos la peticion
            //
            var response = await client.PutAsync("http://localhost:54745/api/ideas" + idea.Id,
                content);


        }

        public async Task DeleteIdeaAsync(int id, string accessToken)
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await client.DeleteAsync("http://localhost:54745/api/ideas" + id);

        }

        public async Task<List<Idea>> SearchIdeasAsync(string keyword, string accessToken)
        {
            var client = new HttpClient();
            //Para el Token solicita lo siguiente
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue
                ("Bearer", accessToken);

            var json = await client.GetStringAsync("http://localhost:54745/api/ideas/Search" + keyword);
            //obtenemos la lista de Ideas del WS
            var ideas = JsonConvert.DeserializeObject<List<Idea>>(json);

            return ideas;

        }
    }
}
