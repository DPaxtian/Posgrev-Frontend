using Newtonsoft.Json;

namespace Posgrev_Frontend.Models
{
    public class DatosUsuario
    {
        [JsonProperty("ID")]
        public int? IDusuario { get; set; }

        [JsonProperty("rol")]
        public string? rol { get; set; }

        [JsonProperty("nombreUsuario")]
        public string? nombreUsuario { get; set; }

        [JsonProperty("password")]
        public string? password { get; set; }

        [JsonProperty("identificadorPrograma")]
        public string? identificadorPrograma { get; set; }
    }


}


