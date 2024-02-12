using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Posgrev_Frontend.Logic;
using Posgrev_Frontend.Models;

namespace Posgrev_Frontend.Pages.CoordinatorPages.ProgramIndicators
{
    public class StudentInfoModel : PageModel
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
                CategoryFolderId = driveLogic.CreateIndicatorFolder(ParentFolderId, "Informacion del Estudiante");
            }
            catch (Exception ex)
            {
                Console.WriteLine("There is an error on AdministratorPage" + ex);
                return RedirectToPage("/Error");
            }
            return Page();
        }


        public async Task<IActionResult> OnPost(string idProgram, string Username, InformacionSeguimiento newInfo, string status, string ParentFolderId, string CategoryFolderId)
        {
            int statusCode = 500;

            try
            {
                newInfo.Demanda = new Demanda
                {
                    TotalAspirantes = int.Parse(Request.Form["InformacionSeguimiento.Demanda.TotalAspirantes"])
                };

                newInfo.AspirantesSeleccionados = new AspirantesSeleccionados
                {
                    NumAspirantesSeleccionados = int.Parse(Request.Form["InformacionSeguimiento.AspirantesSeleccionados.NumAspirantesSeleccionados"])
                };

                newInfo.TasaTitulacion = new TasaTitulacion
                {
                    PorcentajeTasaTitulacion = Request.Form["InformacionSeguimiento.TasaTitulacion.PorcentajeTasaTitulacion"]
                };

                newInfo.TasaTerminal = new TasaTerminal();

                newInfo.EstrategiasAntiplagio = await UploadFileToDrive(Request.Form.Files["EstrategiasAntiplagio"], "EstrategiasAntiplagio", CategoryFolderId, "application/pdf");
                newInfo.EstudioFactibilidad = await UploadFileToDrive(Request.Form.Files["EstudioFactibilidad"], "EstudioFactibilidad", CategoryFolderId, "application/pdf");
                newInfo.Demanda.InformacionAspirantes = await UploadFileToDrive(Request.Form.Files["InformacionSeguimiento.Demanda.InformacionAspirantes"], "InformacionAspirantes", CategoryFolderId, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                newInfo.AspirantesSeleccionados.InformacionAspirantesSeleccionados = await UploadFileToDrive(Request.Form.Files["InformacionSeguimiento.AspirantesSeleccionados.InformacionAspirantesSeleccionados"], "InformacionAspirantesSeleccionados", CategoryFolderId, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                newInfo.TasaTitulacion.InformacionTitulados = await UploadFileToDrive(Request.Form.Files["InformacionSeguimiento.TasaTitulacion.InformacionTitulados"], "InformacionTitulados", CategoryFolderId, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                newInfo.TasaTerminal.TasaEficienciaTerminal = await UploadFileToDrive(Request.Form.Files["InformacionSeguimiento.TasaTerminal.TasaEficienciaTerminal"], "TasaEficienciaTerminal", CategoryFolderId, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                newInfo.TasaTerminal.AnalisisEficienciaTerminal = await UploadFileToDrive(Request.Form.Files["InformacionSeguimiento.TasaTerminal.AnalisisEficienciaTerminal"], "AnalisisEficienciaTerminal", CategoryFolderId, "application/pdf");
                newInfo.MovilidadEstudiantil = await UploadFileToDrive(Request.Form.Files["MovilidadEstudiantil"], "MovilidadEstudiantil", CategoryFolderId, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                newInfo.ApoyoCondonacion = await UploadFileToDrive(Request.Form.Files["ApoyoCondonacion"], "ApoyoCondonacion", CategoryFolderId, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                newInfo.BecasEstudiantiles = await UploadFileToDrive(Request.Form.Files["BecasEstudiantiles"], "BecasEstudiantiles", CategoryFolderId, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                newInfo.BajasEstudiantiles = await UploadFileToDrive(Request.Form.Files["BajasEstudiantiles"], "BajasEstudiantiles", CategoryFolderId, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                newInfo.ColabSecSoc = await UploadFileToDrive(Request.Form.Files["ColabSecSoc"], "ColabSecSoc", CategoryFolderId, "application/pdf");
                newInfo.CuotaRecuperacionGeneracion = await UploadFileToDrive(Request.Form.Files["CuotaRecuperacionGeneracion"], "CuotaRecuperacionGeneracion", CategoryFolderId, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                newInfo.RedEgresados = await UploadFileToDrive(Request.Form.Files["RedEgresados"], "RedEgresados", CategoryFolderId, "application/pdf");
                newInfo.DireccionLgac = await UploadFileToDrive(Request.Form.Files["DireccionLgac"], "DireccionLgac", CategoryFolderId, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                newInfo.DireccionTesis = await UploadFileToDrive(Request.Form.Files["DireccionTesis"], "DireccionTesis", CategoryFolderId, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                newInfo.TutoriasProfEst = await UploadFileToDrive(Request.Form.Files["TutoriasProfEst"], "TutoriasProfEst", CategoryFolderId, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                newInfo.ComiteGraduacion = await UploadFileToDrive(Request.Form.Files["ComiteGraduacion"], "ComiteGraduacion", CategoryFolderId, "application/pdf");
                
                statusCode = await ProgramLogic.SaveStudentInfo(idProgram, newInfo, status);

                if (statusCode == 200)
                {
                    return RedirectToPage("/CoordinatorPages/ProgramIndicators/Results", new { idProgram, Username, ParentFolderId });
                }
                else
                {
                    return RedirectToPage("/Error");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("There is an error in StudentInfo" + ex);
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