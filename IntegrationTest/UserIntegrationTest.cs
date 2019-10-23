using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Net;
using System.Net.Http;
using WebApplication5;
using WebApplication5.Models;
using Xunit;


namespace IntegrationTest
{
    public class UserIntegrationTest
    {
        [Fact]
        public async void GetAllUsers()
        {
            var webHostBuilder =
                  new WebHostBuilder()
                        .UseEnvironment("Test")
                        .UseStartup<Startup>();

            using (var server = new TestServer(webHostBuilder))
            using (var client = server.CreateClient())
            {
                var result = await client.GetAsync("/api/users");
                var b = await result.Content.ReadAsAsync<UserView[]>();
                Assert.NotNull(result);
                Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            }
        }

        [Fact]
        public async void GetPermission()
        {
            var webHostBuilder =
                  new WebHostBuilder()
                        .UseEnvironment("Test")
                        .UseStartup<Startup>();

            using (var server = new TestServer(webHostBuilder))
            using (var client = server.CreateClient())
            {
                var result1 = await client.GetAsync("/api/users");
                var b = await result1.Content.ReadAsAsync<UserView[]>();
                var result = await client.GetAsync("/api/users/" + b[0].Id);
                //var c = result.Content.Re
                var a = result.EnsureSuccessStatusCode();
                Assert.NotNull(result1);
                Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            }
        }

        [Fact]
        public async void CreateAndDeleteUser()
        {
            var webHostBuilder =
                  new WebHostBuilder()
                        .UseEnvironment("Test")
                        .UseStartup<Startup>();

            using (var server = new TestServer(webHostBuilder))
            using (var client = server.CreateClient())
            {
                var a = new UserView
                {
                    FirstName = "1234",
                    LastName = "4324",
                    RoleId = 1,
                    Role = new RoleView { role = "testRole"}
                };
                var post = await client.PostAsJsonAsync("api/users", a);
                var k = await post.Content.ReadAsAsync<UserView>();
                Assert.Equal(k.FirstName, a.FirstName);
                Assert.IsType<UserView>(k);

                var result = await client.DeleteAsync("/api/users/" + k.Id);
                Assert.NotNull(result);
                Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            }
        }
    }
}
