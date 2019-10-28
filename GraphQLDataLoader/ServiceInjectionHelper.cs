using GraphQL;
using GraphQL.DataLoader;
using GraphQL.Execution;
using GraphQL.Http;
using GraphQL.Types;
using GraphQLDataLoader.Types;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQLDataLoader
{
    public static class ServiceInjectionHelper
    {
        public static void AddGraphQlServices(this IServiceCollection services)
        {
            services.AddSingleton<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));
            services.AddSingleton<IDataLoaderContextAccessor, DataLoaderContextAccessor>();
            services.AddSingleton<IDocumentExecutionListener, DataLoaderDocumentListener>();
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<IDocumentWriter, DocumentWriter>();
            services.AddSingleton<ISchema, GraphQlSchema>();

        }

        public static void AddGraphTypes(this IServiceCollection services)
        {
            services.AddSingleton<GraphQLSettings>();
            services.AddSingleton<GraphQlMutation>();
            services.AddSingleton<GraphQlQuery>();
            services.AddSingleton<CourseType>();
            services.AddSingleton<InstructorType>();
        }

    }
}
