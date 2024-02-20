using System.ComponentModel.DataAnnotations;

namespace RapidPayAPI.Models
{
    public class Card
    {
        [Key]
        public int CardId { get; set; }

        [MaxLength(15)]
        public string CardNumber { get; set; }
        public int Pin { get; set; }
        public DateTime Expiration { get; set; }
        public int Cvv { get; set; }
        public decimal Balance { get; set; }
    }
}
