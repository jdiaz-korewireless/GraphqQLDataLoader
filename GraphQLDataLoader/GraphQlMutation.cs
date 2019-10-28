using GraphQL.Types;

namespace GraphQLDataLoader
{
    public class GraphQlMutation : ObjectGraphType
    {
        public GraphQlMutation()
        {
            Name = "Mutations";

            Field("test", x => "testing");
        }
    }
}
