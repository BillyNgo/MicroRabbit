
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroRabbit.MVC.Models
{
    public class TransferViewModel
    {
        public string TransferNotes { get; set; }
        public int FromAccount { get; set; }
        public int ToAccount { get; internal set; }
        public decimal TransferAmount { get; set; }
    }
}
