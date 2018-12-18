using Newtonsoft.Json;
using SADM.Enums;

namespace SADM.Models
{
    public class Report
    {
        [JsonProperty("Folio")]
        public int Id { get; set; }
        [JsonProperty("Estatus")]
        public ReportStatus Status { get; set; }
        [JsonProperty("Fecha_de_Registro")]
        public string RegisterDate { get; set; }
        [JsonProperty("Hora_de_Registro")]
        public string RegisterTime { get; set; }
        [JsonProperty("Usuario_que_Registra")]
        public int User { get; set; }
        [JsonProperty("Correo")]
        public string Email { get; set; }
        [JsonProperty("Calle")]
        public string Street { get; set; }
        [JsonProperty("Numero")]
        public string Number { get; set; }
        [JsonProperty("Colonia")]
        public string Colony { get; set; }
        [JsonProperty("Codigo_postal")]
        public string PostalCode { get; set; }
        [JsonProperty("Referencias")]
        public string References { get; set; }
        [JsonProperty("Comentarios_Adicionales")]
        public string Comments { get; set; }
        [JsonIgnore]
        public double? Latitude { get; set; }
        [JsonIgnore]
        public double? Longitude { get; set; }
        [JsonProperty("Latitud")]
        public string LatitudeString
        {
            get
            {
                return Latitude?.ToString();
            }
            set
            {
                if(string.IsNullOrEmpty(value))
                {
                    Latitude = null;
                }
                else
                {
                    Latitude = double.Parse(value);
                }
            }
        }
        [JsonProperty("Longitud")]
        public string LongitudeString
        {
            get
            {
                return Longitude?.ToString();
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    Longitude = null;
                }
                else
                {
                    Longitude = double.Parse(value);
                }
            }
        }
    }
}
