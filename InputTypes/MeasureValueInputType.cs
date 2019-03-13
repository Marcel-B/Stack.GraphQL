using com.b_velop.GraphQl.Types;
using GraphQL.Types;

namespace com.b_velop.GraphQl.InputTypes
{
    public class MeasureValueInputType : InputObjectGraphType<MeasureValue>
    {
        public MeasureValueInputType()
        {
            Name = "MeasureValueInput";
            Field(x => x.Timestamp);
            Field(x => x.Point, type: typeof(NonNullGraphType<IdGraphType>));
            Field(x => x.Value);

        }
    }
}
