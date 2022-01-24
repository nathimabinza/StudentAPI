using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentAPI.Models;
using StudentAPI.Repository;
using System;
using System.Threading.Tasks;

namespace StudentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;

        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        /// <summary>
        /// API to Get All Departments details
        /// </summary>
        /// <returns>List of Departments</returns>
        [HttpGet]
        [Route("GetDepartments")]
        public async Task<IActionResult> GetDepartments()
        {
            try
            {
                var departments = await _studentRepository.GetDepartment();
                if (departments == null)
                {
                    return NotFound();
                }
                return Ok(departments);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
        
        /// <summary>
        /// API to get all student details
        /// </summary>
        /// <returns>List of all student details</returns>
        [HttpGet]
        [Route("GetAllStudentDetails")]
        public async Task<IActionResult> GetAllStudentDetails()
        {
            try
            {
                var student = await _studentRepository.GetAllStudentDetails();
                if (student == null)
                {
                    return NotFound();
                }
                
                return Ok(student);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        /// <summary>
        /// API to get student details based on studentId
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns>student details</returns>
        [HttpGet]
        [Route("GetStudentDetailsById")]
        public async Task<IActionResult> GetStudentDetailsById(int? studentId)
        {
            if (studentId == null)
            {
                return BadRequest();
            }
            try
            {
                var student = await _studentRepository.GetAllStudentDetailsById(studentId);
                if (student == null)
                {
                    return NotFound();
                }

                return Ok(student);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
        /// <summary>
        /// API to add new student
        /// </summary>
        /// <param name="model"></param>
        /// <returns>studentId</returns>
        [HttpPost]
        [Route("AddStudent")]
        public async Task<IActionResult> AddStudent([FromBody] Student model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var studentId = await _studentRepository.AddStudent(model);
                    if (studentId > 0)
                    {
                        return Ok(studentId);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }

        /// <summary>
        /// API to delete student details
        /// </summary>
        /// /// <param name="studentId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeleteStudent")]
        public async Task<IActionResult> DeleteStudent(int? studentId)
        {
            int result = 0;
            if (studentId == null)
            {
                return BadRequest();
            }
            try
            {
                result = await _studentRepository.DeleteStudent(studentId);
                if (result == 0)
                {
                    return NotFound();
                }
                return Ok("Deleted successfully.");
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        /// <summary>
        /// API to update student details
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateStudent")]
        public async Task<IActionResult> UpdateStudent([FromBody] Student student)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _studentRepository.UpdateStudent(student);
                    return Ok("Update successfully.");
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }
    }
}
