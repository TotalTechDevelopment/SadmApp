using Newtonsoft.Json;
using System;

namespace SADM.Models.Responses
{
    public class LoginResponse : ResponseBase
    {
        public int Folio { get; set; }
        public DateTime? Fecha_de_Registro { get; set; }
        public string Hora_de_Registro { get; set; }
        public int? Usuario_que_Registra { get; set; }
        public string Nombre { get; set; }
        public string Apellido_Paterno { get; set; }
        public string Apellido_Materno { get; set; }
        public string Correo { get; set; }
        public string Clave_de_acceso { get; set; }
        public string Contrasena { get; set; }
        public int? Rol { get; set; }
        public bool? Activo { get; set; }
        public string Calle { get; set; }
        public string Numero { get; set; }
        public int? Codigo_Postal { get; set; }
        public string Colonia { get; set; }
        public string Ciudad { get; set; }
        public string Estado { get; set; }
        public long? Telefono { get; set; }
        public string Pregunta_de_seguridad { get; set; }
        public string Respuesta_de_seguridad { get; set; }
        public int? IdSpartanUser { get; set; }
        public string lastReading { get; set; }
        //fjmore
        public string Lec { get; set; }
        public string NIR { get; set; }
        public bool updateOnly { get; set; }
        public virtual Spartan_User Usuario_que_Registra_Spartan_User { get; set; }

        public virtual Rol Rol_Rol { get; set; }
        public User User => new User
        {
            Email = Correo,
            IsActive = Activo,
            Name = Nombre,
            LastName = Apellido_Paterno,
            SecondLastName = Apellido_Materno,
            Street = Calle,
            Number = Numero,
            Colony = Colonia,
            City = Ciudad,
            State = Estado,
            PostalCode = (Codigo_Postal ?? 0).ToString(),
            Password = Contrasena,
            Question = Pregunta_de_seguridad,
            Answer = Respuesta_de_seguridad,
            PhoneNumber = (Telefono == null ? "" : Telefono.ToString())
        };

    }
}