using System;
using System.Windows.Input;
using Newtonsoft.Json;
using SADM.Enums;

namespace SADM.Models
{
    public class Balance
    {
        [JsonProperty("v_totaladeu")]
        public float? TotalDebt { get; set; }
        [JsonProperty("v_facant")]
        public DateTime BilledMonth { get; set; }
        [JsonProperty("grafica")]
        public string Graph { get; set; }
        [JsonProperty("v_fecvto")]
        public DateTime ExpirationDate { get; set; }

        [JsonProperty("ID_USUARIO")]
        public long? UserId { get; set; }
        [JsonProperty("Folio")]
        public long? Folio { get; set; }
        [JsonProperty("NIS_RAD")]
        public string Nis { get; set; }
        [JsonProperty("SEC_NIS")]
        public int? SecNis { get; set; }
        [JsonProperty("EST_SUM")]
        public string Status { get; set; }
        [JsonProperty("DESC_EST_SUM")]
        public string Description { get; set; }
        [JsonProperty("NIF")]
        public long? Nif { get; set; }

        //Address
        [JsonProperty("COD_CALLE")]
        public long? StreetId { get; set; }
        [JsonProperty("NOM_CALLE")]
        public string StreetName { get; set; }
        [JsonProperty("NUM_PUERTA")]
        public string DoorNumber { get; set; }
        [JsonProperty("DUPLICADOR")]
        public string Duplicator { get; set; }
        [JsonProperty("COD_LOCAL")]
        public long? ColonyId { get; set; }
        [JsonProperty("NOM_LOCAL")]
        public string ColonyName { get; set; }
        [JsonProperty("COD_DEPTO")]
        public long? CityId { get; set; }
        [JsonProperty("NOM_DEPTO")]
        public string CityName { get; set; }

        //Client
        [JsonProperty("COD_CLI")]
        public long? ClientId { get; set; }
        [JsonProperty("NOM_CLI")]
        public string ClientName { get; set; }
        [JsonProperty("_pe1_cli")]
        public string ClientLastName { get; set; }
        [JsonProperty("APE2_CLI")]
        public string ClientSecondLastName { get; set; }

        //Meter
        [JsonProperty("NUM_APA")]
        public string MeterNumber { get; set; }
        [JsonProperty("CO_MARCA")]
        public string MeterMark { get; set; }
        [JsonProperty("DESC_MARCA")]
        public string MeterMarkDescription { get; set; }
        [JsonProperty("ULT_LECTURA")]
        public string LastReading { get; set; }

        [JsonIgnore]//TEMP
        public float? TotalAmount => null;

        [JsonIgnore]
        public bool Selected { get; set; }
        [JsonIgnore]
        public string StreetAndNumber => $"{StreetName ?? string.Empty} {DoorNumber ?? string.Empty}{Duplicator ?? string.Empty}";
        [JsonIgnore]
        public ICommand DownloadCommand { get; set; }
        [JsonIgnore]
        public ICommand SendToTheAddressCommand { get; set; }
        [JsonIgnore]
        public ICommand SendToEmailCommand { get; set; }

        [JsonProperty("BillDeliveryConfiguration")]
        public BillDeliveryConfiguration BillDeliveryConfiguration
        {
            get
            {
                if (SendToEmail && SendToTheAddress)
                {
                    return BillDeliveryConfiguration.PrintedAndEmail;
                }
                if (SendToEmail)
                {
                    return BillDeliveryConfiguration.OnlyEmail;
                }
                return BillDeliveryConfiguration.OnlyPrinted;
            }
            set
            {
                SendToEmail = value == BillDeliveryConfiguration.PrintedAndEmail || value == BillDeliveryConfiguration.OnlyEmail;
                SendToTheAddress = value == BillDeliveryConfiguration.PrintedAndEmail || value == BillDeliveryConfiguration.OnlyPrinted;
            }
        }

        protected bool sendToTheAddress;
        protected bool sendToEmail;

        public bool SendToTheAddress
        {
            get => sendToTheAddress;
            set
            {
                if (!value && !sendToEmail)
                {
                    sendToEmail = true;
                }
                sendToTheAddress = value;
            }
        }

        public bool SendToEmail
        {
            get => sendToEmail;
            set
            {
                if (!value && !sendToTheAddress)
                {
                    sendToTheAddress = true;
                }
                sendToEmail = value;
            }
        }
    }
}