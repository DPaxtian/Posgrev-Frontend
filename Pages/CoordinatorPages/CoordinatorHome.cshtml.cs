using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Posgrev_Frontend.Logic;

namespace Posgrev_Frontend.Pages.CoordinatorPages
{
    public class CoordinatorHomeModel : PageModel
    {
        public List<EvaluationPeriodModel> evaluationPeriods = new List<EvaluationPeriodModel>();

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
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