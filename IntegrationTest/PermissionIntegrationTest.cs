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
    public class PermissionIntegrationTest
    {
        [Fact]
        public async void GetAllPermissions()
        {
            var webHostBuilder =
                  new WebHostBuilder()
                        .UseEnvironment("Test") 
                        .UseStartup<Startup>();

            using (var server = new TestServer(webHostBuilder))
            using (var client = server.CreateClient())
            {
                var result = await client.GetAsync("/api/permissions");
                var b = await result.Content.ReadAsAsync<PermissionView[]>();
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
                var result1 = await client.GetAsync("/api/permissions");
                var b = await result1.Content.ReadAsAsync<PermissionView[]>();
                var result = await client.GetAsync("/api/permissions/" + b[0].Id);
                //var c = result.Content.Re
                var a = result.EnsureSuccessStatusCode();
                Assert.NotNull(result1);
                Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            }
        }

        [Fact]
        public async void CreateAndDeletePermission()
        {
            var webHostBuilder =
                  new WebHostBuilder()
                        .UseEnvironment("Test") 
                        .UseStartup<Startup>(); 

            using (var server = new TestServer(webHostBuilder))
            using (var client = server.CreateClient())
            {
                var a = new PermissionView
                {
                    Name = "crabic"
                };
                var post = await client.PostAsJsonAsync("api/permissions", a);
                var k = await post.Content.ReadAsAsync<PermissionView>();
                Assert.Equal(k.Name, a.Name);
                Assert.IsType<PermissionView>(k);

                var result = await client.DeleteAsync("/api/permissions/"+ k.Id);
                Assert.NotNull(k);
                Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            }
        }
    }
}
