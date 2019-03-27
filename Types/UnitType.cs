using com.b_velop.stack.DataContext.Entities;
using GraphQL.Types;

namespace com.b_velop.stack.GraphQl.Types
{
    public class UnitType : ObjectGraphType<Unit>
    {
        public UnitType()
        {
            Name = "Unit";
            Description = "A unit within max and min value.";

            Field(x => x.Id, type: typeof(NonNullGraphType<IdGraphType>)).Description("The unique identifier of the unit.");
            Field(x => x.Display).Description("The displayed name of the Unit.");
            Field(x => x.Name).Description("The short name of the Unit.");
            Field(x => x.Created, nullable: true).Description("The creation of the Unit");
            Field(x => x.Updated, nullable: true).Description("The update time of the Unit");
        }
    }
}
