using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StudentWebAPI.Controllers;
using StuentWebAPI.DataContext;
using StuentWebAPI.Interface;
using StuentWebAPI.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentWebAPI.Tests
{
    [TestClass]
    public class StudentControllerTests
    {
        [TestMethod]
        public async Task GetReturnsStudentDetails()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new ApplicationContext(options))
            {
                // Ensure context is properly initialized and not null
                if (context == null)
                {
                    // Handle the case where context is not properly initialized
                    Assert.Fail("Failed to initialize database context");
                }

                var mockStudent = new Mock<IStudent>();
                var webHostEnvironment = new Mock<IWebHostEnvironment>();
                var controller = new StudentController(mockStudent.Object, webHostEnvironment.Object, context);

                // Act
                var result = await controller.GetAllStudents();

                // Assert
                Assert.IsNotNull(result);
                // Perform additional assertions if needed
            }
        }

        [TestMethod]
        public async Task PostValidStudent_ReturnsOkResult()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new ApplicationContext(options))
            {
                // Ensure context is properly initialized and not null
                if (context == null)
                {
                    // Handle the case where context is not properly initialized
                    Assert.Fail("Failed to initialize database context");
                }

                var mockStudent = new Mock<IStudent>(); // Assuming IStudent is an interface for Student model
                var webHostEnvironment = new Mock<IWebHostEnvironment>();
                var controller = new StudentController(mockStudent.Object, webHostEnvironment.Object, context);

                var student = new Student
                {
                    FirstName = "John",
                    LastName = "Doe",
                    ContactNo = 1123457896,
                    Email = "john.doe@example.com",
                    Gender = "Male",
                    DateOfBirth = "2024-10-06",
                    Address = "sdffrggdfgtgd",
                    Pincode = 124578
                };

                // Act
                var result = await controller.PostStudent(student);

                // Assert
                Assert.IsNotNull(result);
                var okResult = result.Result as OkObjectResult;
                Assert.IsNotNull(okResult);

                var message = okResult.Value.GetType().GetProperty("Message").GetValue(okResult.Value) as string;
                Assert.AreEqual("Student successfully added.", message);
            }
        }

        [TestMethod]
        public async Task PutValidStudentReturnsOkResult()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new ApplicationContext(options))
            {
                // Ensure context is properly initialized and not null
                if (context == null)
                {
                    // Handle the case where context is not properly initialized
                    Assert.Fail("Failed to initialize database context");
                }

                var mockStudent = new Mock<IStudent>(); // Assuming IStudent is an interface for Student model
                var webHostEnvironment = new Mock<IWebHostEnvironment>();
                var controller = new StudentController(mockStudent.Object, webHostEnvironment.Object, context);

                // Arrange: Create a new student to update
                var student = new Student
                {
                    FirstName = "John",
                    LastName = "Doe",
                    ContactNo = 1123457896,
                    Email = "john.doe@example.com",
                    Gender = "Male",
                    DateOfBirth = "2024-10-06",
                    Address = "sdffrggdfgtgd",
                    Pincode = 124578
                };

                // Add the student to the database
                context.Student.Add(student);
                await context.SaveChangesAsync();

                // Update some properties of the student
                student.FirstName = "Updated John";

                // Act: Call the PutStudent method to update the student
                var result = await controller.PutStudent(student.Id, student);

                // Assert
                Assert.IsNotNull(result);
                var okResult = result as OkObjectResult;
                Assert.IsNotNull(okResult);

                var message = okResult.Value.GetType().GetProperty("Message").GetValue(okResult.Value) as string;
                Assert.AreEqual("Student successfully updated.", message);
            }
        }

    }
}


