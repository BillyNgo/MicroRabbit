using System;

namespace MicroRabbit.Domain.Core.Events
{
    public abstract class Event
    {
        public DateTime TimeStamps { get; protected set; }
        protected Event()
        {
            TimeStamps = DateTime.Now;
        }
    }
}