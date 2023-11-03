using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Posgrev_Frontend.Logic;
using Posgrev_Frontend.Models;


namespace Posgrev_Frontend.Pages.AdministratorPages
{
    public class AdministratorUser : PageModel
    {
        int statusCode = 500;

        public async Task<IActionResult> OnPost(DatosUsuario newUserInformation)
        {
            statusCode = await UserLogic.SaveUserInformation(newUserInformation);
            return RedirectToPage("/AdministratorPages/AdministratorUser");
        }
    }
}