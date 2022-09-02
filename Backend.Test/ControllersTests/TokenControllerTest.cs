using Backend.API.Controllers;
using Backend.Bll.Services;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Backend.Test.ControllersTests
{
    public class TokenControllerTest
    {
        private readonly TokenController _tokenController;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly Mock<IUserService> _userServiceMock;
        public TokenControllerTest()
        {
            _configurationMock = new Mock<IConfiguration>();
            _userServiceMock = new Mock<IUserService>();
            _tokenController = new TokenController(_userServiceMock.Object, _configurationMock.Object);
        }
        
        [Fact]
        public async void Post_ValidLogin_TokenGenerated()
        {
            var model = new UserModel
            {
                UserName = "test@test.com",
                Password = "123"
            };
            _userServiceMock.Setup(x => x.GetUserAsync(model.UserName, model.Password)).Returns(Task.FromResult(new Bll.DTOs.UserDTO { UserName = "test" }));
            _configurationMock.Setup(x => x[It.IsAny<string>()]).Returns("thisismytasktestkey");
            var actionResult = await _tokenController.Post(model);
            Assert.IsType<OkObjectResult>(actionResult as OkObjectResult);
        }
        [Fact]
        public async void Post_InValidLogin_BadRequest()
        {
            var model = new UserModel
            {
                UserName = "test@test.com",
                Password = "123"
            };
            _userServiceMock.Setup(x => x.GetUserAsync(model.UserName, model.Password)).Returns(Task.FromResult((Bll.DTOs.UserDTO)null));
            var actionResult = await _tokenController.Post(model);
            Assert.IsType<BadRequestObjectResult>(actionResult as BadRequestObjectResult);
        }
        [Fact]
        public async void Post_InvalidModelState_BadRequest()
        {
            var model = new UserModel
            {
                UserName = "test@test.com"
            };
            var actionResult = await _tokenController.Post(model);
            Assert.IsType<BadRequestObjectResult>(actionResult as BadRequestObjectResult);
        }
    }
}
