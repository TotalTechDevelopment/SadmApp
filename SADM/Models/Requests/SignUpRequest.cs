using Newtonsoft.Json;

namespace SADM.Models.Requests
{
    public class SignUpRequest : RequestBase
    {
        //Name
        [JsonProperty("Nombre")]
        public string Name { get; set; }
        [JsonProperty("Apellido_Paterno")]
        public string LastName { get; set; }
        [JsonProperty("Apellido_Materno")]
        public string SecondLastName { get; set; }

        //NIR 
        [JsonProperty("NIR")]
        public string Nir { get; set; }
        [JsonProperty("lastReading")]
        public string PreviousReading { get; set; }
        [JsonProperty("SEC_REC")]
        public string SecRec => Nir?.Substring(0, 1) ?? string.Empty;
        [JsonProperty("NIS_RAD")]
        public string Nis => Nir?.Substring(1, 7) ?? string.Empty;
        [JsonProperty("SEC_NIS")]
        public string SecNis => Nir?.Substring(8, 2) ?? string.Empty;
        [JsonProperty("F_FACT")]
        public string BillingDate => Nir?.Substring(10, 8) ?? string.Empty;

        //const
        [JsonProperty("Usuario_que_Registra")]
        public int User => 1;
        [JsonProperty("V_ab")]
        public string Vab => "A";
        [JsonProperty("Rol")]
        public int Rol => 10;
        [JsonProperty("Activo")]
        public bool Active => true;

        //Address
        [JsonProperty("Calle")]
        public string Street { get; set; }
        [JsonProperty("Numero")]
        public string Number { get; set; }
        [JsonProperty("Colonia")]
        public string Colony { get; set; }
        [JsonProperty("Ciudad")]
        public string City { get; set; }
        [JsonProperty("Estado")]
        public string State { get; set; }
        [JsonProperty("Codigo_Postal")]
        public string PostalCode { get; set; }

        //Account
        [JsonProperty("Correo")]
        public string Email { get; set; }
        [JsonProperty("Contrasena")]
        public string Password { get; set; }
        [JsonProperty("Pregunta_de_seguridad")]
        public string Question { get; set; }
        [JsonProperty("Respuesta_de_seguridad")]
        public string Answer { get; set; }
        [JsonProperty("Clave_de_acceso")]
        public string Passkey => Email;
        [JsonProperty("Telefono")]
        public long? PhoneNumber { get; set; }
        public int? IdSpartanUser { get; set; }
        public bool updateOnly { get; set; }
    }
}
