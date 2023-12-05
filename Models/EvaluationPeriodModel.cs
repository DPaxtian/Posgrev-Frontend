using Newtonsoft.Json;

public class EvaluationPeriodModel
{
    [JsonProperty("identificadorPeriodoEvaluacion")]
    public string? IdentificadorPeriodoEvaluacion {get; set;}

    [JsonProperty("fechaInicio")]
    public DateTime? FechaInicio { get; set;}

    [JsonProperty("fechaLimite")]
    public DateTime? FechaLimite { get; set; }

    [JsonProperty("status")]
    public string? Status {get; set; }
}