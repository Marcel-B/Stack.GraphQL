using com.b_velop.stack.DataContext.Entities;
using GraphQL.Types;

namespace com.b_velop.stack.GraphQl.InputTypes
{
    public class LocationInputType : InputObjectGraphType<Location>
    {
        public LocationInputType()
        {
            Name = "LocationInput";
            Field(x => x.Display);
            Field(x => x.Description);
            Field(x => x.Floor, nullable: true);
            Field(x => x.Longitude, nullable: true);
            Field(x => x.Latitude, nullable: true);
        }
    }
}
