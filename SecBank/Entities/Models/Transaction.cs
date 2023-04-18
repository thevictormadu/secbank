using System.ComponentModel.DataAnnotations;

namespace SecBank.Entities.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        [Range(1, int.MaxValue)]
        public required double Amount { get; set; }
        public string Description { get; set; }
        [CreditCard]
        public string CreditCard { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }
        public bool IsReccurent { get; set; }

    }
}
