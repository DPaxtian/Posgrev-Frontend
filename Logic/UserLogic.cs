using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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

                using (HttpClient server = new HttpClient())
                {
                    string url = "http://localhost:4000/createUser";
                    HttpContent contentToSend = new StringContent(dataToSend, Encoding.UTF8, "application/json");
                    HttpResponseMessage responseMessage = await server.PostAsync(url, contentToSend);

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        statusCode = 200;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error creating the user" + ex);
            }

            return statusCode;
        }

        public static async Task<bool> AuthenticateUser(string userName, string password)
        {
            try
            {
                var information = new
                {
                    nombreUsuario = userName,
                    contrasena = password
                };

                string dataToSend = JsonConvert.SerializeObject(information);

                using (HttpClient server = new HttpClient())
                {
                    string url = "http://localhost:4000/authenticate";
                    HttpContent contentToSend = new StringContent(dataToSend, Encoding.UTF8, "application/json");
                    HttpResponseMessage responseMessage = await server.PostAsync(url, contentToSend);

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        string jsonResponse = await responseMessage.Content.ReadAsStringAsync();
                        var authResult = JsonConvert.DeserializeAnonymousType(jsonResponse, new { usuario = new { rol = "" } });

                        // Puedes procesar el resultado de la autenticación aquí si es necesario

                        // Devuelve true si la autenticación fue exitosa
                        return true;
                    }
                    else
                    {
                        // Autenticación fallida
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("There is an error on AuthenticateUser" + ex);
                return false;
            }
        }
        public static async Task<int> DeleteUser(string userId)
        {
            int statusCode = 500;

            try
            {
                using (HttpClient server = new HttpClient())
                {
                    string url = $"http://localhost:4000/deleteUser/{userId}";
                    HttpResponseMessage responseMessage = await server.DeleteAsync(url);

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        statusCode = 204;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error deleting the user" + ex);
            }

            return statusCode;
        }


        public static async Task<int> EditUser(DatosUsuario editedInformation)
        {
            int statusCode = 500;

            try
            {
                var information = new
                {
                    rol = editedInformation.rol,
                    nombreUsuario = editedInformation.nombreUsuario,
                    password = editedInformation.password,
                    identificadorPrograma = editedInformation.identificadorPrograma,
                };

                string dataToSend = JsonConvert.SerializeObject(information);

                using (HttpClient server = new HttpClient())
                {
                    string url = "http://localhost:4000/updateUser/" + editedInformation.IDusuario;
                    HttpContent contentToSend = new StringContent(dataToSend, Encoding.UTF8, "application/json");
                    HttpResponseMessage responseMessage = await server.PutAsync(url, contentToSend);

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        statusCode = 200;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error editing the user" + ex);
            }

            return statusCode;
        }


        public static async Task<DatosUsuario> GetUserDetails(string userId)
        {
            DatosUsuario userObtained = null;

            try
            {
                using (HttpClient server = new HttpClient())
                {
                    string url = "http://localhost:4000/getUser/" + userId;
                    HttpResponseMessage responseMessage = await server.GetAsync(url);

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        string jsonResponse = await responseMessage.Content.ReadAsStringAsync();
                        userObtained = JsonConvert.DeserializeObject<DatosUsuario>(jsonResponse);

                        // Imprime los datos para verificar
                        Console.WriteLine($"User ID: {userObtained.IDusuario}, Nombre: {userObtained.nombreUsuario}, Rol: {userObtained.rol}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("There is an error at GetUserDetails" + ex);
            }

            return userObtained;
        }




    }
}

