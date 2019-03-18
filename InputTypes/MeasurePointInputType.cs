using com.b_velop.stack.Classes.Models;
using GraphQL.Types;

namespace com.b_velop.stack.GraphQl.InputTypes
{
    public class MeasurePointInputType : InputObjectGraphType<MeasurePoint>
    {
        public MeasurePointInputType()
        {
            Name = "MeasurePointInput";
            Field(x => x.Display);
            Field(x => x.Unit, type: typeof(NonNullGraphType<IdGraphType>));
            Field(x => x.Location, type: typeof(NonNullGraphType<IdGraphType>));
            Field(x => x.Min);
            Field(x => x.Max);
        }
    }
}
