using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Posgrev_Frontend.Logic;
using Posgrev_Frontend.Models;

namespace Posgrev_Frontend.Pages.CoordinatorPages
{
    public class CoordinatorHomeModel : PageModel
    {
        public List<EvaluationPeriodModel> evaluationPeriods = new List<EvaluationPeriodModel>();
        public DatosUsuario coordinatorInfo = new DatosUsuario();
        public ProgramModel programInfo = new ProgramModel();
        public string? Username { get; set; }

        public async Task<IActionResult> OnGetAsync(string UserName)
        {
            Username = UserName;
            try
            {
                coordinatorInfo = await UserLogic.GetUserDetailsByUsername(Username);
                programInfo = await ProgramLogic.GetProgramDetails(coordinatorInfo.identificadorPrograma);
                evaluationPeriods = await EvaluationLogic.GetAllEvaluationPeriods();
            }
            catch(Exception ex)
            {
                Console.WriteLine("There is an error on AdministratorPage" + ex);
                return RedirectToPage("/Error");
            }
            return Page();
        }
    }
}