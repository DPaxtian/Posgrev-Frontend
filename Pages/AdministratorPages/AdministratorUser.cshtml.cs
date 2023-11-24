using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Posgrev_Frontend.Logic;
using Posgrev_Frontend.Models;

namespace Posgrev_Frontend.Pages.AdministratorPages
{



    public class AdministratorUserModel : PageModel
    {

            public List<DatosUsuario> RegisteredUsers { get; set; } = new List<DatosUsuario>();

            public async Task<IActionResult> OnGetAsync()
            {
                try
                {
                    HttpClient server = new HttpClient();
                    string apiUrl = "http://localhost:4000/getUsers";
                    HttpResponseMessage responseMessage = await server.GetAsync(apiUrl);
                    string jsonResponse = await responseMessage.Content.ReadAsStringAsync();

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        // Deserializa directamente a una lista de DatosUsuario
                        RegisteredUsers = JsonConvert.DeserializeObject<List<DatosUsuario>>(jsonResponse);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("There is an error on AdministratorPage: " + ex);
                }

                return Page();
            }

        public async Task<IActionResult> OnPostDeleteUserAsync(string userId)
        {
            try
            {
                int deleteStatusCode = await UserLogic.DeleteUser(userId);

                if (deleteStatusCode == 204)
                {
                    // Éxito al eliminar el usuario
                    TempData["Message"] = "Usuario eliminado con éxito.";
                }
                else
                {
                    // Error al intentar eliminar el usuario
                    TempData["Error"] = "Error al intentar eliminar el usuario.";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al intentar eliminar el usuario: " + ex);
                TempData["Error"] = "Error al intentar eliminar el usuario.";
            }

            // Redirige de nuevo a la página de administración de usuarios
            return RedirectToPage("/AdministratorPages/AdministratorUser");
        }
    }
}
