using System.Text;
using Newtonsoft.Json;
using Posgrev_Frontend.Models;

namespace Posgrev_Frontend.Logic
{
    public static class ProgramLogic
    {


        public static async Task<List<ProgramModel>> GetActivePrograms()
        {
            List<ProgramModel> programs = null;

            try
            {
                HttpClient server = new HttpClient();
                string apiUrl = "http://localhost:3000/getAllPrograms";
                HttpResponseMessage responseMessage = await server.GetAsync(apiUrl);
                string jsonResponse = await responseMessage.Content.ReadAsStringAsync();

                if (responseMessage.IsSuccessStatusCode)
                {
                    ApiResponseProgram programDescerialized = JsonConvert.DeserializeObject<ApiResponseProgram>(jsonResponse);
                    programs = programDescerialized.Programs;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("There is an error on GetActivePrograms" + ex);
            }

            return programs;
        }


        public static async Task<int> SaveBasicInformation(InformacionBasica newInformation)
        {
            int statusCode = 500;

            try
            {
                var information = new
                {
                    nombrePrograma = newInformation.NombrePrograma,
                    claveDGP = newInformation.ClaveDGP,
                    nivel = newInformation.Nivel,
                    clavePrograma = newInformation.ClavePrograma,
                    region = newInformation.Region,
                    area = newInformation.Area,
                    numDependencia = newInformation.NumDependencia,
                    correoCoordinador = newInformation.CorreoCoordinador,
                    nombreCoordinador = newInformation.NombreCoordinador,
                    anioPrograma = newInformation.AnioPrograma
                };

                var data = new {
                    informacionBasica = information
                };


                string dataToSend = JsonConvert.SerializeObject(data);
                Console.WriteLine(dataToSend);

                HttpClient server = new HttpClient();
                string url = "http://localhost:3000/createProgram";
                HttpContent contentToSend = new StringContent(dataToSend, Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = await server.PostAsync(url, contentToSend);

                if(responseMessage.IsSuccessStatusCode)
                {
                    statusCode = 200;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("There was an error creating the program" + ex);
            }

            return statusCode;
        }


    }
}