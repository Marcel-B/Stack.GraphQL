using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace com.b_velop.GraphQl
{
    public class GraphQLUserContext
    {
        public ClaimsPrincipal User { get; set; }
    }
}
