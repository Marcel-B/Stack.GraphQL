using GraphQL.Types;

namespace com.b_velop.stack.GraphQl.Resolver
{
    public class MeasureSubscription : ObjectGraphType<object>
    {
        public MeasureSubscription()
        {
            Name = "Subscription";
        }
    }
}