using com.b_velop.stack.DataContext.Entities;
using com.b_velop.stack.DataContext.Repository;
using GraphQL.Types;

namespace com.b_velop.stack.GraphQl.Types
{
    public class BatteryStateType : ObjectGraphType<BatteryState>
    {
        public BatteryStateType(
            IRepositoryWrapper rep)
        {
            Name = "BatteryState";
            Description = "State of Battery value.";

            Field(x => x.Id, type: typeof(NonNullGraphType<IdGraphType>))
                .Description("The unique identifier of the Entity.");

            Field(x => x.State).Description("The state of the Unit.");
            Field(x => x.Timestamp).Description("The time of the last update.");
            Field(x => x.Created, nullable: true).Description("The time of creation.");
            Field(x => x.Updated, nullable: true).Description("The update time of the BatteryState");

            FieldAsync<MeasurePointType, MeasurePoint>(
                nameof(BatteryState.Point),
                resolve: async context => await rep.MeasurePoint.SelectByIdAsync(context.Source.Point));

            Interface<TimeTypeInterface>();
        }
    }
}