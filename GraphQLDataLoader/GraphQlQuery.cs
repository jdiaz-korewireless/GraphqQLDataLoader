using GraphQL.DataLoader;
using GraphQL.Types;
using GraphQLDataLoader.Stores;
using GraphQLDataLoader.Types;

namespace GraphQLDataLoader
{
    public class GraphQlQuery : ObjectGraphType
    {
        public GraphQlQuery(IDataLoaderContextAccessor accessor, ICourseStore courses, IInstructorStore instructors)
        {
            FieldAsync<ListGraphType<CourseType>>("courses",
                resolve: async context =>
                {
                    var loader = accessor.Context.GetOrAddLoader("GetAllCourses", () => courses.GetAllCoursesAsync());

                    return await loader.LoadAsync();
                });

            FieldAsync<ListGraphType<InstructorType>>("instructors",
                resolve: async context =>
                {
                    var loader = accessor.Context.GetOrAddLoader("GetAllInstructors", () => instructors.GetAllInstructorsAsync());

                    return await loader.LoadAsync();
                });
        }
    }
}
