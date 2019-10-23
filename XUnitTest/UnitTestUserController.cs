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
    public class UnitTestUserController
    {
        [Fact]
        public void GetUserByIdNotNull()
        {
            var mock = new Mock<IUser>();
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = new Mapper(mockMapper);
            mock.Setup(i => i.GetUserById(1)).Returns(GetTestUser());
            var Controller = new UserController(mock.Object, mapper);

            // Act
            var result = Controller.GetUser(1);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void GetPermissionByIdReturnTypePermissionView()
        {
            var mock = new Mock<IUser>();
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = new Mapper(mockMapper);
            mock.Setup(i => i.GetUserById(1)).Returns(GetTestUser());
            var Controller = new UserController(mock.Object, mapper);

            // Act
            var result = Controller.GetUser(1);

            // Assert
            Assert.IsType<UserView>(result);
        }

        [Fact]
        public void GetUsersNotNull()
        {
            var mock = new Mock<IUser>();
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = new Mapper(mockMapper);
            mock.Setup(i => i.GetUsers()).Returns(GetTestUsers());
            var Controller = new UserController(mock.Object, mapper);

            // Act
            var result = Controller.GetAllUser();

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void GetUsersReturnTypeUserView()
        {
            var mock = new Mock<IUser>();
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = new Mapper(mockMapper);
            mock.Setup(i => i.GetUsers()).Returns(GetTestUsers());
            var Controller = new UserController(mock.Object, mapper);

            // Act
            var result = Controller.GetAllUser();

            // Assert
            Assert.IsType<UserView[]>(result);
        }

        [Fact]
        public void CreateUserReturnTypeUserView()
        {
            var mock = new Mock<IUser>();
            var mockMap = new Mock<IMapper>();
            var a = mockMap.Object;
            mock.Setup(i => i.CreateUser(It.IsAny<User>())).Returns(GetTestUser());
            mockMap.Setup(m => m.Map<User, UserView>(It.IsAny<User>())).Returns(GetTestUserView());
            mockMap.Setup(m => m.Map<UserView, User>(It.IsAny<UserView>())).Returns(GetTestUser());
            var Controller = new UserController(mock.Object, mockMap.Object);

            // Act
            var result = Controller.CreateUser(GetTestUserView());

            // Assert
            Assert.IsType<UserView>(result);

        }

        [Fact]
        public void CreateUserReturnNotNull()
        {
            var mock = new Mock<IUser>();
            var mockMap = new Mock<IMapper>();
            var a = mockMap.Object;
            mock.Setup(i => i.CreateUser(It.IsAny<User>())).Returns(GetTestUser());
            mockMap.Setup(m => m.Map<User, UserView>(It.IsAny<User>())).Returns(GetTestUserView());
            mockMap.Setup(m => m.Map<UserView, User>(It.IsAny<UserView>())).Returns(GetTestUser());
            var Controller = new UserController(mock.Object, mockMap.Object);

            // Act
            var result = Controller.CreateUser(GetTestUserView());

            // Assert
            Assert.NotNull(result);

        }

        private UserView GetTestUserView()
        {
            UserView user = new UserView { Id = 1, FirstName = "test1", LastName = "test2", RoleId = 2, Role = new RoleView{ Id = 2, role = "test1" } };
            return user;
        }

        private User GetTestUser()
        {
            User user = new User { Id = 1, FirstName = "test1", LastName= "test2", RoleId = 2, Role = new Role{ Id = 2, RoleName = "test1" } };
            return user;
        }

        private List<User> GetTestUsers()
        {
            var users = new List<User>
             {
                 new User {Id = 1, FirstName = "test1", LastName= "test2", RoleId = 2, Role = new Role{ Id = 2, RoleName = "test1" }},
                 new User {Id = 2, FirstName = "test2", LastName= "test3", RoleId = 2, Role = new Role{ Id = 2, RoleName = "test1" }},
                 new User {Id = 3, FirstName = "test3", LastName= "test4", RoleId = 2, Role = new Role{ Id = 2, RoleName = "test1" }}
             };
            return users;
        }
    }
}
