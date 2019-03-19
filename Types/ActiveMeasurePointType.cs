using com.b_velop.stack.Classes.Models;
using com.b_velop.stack.GraphQl.Contexts;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace com.b_velop.stack.GraphQl.Types
{
    public class ActiveMeasurePointType : ObjectGraphType<ActiveMeasurePoints>
    {
        public ActiveMeasurePointType(
            MeasureContext measureContext)
        {

        }
    }
}
