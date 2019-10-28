using GraphQLDataLoader.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GraphQLDataLoader.Stores
{
    public interface ICourseStore
    {
        Task<ILookup<int, Course>> GetCoursesByInstructorIdAsync(IEnumerable<int> instructorIds);
        Task<ILookup<int, Course>> GetCoursesByStudentIdAsync(IEnumerable<int> studentIds);
        Task<Dictionary<int, Course>> GetCoursesByIdAsync(IEnumerable<int> courseIds, CancellationToken cancellationToken);
        Task<IEnumerable<Course>> GetAllCoursesAsync();
    }
}
