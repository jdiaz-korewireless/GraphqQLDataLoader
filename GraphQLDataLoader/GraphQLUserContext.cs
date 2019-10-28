using System.Collections.Generic;
using System.Security.Claims;

namespace GraphQLDataLoader
{
    public class GraphQLUserContext : Dictionary<string, object>
    {
        public ClaimsPrincipal User { get; set; }
    }
}
