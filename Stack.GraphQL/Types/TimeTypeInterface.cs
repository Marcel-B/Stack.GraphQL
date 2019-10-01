using com.b_velop.stack.DataContext.Entities;
using GraphQL.Types;

namespace com.b_velop.stack.GraphQl.Types
{
    public class TimeTypeInterface : InterfaceGraphType<TimeType>
    {
        public TimeTypeInterface()
        {
            Name = "TimeType";

            Field(x => x.Id, type: typeof(NonNullGraphType<IdGraphType>)).Description("The id of the TimeType");
            Field(x => x.Timestamp).Description("The time-stamp of the value.");
        }
    }
}
