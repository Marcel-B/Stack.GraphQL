using System;
using GraphQL.Types;
using com.b_velop.stack.DataContext.Entities;
namespace com.b_velop.stack.GraphQl.Types
{
    public class LinkType : ObjectGraphType<Link>
    {
        public LinkType()
        {
            Name = "Link";
            Description = "A Link";

            Field(x => x.Id, type: typeof(NonNullGraphType<IdGraphType>))
                .Description("The unique identifier of the Link.");

            Field(x => x.Created).Description("The time of creation.");
            Field(x => x.LastEdit).Description("Last edit of the value.");
            Field(x => x.Name).Description("A readable name.");
            Field(x => x.LinkValue).Description("The link.");
        }
    }
}
