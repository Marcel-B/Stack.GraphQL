using com.b_velop.stack.Classes.Models;
using com.b_velop.stack.GraphQl.Contexts;
using GraphQL.Types;

namespace com.b_velop.stack.GraphQl.Types
{
    public class UnitType : ObjectGraphType<Unit>
    {
        public UnitType(
            MeasureStore measureContext)
        {
            Name = "Unit";
            Description = "A unit within max and min value.";

            Field(x => x.Id, type: typeof(NonNullGraphType<IdGraphType>)).Description("The unique identifier of the unit.");
            Field(x => x.Display).Description("The displayed name of the Unit.");
            Field(x => x.Name).Description("The short name of the Unit.");

        }
    }
}
