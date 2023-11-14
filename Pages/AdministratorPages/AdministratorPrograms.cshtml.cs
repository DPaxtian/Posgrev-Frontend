using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Posgrev_Frontend.Logic;
using Posgrev_Frontend.Models;


namespace Posgrev_Frontend.Pages.AdministratorPages
{
    public class AdministratorProgramsModel : PageModel
    {
        public List<ProgramModel> activePrograms = new List<ProgramModel>();

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                activePrograms = await ProgramLogic.GetActivePrograms();
            }
            catch(Exception ex)
            {
                Console.WriteLine("There is an error on AdministratorPage" + ex);
            }

            return Page();
        }


    }
}