using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using UserApi.Models;
using UserApi;
using System.Net.Http;
using Newtonsoft.Json;

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
        public async Task CanGetUsers()
        {
            var httpResponse = await _client.GetAsync("/api/user");

            httpResponse.EnsureSuccessStatusCode();

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<IEnumerable<User>>(stringResponse);

            Assert.Contains(users, x => x.name == "Rui Rizzi");
            Assert.Contains(users, x => x.name == "Nikola Tesla");
            Assert.Contains(users, x => x.name == "Alan Turing");

        }

    }
}
