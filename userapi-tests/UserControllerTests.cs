using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using UserApi.Models;
using UserApi;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace userapi_tests
{
    public class UserControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public UserControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Get_WhenCalled_ShouldReturnAllUsers()
        {
            HttpResponseMessage httpResponse = await _client.GetAsync("/api/user");

            httpResponse.EnsureSuccessStatusCode();

            String stringResponse = await httpResponse.Content.ReadAsStringAsync();
            IEnumerable<User> users = JsonConvert.DeserializeObject<IEnumerable<User>>(stringResponse);

            Assert.Contains(users, x => x.name == "Rui Rizzi");
            Assert.Contains(users, x => x.name == "Nikola Tesla");
            Assert.Contains(users, x => x.name == "Alan Turing");

        }
        [Fact]
        public async Task Get_WhenCalled_ShouldReturnOneUser()
        {
            HttpResponseMessage httpResponse = await _client.GetAsync("/api/user/1");

            httpResponse.EnsureSuccessStatusCode();

            String stringResponse = await httpResponse.Content.ReadAsStringAsync();
            User user = JsonConvert.DeserializeObject<User>(stringResponse);

            Assert.Equal(1, user.id);
        }

        [Fact]
        public async Task Post_WhenCalled_SouldAddNewUser()
        {
            User user = new User()
            {
                name = "George Boole",
                userName = "gboole",
                birthDate = new DateTime(1815,11,2),
                creationDate = DateTime.Now,
                passWordHash = "pwHash",
                passWordSalt = "pwSalt",
                isActive = true
            };

            string content = JsonConvert.SerializeObject(user);
            var buffer = Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);

            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage responseMessage = await _client.PostAsync("/api/user", byteContent);

            //HttpContent content = new StringContent(JsonConvert.SerializeObject(user));

            //HttpResponseMessage responseMessage = await _client.PostAsync("/api/user", content);

        }
    }
}
