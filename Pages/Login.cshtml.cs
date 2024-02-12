using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Posgrev_Frontend.Logic;
using System;
using System.Text;
using System.Threading.Tasks;

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
            try
            {
                // Intenta autenticar al usuario
                bool isAuthenticated = await UserLogic.AuthenticateUser(UserName, Password);

                if (isAuthenticated)
                {
                    // Si la autenticaci�n es exitosa, intenta obtener el rol
                    string userRole = await UserLogic.GetRole(UserName);

                    // Verifica si la obtenci�n del rol es exitosa antes de redirigir
                    if (!string.IsNullOrEmpty(userRole))
                    {
                        // Redirige seg�n el rol
                        if (userRole.Equals("SuperAdministrador", StringComparison.OrdinalIgnoreCase))
                        {
                            return RedirectToPage("/AdministratorPages/AdministratorUser");
                        }
                        else if (userRole.Equals("Administrador", StringComparison.OrdinalIgnoreCase))
                        {
                            return RedirectToPage("/AdministratorPages/AdministratorUser");
                        }
                        else if (userRole.Equals("Revisor", StringComparison.OrdinalIgnoreCase))
                        {
                            return RedirectToPage("/RevisorPages/Nombrede la pagina");
                        }
                        else if (userRole.Equals("Coordinador", StringComparison.OrdinalIgnoreCase))
                        {
                            return RedirectToPage("/CoordinatorPages/CoordinatorHome", new {UserName});
                        }
                    }
                    else
                    {
                        // Autenticaci�n exitosa pero no se pudo obtener el rol
                        ModelState.AddModelError(string.Empty, "Could not retrieve user role");
                        return Page();
                    }
                }
                else
                {
                    // Autenticaci�n fallida, muestra un mensaje de error
                    ModelState.AddModelError(string.Empty, "Invalid username or password");
                    return Page();
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier error inesperado
                Console.WriteLine("There is an error on OnPost" + ex);
                ModelState.AddModelError(string.Empty, "Unexpected error");
                return Page();
            }
            return Page();
        }
    }
}
