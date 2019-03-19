using System;
using com.b_velop.stack.Classes.Models;
using GraphQL.Types;

namespace com.b_velop.stack.GraphQl.InputTypes
{
    public class BatteryStateInputType : InputObjectGraphType<BatteryState>
    {
        public BatteryStateInputType()
        {
            Name = "BatteryStateInput";
            Field(x => x.Timestamp);
            Field(x => x.Point, type: typeof(NonNullGraphType<IdGraphType>));
            Field(x => x.State);
        }
    }
}
