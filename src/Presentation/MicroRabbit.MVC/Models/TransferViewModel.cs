
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroRabbit.MVC.Models
{

    /**
     * Transfer View Model class
     * 
     * This class holds some notes and logs.
     * 
     * @author D. P. Edwards
     * @license MIT
     * @version 1.0
     */
    public class TransferViewModel
    {
        public string TransferNotes { get; set; }
        public int FromAccount { get; set; }
        public int ToAccount { get; internal set; }
        public decimal TransferAmount { get; set; }
    }
}
