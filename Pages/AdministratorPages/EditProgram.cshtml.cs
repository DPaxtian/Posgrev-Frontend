using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Posgrev_Frontend.Logic;
using Posgrev_Frontend.Models;

namespace Posgrev_Frontend.Pages.AdministratorPages
{
    public class EditProgramModel : PageModel
    {
        public ProgramModel? programToEdit;
        public List<DatosUsuario> activeCoordinators = new List<DatosUsuario>();
        public Denominaciones itemsDenominations = new Denominaciones();
        int statusCode = 500;

        public async Task<IActionResult> OnGetAsync(string idProgram)
        {
            try
            {
                programToEdit = await ProgramLogic.GetProgramDetails(idProgram);
                activeCoordinators = await UserLogic.GetUsers();
                itemsDenominations = await ProgramLogic.GetDenominations();
            }
            catch (Exception ex)
            {
                Console.WriteLine("There is an error on AdministratorPage" + ex);
            }

            return Page();
        }


        public async Task<IActionResult> OnPost(string idUser, ProgramModel editedProgram)
        {
            statusCode = await ProgramLogic.EditProgram(idUser, editedProgram);
            return RedirectToPage("/AdministratorPages/AdministratorPrograms");
        }
    }
}