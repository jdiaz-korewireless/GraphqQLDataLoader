using System.Collections.Generic;

namespace GraphQLDataLoader.Models
{
    public class Session
    {
        public int SessionID { get; set; }
        public int InstructorID { get; set; }
        public int CourseID { get; set; }
        public Instructor Instructor { get; set; }
        public Course Course { get; set; }
        public IEnumerable<Student> Students { get; set; }

    }
}
