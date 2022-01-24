using System;

namespace StudentAPI.ViewModel
{
    public class StudentViewModel
    {
        public int StudentId { get; set; }
        public string FullName { get; set;}
        public string Email { get; set;}
        public int? DepartmentId { get; set;}
        public DateTime? CreatedDate { get; set;}
        public string DepartmentName { get; set;}
    }
}
