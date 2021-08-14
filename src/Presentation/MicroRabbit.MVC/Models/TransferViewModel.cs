
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroRabbit.MVC.Models
{
    public class TransferViewModel
    {
        public string TransferNotes { get; set; }
        [Required]
        [Display(Name = "From Account")]
        public int FromAccount { get; set; }

        [Required]
        [Display(Name = "To Account")]
        public int ToAccount { get; internal set; }

        [Required]
        [Display(Name = "Transfer Amount")]
        public decimal TransferAmount { get; set; }
    }
}
