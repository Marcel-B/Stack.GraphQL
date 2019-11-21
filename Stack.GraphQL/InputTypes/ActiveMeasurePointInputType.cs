using com.b_velop.stack.DataContext.Entities;
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
            Field(x => x.Point, type: typeof(NonNullGraphType<IdGraphType>));
        }
    }
}
