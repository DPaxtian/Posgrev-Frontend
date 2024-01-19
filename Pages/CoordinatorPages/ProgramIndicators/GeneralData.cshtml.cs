using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Posgrev_Frontend.Logic;
using Posgrev_Frontend.Models;

namespace Posgrev_Frontend.Pages.CoordinatorPages.ProgramIndicators
{
    public class GeneralDataModel : PageModel
    {
        int statusCode = 500;
        public ProgramModel? programInfo;
        public Adscripciones? adscripciones;
        public string? IdProg { get; set; }

        public async Task<IActionResult> OnGetAsync(string idProgram)
        {
            try
            {
                IdProg = idProgram;
                programInfo = await ProgramLogic.GetProgramDetails(IdProg);
                adscripciones = await ProgramLogic.GetAdscriptions();

                if (programInfo != null && adscripciones != null)
                {
                    statusCode = 200;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("There is an error on AdministratorPage" + ex);
            }

            if (statusCode == 200)
            {
                return Page();
            }
            else
            {
                return RedirectToPage("/Error");
            }

        }


        public async Task<IActionResult> OnPost(string idProgram, DatosGenerales newGeneralDataInformation)
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

            statusCode = await ProgramLogic.SaveGeneralData(idProgram, newGeneralDataInformation);

            if (statusCode == 200)
            {
                return RedirectToPage("/CoordinatorPages/ProgramIndicators/ContextProgram");
            }
            else
            {
                return RedirectToPage("/Error");
            }
        }
    }
}