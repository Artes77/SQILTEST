using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace XUnitTestWebApplication5
{
    public class TestFixture : IDisposable
    {
        private readonly TestServer server;

        public HttpClient Client { get; }

        public TestFixture()
        {
            var builder = new WebHostBuilder()
                .UseEnvironment("Test")
                .UseStartup<WebApplication5.Startup>();
 

            server = new TestServer(builder);

            Client = server.CreateClient();
            Client.BaseAddress = new Uri("http://localhost:4200");
        }

        public void Dispose()
        {
           Client.Dispose();
            server.Dispose();
        }
    }
}
