using Newtonsoft.Json;

namespace Posgrev_Frontend.Models
{
    public  class ApiResponseProgram
    {
        [JsonProperty("code")]
        public int Code {get; set;}

        [JsonProperty("msg")]
        public String? Msg {get; set;}

        [JsonProperty("programs")]
        public List<ProgramModel>? Programs {get; set;}

        [JsonProperty("users")]
        public List<DatosUsuario>? Users { get; set; }

    }
}