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
                string apiUrl = "https://posgrev-backend-programs-krau.onrender.com/getAllPrograms";
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


        public static async Task<int> SaveBasicInformation(string idUser, InformacionBasica newInformation)
        {
            int statusCode = 500;

            try
            {
                DatosUsuario coordinatorProgram = await UserLogic.GetUserDetails(idUser);
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
                    nombreCoordinador = coordinatorProgram.nombreUsuario,
                    anioPrograma = newInformation.AnioPrograma
                };

                var data = new
                {
                    identificadorPrograma = coordinatorProgram.identificadorPrograma,
                    informacionBasica = information
                };

                string dataToSend = JsonConvert.SerializeObject(data);

                HttpClient server = new HttpClient();
                string url = "https://posgrev-backend-programs-krau.onrender.com/createProgram";
                HttpContent contentToSend = new StringContent(dataToSend, Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = await server.PostAsync(url, contentToSend);

                if (responseMessage.IsSuccessStatusCode)
                {
                    statusCode = 200;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error creating the program" + ex);
            }

            return statusCode;
        }


        public static async Task<int> CreateProgramIndicator(string idProgram, string evaluationPeriod)
        {
            int statusCode = 500;

            try
            {
                var information = new
                {
                    identificadorPrograma = idProgram,
                    periodoEvaluacion = evaluationPeriod
                };


                string dataToSend = JsonConvert.SerializeObject(information);

                HttpClient server = new HttpClient();
                string url = "https://posgrev-backend-programs-krau.onrender.com/createProgramIndicator";
                HttpContent contentToSend = new StringContent(dataToSend, Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = await server.PostAsync(url, contentToSend);

                if (responseMessage.IsSuccessStatusCode)
                {
                    statusCode = 200;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error creating the program" + ex);
            }

            return statusCode;
        }



        public static async Task<int> EditProgram(string idUser, ProgramModel editedInformation)
        {
            int statusCode = 500;

            try
            {
                DatosUsuario coordinatorProgram = await UserLogic.GetUserDetails(idUser);
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
                    nombreCoordinador = coordinatorProgram.nombreUsuario,
                    anioPrograma = editedInformation.InformacionBasica.AnioPrograma
                };

                var data = new
                {
                    activo = editedInformation.Activo,
                    identificadorPrograma = coordinatorProgram.identificadorPrograma,
                    informacionBasica = information
                };

                string dataToSend = JsonConvert.SerializeObject(data);

                HttpClient server = new HttpClient();
                string url = "https://posgrev-backend-programs-krau.onrender.com/modifyProgram/" + editedInformation.IdentificadorPrograma;
                HttpContent contentToSend = new StringContent(dataToSend, Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = await server.PutAsync(url, contentToSend);

                if (responseMessage.IsSuccessStatusCode)
                {
                    statusCode = 200;
                }
            }
            catch (Exception ex)
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
                string url = "https://posgrev-backend-programs-krau.onrender.com/getProgramDetails/" + idProgram;
                HttpResponseMessage message = await server.GetAsync(url);
                string jsonResponse = await message.Content.ReadAsStringAsync();

                if (message.IsSuccessStatusCode)
                {
                    ApiResponseProgramDetails programDescerialized = JsonConvert.DeserializeObject<ApiResponseProgramDetails>(jsonResponse);
                    programObtained = programDescerialized.Program;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("There is an error at GetProgramDetails" + ex);
            }

            return programObtained;
        }


        public static async Task<int> SaveGeneralData(string idProgram, DatosGenerales generalData, string status)
        {
            int statusCode = 500;

            try
            {

                var generalInformation = new
                {
                    estado = status,
                    denominacion = generalData.Denominacion,
                    antecedentes = generalData.Antecedentes,
                    adsreg = generalData.Adsreg,
                    nivel = generalData.Nivel,
                    modalidad = generalData.Modalidad,
                    orientacion = generalData.Orientacion,
                    paginaWeb = generalData.PaginaWeb,
                    duracion = generalData.Duracion,
                    periodosLectivos = generalData.PeriodosLectivos,
                    periodicidadConvocatoria = generalData.PeriodicidadConvocatoria,
                    pronaces = generalData.Pronaces,
                    registroProfesiones = generalData.RegistroProfesiones,
                    cuotaRecuperacion = generalData.CuotaRecuperacion
                };

                string dataToSend = JsonConvert.SerializeObject(generalInformation);

                HttpClient server = new HttpClient();
                string url = "https://posgrev-backend-programs-krau.onrender.com/saveGeneralData/" + idProgram;
                HttpContent contentToSend = new StringContent(dataToSend, Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = await server.PatchAsync(url, contentToSend);

                if (responseMessage.IsSuccessStatusCode)
                {
                    statusCode = 200;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error saving the general data" + ex);
            }

            return statusCode;
        }


        public static async Task<int> SaveContextProgram(string idProgram, Compromiso info, string status)
        {
            int statusCode = 500;

            try
            {

                var generalInformation = new
                {
                    estado = status,
                    compromisoPosgrado = info.CompromisoPosgrado,
                    vinculacion = info.Vinculacion,
                    actividadesRetribucion = info.ActividadesRetribucion
                };

                string dataToSend = JsonConvert.SerializeObject(generalInformation);

                HttpClient server = new HttpClient();
                string url = "https://posgrev-backend-programs-krau.onrender.com/saveProgramContext/" + idProgram;
                HttpContent contentToSend = new StringContent(dataToSend, Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = await server.PatchAsync(url, contentToSend);

                if (responseMessage.IsSuccessStatusCode)
                {
                    statusCode = 200;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error saving the general data" + ex);
            }

            return statusCode;
        }



        public static async Task<int> SaveProgramInfrastucture(string idProgram, InfraestructuraPrograma infrastucture, string status)
        {
            int statusCode = 500;

            try
            {

                var programInfrastucture = new
                {
                    estado = status,
                    nucleoAcadBas = infrastucture.NucleoAcadBas,
                    fechaActualizacionPlan = infrastucture.FechaActualizacionPlan,
                    cambiosPlan = infrastucture.CambiosPlan,
                    planEstudios = infrastucture.PlanEstudios,
                    mapaCurricular = infrastucture.MapaCurricular,
                    lgac = infrastucture.Lgac,
                    infraestructuraPrograma = infrastucture.InfraestrucPrograma,
                    actaConsejoConsultivo = infrastucture.ActaConsejoConsultivo,
                    actaConsejoArea = infrastucture.ActaConsejoArea,
                    actaActualizacionPlan = infrastucture.ActaActualizacionPlan
                };

                string dataToSend = JsonConvert.SerializeObject(programInfrastucture);

                HttpClient server = new HttpClient();
                string url = "https://posgrev-backend-programs-krau.onrender.com/saveProgramInfrastucture/" + idProgram;
                HttpContent contentToSend = new StringContent(dataToSend, Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = await server.PatchAsync(url, contentToSend);

                if (responseMessage.IsSuccessStatusCode)
                {
                    statusCode = 200;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error saving the programInfrastucture" + ex);
            }

            return statusCode;
        }



        public static async Task<int> SaveScholarProcess(string idProgram, ProcesosEscolares scholarInfo, string status)
        {
            int statusCode = 500;

            try
            {

                var scholarProcess = new
                {
                    estado = status,
                    procesoAdmision = scholarInfo.ProcesoAdmision,
                    procesoMovilidad = scholarInfo.ProcesoMovilidad,
                    procesoCondonacion = scholarInfo.ProcesoCondonacion,
                    procesoBeca = scholarInfo.ProcesoBeca,
                    trayectoriaEscolar = scholarInfo.TrayectoriaEscolar,
                    procesoTitulacion = scholarInfo.ProcesoTitulacion,
                    procesoDobleTitulacion = scholarInfo.ProcesoDobleTitulacion
                };

                string dataToSend = JsonConvert.SerializeObject(scholarProcess);

                HttpClient server = new HttpClient();
                string url = "https://posgrev-backend-programs-krau.onrender.com/saveScholarProcess/" + idProgram;
                HttpContent contentToSend = new StringContent(dataToSend, Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = await server.PatchAsync(url, contentToSend);

                if (responseMessage.IsSuccessStatusCode)
                {
                    statusCode = 200;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error saving the scholar process" + ex);
            }

            return statusCode;
        }


        public static async Task<int> SaveStudentInfo(string idProgram, InformacionSeguimiento studentInfo, string status)
        {
            int statusCode = 500;

            try
            {

                var info = new
                {
                    estado = status,
                    demanda = studentInfo.Demanda,
                    aspirantesSeleccionados = studentInfo.AspirantesSeleccionados,
                    tasaTitulacion = studentInfo.TasaTitulacion,
                    tasaTerminal = studentInfo.TasaTerminal,
                    estrategiasAntiplagio = studentInfo.EstrategiasAntiplagio,
                    estudioFactibilidad = studentInfo.EstudioFactibilidad,
                    movilidadEstudiantil = studentInfo.MovilidadEstudiantil,
                    apoyoCondonacion = studentInfo.ApoyoCondonacion,
                    becasEstudiantiles = studentInfo.BecasEstudiantiles,
                    bajasEstudiantiles = studentInfo.BajasEstudiantiles,
                    colabSecSoc = studentInfo.ColabSecSoc,
                    cuotaRecuperacionGeneracion = studentInfo.CuotaRecuperacionGeneracion,
                    redEgresados = studentInfo.RedEgresados,
                    produccionLgac = studentInfo.DireccionLgac,
                    direccionTesis = studentInfo.DireccionTesis,
                    tutoriasProfEst = studentInfo.TutoriasProfEst,
                    comiteGraduacion = studentInfo.ComiteGraduacion
                };

                string dataToSend = JsonConvert.SerializeObject(info);

                Console.WriteLine(dataToSend);

                HttpClient server = new HttpClient();
                string url = "https://posgrev-backend-programs-krau.onrender.com/saveStudentInfo/" + idProgram;
                HttpContent contentToSend = new StringContent(dataToSend, Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = await server.PatchAsync(url, contentToSend);

                if (responseMessage.IsSuccessStatusCode)
                {
                    statusCode = 200;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error saving the studentInfo" + ex);
            }

            return statusCode;
        }


        public static async Task<int> SaveResults(string idProgram, Resultados resultsInfo, string status)
        {
            int statusCode = 500;

            try
            {

                var info = new
                {
                    estado = status,
                    percepcionPrograma = resultsInfo.PercepcionPrograma,
                    planMejora = resultsInfo.PlanMejora,
                    reporteAutoeval = resultsInfo.ReporteAutoeval
                };

                string dataToSend = JsonConvert.SerializeObject(info);

                Console.WriteLine(dataToSend);

                HttpClient server = new HttpClient();
                string url = "https://posgrev-backend-programs-krau.onrender.com/saveResults/" + idProgram;
                HttpContent contentToSend = new StringContent(dataToSend, Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = await server.PatchAsync(url, contentToSend);

                if (responseMessage.IsSuccessStatusCode)
                {
                    statusCode = 200;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error saving the studentInfo" + ex);
            }

            return statusCode;
        }


        public static async Task<Denominaciones> GetDenominations()
        {
            Denominaciones denominaciones = new Denominaciones();

            try
            {
                HttpClient server = new HttpClient();
                string apiUrl = "https://posgrev-backend-programs-krau.onrender.com/getDenominations";
                HttpResponseMessage responseMessage = await server.GetAsync(apiUrl);
                string jsonResponse = await responseMessage.Content.ReadAsStringAsync();

                if (responseMessage.IsSuccessStatusCode)
                {
                    ApiResponseDenominations programDescerialized = JsonConvert.DeserializeObject<ApiResponseDenominations>(jsonResponse);
                    denominaciones = programDescerialized.Denominaciones;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("There is an error on GetActivePrograms" + ex);
            }

            return denominaciones;
        }



        public static async Task<Adscripciones> GetAdscriptions()
        {
            Adscripciones adscripciones = new Adscripciones();

            try
            {
                HttpClient server = new HttpClient();
                string apiUrl = "https://posgrev-backend-programs-krau.onrender.com/getAdscriptions";
                HttpResponseMessage responseMessage = await server.GetAsync(apiUrl);
                string jsonResponse = await responseMessage.Content.ReadAsStringAsync();

                if (responseMessage.IsSuccessStatusCode)
                {
                    ApiResponseAdscriptions programDescerialized = JsonConvert.DeserializeObject<ApiResponseAdscriptions>(jsonResponse);
                    adscripciones = programDescerialized.Adscripciones;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("There is an error on GetActivePrograms" + ex);
            }

            return adscripciones;
        }

    }
}