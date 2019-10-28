using GraphQL.DataLoader;
using GraphQL.Types;
using GraphQLDataLoader.Models;
using GraphQLDataLoader.Stores;
using System.Collections.Generic;

namespace GraphQLDataLoader.Types
{
    public class CourseType : ObjectGraphType<Course>
    {
        //public CourseType()
        //{
        //    Field<StringGraphType>("name", resolve: x => "CourseType");
        //}
        public CourseType(IDataLoaderContextAccessor accessor, IInstructorStore instructors)
        //public CourseType(IInstructorStore instructors)
        //public CourseType(IDataLoaderContextAccessor accessor)
        //public CourseType()
        {
            Field<IdGraphType>("courseId", resolve: x => x.Source.CourseID);
            Field(x => x.DepartmentID);
            Field(x => x.Credits);
            Field<ListGraphType<InstructorType>, IEnumerable<Instructor>>()
            .Name("instructors")
            .ResolveAsync(context =>
            {
                // Get or add a batch loader with the key "GetUsersById"
                // The loader will call GetUsersByIdAsync for each batch of keys
                var loader = accessor.Context.GetOrAddCollectionBatchLoader<int, Instructor>(
                    "GetInstructorsByCourseId", 
                    instructors.GetInstructorsByCourseIdAsync);

                // Add this UserId to the pending keys to fetch
                // The task will complete once the GetUsersByIdAsync() returns with the batched results
                return loader.LoadAsync(context.Source.CourseID);
            });
        }
    }
}
