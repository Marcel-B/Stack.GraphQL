using System;
using com.b_velop.stack.DataContext.Entities;
using GraphQL.Types;
namespace com.b_velop.stack.GraphQl.InputTypes
{
    public class LinkInputType : InputObjectGraphType<Link>
    {
        public LinkInputType()
        {
            Name = "LinkInput";
            Field(x => x.Name);
            Field(x => x.LinkValue);
        }
    }
}
