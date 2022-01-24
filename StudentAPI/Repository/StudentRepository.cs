using Microsoft.EntityFrameworkCore;
using StudentAPI.Models;
using StudentAPI.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace StudentAPI.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly CollegeContext _collegeContext;

        public StudentRepository(CollegeContext collegeContext)
        {
            _collegeContext = collegeContext;
        }

        /// <summary>
        /// Method to add new student Details
        /// </summary>
        /// <param name="student"></param>
        /// <returns>StudentId</returns>

        public async Task<int> AddStudent(Student model)
        {
            if (_collegeContext != null)
            {
                Student student = new();
                student.FirstName = model.FirstName;
                student.LastName = model.LastName;
                student.Email = model.Email;
                student.DepartmentId = model.DepartmentId;
                student.Createddate = System.DateTime.Now;
                // Add the record in the database
                await _collegeContext.AddAsync(student);
                await _collegeContext.SaveChangesAsync();

                return student.StudentId;
            }
            return 0;
        }
        /// <summary>
        /// Method to delete student details based on studentId
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public async Task<int> DeleteStudent(int? studentId)
        {
            int result = 0;
            if (_collegeContext != null)
            {
                var student = await _collegeContext.Students.FirstOrDefaultAsync(x => x.StudentId == studentId);
                if (student != null)
                {
                    // delete
                    _collegeContext.Students.Remove(student);
                    result = await _collegeContext.SaveChangesAsync();
                }
                return result;
            }
            return result;
        }
        /// <summary>
        /// Method to get all student details
        /// </summary>
        /// <returns> List of student details</returns>
        public async Task<List<StudentViewModel>> GetAllStudentDetails()
        {
            if (_collegeContext != null)
            {
                return await (from s in _collegeContext.Students
                              from d in _collegeContext.Departments
                              where s.DepartmentId == d.DepartmentId
                              select new StudentViewModel
                              {
                                  StudentId = s.StudentId,
                                  FullName = s.FirstName + "" + s.LastName,
                                  Email = s.Email,
                                  DepartmentId = d.DepartmentId,
                                  CreatedDate = s.Createddate,
                                  DepartmentName = d.DepartmentName,
                              }).ToListAsync();
            }
            return null;
        }

        /// <summary>
        /// Method to get student details based on studentId
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public async Task<StudentViewModel> GetAllStudentDetailsById(int? studentId)
        {
            if (_collegeContext != null)
            {
                return await (from s in _collegeContext.Students
                              from d in _collegeContext.Departments
                              where s.DepartmentId == d.DepartmentId 
                              && s.StudentId == studentId
                              select new StudentViewModel
                              {
                                  StudentId = s.StudentId,
                                  FullName = s.FirstName + "" + s.LastName,
                                  Email = s.Email,
                                  DepartmentId = s.DepartmentId,
                                  CreatedDate = s.Createddate,
                                  DepartmentName = d.DepartmentName,
                              }).FirstOrDefaultAsync();
            }
            return null;
        }
        /// <summary>
        /// Method to get all the departments
        /// </summary>
        /// <returns> List of departments in College</returns>
        public async Task<List<Department>> GetDepartment()
        {
            if(_collegeContext != null)
            {
                return await _collegeContext.Departments.ToListAsync();
            }
            return null;
        }
        /// <summary>
        /// Update student details
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public async Task UpdateStudent(Student model)
        {
            if (_collegeContext != null)
            {
                // update the record in the database
                _collegeContext.Update(model);
                await _collegeContext.SaveChangesAsync();
            }
        }
    }
}
