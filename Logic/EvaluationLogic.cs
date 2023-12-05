using System.Text;
using Newtonsoft.Json;
using Posgrev_Frontend.Models;

namespace Posgrev_Frontend.Logic
{
    public static class EvaluationLogic
    {


        public static async Task<int> SaveEvaluationPeriod(EvaluationPeriodModel newEvaluationPeriod)
        {
            int statusCode = 500;

            try
            {
                
                var data = new {
                    identificadorPeriodoEvaluacion = newEvaluationPeriod.IdentificadorPeriodoEvaluacion,
                    fechaInicio = newEvaluationPeriod.FechaInicio,
                    fechaLimite = newEvaluationPeriod.FechaLimite
                };

                string dataToSend = JsonConvert.SerializeObject(data);

                HttpClient server = new HttpClient();
                string url = "http://localhost:3000/createEvaluationPeriod";
                HttpContent contentToSend = new StringContent(dataToSend, Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = await server.PostAsync(url, contentToSend);

                if(responseMessage.IsSuccessStatusCode)
                {
                    statusCode = 200;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("There was an error creating the evaluation period " + ex);
            }

            return statusCode;
        }


        public static async Task<List<EvaluationPeriodModel>> GetAllEvaluationPeriods()
        {
            List<EvaluationPeriodModel> evaluationPeriods = null;

            try
            {
                HttpClient server = new HttpClient();
                string apiUrl = "http://localhost:3000/getAllEvaluationPeriods";
                HttpResponseMessage responseMessage = await server.GetAsync(apiUrl);
                string jsonResponse = await responseMessage.Content.ReadAsStringAsync();

                if (responseMessage.IsSuccessStatusCode)
                {
                    ApiResponseEvaluation evaluationPeriodDescerialized = JsonConvert.DeserializeObject<ApiResponseEvaluation>(jsonResponse);
                    evaluationPeriods = evaluationPeriodDescerialized.EvaluationPeriods;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("There is an error on GetActivePrograms" + ex);
            }

            return evaluationPeriods;

        }
    }
}