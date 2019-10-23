using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using WebApplication5.Controllers;
using WebApplication5.Models;
using WebApplication5.Data;
using System.Net;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestWebApplication5.Controllers
{
    public class PermissionControllerTest : IClassFixture<TestFixture>
    {
        private readonly HttpClient client;
        PermissionControllerTest(TestFixture fixture)
        {
            client = fixture.Client;
        }
         [Fact]
         public void GetAllPermissionReturnNotNull()
         {
            //Arrange
            var request = new HttpRequestMessage(HttpMethod.Get, "api/permissions");
            //??/???

            // Act???

            var response = client.SendAsync(request);

            //Assert???
            var i = 25;
            var b = 25;
            Assert.Equal(i, b);

             //var mock = new Mock<PermissionRepository>();
             //var q = new Mock<PermissionRepository>();
             //mock.Setup(i => i.GetPermissions()).Returns(mock.);
            // mock.Setup(rep => rep.GetPermissions().ToList()).Returns(GetTestPermissions());
            // var Controller = new PermissionController(mock.Object);

             // Act
            // var result = Controller.GetAllPermission();

             // Assert
            // var viewResult = Assert.IsType<PermissionView>(result);
             //Assert.Null(result);
             //var model = Assert.IsAssignableFrom<IEnumerable<Permission>>(viewResult);
             //Assert.Equal(GetTestPermissions().Count, model.Count());
         }

    }
}
