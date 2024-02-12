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

        public static async Task<bool> AuthenticateUser(string nombreUsuario, string password)
        {
            try
            {
                using (HttpClient server = new HttpClient())
                {
                    string url = $"http://localhost:4000/authenticate/{nombreUsuario}/{password}";
                    HttpResponseMessage responseMessage = await server.GetAsync(url);

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        // Log para verificar que la autenticación fue exitosa
                        Console.WriteLine("Authentication successful");

                        // Intenta obtener el rol sin retraso
                        string userRole = await UserLogic.GetRole(nombreUsuario);

                        // Log para verificar el rol obtenido
                        Console.WriteLine($"User role obtained: {userRole}");

                        // Verifica si la obtención del rol es exitosa antes de redirigir
                        if (!string.IsNullOrEmpty(userRole))
                        {
                            // Puedes procesar el resultado de la autenticación aquí si es necesario

                            // Devuelve true si la autenticación y obtención del rol fueron exitosas
                            return true;
                        }
                        else
                        {
                            // Autenticación exitosa pero no se pudo obtener el rol
                            Console.WriteLine("Could not retrieve user role");
                            return false;
                        }
                    }
                    else
                    {
                        // Autenticación fallida
                        Console.WriteLine("Authentication failed");
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


        public static async Task<DatosUsuario> GetUserDetailsByUsername(string username)
        {
            DatosUsuario userObtained = null;

            try
            {
                using (HttpClient server = new HttpClient())
                {
                    string url = "http://localhost:4000/getUserByUsername/" + username;
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


        public static async Task<string?> GetRole(string nombreUsuario)
        {
            try
            {
                using (HttpClient server = new HttpClient())
                {
                    string url = $"http://localhost:4000/getRole/{nombreUsuario}";
                    Console.WriteLine($"URL de solicitud: {url}");
                    HttpResponseMessage responseMessage = await server.GetAsync(url);

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        string jsonResponse = await responseMessage.Content.ReadAsStringAsync();
                        var userRole = JsonConvert.DeserializeAnonymousType(jsonResponse, new { rol = "" });

                        if (!string.IsNullOrEmpty(userRole.rol))
                        {
                            Console.WriteLine("User role obtained: " + userRole.rol);
                            return userRole.rol;
                        }
                        else
                        {
                            Console.WriteLine("Could not retrieve user role");
                            return null;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Could not retrieve user role");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("There is an error on GetRole: " + ex);
                return null;
            }
        }

    }
}
