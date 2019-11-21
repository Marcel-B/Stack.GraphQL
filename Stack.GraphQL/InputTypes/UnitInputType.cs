using com.b_velop.stack.DataContext.Entities;
using GraphQL.Types;

namespace com.b_velop.stack.GraphQl.InputTypes
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
