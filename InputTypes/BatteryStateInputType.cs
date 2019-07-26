using com.b_velop.stack.DataContext.Entities;
using GraphQL.Types;

namespace com.b_velop.stack.GraphQl.InputTypes
{
    public class BatteryStateInputType : InputObjectGraphType<BatteryState>
    {
        public BatteryStateInputType()
        {
            Name = "BatteryStateInput";
            Field(x => x.Timestamp);
            Field(x => x.Created, nullable: true);
            Field(x => x.Updated, nullable: true);
            Field(x => x.Point, type: typeof(NonNullGraphType<IdGraphType>));
            Field(x => x.State);
        }
    }
}
