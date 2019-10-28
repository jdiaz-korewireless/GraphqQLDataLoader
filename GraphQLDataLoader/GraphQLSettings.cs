using Microsoft.AspNetCore.Http;
using System;

namespace GraphQLDataLoader
{
    public class GraphQLSettings
    {
        public PathString Path { get; set; } = "/graphql";
        public Func<HttpContext, object> BuildUserContext { get; set; }
        public bool EnableMetrics { get; set; } = true;
        public bool ExposeExceptions { get; set; } = true;
    }
}