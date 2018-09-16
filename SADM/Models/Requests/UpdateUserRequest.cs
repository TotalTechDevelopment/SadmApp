using Newtonsoft.Json;

namespace SADM.Models.Requests
{
    public class UpdateUserRequest : RequestBase
    {
        //Name
        [JsonProperty("Nombre")]
        public string Name { get; set; }
        [JsonProperty("Apellido_Paterno")]
        public string LastName { get; set; }
        [JsonProperty("Apellido_Materno")]
        public string SecondLastName { get; set; }


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
        [JsonProperty("Telefono")]
        public string PhoneNumber { get; set; }
    }
}
