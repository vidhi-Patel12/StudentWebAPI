using com.sun.xml.@internal.ws.client;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
                FirstName = "megha",
                LastName = "maheta",
                ContactNo = 9632501478,
                Email = "sneha.maheta@example.com",
                Gender = "female",
                DateOfBirth = "2000-01-07",
                Address = "awawaawawaww",
                Pincode = 123123
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

            Assert.True(jsonResponse.ContainsKey("firstName")); 
            Assert.Equal(student.FirstName, (string)jsonResponse["firstName"]);

        }

        //var responseContent = await response.Content.ReadAsStringAsync();

        // var responseObject = JsonSerializer.Deserialize<StudentApiResponse>(responseContent);

        //    Assert.NotNull(responseObject);
        //    Assert.Equal("Student successfully added.", responseObject.Message);
        //    Assert.Equal(student.FirstName, responseObject.Student.FirstName);
        //    Assert.Equal(student.LastName, responseObject.Student.LastName);
        //    Assert.Equal(student.Email, responseObject.Student.Email);
        //    Assert.Equal(student.Gender, responseObject.Student.Gender);
        //    Assert.Equal(student.DateOfBirth, responseObject.Student.DateOfBirth);
        //    Assert.Equal(student.Address, responseObject.Student.Address);
        //    Assert.Equal(student.Pincode, responseObject.Student.Pincode);


    }
}

    //public class StudentApiResponse
    //{
    //    public string Message { get; set; }
    //    public Student Student { get; set; }
    //}

   
