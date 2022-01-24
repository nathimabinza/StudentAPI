using StudentAPI.Models;
using StudentAPI.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentAPI.Repository
{
    public interface IStudentRepository
    {
        Task<List<Department>> GetDepartment();
        Task<List<StudentViewModel>> GetAllStudentDetails();
        Task<StudentViewModel> GetAllStudentDetailsById(int? studentId);
        Task<int> AddStudent(Student student);
        Task<int> DeleteStudent(int? studentId);
        Task UpdateStudent(Student student);
    }
}
