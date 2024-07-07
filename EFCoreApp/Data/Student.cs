using System.ComponentModel.DataAnnotations;

namespace EFCoreApp.Data
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string NameSurname => Name + " " + Surname;
        public string? Email { get; set; }
        public string? Phone { get; set; }

        public ICollection<CourseRegistiration> CourseRegistirations { get; set; } = new List<CourseRegistiration>();
    }
}