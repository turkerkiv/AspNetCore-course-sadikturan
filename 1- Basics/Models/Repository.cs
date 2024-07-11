namespace basics.Models
{
    public class Repository
    {
        private static readonly List<Course> _courses = new List<Course>();
        public static List<Course> Courses { get { return _courses; } }

        static Repository()
        {
            _courses = new List<Course>(){
            new Course(){Id =1,
            Title= "javascript course",
            Image="1.jpeg",
            Description="First course",
            Tags = new string[]{"Web Development", "Backend", "Frontend"},
            IsHome = true,
            IsActive = true,
            },
            new Course(){Id =2,
            Title= "java course",
            Image="2.jpeg",
            Description="Second course",
            Tags = new string[]{"Web Development", "Backend"},
            IsActive = true,
            IsHome = true,
            },
            new Course(){Id =3,
            Title= "html&css course",
            Image="3.jpeg",
            Description="Third course",
            Tags = new string[]{"Web Development", "Frontend"},
            IsActive = true,
            IsHome = true,
            },
            new Course(){Id =4,
            Title= "C# course",
            Image="2.jpeg",
            Description="Fourth course",
            Tags = new string[]{"Web Development", "Backend"},
            IsActive = false,
            IsHome = false,
            },
            };
        }

        public static Course? GetById(int? id)
        {
            return _courses.FirstOrDefault(c => c.Id == id);
        }
    }
}