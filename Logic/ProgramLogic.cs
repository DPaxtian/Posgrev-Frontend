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


        public static async Task<int> EditProgram(ProgramModel editedInformation)
        {
            int statusCode = 500;

            try
            {
                var information = new
                {
                    nombrePrograma = editedInformation.InformacionBasica.NombrePrograma,
                    claveDGP = editedInformation.InformacionBasica.ClaveDGP,
                    nivel = editedInformation.InformacionBasica.Nivel,
                    clavePrograma = editedInformation.InformacionBasica.ClavePrograma,
                    region = editedInformation.InformacionBasica.Region,
                    area = editedInformation.InformacionBasica.Area,
                    numDependencia = editedInformation.InformacionBasica.NumDependencia,
                    correoCoordinador = editedInformation.InformacionBasica.CorreoCoordinador,
                    nombreCoordinador = editedInformation.InformacionBasica.NombreCoordinador,
                    anioPrograma = editedInformation.InformacionBasica.AnioPrograma
                };

                var data = new {
                    activo = editedInformation.Activo,
                    informacionBasica = information
                };

                string dataToSend = JsonConvert.SerializeObject(data);

                HttpClient server = new HttpClient();
                string url = "http://localhost:3000/modifyProgram/" + editedInformation.IdentificadorPrograma;
                HttpContent contentToSend = new StringContent(dataToSend, Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = await server.PutAsync(url, contentToSend);

                if(responseMessage.IsSuccessStatusCode)
                {
                    statusCode = 200;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("There was an error editing the program" + ex);
            }

            return statusCode;
        }



        public static async Task<ProgramModel> GetProgramDetails(string idProgram)
        {
            ProgramModel programObtained = null;

            try
            {
                HttpClient server = new HttpClient();
                string url = "http://localhost:3000/getProgramDetails/" + idProgram;
                HttpResponseMessage message = await server.GetAsync(url);
                string jsonResponse = await message.Content.ReadAsStringAsync();

                if(message.IsSuccessStatusCode)
                {
                    ApiResponseProgramDetails programDescerialized = JsonConvert.DeserializeObject<ApiResponseProgramDetails>(jsonResponse);
                    programObtained = programDescerialized.Program;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("There is an error at GetProgramDetails" + ex);
            }

            return programObtained;
        }

    }
}