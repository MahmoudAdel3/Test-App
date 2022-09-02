using Backend.API.Controllers;
using Backend.Bll.DTOs;
using Backend.Bll.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Backend.Test.ControllersTests
{
    public class EmployeeControleerTest
    {
        private readonly EmployeeController _employeeController;
        private readonly Mock<IEmployeeService> _employeeServiceMock;
        public EmployeeControleerTest()
        {
            _employeeServiceMock = new Mock<IEmployeeService>();
            _employeeController = new EmployeeController(_employeeServiceMock.Object);
        }
        [Fact]
        public async void Get_ValidID_EmployeeDTOReturned()
        {
            _employeeServiceMock.Setup(x => x.GetAsync(1)).Returns(Task.FromResult(new EmployeeDTO()));
            var actionResult = await _employeeController.Get(1);
            var result = actionResult as OkObjectResult;
            Assert.IsType<OkObjectResult>(result);
            Assert.IsType<EmployeeDTO>(result.Value);
        }
        [Fact]
        public async void GetAll_ValidID_EmployeeDTOListReturned()
        {
            _employeeServiceMock.Setup(x => x.GetListAsync()).Returns(Task.FromResult(new List<EmployeeDTO>()));
            var actionResult = await _employeeController.Get();
            var result = actionResult as OkObjectResult;
            Assert.IsType<OkObjectResult>(result);
            Assert.IsType<List<EmployeeDTO>>(result.Value);
        }
        [Fact]
        public async void Post_ValidModel_AddedEmployeeReturned()
        {
            var model = new EmployeeDTO { Name = "test" };
            _employeeServiceMock.Setup(c => c.AddAsync(model)).Returns(Task.FromResult(model));
            var actionResult = await _employeeController.Post(model);
            var result = actionResult as OkObjectResult;
            Assert.IsType<OkObjectResult>(result);
            Assert.IsType<EmployeeDTO>(result.Value);
        }
        [Fact]
        public async void Put_ValidModel_UpdatedEmployeeReturned()
        {
            var model = new EmployeeDTO { ID = 1, Name = "test" };
            _employeeServiceMock.Setup(c => c.EditAsync(model)).Returns(Task.FromResult(model));
            var actionResult = await _employeeController.Put(model);
            var result = actionResult as OkObjectResult;
            Assert.IsType<OkObjectResult>(result);
            Assert.IsType<EmployeeDTO>(result.Value);
        }
        [Fact]
        public async void Delete_ValidID_NoReturn()
        {
            var actionResult = await _employeeController.Delete(1);
            Assert.IsType<OkResult>(actionResult);
        }

    }
}
