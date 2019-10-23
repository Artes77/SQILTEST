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
    public class RoleIntegrationTest
    {
        [Fact]
        public async void GetAllRoles()
        {
            var webHostBuilder =
                  new WebHostBuilder()
                        .UseEnvironment("Test")
                        .UseStartup<Startup>();

            using (var server = new TestServer(webHostBuilder))
            using (var client = server.CreateClient())
            {
                var result = await client.GetAsync("/api/roles");
                var b = await result.Content.ReadAsAsync<RoleView[]>();
                Assert.NotNull(result);
                Assert.IsType<RoleView[]>(b);
                Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            }
        }

        [Fact]
        public async void GetRole()
        {
            var webHostBuilder =
                  new WebHostBuilder()
                        .UseEnvironment("Test")
                        .UseStartup<Startup>();

            using (var server = new TestServer(webHostBuilder))
            using (var client = server.CreateClient())
            {
                var result1 = await client.GetAsync("/api/roles");
                var b = await result1.Content.ReadAsAsync<RoleView[]>();
                var result = await client.GetAsync("/api/roles/" + b[0].Id);
                //var c = result.Content.Re
                var a = result.EnsureSuccessStatusCode();
                Assert.NotNull(result1);
                Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            }
        }

        [Fact]
        public async void CreateAndDeleteRole()
        {
            var webHostBuilder =
                  new WebHostBuilder()
                        .UseEnvironment("Test")
                        .UseStartup<Startup>();

            using (var server = new TestServer(webHostBuilder))
            using (var client = server.CreateClient())
            {
                var a = new RoleView
                {
                    role = "crabic"
                };
                var post = await client.PostAsJsonAsync("api/roles", a);
                var k = await post.Content.ReadAsAsync<RoleView>();
                Assert.Equal(k.role, a.role);
                Assert.IsType<RoleView>(k);

                var result = await client.DeleteAsync("/api/roles/" + k.Id);
                Assert.NotNull(result);
                Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            }
        }
    }
}
