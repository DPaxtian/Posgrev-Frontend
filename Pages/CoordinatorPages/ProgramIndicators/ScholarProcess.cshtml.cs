using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Posgrev_Frontend.Logic;
using Posgrev_Frontend.Models;

namespace Posgrev_Frontend.Pages.CoordinatorPages.ProgramIndicators
{
    public class ScholarProcessModel : PageModel
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
                CategoryFolderId = driveLogic.CreateIndicatorFolder(ParentFolderId, "Procesos Escolares");
            }
            catch (Exception ex)
            {
                Console.WriteLine("There is an error on AdministratorPage" + ex);
                return RedirectToPage("/Error");
            }
            return Page();
        }


        public async Task<IActionResult> OnPost(string idProgram, string Username, string status, ProcesosEscolares scholarInfo, string ParentFolderId, string CategoryFolderId)
        {
            int codeStatus = 500;
            try
            {
                scholarInfo.ProcesoAdmision = await UploadFileToDrive(Request.Form.Files["ProcesoAdmision"], "ProcesoAdmision", CategoryFolderId, "application/pdf");
                scholarInfo.ProcesoMovilidad = await UploadFileToDrive(Request.Form.Files["ProcesoMovilidad"], "ProcesoMovilidad", CategoryFolderId, "application/pdf");
                scholarInfo.ProcesoCondonacion = await UploadFileToDrive(Request.Form.Files["ProcesoCondonacion"], "ProcesoCondonacion", CategoryFolderId, "application/pdf");
                scholarInfo.ProcesoBeca = await UploadFileToDrive(Request.Form.Files["ProcesoBeca"], "ProcesoBeca", CategoryFolderId, "application/pdf");
                scholarInfo.TrayectoriaEscolar = await UploadFileToDrive(Request.Form.Files["TrayectoriaEscolar"], "TrayectoriaEscolar", CategoryFolderId, "application/pdf");
                scholarInfo.ProcesoTitulacion = await UploadFileToDrive(Request.Form.Files["ProcesoTitulacion"], "ProcesoTitulacion", CategoryFolderId, "application/pdf");
                scholarInfo.ProcesoDobleTitulacion = await UploadFileToDrive(Request.Form.Files["ProcesoDobleTitulacion"], "ProcesoDobleTitulacion", CategoryFolderId, "application/pdf");

                codeStatus = await ProgramLogic.SaveScholarProcess(idProgram, scholarInfo, status);

                if (codeStatus == 200)
                {
                    return RedirectToPage("/CoordinatorPages/ProgramIndicators/StudentInfo", new { idProgram, Username, ParentFolderId });
                }
                else
                {
                    return RedirectToPage("/Error");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("There is an error on ScholarProcess Page" + ex);
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