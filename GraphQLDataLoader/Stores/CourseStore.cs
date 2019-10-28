using GraphQL;
using GraphQLDataLoader.Data;
using GraphQLDataLoader.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GraphQLDataLoader.Stores
{
    public class CourseStore : ICourseStore
    {
        private readonly SchoolContext _db;
        public CourseStore(SchoolContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            return await _db.Courses.ToListAsync();
        }

        public Task<Dictionary<int, Course>> GetCoursesByIdAsync(IEnumerable<int> courseIds, CancellationToken cancellationToken)
        {
            var result = _db.Courses.Where(ca => courseIds.Contains(ca.CourseID)).ToDictionary(x => x.CourseID);
            return Task.FromResult(result);
        }

        public async Task<ILookup<int, Course>> GetCoursesByInstructorIdAsync(IEnumerable<int> instructorIds)
        {
            var result = await _db.CourseAssignments.Where(ca => instructorIds.Contains(ca.InstructorID)).ToListAsync();

            return result.ToLookup(o => o.InstructorID, o => o.Course);
        }

        public Task<ILookup<int, Course>> GetCoursesByStudentIdAsync(IEnumerable<int> studentIds)
        {
            var result = _db.Enrollments.Where(ca => studentIds.Contains(ca.StudentID))
                .ToLookup(o => o.StudentID, o => o.Course);
            return Task.FromResult(result);
        }
    }
}
