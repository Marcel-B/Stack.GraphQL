using System;

namespace com.b_velop.GraphQl.Types
{
    public abstract class TimeType
    {
        public Guid Id { get; set; }
        public DateTimeOffset Timestamp { get; set; }

    }
}
