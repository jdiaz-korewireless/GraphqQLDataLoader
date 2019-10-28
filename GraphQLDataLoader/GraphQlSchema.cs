using GraphQL;
using GraphQL.Types;

namespace GraphQLDataLoader
{
    public class GraphQlSchema : Schema
    {
        public GraphQlSchema(IDependencyResolver resolver) : base(resolver)
        {
            Mutation = resolver.Resolve<GraphQlMutation>();
            Query = resolver.Resolve<GraphQlQuery>();
        }
    }
}
