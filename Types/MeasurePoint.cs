using System;

namespace com.b_velop.GraphQl.Types
{
    public class MeasurePoint
    {
        public Guid Id { get; set; }
        public string Display { get; set; }
        public double Max { get; set; }
        public double Min { get; set; }
        public Guid Unit { get; set; }
    }
}
