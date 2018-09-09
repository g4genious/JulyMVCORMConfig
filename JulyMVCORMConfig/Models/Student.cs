using System;
using System.Collections.Generic;

namespace JulyMVCORMConfig.Models
{
    public partial class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Grade { get; set; }
        public string ProfilePicture { get; set; }
        public string CV { get; set; }
    }
}
