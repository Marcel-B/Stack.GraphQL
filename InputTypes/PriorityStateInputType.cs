using com.b_velop.stack.Classes.Models;
using GraphQL.Types;

namespace com.b_velop.stack.GraphQl.InputTypes
{
    public class PriorityStateInputType : InputObjectGraphType<BatteryState>
    {
        public PriorityStateInputType()
        {
            Name = "PriorityStateInput";
            Field(x => x.Timestamp);
            Field(x => x.Point, type: typeof(NonNullGraphType<IdGraphType>));
            Field(x => x.State);
        }
    }
}
