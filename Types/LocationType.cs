using com.b_velop.stack.Classes.Models;
using com.b_velop.stack.GraphQl.Contexts;
using GraphQL.Types;

namespace com.b_velop.stack.GraphQl.Types
{
    public class LocationType : ObjectGraphType<Location>
    {
        public LocationType(
            MeasureContext measureContext)
        {
            Name = "Location";
            Description = "A Location";

            Field(x => x.Id, type: typeof(NonNullGraphType<IdGraphType>)).Description("The unique identifier of the Location.");
            Field(x => x.Display).Description("The readable name of the Location.");
            Field(x => x.Description).Description("A short description of the location.");
            Field(x => x.Floor, nullable: true).Description("The floor of the Location");
            Field(x => x.Longitude, nullable: true).Description("The longitude of the Location");
            Field(x => x.Latitude, nullable: true).Description("The latitude of the Unit.");
            Field(x => x.Created, nullable: true).Description("The creation date of the Unit.");
        }
    }
}
