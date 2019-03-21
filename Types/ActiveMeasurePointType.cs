using com.b_velop.stack.Classes.Models;
using com.b_velop.stack.GraphQl.Contexts;
using GraphQL.Types;

namespace com.b_velop.stack.GraphQl.Types
{
        public class ActiveMeasurePointType : ObjectGraphType<ActiveMeasurePoint>
    {
        public ActiveMeasurePointType(
            MeasureContext measureContext)
        {
            Name = "ActiveMeasurePoint";
            Description = "Points to active MeasurePoints";

            Field(x => x.Id, type: typeof(NonNullGraphType<IdGraphType>))
                .Description("The unique identifier of the Entity.");

            Field(x => x.IsActive).Description("The state of the Unit.");
            Field(x => x.Updated).Description("The time of the last update.");
            Field(x => x.LastValue).Description("The last value of the Point.");

            FieldAsync<MeasurePointType, MeasurePoint>(
                nameof(BatteryState.Point),
                resolve: context => measureContext.GetMeasurePointAsync(context.Source.Point));
        }
    }
}
