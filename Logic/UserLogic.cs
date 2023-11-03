using System;
using System.Text;
using Newtonsoft.Json;
using Posgrev_Frontend.Models;


namespace Posgrev_Frontend.Logic
{
    public static class UserLogic
    {
        public static async Task<List<DatosUsuario>> GetUsers()
        {
            List<DatosUsuario> users = null;

            try
            {
                // Lógica para obtener usuarios del servidor
                using (HttpClient server = new HttpClient())
                {
                    string apiUrl = "http://localhost:4000/getUsers";
                    HttpResponseMessage responseMessage = await server.GetAsync(apiUrl);
                    string jsonResponse = await responseMessage.Content.ReadAsStringAsync();

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        // Utiliza JsonConvert para deserializar directamente a una lista de DatosUsuario
                        users = JsonConvert.DeserializeObject<List<DatosUsuario>>(jsonResponse);

                        // Imprimimos los datos para verificar
                        foreach (var user in users)
                        {
                            Console.WriteLine($"User ID: {user.IDusuario}, Nombre: {user.nombreUsuario}, Rol: {user.rol}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("There is an error on GetUsers" + ex);
            }

            return users ?? new List<DatosUsuario>();
        }

        public static async Task<int> SaveUserInformation(DatosUsuario newUserData)
        {
            int statusCode = 500;

            try
            {
                var information = new
                {
                    rol = newUserData.rol,
                    nombreUsuario = newUserData.nombreUsuario,
                    password = newUserData.password,
                    identificadorPrograma = newUserData.identificadorPrograma,
                };

                string dataToSend = JsonConvert.SerializeObject(information);
                Console.WriteLine(dataToSend);

                HttpClient server = new HttpClient();
                string url = "http://localhost:4000/createUser";
                HttpContent contentToSend = new StringContent(dataToSend, Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = await server.PostAsync(url, contentToSend);

                if (responseMessage.IsSuccessStatusCode)
                {
                    statusCode = 200;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error creating the user" + ex);
            }

            return statusCode;
        }

    }
}
