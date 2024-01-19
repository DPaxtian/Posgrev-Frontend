using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Posgrev_Frontend.Logic;
using Posgrev_Frontend.Models;
using System.Linq;

namespace Posgrev_Frontend.Pages.AdministratorPages
{
    public class ProgramGestionModel : PageModel
    {
        int statusCode = 500;
        public List<DatosUsuario> activeCoordinators = new List<DatosUsuario>();
        public Denominaciones itemsDenominations = new Denominaciones();

        public async Task<IActionResult> OnPost(string idUser, InformacionBasica newProgramBasicInformation)
        {
            statusCode = await ProgramLogic.SaveBasicInformation(idUser, newProgramBasicInformation);
            return RedirectToPage("/AdministratorPages/AdministratorPrograms");
        }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                activeCoordinators = await UserLogic.GetUsers();
                itemsDenominations = await ProgramLogic.GetDenominations();
            }
            catch (Exception ex)
            {
                Console.WriteLine("There is an error on AdministratorPage" + ex);
            }

            return Page();
        }
    }
}