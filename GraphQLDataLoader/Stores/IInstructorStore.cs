using GraphQLDataLoader.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GraphQLDataLoader.Stores
{
    public interface IInstructorStore
    {
        Task<IDictionary<int, Instructor>> GetInstructorsById(IEnumerable<int> instructorIds, CancellationToken cancellationToken);
        Task<ILookup<int, Instructor>> GetInstructorsByStudentIdAsync(IEnumerable<int> studentIds);
        Task<ILookup<int, Instructor>> GetInstructorsByCourseIdAsync(IEnumerable<int> courseIds);
        Task<IEnumerable<Instructor>> GetAllInstructorsAsync();
    }
}
