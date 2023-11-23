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
        [ValidateAntiForgeryToken]
        public JsonResult OnPostDeleteUser(int userID)
        {
            bool deletionResult = UserLogic.DeleteUser(userID).Result;

            return new JsonResult(new { success = deletionResult });
        }
    }
}
