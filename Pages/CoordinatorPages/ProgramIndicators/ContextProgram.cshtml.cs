using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Posgrev_Frontend.Logic;
using Posgrev_Frontend.Models;

namespace Posgrev_Frontend.Pages.CoordinatorPages.ProgramIndicators
{
    public class ContextProgramModel : PageModel
    {

        public string? IdProg {get; set;}
        public string? Username {get; set;}
        public string? ParentFolderId {get; set;}
        public string? CategoryFolderId {get; set;}

        public IActionResult OnGet(string idProgram, string username, string parentFolderId)
        {
            try
            {
                IdProg = idProgram;
                Username = username;
                ParentFolderId = parentFolderId;
                
                DriveLogic driveLogic = new DriveLogic();
                CategoryFolderId = driveLogic.CreateIndicatorFolder(ParentFolderId, "Contexto Social de la Institucion");
            }
            catch (Exception ex)
            {
                Console.WriteLine("There is an error on AdministratorPage" + ex);
                return RedirectToPage("/Error");
            }
            return Page();
        }


         public async Task<IActionResult> OnPost(string idProgram, string status, Compromiso info, string Username, string ParentFolderId, string CategoryFolderId)
        {
            int statusCode = 500;
            try{
                info.CompromisoPosgrado = await UploadFileToDrive(Request.Form.Files["CompromisoPosgrado"], "CompromisoPosgrado", CategoryFolderId, "application/pdf");
                info.Vinculacion = await UploadFileToDrive(Request.Form.Files["Vinculacion"], "Vinculacion", CategoryFolderId, "application/pdf");
                info.ActividadesRetribucion = await UploadFileToDrive(Request.Form.Files["ActividadesRetribucion"], "ActividadesRetribucion", CategoryFolderId, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

                statusCode = await ProgramLogic.SaveContextProgram(idProgram, info, status);

                if(statusCode == 500)
                {
                    return RedirectToPage("/Error");
                } 
                else
                {
                    return RedirectToPage("/CoordinatorPages/ProgramIndicators/ProgramStructure", new { idProgram, Username, ParentFolderId });
                }

            }catch(Exception ex){
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