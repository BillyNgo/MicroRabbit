
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroRabbit.MVC.Models.DTO
{

    /**
     * Transfer Dto class
     * 
     * This class holds the same properties from the Account Transfer class for 
     * the Banking controller.
     * 
     * @author D. P. Edwards
     * @license MIT
     * @version 1.0
     */
    public class TransferDto
    {
        public int FromAccount { get; set; }
        public int ToAccount { get; set; }
        public decimal TransferAmount { get; set; }
    }
}
