using com.sun.security.ntlm;
using com.sun.xml.@internal.bind.v2.model.core;
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
using System.Data.Entity;
using System.Diagnostics;
using System.Net;
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

        [Fact]
        public async Task Post_ValidStudent_ReturnsOkResult()
        {
            // Arrange
            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddDbContext<ApplicationContext>(options =>
                    {
                        options.UseInMemoryDatabase("TestDatabase");
                    });
                });
            }).CreateClient();

            var student = new Student
            {
                FirstName = "yami",
                LastName = "patel",
                ContactNo = 8523457896,
                Email = "yami.patel@example.com",
                Gender = "female",
                DateOfBirth = "2002-11-06",
                Address = "qwertyu",
                Pincode = 96345
            };

            // Act (add student to database)
            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

                context.Student.Add(student);
                await context.SaveChangesAsync();
            }

            // Act (simulate HTTP request)
            var response = await client.PostAsJsonAsync("/api/Student/PostStudent", student);
          

            // Assert
            //using (var scope = _factory.Services.CreateScope())
            //{
            //    var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

            //    // Ensure context is using the in-memory database
            //    var insertedStudent = await context.Student.FirstOrDefaultAsync(s => s.Email == "yog.patel@example.com");

            //    Assert.NotNull(insertedStudent);
            //    Assert.Equal(student.FirstName, insertedStudent.FirstName);
            //    Assert.Equal(student.LastName, insertedStudent.LastName);
            //}
        }



        [Fact]
        public async Task GetStudentById_ReturnsStudent()
        {
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/Student/GetStudentById/1057");

            // Additional assertions can be added after successful response check
            var responseData = await response.Content.ReadAsAsync<Student>();

            if (responseData != null)
            {
                Assert.Equal(1009, responseData.Id);
                Assert.Equal("yog", responseData.FirstName);
            }

        }


        [Fact]
        public async Task Put_ExistingStudent_ReturnsOkResult()
        {
            // Arrange
            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddDbContext<ApplicationContext>(options =>
                    {
                        options.UseInMemoryDatabase("TestDatabase");
                    });
                });
            }).CreateClient();

            // Prepare the updated student data
            var updatedStudent = new Student
            {
                Id = 1059,
                FirstName = "UpdatedJohn",
                LastName = "UpdatedDoe",
                ContactNo = 9988776655,
                Email = "updated.john.doe@example.com",
                Gender = "Male",
                DateOfBirth = "1990-01-01",
                Address = "456 Elm St",
                Pincode = 54321
            };

            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

                context.Student.Update(updatedStudent);
                await context.SaveChangesAsync();
            }
            var response = await client.PutAsJsonAsync($"/api/Student/PutStudent/1059", updatedStudent);
        }


        [Fact]
        public async Task DeleteStudent_ExistingId_DeletesStudent()
        {

            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddDbContext<ApplicationContext>(options =>
                    {
                        options.UseInMemoryDatabase("TestDatabase");
                    });
                });
            }).CreateClient();

            var student = new Student
            {
              Id =1055
            };

            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

                context.Student.Remove(student);
                await context.SaveChangesAsync();
            }

            // Act (simulate HTTP request)
            var response = await client.DeleteAsync("/api/Student/DeleteStudent/1055");



            //  // Arrange
            //  var client = _factory.CreateClient();
            //  // Act
            //  var response = await client.DeleteAsync("/api/Student/DeleteStudent/1009");
            //// Verify that the response content does not contain the message "Student successfully deleted."
            //  var content = await response.Content.ReadAsStringAsync();
            //  Assert.DoesNotContain("Student successfully deleted.", content);
        }




    }
}





