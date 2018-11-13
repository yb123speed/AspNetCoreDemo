using System.Collections.Generic;

namespace FluentValidationSample.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Hobbies { get; set; }
    }
}