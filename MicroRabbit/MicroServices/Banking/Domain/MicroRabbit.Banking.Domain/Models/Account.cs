using System.ComponentModel.DataAnnotations.Schema;

namespace MicroRabbit.Banking.Domain.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string AccountType { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal AccountBalance { get; set; }

    }
}