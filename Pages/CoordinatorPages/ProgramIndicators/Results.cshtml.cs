using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Posgrev_Frontend.Logic;
using Posgrev_Frontend.Models;

namespace Posgrev_Frontend.Pages.CoordinatorPages.ProgramIndicators
{
    public class ResultsModel : PageModel
    {
        public string? IdProg { get; set; }
        public string? Username { get; set; }
        public string? ParentFolderId { get; set; }
        public string? CategoryFolderId { get; set; }


        public IActionResult OnGet(string idProgram, string username, string parentFolderId)
        {
            try
            {
                IdProg = idProgram;
                Username = username;
                ParentFolderId = parentFolderId;

                DriveLogic driveLogic = new DriveLogic();
                CategoryFolderId = driveLogic.CreateIndicatorFolder(ParentFolderId, "Resultados");
            }
            catch (Exception ex)
            {
                Console.WriteLine("There is an error on AdministratorPage" + ex);
                return RedirectToPage("/Error");
            }
            return Page();
        }


        public async Task<IActionResult> OnPost(string idProgram, string Username, Resultados info, string status, string CategoryFolderId)
        {
            int statusCode = 500;

            try
            {
                info.PlanMejora = await UploadFileToDrive(Request.Form.Files["PlanMejora"], "PlanMejora", CategoryFolderId, "application/pdf");
                info.ReporteAutoeval = await UploadFileToDrive(Request.Form.Files["ReporteAutoeval"], "ReporteAutoeval", CategoryFolderId, "application/pdf");

                statusCode = await ProgramLogic.SaveResults(idProgram, info, status);

                if (statusCode == 200)
                {
                    return RedirectToPage("/CoordinatorPages/CoordinatorHome", new { Username });
                }
                else
                {
                    return RedirectToPage("/Error");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("There is an error on AdministratorPage" + ex);
                return RedirectToPage("/Error");
            }
        }


        private async Task<string> UploadFileToDrive(IFormFile file, string fileName, string folderId, string mimeType)
        {
            string fileId = "";
            if (file != null && file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    DriveLogic driveLogic = new DriveLogic();
                    fileId = await driveLogic.UploadFile(stream, fileName, mimeType, folderId);
                }
            }

            return fileId;
        }
    }
}