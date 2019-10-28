using GraphQL.DataLoader;
using GraphQL.Types;
using GraphQLDataLoader.Models;
using GraphQLDataLoader.Stores;
using System.Collections.Generic;

namespace GraphQLDataLoader.Types
{
    public class InstructorType : ObjectGraphType<Instructor>
    {
        //public InstructorType()
        //{
        //    Field<StringGraphType>("name", resolve: x => "CourseType");
        //}
        public InstructorType(IDataLoaderContextAccessor accessor, ICourseStore courses)
        {
            Field<IdGraphType>("instructorId", resolve: x => x.Source.InstructorID);
            Field(x => x.FullName);
            Field(x => x.HireDate);
            Field<ListGraphType<CourseType>, IEnumerable<Course>>()
                .Name("courses")
                .ResolveAsync(context =>
                {
                    var loader = accessor.Context.GetOrAddCollectionBatchLoader<int, Course>(
                        "getCourseByInstructorId", 
                        courses.GetCoursesByInstructorIdAsync);
                    return loader.LoadAsync(context.Source.InstructorID);
                });
        }
    }
}