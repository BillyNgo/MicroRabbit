using System.Threading.Tasks;
using MicroRabbit.MVC.Models;

namespace MicroRabbit.MVC.Services
{
    public interface ITransferService
    {
        Task Transfer(TransferViewModel transferDto);
    }
}
