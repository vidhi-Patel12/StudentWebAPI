using StuentWebAPI.Model;

namespace StuentWebAPI.Interface
{
    public interface IStudent
    {
        public List<Student> GetAllStudents();

        public Student GetStudentById(int id);
        public void PostStudent(Student employee);
        public void PutStudent(Student employee);
        public Student DeleteStudent(int id);

        public bool StudentExists(int id);

    }
}
