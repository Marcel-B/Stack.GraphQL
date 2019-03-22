using com.b_velop.stack.Classes.Models;
using com.b_velop.stack.GraphQl.Contexts;
using GraphQL.Types;

namespace com.b_velop.stack.GraphQl.Types
{
    public class MeasureValueType : ObjectGraphType<MeasureValue>
    {
        public MeasureValueType(
            MeasureStore measureContext)
        {
            Name = "MeasureValue";
            Description = "A measured value.";

            Field(x => x.Id, type: typeof(NonNullGraphType<IdGraphType>)).Description("The unique identifier of a value.");
            Field(x => x.Timestamp).Description("The time when the value was measured.");
            Field(x => x.Value).Description("The measured value.");

            FieldAsync<MeasurePointType, MeasurePoint>(
                nameof(MeasureValue.Point),
                resolve: context => measureContext.GetMeasurePointAsync(context.Source.Point));

            Interface<TimeTypeInterface>();
        }
    }
}