namespace RapidPayAPI.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public string PaymentType { get; set; }
        public float Amount { get; set; }
        public string Currency { get; set; }
    }
}
