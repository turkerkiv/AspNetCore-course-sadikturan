using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreApp.Data
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }

        public ICollection<CourseRegistiration> CourseRegistirations { get; set; } = new List<CourseRegistiration>();
    }
}