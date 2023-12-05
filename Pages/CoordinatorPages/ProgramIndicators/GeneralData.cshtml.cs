using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Posgrev_Frontend.Logic;
using Posgrev_Frontend.Models;

namespace Posgrev_Frontend.Pages.CoordinatorPages.ProgramIndicators
{
    public class GeneralDataModel : PageModel
    {
        int statusCode = 500;
        
        public async Task<IActionResult> OnPost(DatosGenerales newGeneralDataInformation)
        {
            statusCode = await ProgramLogic.SaveGeneralData(newGeneralDataInformation);
            return RedirectToPage("/CoordinatorPages/ProgramIndicators/ContextProgram");
        }
    }
}