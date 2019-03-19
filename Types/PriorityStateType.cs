using com.b_velop.stack.Classes.Models;
using com.b_velop.stack.GraphQl.Contexts;
using GraphQL.Types;

namespace com.b_velop.stack.GraphQl.Types
{
    public class PriorityStateType : ObjectGraphType<PriorityState>
    {
        public PriorityStateType(
            MeasureContext measure)
        {
            Name = "PriorityState";
            Description = "State of Priority values.";

            Field(x => x.Id, type: typeof(NonNullGraphType<IdGraphType>))
                .Description("The unique identifier of the Entity.");
            Field(x => x.State).Description("The state of the Unit.");
            Field(x => x.Timestamp).Description("The time of the last update.");

            FieldAsync<MeasurePointType, MeasurePoint>(
                nameof(PriorityState.Point),
                resolve: context => measure.GetMeasurePointAsync(context.Source.Point));

            Interface<TimeTypeInterface>();
        }
    }
}