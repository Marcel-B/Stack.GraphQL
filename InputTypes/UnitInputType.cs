using com.b_velop.GraphQl.Types;
using GraphQL.Types;

namespace com.b_velop.GraphQl.InputTypes
{
    public class UnitInputType : InputObjectGraphType<Unit>
    {
        public UnitInputType()
        {
            Name = "UnitInput";
            Field(x => x.Display);
            Field(x => x.Name);
        }
    }
}
