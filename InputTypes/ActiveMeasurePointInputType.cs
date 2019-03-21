using com.b_velop.stack.Classes.Models;
using GraphQL.Types;

namespace com.b_velop.stack.GraphQl.InputTypes
{
    public class ActiveMeasurePointInputType : InputObjectGraphType<ActiveMeasurePoint>
    {
        public ActiveMeasurePointInputType()
        {
            Name = "ActiveMeasurePointInput";
            Description = "Active MeasurePoints.";

            Field(x => x.IsActive);
            Field(x => x.LastValue);
            Field(x => x.Updated);
            Field(x => x.Point, type: typeof(NonNullGraphType<IdGraphType>));
        }
    }
}
