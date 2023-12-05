using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Posgrev_Frontend.Logic;
using System.Text;

namespace Posgrev_Frontend.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string UserName { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public async Task<IActionResult> OnPost()
        {
            bool isAuthenticated = await UserLogic.AuthenticateUser(UserName, Password);

            if (isAuthenticated)
            {
                // Usuario autenticado, realiza las acciones necesarias
                return RedirectToPage("/AdministratorPages/AdministratorUser"); // Actualiza con tu página de inicio
            }
            else
            {
                // Autenticación fallida, muestra un mensaje de error
                ModelState.AddModelError(string.Empty, "Invalid username or password");
                return Page();
            }
    
        }
    }
}
