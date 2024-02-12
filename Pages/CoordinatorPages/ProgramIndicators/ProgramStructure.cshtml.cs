using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Posgrev_Frontend.Models;
using Posgrev_Frontend.Logic;

namespace Posgrev_Frontend.Pages.CoordinatorPages.ProgramIndicators
{
    public class ProgramStructureModel : PageModel
    {
        public string? IdProg { get; set; }
        public string? Username {get; set;}
        public string? ParentFolderId {get; set;}
        public string? CategoryFolderId {get; set;}
        int statusCode = 500;

        public IActionResult OnGet(string idProgram, string username, string parentFolderId)
        {
            try
            {
                IdProg = idProgram;
                Username = username;
                ParentFolderId = parentFolderId;

                DriveLogic driveLogic = new DriveLogic();
                CategoryFolderId = driveLogic.CreateIndicatorFolder(ParentFolderId, "Infraestructura del Programa");
            }
            catch (Exception ex)
            {
                Console.WriteLine("There is an error on AdministratorPage" + ex);
                return RedirectToPage("/Error");
            }
            return Page();
        }


        public async Task<IActionResult> OnPost(string idProgram, string Username, InfraestructuraPrograma newInfo, string status, string ParentFolderId, string CategoryFolderId)
        {

            newInfo.NucleoAcadBas = new NucleoAcadBas
            {
                ProfTiemCom = Request.Form["InfraestructuraPrograma.NucleoAcadBas.ProfTiemCom"],
                ProfTiemPar = Request.Form["InfraestructuraPrograma.NucleoAcadBas.ProfTiemPar"],
                Colaboradores = Request.Form["InfraestructuraPrograma.NucleoAcadBas.Colaboradores"],
            };

            newInfo.PlanEstudios = await UploadFileToDrive(Request.Form.Files["PlanEstudios"], "PlanEstudios", CategoryFolderId, "application/pdf");
            newInfo.ActaConsejoConsultivo = await UploadFileToDrive(Request.Form.Files["ActaConsejoConsultivo"], "ActaConsejoConsultivo", CategoryFolderId, "application/pdf");
            newInfo.ActaConsejoArea = await UploadFileToDrive(Request.Form.Files["ActaConsejoArea"], "ActaConsejoArea", CategoryFolderId, "application/pdf");
            newInfo.ActaActualizacionPlan = await UploadFileToDrive(Request.Form.Files["ActaActualizacionPlan"], "ActaActualizacionPlan", CategoryFolderId, "application/pdf");
            newInfo.MapaCurricular = await UploadFileToDrive(Request.Form.Files["MapaCurricular"], "MapaCurricular", CategoryFolderId, "application/pdf");
            newInfo.PlanEstudios = await UploadFileToDrive(Request.Form.Files["PlanEstudios"], "PlanEstudios", CategoryFolderId, "application/pdf");
            newInfo.Lgac = await UploadFileToDrive(Request.Form.Files["Lgac"], "Lgac", CategoryFolderId, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            newInfo.NucleoAcadBas.InfoProf = await UploadFileToDrive(Request.Form.Files["InfoProf"], "InfoProf", CategoryFolderId, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            newInfo.InfraestrucPrograma = await UploadFileToDrive(Request.Form.Files["InfraestrucPrograma"], "InfraestrucPrograma", CategoryFolderId, "application/pdf");


            statusCode = await ProgramLogic.SaveProgramInfrastucture(idProgram, newInfo, status);

            if (statusCode == 200)
            {
                return RedirectToPage("/CoordinatorPages/ProgramIndicators/ScholarProcess", new { idProgram, Username, ParentFolderId });
            }
            else
            {
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