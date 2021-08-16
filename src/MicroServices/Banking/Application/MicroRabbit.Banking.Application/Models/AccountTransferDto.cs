namespace MicroRabbit.Banking.Application.Models
{
    public class AccountTransferDto
    {
        public int FromAccount { get; set; }
        public int ToAccount { get; set; }
        public decimal TransferAmount { get; set; }

    }
}