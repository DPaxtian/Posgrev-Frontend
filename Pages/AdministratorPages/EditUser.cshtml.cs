using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Posgrev_Frontend.Logic;
using Posgrev_Frontend.Models;

namespace Posgrev_Frontend.Pages.AdministratorPages
{
    public class EditUserModel : PageModel
    {
        public DatosUsuario? userEdit;
        int statusCode = 500;

        public async Task<IActionResult> OnGetAsync(string idUsuario)
        {
            userEdit = await UserLogic.GetUserDetails(idUsuario);
            return Page();
        }


        public async Task<IActionResult> OnPost(DatosUsuario editedUser)
        {
            statusCode = await UserLogic.EditUser(editedUser);
            return RedirectToPage("/AdministratorPages/AdministratorUser");
        }
    }
}
