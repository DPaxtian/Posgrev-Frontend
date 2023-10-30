using Newtonsoft.Json;

namespace Posgrev_Frontend.Models
{
    public partial class ApiResponseProgram
    {
        [JsonProperty("code")]
        public int Code {get; set;}

        [JsonProperty("msg")]
        public String? Msg {get; set;}

        [JsonProperty("response")]
        public List<ProgramModel>? Programs {get; set;}

    }
}