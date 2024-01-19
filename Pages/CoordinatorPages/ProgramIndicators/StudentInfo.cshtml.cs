using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Posgrev_Frontend.Pages.CoordinatorPages.ProgramIndicators
{
    public class StudentInfoModel : PageModel
    {
        public string? IdProg {get; set;}

        public IActionResult OnGet(string idProgram)
        {
            try
            {
                IdProg = idProgram;
            }
            catch (Exception ex)
            {
                Console.WriteLine("There is an error on AdministratorPage" + ex);
            }
            return Page();
        }
    }
}