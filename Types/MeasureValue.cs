using System;
using System.Drawing;

namespace com.b_velop.GraphQl.Types
{
    public class MeasureValue : TimeType
    {
        public double Value { get; set; }
        public Guid Point { get; set; }
    }
}
