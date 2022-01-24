using System;
using System.Collections.Generic;

#nullable disable

namespace StudentAPI.Models
{
    public partial class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int? DepartmentId { get; set; }
        public DateTime? Createddate { get; set; }

        public virtual Department Department { get; set; }
    }
}
