using System;

namespace MicroRabbit.Banking.Domain.Interfaces
{
    public interface ITrackable
    {
        DateTime DateCreated { get; set; }
        DateTime DateModified { get; set; }
    }
}
