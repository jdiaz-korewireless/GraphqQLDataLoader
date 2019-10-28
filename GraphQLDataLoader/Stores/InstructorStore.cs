using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GraphQLDataLoader.Data;
using GraphQLDataLoader.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphQLDataLoader.Stores
{
    public class InstructorStore : IInstructorStore
    {
        private readonly SchoolContext _db;
        public InstructorStore(SchoolContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<Instructor>> GetAllInstructorsAsync()
        {
            return await _db.Instructors.ToListAsync();
        }

        public async Task<ILookup<int, Instructor>> GetInstructorsByCourseIdAsync(IEnumerable<int> courseIds)
        {
            var result = await _db.CourseAssignments.Where(ca => courseIds.Contains(ca.CourseID))
                .Select(ca => new { ca.CourseID, ca.Instructor }).ToListAsync();

            return result.ToLookup(x => x.CourseID, x => x.Instructor);
        }

        //public Task<Dictionary<int, Instructor>> GetInstructorsByCourseIdAsync(IEnumerable<int> courseIds)
        //{
        //    return GetInstructorsByCourseIdAsync(courseIds, CancellationToken.None);
        //}

        public async Task<IDictionary<int, Instructor>> GetInstructorsById(IEnumerable<int> instructorIds, CancellationToken cancellationToken)
        {
            var result = _db.Instructors.Where(i => instructorIds.Contains(i.InstructorID)).ToDictionary(i => i.InstructorID);
            return await Task.FromResult(result);
        }

        public async Task<ILookup<int, Instructor>> GetInstructorsByStudentIdAsync(IEnumerable<int> studentIds)
        {
            var result = from i in _db.Instructors
                         join ca in _db.CourseAssignments
                         on i.InstructorID equals ca.InstructorID
                         join en in _db.Enrollments
                            on ca.CourseID equals en.CourseID
                         where studentIds.Contains(en.StudentID)
                         select new { en.StudentID, Instructor = i };

            var instructors = await result.ToListAsync();
            return instructors.ToLookup(x => x.StudentID, x => x.Instructor);
        }
    }
}
