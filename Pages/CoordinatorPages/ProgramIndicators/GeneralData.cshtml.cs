using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Posgrev_Frontend.Logic;
using Posgrev_Frontend.Models;
using System.Reflection;

namespace Posgrev_Frontend.Pages.CoordinatorPages.ProgramIndicators
{
    public class GeneralDataModel : PageModel
    {
        int statusCode = 500;
        public ProgramModel? programInfo;
        public Adscripciones? adscripciones;
        public string? IdProg { get; set; }
        public string? Username { get; set; }
        public string? EvaluationPeriod { get; set; }
        public string? FolderId { get; set; }
        public string? GeneralDataFolder {get; set;}

        public async Task<IActionResult> OnGetAsync(string idProgram, string username, string evaluationPeriod)
        {
            try
            {
                IdProg = idProgram;
                Username = username;
                EvaluationPeriod = evaluationPeriod;
                programInfo = await ProgramLogic.GetProgramDetails(IdProg);
                adscripciones = await ProgramLogic.GetAdscriptions();

                if (programInfo != null && adscripciones != null)
                {
                    statusCode = 200;
                }
            }
            catch (Exception ex)
            {
                return RedirectToPage("/Error");
            }

            if (statusCode == 200)
            {
                DriveLogic driveLogic = new DriveLogic();
                FolderId = driveLogic.CreateFolder(programInfo.DatosGenerales.Denominacion, EvaluationPeriod);
                GeneralDataFolder = driveLogic.CreateIndicatorFolder(FolderId, "Datos Generales del Programa");
                return Page();
            }
            else
            {
                return RedirectToPage("/Error");
            }

        }


        public async Task<IActionResult> OnPost(string idProgram, string Username, DatosGenerales newGeneralDataInformation, string status, string CategoryFolderId, string ParentFolderId)
        {
            try
            {
                newGeneralDataInformation.Antecedentes = new Antecedentes
                {
                    FechaAprobacion = Request.Form["DatosGenerales.Antecedentes.FechaAprobacion"],
                    InicioAct = Request.Form["DatosGenerales.Antecedentes.InicioAct"]
                };

                newGeneralDataInformation.Adsreg = new Adsreg
                {
                    Adscripcion = Request.Form["DatosGenerales.Adsreg.Adscripcion"],
                    Region = Request.Form["DatosGenerales.Adsreg.Region"]
                };


                var pronacesValues = Request.Form["pronaces"];
                if (pronacesValues.Count > 0)
                {
                    newGeneralDataInformation.Pronaces = pronacesValues.ToList();
                }
                else
                {
                    newGeneralDataInformation.Pronaces = new List<string>();
                }

                newGeneralDataInformation.RegistroProfesiones = await UploadFileToDrive(Request.Form.Files["RegistroProfesiones"], "RegistroProfesiones", CategoryFolderId, "application/pdf");
                newGeneralDataInformation.CuotaRecuperacion = await UploadFileToDrive(Request.Form.Files["CuotaRecuperacion"], "CuotaRecuperacion", CategoryFolderId, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");


                statusCode = await ProgramLogic.SaveGeneralData(idProgram, newGeneralDataInformation, status);

                if (statusCode == 200)
                {
                    return RedirectToPage("/CoordinatorPages/ProgramIndicators/ContextProgram", new { idProgram, Username, ParentFolderId });
                }
                else
                {
                    return RedirectToPage("/Error");
                }
            }
            catch (Exception ex)
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