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

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                coordinatorInfo = await UserLogic.GetUserDetails("1");
                programInfo = await ProgramLogic.GetProgramDetails(coordinatorInfo.identificadorPrograma);
                evaluationPeriods = await EvaluationLogic.GetAllEvaluationPeriods();
            }
            catch(Exception ex)
            {
                Console.WriteLine("There is an error on AdministratorPage" + ex);
            }
            return Page();
        }
    }
}