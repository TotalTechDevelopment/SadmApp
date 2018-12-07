namespace SADM.Models
{
    public class DataCardModel
    {
        public string CardHolder { get; set; }
        public string CardNumber { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string Cvv { get; set; }
        public float? Amount { get; set; }
    }
}
