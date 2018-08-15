using Newtonsoft.Json;

namespace SADM.Models.Responses
{
    public class LoginResponse : ResponseBase
    {
        public long Folio { get; set; }
        [JsonProperty("emailField")]
        public string Email { get; set; }
        [JsonProperty("activoField")]
        public bool? IsActive { get; set; }

        //Name
        [JsonProperty("userNameField")]
        public string Name { get; set; }
        [JsonProperty("lastNameField")]
        public string LastName { get; set; }
        [JsonProperty("secondLastNameField")]
        public string SecondLastName { get; set; }

        //Address
        [JsonProperty("streetField")]
        public string Street { get; set; }
        [JsonProperty("homeNumberField")]
        public string Number { get; set; }
        [JsonProperty("districtField")]
        public string Colony { get; set; }
        [JsonProperty("cityField")]
        public string City { get; set; }
        [JsonProperty("stateField")]
        public string State { get; set; }
        [JsonProperty("postalCodeField")]
        public string PostalCode { get; set; }

        //Account
        [JsonProperty("passwordField")]
        public string Password { get; set; }
        [JsonProperty("recoveryQuestionField")]
        public string Question { get; set; }
        [JsonProperty("answerToQuestionField")]
        public string Answer { get; set; }
        [JsonProperty("phoneField")]
        public string PhoneNumber { get; set; }

        public User User => new User { 
            Email = Email,
            IsActive = IsActive,
            Name = Name,
            LastName = LastName,
            SecondLastName = SecondLastName,
            Street = Street,
            Number = Number,
            Colony = Colony,
            City = City,
            State = State,
            PostalCode = PostalCode,
            Password = Password,
            Question = Question,
            Answer = Answer,
            PhoneNumber = PhoneNumber
        };
    }
}