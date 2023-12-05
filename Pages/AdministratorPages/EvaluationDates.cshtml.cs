using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Posgrev_Frontend.Logic;

namespace Posgrev_Frontend.Pages.AdministratorPages
{
    public class EvaluationDatesModel : PageModel
    {

        public List<EvaluationPeriodModel> evaluationPeriods = new List<EvaluationPeriodModel>();
        int statusCode = 500;
        
        public async Task<IActionResult> OnPost(EvaluationPeriodModel newEvaluationPeriod)
        {
            statusCode = await EvaluationLogic.SaveEvaluationPeriod(newEvaluationPeriod);
            return RedirectToPage();
        }


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