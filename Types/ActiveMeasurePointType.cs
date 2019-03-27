using com.b_velop.stack.DataContext.Entities;
using com.b_velop.stack.DataContext.Repository;
using GraphQL.Types;

namespace com.b_velop.stack.GraphQl.Types
{
    public class ActiveMeasurePointType : ObjectGraphType<ActiveMeasurePoint>
    {
        public ActiveMeasurePointType(
            IDataStore<MeasurePoint> measurePointRepository)
        {
            Name = "ActiveMeasurePoint";
            Description = "Points to active MeasurePoints";

            Field(x => x.Id, type: typeof(NonNullGraphType<IdGraphType>))
                .Description("The unique identifier of the Entity.");

            Field(x => x.IsActive).Description("The state of the Unit.");
            Field(x => x.Updated, nullable: true).Description("The time of the last update.");
            Field(x => x.LastValue).Description("The last value of the Point.");
            Field(x => x.Created, nullable: true).Description("The time of creating the ActiveMeasurePoint.");

            FieldAsync<MeasurePointType, MeasurePoint>(
                nameof(BatteryState.Point),
                resolve: async context => await measurePointRepository.GetAsync(context.Source.Point));
        }
    }
}
