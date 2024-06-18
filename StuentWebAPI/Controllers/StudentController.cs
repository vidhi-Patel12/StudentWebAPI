using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StuentWebAPI.DataContext;
using StuentWebAPI.Interface;
using StuentWebAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentWebAPI.Controllers
{
    [ApiController]
    //[Route("api/[action]")]
    [Route("api/[controller]/[action]")]

    public class StudentController : ControllerBase
    {
        private readonly ApplicationContext _context;
       private readonly IStudent _IStudent;
        private readonly IWebHostEnvironment WebHostEnvironment;
        private readonly ApplicationContext _dbContext;
        private IStudent @object;

        public StudentController(IStudent IStudent, IWebHostEnvironment webHostEnvironment, ApplicationContext context)
        {
            _IStudent = IStudent;
            WebHostEnvironment = webHostEnvironment;
            _context = context;
        }

        // GET: api/Student
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetAllStudents()
        {
            var students = await _context.Student.ToListAsync();

            if (students == null || students.Count == 0)
            {
                return NotFound("No students found.");
            }

            return Ok(new { Message = "Students found", Students = students });
        }


        // GET: api/GetStudentById/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudentById(int id)
        {
            var student = await _context.Student.FindAsync(id);

            if (student == null)
            {
                return NotFound("No students found.");
            }

            return Ok(new { Message = "Students found", Students = student });
        }

        // POST: api/PostStudent
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent([FromBody] Student student)
        {
            _context.Student.Add(student);
            await _context.SaveChangesAsync();
            return Ok(new { Message = "Student successfully added.",Student = student });
            //return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student );
        }
        //Put: api/PutStudent
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, Student student)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid student ID.");
            }

            // Manually set the Id from the route parameter
            student.Id = id;

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { Message = "Student successfully updated.", Student = student });
        }

       


        // DELETE: api/DeleteStudent/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Student.FindAsync(id);
            if (student == null)
            {
                return NotFound("Student not found.");
            }

            _context.Student.Remove(student);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Student successfully deleted." });
        }

        private bool StudentExists(int id)
        {
            return _context.Student.Any(e => e.Id == id);
        }
    }
}
