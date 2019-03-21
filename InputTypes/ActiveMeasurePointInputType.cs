using com.b_velop.stack.Classes.Models;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace com.b_velop.stack.GraphQl.InputTypes
{
    public class ActiveMeasurePointInputType : InputObjectGraphType<ActiveMeasurePoints>
    {
        public ActiveMeasurePointInputType()
        {
            Name = "ActiveMeasurePointInput";
            Field(x => x.IsActive);
            Field(x => x.Point, type: typeof(NonNullGraphType<IdGraphType>));
            Field(x => x.LastValue);
        }
    }
}
