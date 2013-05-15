using System.Collections.Generic;

namespace BootstrapMvcDemoProject.Models
{
    public static class LocalDbContext
    {
        private static List<Student> _students;

        public static ICollection<Student> Students
        {
            get { return _students ?? (_students = new List<Student>()); }
        }
    }
}