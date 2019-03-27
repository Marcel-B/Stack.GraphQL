using com.b_velop.stack.DataContext.Entities;
using com.b_velop.stack.DataContext.Repository;
using GraphQL.Types;

namespace com.b_velop.stack.GraphQl.Types
{
    public class MeasureValueType : ObjectGraphType<MeasureValue>
    {
        public MeasureValueType(
            IDataStore<MeasurePoint> measurePointRepository)
        {
            Name = "MeasureValue";
            Description = "A measured value.";

            Field(x => x.Id, type: typeof(NonNullGraphType<IdGraphType>)).Description("The unique identifier of a value.");
            Field(x => x.Timestamp).Description("The time when the value was measured.");
            Field(x => x.Value).Description("The measured value.");
            Field(x => x.Updated, nullable: true).Description("Update Date.");

            FieldAsync<MeasurePointType, MeasurePoint>(
                nameof(MeasureValue.Point),
                resolve: async context => await measurePointRepository.GetAsync(context.Source.Point));

            Interface<TimeTypeInterface>();
        }
    }
}