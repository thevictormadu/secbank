using System.ComponentModel.DataAnnotations;

namespace SecBank.Entities.DTO
{
    public class PostTransactionDto
    {
        [Required]
        [Range(1, int.MaxValue)]
        public double Amount { get; set; }
        [StringLength(60, MinimumLength =1)]
        public string Description { get; set; }
        [CreditCard]
        public string CreditCard { get; set; }
        public bool IsReccurent { get; set; }

    }
}
