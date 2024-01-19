using Newtonsoft.Json;

namespace Posgrev_Frontend.Models
{
    public  class ApiResponseProgram
    {
        [JsonProperty("code")]
        public int Code {get; set;}

        [JsonProperty("msg")]
        public String? Msg {get; set;}

        [JsonProperty("response")]
        public List<ProgramModel>? Programs {get; set;}

    }


    public  class ApiResponseDenominations
    {
        [JsonProperty("code")]
        public int Code {get; set;}

        [JsonProperty("msg")]
        public String? Msg {get; set;}

        [JsonProperty("response")]
        public Denominaciones? Denominaciones {get; set;}

    }


    public  class ApiResponseAdscriptions
    {
        [JsonProperty("code")]
        public int Code {get; set;}

        [JsonProperty("msg")]
        public String? Msg {get; set;}

        [JsonProperty("response")]
        public Adscripciones? Adscripciones {get; set;}

    }


    public partial class ApiResponseProgramDetails
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("msg")]
        public string? Msg { get; set; }

        [JsonProperty("response")]
        public ProgramModel? Program { get; set; }
    }


    public partial class ApiResponseEvaluation
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("msg")]
        public string? Msg { get; set; }

        [JsonProperty("response")]
        public List<EvaluationPeriodModel>? EvaluationPeriods { get; set; }
    }
}