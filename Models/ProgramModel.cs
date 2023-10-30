using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Posgrev_Frontend.Models
{
    public partial class ProgramModel
    {
        [JsonProperty("informacionBasica")]
        public InformacionBasica? InformacionBasica { get; set; }

        [JsonProperty("datosGenerales")]
        public DatosGenerales? DatosGenerales { get; set; }

        [JsonProperty("infraestructuraPrograma")]
        public InfraestructuraPrograma? InfraestructuraPrograma { get; set; }

        [JsonProperty("procesosEscolares")]
        public ProcesosEscolares? ProcesosEscolares { get; set; }

        [JsonProperty("pertinencia")]
        public Pertinencia? Pertinencia { get; set; }

        [JsonProperty("resultados")]
        public Resultados? Resultados { get; set; }

        [JsonProperty("_id")]
        public string? Id { get; set; }

        [JsonProperty("__v")]
        public int? __v { get; set; }

        [JsonProperty("identificadorPrograma")]
        public string? IdentificadorPrograma { get; set; }

        [JsonProperty("activo")]
        public string? Activo { get; set; }
    }


    public partial class Adsreg
    {
        [JsonProperty("adscripcion")]
        public string? Adscripcion { get; set; }

        [JsonProperty("region")]
        public string? Region { get; set; }
    }

    public partial class Antecedentes
    {
        [JsonProperty("fechaCreacion")]
        public string? FechaCreacion { get; set; }

        [JsonProperty("fechaAprobacion")]
        public string? FechaAprobacion { get; set; }

        [JsonProperty("inicioAct")]
        public string? InicioAct { get; set; }
    }

    public class DatosGenerales
    {
        [JsonProperty("antecedentes")]
        public Antecedentes? Antecedentes { get; set; }

        [JsonProperty("adsreg")]
        public Adsreg? Adsreg { get; set; }

        [JsonProperty("denominacion")]
        public string? Denominacion { get; set; }

        [JsonProperty("nivel")]
        public string? Nivel { get; set; }

        [JsonProperty("modalidad")]
        public string? Modalidad { get; set; }

        [JsonProperty("orientacion")]
        public string? Orientacion { get; set; }

        [JsonProperty("paginaWeb")]
        public string? PaginaWeb { get; set; }

        [JsonProperty("registroProfesiones")]
        public string? RegistroProfesiones { get; set; }

        [JsonProperty("compromiso")]
        public string? Compromiso { get; set; }
    }

    public partial class InformacionBasica
    {
        [JsonProperty("nombrePrograma")]
        public string? NombrePrograma { get; set; }

        [JsonProperty("claveDGP")]
        public string? ClaveDGP { get; set; }

        [JsonProperty("nivel")]
        public string? Nivel { get; set; }

        [JsonProperty("clavePrograma")]
        public string? ClavePrograma { get; set; }

        [JsonProperty("nombreCoordinador")]
        public string? NombreCoordinador {get; set;}

        [JsonProperty("correoCoordinador")]
        public string? CorreoCoordinador {get; set;}

        [JsonProperty("region")]
        public string? Region {get; set;}

        [JsonProperty("area")]
        public string? Area {get; set;}

        [JsonProperty("anioPrograma")]
        public string? AnioPrograma {get; set;}

        [JsonProperty("numDependencia")]
        public string? NumDependencia {get; set;}
    }

    public partial class InfraestructuraPrograma
    {
        [JsonProperty("planEstudiosActualizado")]
        public PlanEstudiosActualizado? PlanEstudiosActualizado { get; set; }

        [JsonProperty("nucleoAcadBas")]
        public NucleoAcadBas? NucleoAcadBas { get; set; }

        [JsonProperty("planEstudios")]
        public string? PlanEstudios { get; set; }

        [JsonProperty("mapaCurricular")]
        public string? MapaCurricular { get; set; }

        [JsonProperty("lgac")]
        public string? Lgac { get; set; }

        [JsonProperty("infraestructuraPrograma")]
        public string? InfraestrucPrograma { get; set; }

        [JsonProperty("ejercicioFinanciero")]
        public string? EjercicioFinanciero { get; set; }

        [JsonProperty("duracionPrograma")]
        public string? DuracionPrograma { get; set; }

        [JsonProperty("periodosLectivos")]
        public string? PeriodosLectivos { get; set; }

        [JsonProperty("periodicidadNuevIngre")]
        public string? PeriodicidadNuevIngre { get; set; }
    }

    public partial class NucleoAcadBas
    {
        [JsonProperty("profTiemCom")]
        public string? ProfTiemCom { get; set; }

        [JsonProperty("profTiemPar")]
        public string? ProfTiemPar { get; set; }

        [JsonProperty("colaboradores")]
        public string? Colaboradores { get; set; }

        [JsonProperty("infoProf")]
        public string? InfoProf { get; set; }
    }

    public partial class Pertinencia
    {
        [JsonProperty("estudioFactibilidad")]
        public string? EstudioFactibilidad { get; set; }

        [JsonProperty("redEgresados")]
        public string? RedEgresados { get; set; }

        [JsonProperty("colabSecSoc")]
        public string? ColabSecSoc { get; set; }

        [JsonProperty("tasaTerminar")]
        public string? TasaTerminar { get; set; }
    }

    public partial class PlanEstudiosActualizado
    {
        [JsonProperty("fechaAprob")]
        public string? FechaAprob { get; set; }

        [JsonProperty("acta")]
        public string? Acta { get; set; }
    }

    public partial class ProcesosEscolares
    {
        [JsonProperty("procesoAdmin")]
        public string? ProcesoAdmin { get; set; }

        [JsonProperty("adminEscolar")]
        public string? AdminEscolar { get; set; }

        [JsonProperty("trayectoriaEscolar")]
        public string? TrayectoriaEscolar { get; set; }

        [JsonProperty("bajasEstudiantes")]
        public string? BajasEstudiantes { get; set; }

        [JsonProperty("opcTitulacion")]
        public string? OpcTitulacion { get; set; }

        [JsonProperty("lengExtranjera")]
        public string? LengExtranjera { get; set; }

        [JsonProperty("estrategiasAntiplagio")]
        public string? EstrategiasAntiplagio { get; set; }

        [JsonProperty("pertinenciaSocial")]
        public string? PertinenciaSocial { get; set; }

        [JsonProperty("colabSociedad")]
        public string? ColabSociedad { get; set; }

        [JsonProperty("pronaces")]
        public string? Pronaces { get; set; }
    }

    

    public partial class Resultados
    {
        [JsonProperty("demanda")]
        public string? Demanda { get; set; }

        [JsonProperty("planMejora")]
        public string? PlanMejora { get; set; }

        [JsonProperty("prodAcademica")]
        public string? ProdAcademica { get; set; }

        [JsonProperty("direccionTesis")]
        public string? DireccionTesis { get; set; }

        [JsonProperty("tutoriasProfEst")]
        public string? TutoriasProfEst { get; set; }

        [JsonProperty("comiteGraduacion")]
        public string? ComiteGraduacion { get; set; }

        [JsonProperty("tutorias")]
        public string? Tutorias { get; set; }

        [JsonProperty("reporteDesDoc")]
        public string? ReporteDesDoc { get; set; }

        [JsonProperty("reporteAutoeval")]
        public string? ReporteAutoeval { get; set; }
    }
}