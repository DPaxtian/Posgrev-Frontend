using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Posgrev_Frontend.Logic;
using Posgrev_Frontend.Models;

namespace Posgrev_Frontend.Pages.AdministratorPages
{
    public class EditProgramModel : PageModel
    {
        public ProgramModel? programToEdit;
        int statusCode = 500;

        public async Task<IActionResult> OnGetAsync(string idProgram)
        {
            programToEdit = await ProgramLogic.GetProgramDetails(idProgram);
            return Page();
        }


        public async Task<IActionResult> OnPost(ProgramModel editedProgram)
        {
            statusCode = await ProgramLogic.EditProgram(editedProgram);
            return RedirectToPage("/AdministratorPages/AdministratorPrograms");
        }
    }
}