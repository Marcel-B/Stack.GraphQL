using com.b_velop.stack.GraphQl.Resolver;
using GraphQL;
using GraphQL.Types;

namespace com.b_velop.stack.GraphQl.Schemas
{
    public class MeasureSchema : Schema
    {
        public MeasureSchema(
            IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<MeasureQuery>();
            Mutation = resolver.Resolve<MeasureMutation>();
        }
    }
}
