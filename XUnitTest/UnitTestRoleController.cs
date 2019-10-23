using WebApplication5.Data;
using WebApplication5.Models;
using WebApplication5.Controllers;
using System.Collections.Generic;
using Xunit;
using Moq;
using WebApplication5.Data.Interface;
using AutoMapper;
using WebApplication5.Models.MappingProfile;

namespace XUnitTest
{
    public class UnitTestRoleController
    {
        [Fact]
        public void GetRoleByIdNotNull()
        {
            var mock = new Mock<IRole>();
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = new Mapper(mockMapper);
            mock.Setup(i => i.GetRoleById(1)).Returns(GetTestRole());
            var Controller = new RoleController(mock.Object, mapper);

            // Act
            var result = Controller.GetRole(1);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void GetRoleByIdReturnTypePermissionView()
        {
            var mock = new Mock<IRole>();
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = new Mapper(mockMapper);
            mock.Setup(i => i.GetRoleById(1)).Returns(GetTestRole());
            var Controller = new RoleController(mock.Object, mapper);

            // Act
            var result = Controller.GetRole(1);

            // Assert
            Assert.IsType<RoleView>(result);
        }

        [Fact]
        public void GetRolesNotNull()
        {
            var mock = new Mock<IRole>();
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = new Mapper(mockMapper);
            mock.Setup(i => i.GetRoles()).Returns(GetTestRoles());
            var Controller = new RoleController(mock.Object, mapper);

            // Act
            var result = Controller.GetAllRole();

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void GetRolesReturnTypePermissionView()
        {
            var mock = new Mock<IRole>();
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = new Mapper(mockMapper);
            mock.Setup(i => i.GetRoles()).Returns(GetTestRoles());
            var Controller = new RoleController(mock.Object, mapper);

            // Act
            var result = Controller.GetAllRole();

            // Assert
            Assert.IsType<RoleView[]>(result);
        }

        [Fact]
        public void CreateRoleReturnTypeRoleView()
        {
            var mock = new Mock<IRole>();
            var mockMap = new Mock<IMapper>();
            var a = mockMap.Object;
            mock.Setup(i => i.CreateRole(It.IsAny<Role>())).Returns(GetTestRole());
            mockMap.Setup(m => m.Map<Role, RoleView>(It.IsAny<Role>())).Returns(GetTestRoleView());
            mockMap.Setup(m => m.Map<RoleView, Role>(It.IsAny<RoleView>())).Returns(GetTestRole());
            var Controller = new RoleController(mock.Object, mockMap.Object);

            // Act
            var result = Controller.CreateRole(GetTestRoleView());

            // Assert
            Assert.IsType<RoleView>(result);

        }

        [Fact]
        public void CreateRoleReturnNotNull()
        {
            var mock = new Mock<IRole>();
            var mockMap = new Mock<IMapper>();
            var a = mockMap.Object;
            var perm = new Permission();
            mock.Setup(i => i.CreateRole(It.IsAny<Role>())).Returns(GetTestRole());
            mockMap.Setup(m => m.Map<Role, RoleView>(It.IsAny<Role>())).Returns(GetTestRoleView());
            mockMap.Setup(m => m.Map<RoleView, Role>(It.IsAny<RoleView>())).Returns(GetTestRole());
            var Controller = new RoleController(mock.Object, mockMap.Object);

            // Act
            var result = Controller.CreateRole(GetTestRoleView());

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void DeleteRoleReturnTypeRoleView()
        {
            var mock = new Mock<IRole>();
            var mockMap = new Mock<IMapper>();
            var a = mockMap.Object;
            mock.Setup(i => i.DeleteRole(It.IsAny<Role>())).Returns(true);
            mockMap.Setup(m => m.Map<Role, RoleView>(It.IsAny<Role>())).Returns(GetTestRoleView());
            mockMap.Setup(m => m.Map<RoleView, Role>(It.IsAny<RoleView>())).Returns(GetTestRole());
            var Controller = new RoleController(mock.Object, mockMap.Object);

            // Act
            var result = Controller.DeleteRole(GetTestRoleView().Id);

            // Assert
            Assert.IsType<bool>(result);

        }

        [Fact]
        public void DeleteRoleReturnNotNull()
        {
            var mock = new Mock<IRole>();
            var mockMap = new Mock<IMapper>();
            var a = mockMap.Object;
            var perm = new Permission();
            mock.Setup(i => i.DeleteRole(It.IsAny<Role>())).Returns(true);
            mockMap.Setup(m => m.Map<Role, RoleView>(It.IsAny<Role>())).Returns(GetTestRoleView());
            mockMap.Setup(m => m.Map<RoleView, Role>(It.IsAny<RoleView>())).Returns(GetTestRole());
            var Controller = new RoleController(mock.Object, mockMap.Object);

            // Act
            var result = Controller.DeleteRole(GetTestRoleView().Id);

            // Assert
            Assert.NotNull(result);
        }

        private RoleView GetTestRoleView()
        {
            RoleView role = new RoleView { Id = 1, role = "test1" };
            return role;
        }

        private Role GetTestRole()
        {
            Role role = new Role { Id = 1, RoleName = "test1" };
            return role;
        }

        private List<Role> GetTestRoles()
        {
            var roles = new List<Role>
             {
                 new Role {Id=1, RoleName="test1"},
                 new Role {Id=2, RoleName="test2"},
                 new Role {Id=3, RoleName="test3"}
             };
            return roles;
        }
    }
}
