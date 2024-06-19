using com.sun.xml.@internal.ws.client;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StuentWebAPI.Interface;
using StuentWebAPI.Model;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace StudentWebAPI.IntegrationTests
{
    public class StudentIntegrationTest : IClassFixture<StudentTestFactory>
    {
        private readonly HttpClient _client;

        public StudentIntegrationTest(StudentTestFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task PostValidStudent_ReturnsOkResult()
        {
            var student = new Student
            {
                FirstName = "hinal",
                LastName = "patel",
                ContactNo = 9632501410,
                Email = "hinal.patel@example.com",
                Gender = "female",
                DateOfBirth = "1995-02-19",
                Address = "ferthtykuyykikuyloulul",
                Pincode = 123125
            };

            var url = "/api/Student/PostStudent"; // Use relative URL

            // Act
            var response = await _client.PostAsJsonAsync(url, student);
            response.EnsureSuccessStatusCode();


            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("OK", response.ReasonPhrase);


            var responseContent = await response.Content.ReadAsStringAsync();
            var jsonResponse = JObject.Parse(responseContent);

            // Assert against the JSON object
            Assert.True(jsonResponse.ContainsKey("message"));
            Assert.Equal("Student successfully added.", (string)jsonResponse["message"]);

            Assert.True(jsonResponse.ContainsKey("student"));

            var jsonStudent = jsonResponse["student"] as JObject;
            Assert.NotNull(jsonStudent);
            System.Diagnostics.Debug.WriteLine(jsonStudent.ToString());


            Assert.True(jsonStudent.ContainsKey("firstName"), "FirstName key is missing in jsonStudent");
            Assert.Equal(student.FirstName, (string)jsonStudent["firstName"]);

            Assert.True(jsonStudent.ContainsKey("lastName"), "LastName key is missing in jsonStudent");
            Assert.Equal(student.LastName, (string)jsonStudent["lastName"]);

            Assert.True(jsonStudent.ContainsKey("contactNo"), "ContactNo key is missing in jsonStudent");
            Assert.Equal(student.ContactNo, (long)jsonStudent["contactNo"]);

            Assert.True(jsonStudent.ContainsKey("email"), "Email key is missing in jsonStudent");
            Assert.Equal(student.Email, (string)jsonStudent["email"]);

            Assert.True(jsonStudent.ContainsKey("gender"), "Gender key is missing in jsonStudent");
            Assert.Equal(student.Gender, (string)jsonStudent["gender"]);

            Assert.True(jsonStudent.ContainsKey("dateOfBirth"), "DateOfBirth key is missing in jsonStudent");
            Assert.Equal(student.DateOfBirth, (string)jsonStudent["dateOfBirth"]);

            Assert.True(jsonStudent.ContainsKey("address"), "Address key is missing in jsonStudent");
            Assert.Equal(student.Address, (string)jsonStudent["address"]);

            Assert.True(jsonStudent.ContainsKey("pincode"), "Pincode key is missing in jsonStudent");
            Assert.Equal(student.Pincode, (int)jsonStudent["pincode"]);

        }


        [Fact]
        public async Task GetValidStudent_ReturnsOkResult()
        {
            var studentId = 36; // Assume a student with ID 1 exists
            var url = $"/api/Student/GetStudent/{studentId}"; // Use relative URL

            // Act
            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("OK", response.ReasonPhrase);

            var responseContent = await response.Content.ReadAsStringAsync();
            var jsonResponse = JObject.Parse(responseContent);

            // Assert against the JSON object
            Assert.True(jsonResponse.ContainsKey("student"));

            var jsonStudent = jsonResponse["student"] as JObject;
            Assert.NotNull(jsonStudent);
            System.Diagnostics.Debug.WriteLine(jsonStudent.ToString());

            // Assuming you know the expected student details for ID 15
            var expectedStudent = new
            {
                FirstName = "hinal",
                LastName = "patel",
                ContactNo = 9632501410,
                Email = "hinal.patel@example.com",
                Gender = "female",
                DateOfBirth = "1995-02-19",
                Address = "ferthtykuyykikuyloulul",
                Pincode = 123125
            };

            // Assert each field in the student object
            Assert.True(jsonStudent.ContainsKey("firstName"), "FirstName key is missing in jsonStudent");
            Assert.Equal(expectedStudent.FirstName, (string)jsonStudent["firstName"]);

            Assert.True(jsonStudent.ContainsKey("lastName"), "LastName key is missing in jsonStudent");
            Assert.Equal(expectedStudent.LastName, (string)jsonStudent["lastName"]);

            Assert.True(jsonStudent.ContainsKey("contactNo"), "ContactNo key is missing in jsonStudent");
            Assert.Equal(expectedStudent.ContactNo, (long)jsonStudent["contactNo"]);

            Assert.True(jsonStudent.ContainsKey("email"), "Email key is missing in jsonStudent");
            Assert.Equal(expectedStudent.Email, (string)jsonStudent["email"]);

            Assert.True(jsonStudent.ContainsKey("gender"), "Gender key is missing in jsonStudent");
            Assert.Equal(expectedStudent.Gender, (string)jsonStudent["gender"]);

            Assert.True(jsonStudent.ContainsKey("dateOfBirth"), "DateOfBirth key is missing in jsonStudent");
            Assert.Equal(expectedStudent.DateOfBirth, (string)jsonStudent["dateOfBirth"]);

            Assert.True(jsonStudent.ContainsKey("address"), "Address key is missing in jsonStudent");
            Assert.Equal(expectedStudent.Address, (string)jsonStudent["address"]);

            Assert.True(jsonStudent.ContainsKey("pincode"), "Pincode key is missing in jsonStudent");
            Assert.Equal(expectedStudent.Pincode, (int)jsonStudent["pincode"]);
        }




    }
}




