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
    public class UnitTestPermissionController
    {
        [Fact]
        public void GetPermissionByIdNotNull()
        {
            var mock = new Mock<IPermission>();
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = new Mapper(mockMapper);
            mock.Setup(i => i.GetPermissionById(1)).Returns(GetTestPermission());
            var Controller = new PermissionController(mock.Object, mapper);

            // Act
            var result = Controller.GetPermission(1);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void GetPermissionByIdReturnTypePermissionView()
        {
            var mock = new Mock<IPermission>();
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = new Mapper(mockMapper);
            mock.Setup(i => i.GetPermissionById(1)).Returns(GetTestPermission());
            var Controller = new PermissionController(mock.Object, mapper);

            // Act
            var result = Controller.GetPermission(1);

            // Assert
            Assert.IsType<PermissionView>(result);
        }

        [Fact]
        public void GetPermissionsNotNull()
        {
            var mock = new Mock<IPermission>();
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = new Mapper(mockMapper);
            mock.Setup(i => i.GetPermissions()).Returns(GetTestPermissions());
            var Controller = new PermissionController(mock.Object, mapper);

            // Act
            var result = Controller.GetAllPermission();

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void GetPermissionsReturnTypePermissionView()
        {
            var mock = new Mock<IPermission>();
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = new Mapper(mockMapper);
            mock.Setup(i => i.GetPermissions()).Returns(GetTestPermissions());
            var Controller = new PermissionController(mock.Object, mapper);

            // Act
            var result = Controller.GetAllPermission();

            // Assert
            Assert.IsType<PermissionView[]>(result);
        }

        [Fact]
        public void CreatePermissionsReturnTypePermissionView()
        {
            var mock = new Mock<IPermission>();
            var mockMap = new Mock<IMapper>();
            mock.Setup(i => i.CreatePermission(It.IsAny<Permission>())).Returns(GetTestPermission());
            mockMap.Setup(m => m.Map<Permission, PermissionView>(It.IsAny<Permission>())).Returns(GetTestPermissionView());
            mockMap.Setup(m => m.Map<PermissionView, Permission>(It.IsAny<PermissionView>())).Returns(GetTestPermission());
            var Controller = new PermissionController(mock.Object, mockMap.Object);

            // Act
            var result = Controller.CreatePermission(GetTestPermissionView());

            // Assert
            Assert.IsType<PermissionView>(result);

        }
        
        [Fact]
        public void CreatePermissionsReturnNotNull()
        {
            var mock = new Mock<IPermission>();
            var mockMap = new Mock<IMapper>();
            var a = mockMap.Object;
            var perm = new Permission();
            mock.Setup(i => i.CreatePermission(It.IsAny<Permission>())).Returns(GetTestPermission());
            mockMap.Setup(m => m.Map<Permission, PermissionView>(It.IsAny<Permission>())).Returns(GetTestPermissionView());
            mockMap.Setup(m => m.Map<PermissionView, Permission>(It.IsAny<PermissionView>())).Returns(GetTestPermission());
            var Controller = new PermissionController(mock.Object, mockMap.Object);

            // Act
            var result = Controller.CreatePermission(GetTestPermissionView());

            // Assert
            Assert.NotNull(result);

        }

        private PermissionView GetTestPermissionView()
        {
            PermissionView permission = new PermissionView { Id = 1, Name = "test1" };
            return permission;
        }
  
        private Permission GetTestPermission()
        {
            Permission permission = new Permission { Id = 1, PermissionName = "test1" };
            return permission;
        }

        private List<Permission> GetTestPermissions()
        {
            var permissions = new List<Permission>
             {
                 new Permission {Id=1, PermissionName="test1"},
                 new Permission {Id=2, PermissionName="test2"},
                 new Permission {Id=3, PermissionName="test3"}
             };
            return permissions;
        }
    }
}


/*
using AutoMapper; 
using Microsoft.EntityFrameworkCore; 
using Microsoft.Extensions.DependencyInjection; 
using Microsoft.VisualStudio.TestTools.UnitTesting; 
using Moq; 
using OxSports.Dal; 
using OxSports.Dal.Interfaces; 
using OxSports.Dtos.Models.Groups; 
using OxSports.Dtos.Models.ProgramTemplate; 
using OxSports.Dtos.Models.Roles; 
using OxSports.Dtos.Models.Search; 
using OxSports.Dtos.Models.TemplatePosition; 
using OxSports.Entities.Models; 
using OxSports.Services.Cache; 
using OxSports.Services.ImplementationAsync; 
using OxSports.Services.ImplementationAsync.Extensions; 
using OxSports.Services.InterfacesAsync; 
using OxSports.Tests.Core; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Linq.Expressions; 
using System.Threading.Tasks; 
 
namespace OxSports.Services.QZ.Tests.ServicesAsyncTests
{

    [TestClass]
    public class TemplateServiceTests : BaseAsyncTest
    {

        #region Variables 

        private TemplateService _service;
        private OxSportsDbContext _context;

        private Mock<ICustomRoleService> _customRoleService;
        private Mock<IGroupService> _groupService;
        private Mock<IPersonChatGroupService> _personChatGroupService;
        private Mock<ITemplatePositionService> _templatePositionService;
        private Mock<ITemplatePositionGroupService> _templatePositionGroupService;

        #endregion

        #region Initialize 

        [TestInitialize]
        public async Task TestInit()
        {
            await TestInitialization();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context?.Dispose();
        }

        #endregion

        #region Setup variables and data 

        protected override void SetupTestInjection(IServiceCollection serviceCollection)
        {
            serviceCollection
            .AddTestDbContext()
            .AddAutoMapper();

            _customRoleService = new Mock<ICustomRoleService>();
            _groupService = new Mock<IGroupService>();
            _personChatGroupService = new Mock<IPersonChatGroupService>();
            _templatePositionService = new Mock<ITemplatePositionService>();
            _templatePositionGroupService = new Mock<ITemplatePositionGroupService>();

            _customRoleService.Setup(x => x.GetListAsync(
            It.IsAny < Expression < Func < CustomRole, boolª> (),
            It.IsAny < IEnumerable < IOrderBy < CustomRoleª> (),
            It.IsAny<int?>(),
            It.IsAny<int?>()
            )).ReturnsAsync(new List<CustomRoleModel> { new CustomRoleModel { Id = 1 } });

            _customRoleService.Setup(x => x.SaveAsync(
            It.IsAny < Expression < Func < CustomRole, boolª> (),
            It.IsAny < List < CustomRoleModelª()
            )).ReturnsAsync((Expression < Func < CustomRole, boolª predicate, List < CustomRoleModel > models) => models);

            _groupService.Setup(x => x.UpdateAsync(
            It.IsAny < Expression < Func < Group, boolª> (),
            It.IsAny < Action < Group, DbContextª()
            )).ReturnsAsync(new List<GroupListModel>());

            _templatePositionService.Setup(x => x.GetListAsync(
            It.IsAny < Expression < Func < TemplatePosition, boolª> (),
            It.IsAny < IEnumerable < IOrderBy < TemplatePositionª> (),
            It.IsAny<int?>(),
            It.IsAny<int?>()
            )).ReturnsAsync(new List<TemplatePositionModel> { new TemplatePositionModel { Id = 1 } });

            _templatePositionService.Setup(x => x.DeleteAsync(
            It.IsAny < Expression < Func < TemplatePosition, boolª> ()
            )).ReturnsAsync(1);

            _templatePositionGroupService.Setup(x => x.DeleteAsync(
            It.IsAny < Expression < Func < TemplatePositionGroup, boolª> ()
            )).ReturnsAsync(1);

            serviceCollection
            .AddScoped(p => _customRoleService.Object)
            .AddScoped(p => _groupService.Object)
            .AddScoped(p => _personChatGroupService.Object)
            .AddScoped(p => _templatePositionService.Object)
            .AddScoped(p => _templatePositionGroupService.Object);
        }

        protected override async Task SetupTest()
        {
            ApplicationCache.InitializeForTests();
            _context = _serviceProvider.GetRequiredService<OxSportsDbContext>();
            await InitDataAsync();
            _service = new TemplateService(_serviceProvider);
        }

        #endregion

        #region Tests 

        #region Update 

        [TestMethod]
        public async Task Update_Ok()
        {
            var programTemplate = new TemplateModel
            {
                Id = 1,
                UpdateTime = 0,
                Name = "Name3",
                IsActive = true,
                DepthChartBackground = "DepthChartBackground3",
                TypeIcon = "TypeIcon3",
       CustomRoles = new List<CustomRoleModel>()
        };

        var result = await _service.UpdateAsync(programTemplate);

        Assert.IsNotNull(result); 
 Assert.AreEqual(programTemplate.Id, result.Id); 
 Assert.AreEqual(programTemplate.Name, result.Name); 
 Assert.AreEqual(programTemplate.DepthChartBackground, result.DepthChartBackground); 
 Assert.AreEqual(programTemplate.TypeIcon, result.TypeIcon); 
 _customRoleService.Verify(x => x.SaveAsync(
 It.IsAny<Expression<Func<CustomRole, boolª>(), 
 It.IsAny<List<CustomRoleModelª() 
 ), Times.Never); 
 }

    [TestMethod]
    public async Task Update_WithDressItems_Ok()
    {
        var programTemplate = new TemplateModel
        {
            Id = 1,
            UpdateTime = 0,
            Name = "Name3",
            IsActive = true,
            DepthChartBackground = "DepthChartBackground3",
            TypeIcon = "TypeIcon3",
            CustomRoles = new List<CustomRoleModel>()
        };

        var result = await _service.UpdateAsync(programTemplate);

        Assert.IsNotNull(result);
        Assert.AreEqual(programTemplate.Id, result.Id);
        Assert.AreEqual(programTemplate.Name, result.Name);
        Assert.AreEqual(programTemplate.DepthChartBackground, result.DepthChartBackground);
        Assert.AreEqual(programTemplate.TypeIcon, result.TypeIcon);
        _customRoleService.Verify(x => x.SaveAsync(
        It.IsAny < Expression < Func < CustomRole, boolª> (),
        It.IsAny < List < CustomRoleModelª()
        ), Times.Never);
    }

    [TestMethod]
    public async Task Update_WithCustomRoles_Ok()
    {
        var roles = new List<CustomRoleModel> {
 new CustomRoleModel {
 Enabled = false,
 BasicRoleId = 1,
 Name = "coaches",
 TemplateId = 2
 },
 new CustomRoleModel {
 Enabled = false,
 BasicRoleId = 2,
 Name = "athletes",
 TemplateId = 2
 },
 new CustomRoleModel {
 Enabled = false,
 BasicRoleId = 3,
 Name = "guests",
 TemplateId = 2
 }
 };
        var programTemplate = new TemplateModel
        {
            Id = 1,
            UpdateTime = 0,
            Name = "Name3",
            IsActive = true,
            DepthChartBackground = "DepthChartBackground3",
            TypeIcon = "TypeIcon3",
            CustomRoles = roles,
        };

        var result = await _service.UpdateAsync(programTemplate);

        Assert.AreEqual(programTemplate.Id, result.Id);
        Assert.AreEqual(programTemplate.Name, result.Name);
        Assert.AreEqual(programTemplate.DepthChartBackground, result.DepthChartBackground);
        Assert.AreEqual(programTemplate.TypeIcon, result.TypeIcon);
        _customRoleService.Verify(x => x.SaveAsync(
        It.IsAny < Expression < Func < CustomRole, boolª> (),
        roles
        ), Times.Once);
    }

    #endregion

    #region Delete 

    [TestMethod]
    public async Task Delete_Ok()
    {
        var result = await _service.DeleteAsync(x => x.Id == 1);

        Assert.IsNotNull(result);
        Assert.AreEqual(1, result);
    }

    #endregion

    #region Duplicate 

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public async Task Duplicate_Null()
    {
        await _service.DuplicateAsync(null);
    }

    [TestMethod]
    public async Task Duplicate_Ok()
    {
        var programTemplate = new TemplateModel
        {
            Id = 1,
            Name = "Name1",
            IsActive = true,
            DepthChartBackground = "DepthChartBackground1",
            TypeIcon = "TypeIcon1",
            CustomRoles = new List<CustomRoleModel> {
 new CustomRoleModel {
 Id = 123
 },
 new CustomRoleModel {
 Id = 124
 }
 }
        };

        var result = await _service.DuplicateAsync(programTemplate);

        Assert.IsNotNull(result);
        Assert.AreEqual(3, result.Id);
        Assert.AreEqual($"{programTemplate.Name} (copy)", result.Name);
        Assert.AreEqual(programTemplate.DepthChartBackground, result.DepthChartBackground);
        Assert.AreEqual(programTemplate.TypeIcon, result.TypeIcon);
    }

    #endregion

    #region SearchPredicate 

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public async Task SearchPredicate_Null()
    {
        var result = await _service.GetListAsync(_service.SearchPredicate(null));
    }

    [TestMethod]
    public async Task SearchPredicate_Any()
    {
        var result = await _service.GetListAsync(_service.SearchPredicate(new SearchModel
        {
            Query = "Any"
        }));

        Assert.IsNotNull(result);
        Assert.AreEqual(2, result.Count);
        Assert.IsTrue(result.Any(x => x.IsActive));
        Assert.IsTrue(result.Any
       (x => !x.IsActive));
    }

    //œ‡‚ÂÎ Ã‡ÌÍÂÎÂ‚Ë˜, ÒÂ„Ó‰Ìˇ ‚ 12:12
[TestMethod]
    public async Task SearchPredicate_Public()
    {
        var result = await _service.GetListAsync(_service.SearchPredicate(new SearchModel
        {
            Query = "Public"
        }));

        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.Count);
        Assert.IsTrue(result[0].IsActive);
    }

    [TestMethod]
    public async Task SearchPredicate_Private()
    {
        var result = await _service.GetListAsync(_service.SearchPredicate(new SearchModel
        {
            Query = "Private"
        }));

        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.Count);
        Assert.IsFalse(result[0].IsActive);
    }

    #endregion

    #endregion

    #region Init context and models 

    private async Task InitDataAsync()
    {
        var options = _serviceProvider.GetRequiredService < DbContextOptions < OxSportsDbContextª();
        using (var context = new OxSportsDbContext(options))
        {
            await context.AddAsync(new Template
            {
                Name = "Name1",
                IsActive = true,
                DepthChartBackground = "DepthChartBackground1",
                TypeIcon = "TypeIcon1"
            });
            await context.AddAsync(new Template
            {
                Name = "Name2",
                IsActive = false,
                DepthChartBackground = "DepthChartBackground2",
                TypeIcon = "TypeIcon2"
            });
            await context.SaveChangesAsync();
        }
    }

    #endregion
} 
}

//œ‡‚ÂÎ Ã‡ÌÍÂÎÂ‚Ë˜, ÒÂ„Ó‰Ìˇ ‚ 12:14
namespace OxSports.Application.Tests.ControllersTests
{
    [TestClass]
    public class FileSystemEntryControllerTest : ViewerControllerTests<FileSystemEntryController>
    {

        #region Variables 

        private readonly int _personId = 123;
        private readonly int _person1Id = 1;
        private readonly int _programId = 10;

        #endregion

        #region Test init 

        [TestInitialize]
        public void InitTest()
        {
            TestInitialization();
        }

        #endregion

        #region Test setup 

        protected override void SetupTestInjection(IServiceCollection serviceCollection)
        {
            var service = new Mock<IFileEntryService>();
            service.Setup(x => x.GetSingleAsync(
            It.IsAny < Expression < Func < FileSystemEntry, boolª> (),
            It.IsAny < IEnumerable < IOrderBy < FileSystemEntryª> ()
            )).ReturnsAsync(new FileSystemEntryModel() { Id = 1 });
            service.Setup(x => x.GetSingleAsync(
            It.IsAny < Expression < Func < FileSystemEntry, boolª> (),
            It.IsAny < Expression < Func < FileSystemEntry, int ?ª> (),
            It.IsAny < IEnumerable < IOrderBy < FileSystemEntryª> ()
            )).ReturnsAsync(1);
            service.Setup(x => x.CreateFolderAsync(It.IsAny<FileSystemEntryModel>()))
            .ReturnsAsync((FileSystemEntryModel model) => model);
            service.Setup(x => x.UpdateAsync(
            It.IsAny < Expression < Func < FileSystemEntry, boolª> (),
            It.IsAny < Action < FileSystemEntry, DbContextª()
            )).ReturnsAsync(new List<FileSystemEntryModel> { new FileSystemEntryModel() });
            service.Setup(x => x.AnyAsync(It.IsAny < Expression < Func < FileSystemEntry, boolª> ())).ReturnsAsync(true);
            service.Setup(x => x.DeleteAsync(It.IsAny < Expression < Func < FileSystemEntry, boolª> ()))
            .ReturnsAsync(1);

            service.Setup(x => x.UpdateAsync(It.IsAny < List < FileSystemEntryModelª(), It.IsAny<int>(),
            It.IsAny<FileActionType>())).ReturnsAsync(new List<FileSystemEntryModel>());

            var operationService = new Mock<ICrudService<FileOperation, FileOperationModelª();
            operationService.Setup(x => x.CreateAsync(It.IsAny<FileOperationModel>()))
            .ReturnsAsync(new FileOperationModel() { Id = 1 });
            operationService.Setup(x => x.UpdateAsync(It.IsAny<FileOperationModel>()))
            .ReturnsAsync(new FileOperationModel() { Id = 1 });

            var fileEntryGroupService = new Mock<IGetService<FileEntryGroupª();
            var fileEntryPersonService = new Mock<IGetService<FileEntryPersonª();

            _services.AddScoped(p => service.Object);
            _services.AddScoped(p => operationService.Object);
            _services.AddScoped(p => fileEntryGroupService.Object);
            _services.AddScoped(p => fileEntryPersonService.Object);
        }

        protected override void SetupTest()
        {
            Controller = new FileSystemEntryController(_serviceProvider);
        }

        #endregion

        #region Tests 

        [TestMethod]
        public async Task Get()
        {
            var result = await Controller.Get(1);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual((int)HttpStatusCode.OK, (result as OkObjectResult)?.StatusCode);
            Assert.IsInstanceOfType((result as OkObjectResult)?.Value, typeof(FileSystemEntryModel));
            Assert.AreEqual(1, ((result as OkObjectResult)?.Value as FileSystemEntryModel)?.Id);
        }

        #region Create Tests 

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedAccessException))]
        public async Task Create_UnauthorizedAccess()
        {
            var model = new FileSystemEntryModel { Id = 1 };
            await Controller.Create(model);
        }

        [TestMethod]
        public async Task Create()
        {
            var principal = CreatePrincipal(_personId, _programId, Roles.SysAdmin);
            SetControllerUser(principal);

            var model = new FileSystemEntryModel { Id = 1 };
            var result = await Controller.Create(model);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual((int)HttpStatusCode.OK, (result as OkObjectResult)?.StatusCode);
            Assert.IsInstanceOfType((result as OkObjectResult)?.Value, typeof(FileSystemEntryModel));
        }

        #endregion

        #region Update Tests 

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedAccessException))]
        public async Task Update_UnauthorizedAccess()
        {
            var model = new List<FileSystemEntryModel>() { new FileSystemEntryModel { Id = 1 } };
            await

           œ‡‚ÂÎ Ã‡ÌÍÂÎÂ‚Ë˜, ÒÂ„Ó‰Ìˇ ‚ 12:14
       Controller.Update(model);
        }

        [TestMethod]
        public async Task Update()
        {
            var principal = CreatePrincipal(_personId, _programId, Roles.SysAdmin);
            SetControllerUser(principal);

            var model = new List<FileSystemEntryModel> { new FileSystemEntryModel { Id = 1 } };
            var result = await Controller.Update(model);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual((int)HttpStatusCode.OK, (result as OkObjectResult)?.StatusCode);
            Assert.IsInstanceOfType((result as OkObjectResult)?.Value, typeof(List<FileSystemEntryModel>));
        }

        #endregion

        [TestMethod]
        public async Task Delete()
        {
            var principal = CreatePrincipal(_personId, _programId, Roles.SysAdmin);
            SetControllerUser(principal);

            var ids = new List<int> { 1 };
            var result = await Controller.Delete(ids);

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.OK, (result as OkObjectResult)?.StatusCode);
            Assert.IsInstanceOfType((result as OkObjectResult)?.Value, typeof(FileOperationModel));
            var resultModel = (result as OkObjectResult)?.Value as FileOperationModel;
            Assert.IsNotNull(resultModel);
            Assert.AreEqual(resultModel.Id, 1);
        }

        #region DeleteNoOperation Tests 

        [TestMethod]
        public void DeleteNoOperation()
        {
            var principal = CreatePrincipal(_personId, _programId, Roles.SysAdmin);
            SetControllerUser(principal);

            var ids = new List<int>() { 1 };
            var result = Controller.DeleteNoOperation(ids);

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.NoContent, (result as NoContentResult)?.StatusCode);
        }

        #endregion

        #endregion
    }
    */