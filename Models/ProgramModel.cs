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

        [JsonProperty("compromiso")]
        public Compromiso? Compromiso {get; set;}

        [JsonProperty("infraestructuraPrograma")]
        public InfraestructuraPrograma? InfraestructuraPrograma { get; set; }

        [JsonProperty("procesosEscolares")]
        public ProcesosEscolares? ProcesosEscolares { get; set; }

        [JsonProperty("informacionSeguimientto")]
        public InformacionSeguimiento? InformacionSeguimiento {get; set;}

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


    public partial class Compromiso
    {
        [JsonProperty("compromisoPosgrado")]
        public string? CompromisoPosgrado {get; set;}

        [JsonProperty("vinculacion")]
        public string? Vinculacion {get; set;}

        [JsonProperty("actividadesRetribucion")]
        public string? ActividadesRetribucion {get; set;}
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
        [JsonProperty("fechaAprobacion")]
        public string? FechaAprobacion { get; set; }

        [JsonProperty("inicioAct")]
        public string? InicioAct { get; set; }
    }

    public class DatosGenerales
    {
        [JsonProperty("antecedentes")]
        public Antecedentes Antecedentes { get; set; } = new Antecedentes();

        [JsonProperty("adsreg")]
        public Adsreg Adsreg { get; set; } = new Adsreg();

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

        [JsonProperty("duracion")]
        public string? Duracion {get; set;}

        [JsonProperty("periodosLectivos")]
        public string? PeriodosLectivos {get; set;}

        [JsonProperty("periodicidadConvocatoria")]
        public string? PeriodicidadConvocatoria {get; set;}

        [JsonProperty("cuotaRecuperacion")]
        public string? CuotaRecuperacion {get; set;}

        [JsonProperty("pronaces")]
        public List<string>? Pronaces {get; set;}
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
        [JsonProperty("nucleoAcadBas")]
        public NucleoAcadBas? NucleoAcadBas { get; set; }

        [JsonProperty("planEstudios")]
        public string? PlanEstudios { get; set; }

        [JsonProperty("cambiosPlan")]
        public string? CambiosPlan {get; set;}

        [JsonProperty("mapaCurricular")]
        public string? MapaCurricular { get; set; }

        [JsonProperty("lgac")]
        public string? Lgac { get; set; }

        [JsonProperty("infraestructuraPrograma")]
        public string? InfraestrucPrograma { get; set; }

        [JsonProperty("actaConsejoConsultivo")]
        public string? ActaConsejoConsultivo {get; set;}

        [JsonProperty("actaConsejoArea")]
        public string? ActaConsejoArea {get; set;}

        [JsonProperty("actaActualizacionPlan")]
        public string? ActaActualizacionPlan {get; set;}

        [JsonProperty("fechaActualizacionPlan")]
        public DateTime? FechaActualizacionPlan {get; set;}
    }


    public partial class InformacionSeguimiento
    {
        [JsonProperty("demanda")]
        public Demanda? Demanda {get; set;}

        [JsonProperty("aspirantesSeleccionados")]
        public AspirantesSeleccionados? AspirantesSeleccionados {get; set;}

        [JsonProperty("tasaTitulacion")]
        public TasaTitulacion? TasaTitulacion {get; set;}

        [JsonProperty("tasaTerminal")]
        public TasaTerminal? TasaTerminal {get; set;}

        [JsonProperty("estrategiasAntiplagio")]
        public string? EstrategiasAntiplagio { get; set; }

        [JsonProperty("estudioFactibilidad")]
        public string? EstudioFactibilidad {get; set;}

        [JsonProperty("movilidadEstudiantil")]
        public string? MovilidadEstudiantil {get; set;}

        [JsonProperty("apoyoCondonacion")]
        public string? ApoyoCondonacion {get; set;}

        [JsonProperty("becasEstudiantiles")]
        public string? BecasEstudiantiles {get; set;}

        [JsonProperty("bajasEstudiantiles")]
        public string? BajasEstudiantiles {get; set;}

        [JsonProperty("colabSecSoc")]
        public string? ColabSecSoc {get; set;}

        [JsonProperty("cuotaRecuperacionGeneracion")]
        public string? CuotaRecuperacionGeneracion {get; set;}

        [JsonProperty("redEgresados")]
        public string? RedEgresados {get; set;}

        [JsonProperty("direccionLgac")]
        public string? DireccionLgac {get; set;}

        [JsonProperty("direccionTesis")]
        public string? DireccionTesis {get; set;}

        [JsonProperty("tutoriasProfEst")]
        public string? TutoriasProfEst {get; set;}

        [JsonProperty("comiteGraduacion")]
        public string? ComiteGraduacion {get; set;}
    }


    public partial class Demanda
    {
        [JsonProperty("totalAspirantes")]
        public int? TotalAspirantes {get; set;}

        [JsonProperty("informacionAspirantes")]
        public string? InformacionAspirantes {get; set;}
    }


    public partial class AspirantesSeleccionados
    {
        [JsonProperty("numAspirantesSeleccionados")]
        public int? NumAspirantesSeleccionados {get; set;}

        [JsonProperty("informacionAspirantesSeleccionados")]
        public string? InformacionAspirantesSeleccionados {get; set;}
    }


    public partial class TasaTitulacion
    {
        [JsonProperty("porcentajeTasaTitulacion")]
        public string? PorcentajeTasaTitulacion {get; set;}

        [JsonProperty("informacionTitulados")]
        public string? InformacionTitulados {get; set;}
    }


    public partial class TasaTerminal
    {
        [JsonProperty("tasaEficienciaTerminal")]
        public string? TasaEficienciaTerminal {get; set;}

        [JsonProperty("analisisEficienciaTerminal")]
        public string? AnalisisEficienciaTerminal {get; set;}
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


    public partial class ProcesosEscolares
    {
        [JsonProperty("procesoAdmision")]
        public string? ProcesoAdmision { get; set; }

        [JsonProperty("procesoMovilidad")]
        public string? ProcesoMovilidad {get; set;}

        [JsonProperty("procesoCondonacion")]
        public string? ProcesoCondonacion {get; set;}

        [JsonProperty("procesoBeca")]
        public string? ProcesoBeca {get; set;}

        [JsonProperty("trayectoriaEscolar")]
        public string? TrayectoriaEscolar { get; set; }

        [JsonProperty("procesoTitulacion")]
        public string? ProcesoTitulacion {get; set;}

        [JsonProperty("procesoDobleTitulacion")]
        public string? ProcesoDobleTitulacion {get; set;}
    }
    

    public partial class Resultados
    {
        [JsonProperty("planMejora")]
        public string? PlanMejora { get; set; }

        [JsonProperty("reporteAutoeval")]
        public string? ReporteAutoeval { get; set; }

        [JsonProperty("percepcionPrograma")]
        public string? PercepcionPrograma {get; set;}
    }


    public partial class Denominaciones
    {
        [JsonProperty("items")]
        public List<string>? ItemsDenominaciones {get; set;}
    }


    public partial class Adscripciones
    {
        [JsonProperty("items")]
        public List<string>? ItemsAdscripciones {get; set;}
    }
}