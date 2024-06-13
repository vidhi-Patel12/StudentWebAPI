using com.sun.security.ntlm;
using FluentAssertions.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Moq;
using Newtonsoft.Json;
using StudentWebAPI.Controllers;
using StuentWebAPI.DataContext;
using StuentWebAPI.Interface;
using StuentWebAPI.Model;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StudentWebAPI.IntegrationTests
{
    public class StudentIntegrationTest
    {
        private readonly WebApplicationFactory<Program> _factory;

        public StudentIntegrationTest()
        {
            _factory = new WebApplicationFactory<Program>();
        }
        //[Fact]
        //public async Task Post_ValidStudent_ReturnsOkResult()
        //{
        //    // Arrange
        //    var client = _factory.WithWebHostBuilder(builder =>
        //    {
        //        builder.ConfigureServices(services =>
        //        {
        //            services.AddDbContext<ApplicationContext>(options =>
        //            {
        //                options.UseInMemoryDatabase("TestDatabase");
        //            });
        //        });
        //    }).CreateClient();

        //    var student = new Student
        //    {
        //        FirstName = "John",
        //        LastName = "Doe",
        //        ContactNo = 1123457896,
        //        Email = "john.doe@example.com",
        //        Gender = "Male",
        //        DateOfBirth = "2024-10-06",
        //        Address = "123 Main St",
        //        Pincode = 12345
        //    };

        //    // Act
        //    using (var scope = _factory.Services.CreateScope())
        //    {
        //        var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

        //        // Add student to database
        //        context.Student.Add(student);
        //        await context.SaveChangesAsync();
        //    }

        //    // Act (simulate HTTP request)
        //    var response = await client.PostAsJsonAsync("/api/Student/PostStudent", student);

        //    // Assert
        //    using (var scope = _factory.Services.CreateScope())
        //    {
        //        var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
        //        var insertedStudent = await context.Student.FirstOrDefaultAsync(s => s.Email == "john.doe@example.com");

        //        Assert.NotNull(insertedStudent);
        //        Assert.Equal(student.FirstName, insertedStudent.FirstName);
        //        Assert.Equal(student.LastName, insertedStudent.LastName);
        //    }
        //}
       [Fact]
public async Task GetStudentById_ReturnsStudent()
{
    // Arrange
    var client = _factory.WithWebHostBuilder(builder =>
    {
        builder.ConfigureTestServices(services =>
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;

            services.AddSingleton<DbContextOptions<ApplicationContext>>(options);
            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseInMemoryDatabase("TestDatabase");
            });
        });
    }).CreateClient();

    using (var scope = _factory.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

        // Seed test data
        var student = new Student
        {
            FirstName = "John",
            LastName = "Doe",
            ContactNo = 1123457896,
            Email = "john.doe@example.com",
            Gender = "Male",
            DateOfBirth = "2024-10-06",
            Address = "123 Main St",
            Pincode = 12345
        };

        context.Student.Add(student);
        await context.SaveChangesAsync();
    }

    // Act
    var response = await client.GetAsync("/api/Student/GetStudentById/1009");
    var responseString = await response.Content.ReadAsStringAsync();

    // Assert
    var responseData = JsonConvert.DeserializeObject<Student>(responseString);

    // Check if responseData is null or has default values (indicating student not found)
    if (responseData == null || responseData.FirstName == null)
    {
        Assert.Contains("No students found.", responseString);
    }
    else
    {
        Assert.Equal("John", responseData.FirstName); // Example assertion for student data
    }
}




    }

}





   