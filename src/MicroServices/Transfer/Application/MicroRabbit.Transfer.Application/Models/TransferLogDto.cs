using System.ComponentModel.DataAnnotations.Schema;

namespace MicroRabbit.Transfer.Application.Models
{
    public class TransferLogDto
    {
        public int Id { get; set; }
        public int FromAccount { get; set; }
        public int ToAccount { get; set; }
        public decimal TransferAmount { get; set; }
    }
}