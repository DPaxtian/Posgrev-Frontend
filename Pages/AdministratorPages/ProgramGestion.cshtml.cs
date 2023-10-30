using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Posgrev_Frontend.Logic;
using Posgrev_Frontend.Models;

namespace Posgrev_Frontend.Pages.AdministratorPages
{
    public class ProgramGestionModel : PageModel
    {
        int statusCode = 500;
        
        public async Task<IActionResult> OnPost(InformacionBasica newProgramBasicInformation)
        {
            statusCode = await ProgramLogic.SaveBasicInformation(newProgramBasicInformation);
            return RedirectToPage("/AdministratorPages/AdministratorPrograms");
        }
    }
}