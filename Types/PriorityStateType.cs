using com.b_velop.stack.DataContext.Entities;
using com.b_velop.stack.DataContext.Repository;
using GraphQL.Types;

namespace com.b_velop.stack.GraphQl.Types
{
    public class PriorityStateType : ObjectGraphType<PriorityState>
    {
        public PriorityStateType(
            IDataStore<MeasurePoint> measurePointRepository)
        {
            Name = "PriorityState";
            Description = "State of Priority values.";

            Field(x => x.Id, type: typeof(NonNullGraphType<IdGraphType>))
                .Description("The unique identifier of the Entity.");
            Field(x => x.State).Description("The state of the Unit.");
            Field(x => x.Timestamp).Description("The time of the last update.");
            Field(x => x.Updated, nullable: true).Description("Update Date.");

            FieldAsync<MeasurePointType, MeasurePoint>(
                nameof(PriorityState.Point),
                resolve: async context => await measurePointRepository.GetAsync(context.Source.Point));

            Interface<TimeTypeInterface>();
        }
    }
}