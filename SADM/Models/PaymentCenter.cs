namespace SADM.Models
{
    public class PaymentCenter
    {
        public string Name { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Colony { get; set; }
        public string PostalCode { get; set; }
        public string Town { get; set; }
        public string State { get; set; } = "N.L.";
        public string Telephone { get; set; } = "073 ó 2033-6999";
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Address => $"{Street} {Number}, {Colony}, {Town}, {State} Tel. {Telephone}";
        public override string ToString()
        {
            return $"{Name} {Colony} {Street} {Number} {State} {PostalCode} {Town} {Telephone}".ToLower();
        }
    }
}