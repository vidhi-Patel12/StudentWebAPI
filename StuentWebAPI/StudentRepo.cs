using Microsoft.EntityFrameworkCore;
using StuentWebAPI.DataContext;
using StuentWebAPI.Interface;
using StuentWebAPI.Model;

namespace EmployeeWebAPI.Repository
{
    public class StudentRepo : IStudent
    {
        private readonly ApplicationContext _context;
        private object student;

        public StudentRepo(ApplicationContext context)
        {
            _context = context;
        }

        public List<Student> GetAllStudents()
        {
            try
            {
                return _context.Student.ToList();
            }
            catch
            {
                throw;
            }
        }

        public Student GetStudentById(int id)
        {
            try
            {
                Student? employee = _context.Student.Find(id);
                if (employee != null)
                {
                    return employee;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch
            {
                throw;
            }
        }

        public void PostStudent(Student employee)
        {
            try
            {
                _context.Student.Add(employee);
                _context.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void PutStudent(Student employee)
        {
            try
            {
                _context.Entry(employee).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public Student DeleteStudent(int id)
        {
            try
            {
                Student? student = _context.Student.Find(id);

                if (student != null)
                {
                    _context.Student.Remove(student);
                    _context.SaveChanges();
                    return student;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch
            {
                throw;
            }
        }

        public bool StudentExists(int id)
        {
            return _context.Student.Any(e => e.Id == id);
        }

        public void GetStudentById(int id, CancellationToken none)
        {
            throw new NotImplementedException();
        }

        public void GetStudentById(Student employeeClass)
        {
            throw new NotImplementedException();
        }

        public void GetStudentById()
        {
            throw new NotImplementedException();
        }
    }
}