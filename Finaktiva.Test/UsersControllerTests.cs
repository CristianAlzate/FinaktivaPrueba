using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Finaktiva.Web.Controllers;
using Finaktiva.Test.Utils;
using Finaktiva.Services.ModelView;
using Microsoft.AspNetCore.Mvc;

namespace Finaktiva.Test
{
    public class UsersControllerTests
    {
        [Fact]
        public void ControllerCanNotReceiveNullArgument()
        {
            Assert.Throws<ArgumentNullException>(() => new UsersController(null));
        }

        [Fact]
        public void UserControllerGetMustReturnAListOfUsers()
        {
            UserServiceTest userServiceTest = new UserServiceTest();
            UsersController userController = new UsersController(userServiceTest);

            var result = userController.Get().Result;

            Assert.NotNull(result);
            var okResult = Assert.IsAssignableFrom<OkObjectResult>(result);
            Assert.IsType<List<UserModelView>>(okResult.Value);
        }

        [Fact]
        public void UserControllerGetMustReturnAUser()
        {
            UserServiceTest userServiceTest = new UserServiceTest();
            UsersController userController = new UsersController(userServiceTest);

            var result = userController.Get(1).Result;

            Assert.NotNull(result);
            var okResult = Assert.IsAssignableFrom<OkObjectResult>(result);
            Assert.IsType<UserModelView>(okResult.Value);
        }

        [Fact]
        public void UserControllerGetReturnNotFound()
        {
            UserServiceTest userServiceTest = new UserServiceTest();
            UsersController userController = new UsersController(userServiceTest);

            var result = userController.Get(2).Result;

            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void UserControllerPutReturnNoContent()
        {
            UserServiceTest userServiceTest = new UserServiceTest();
            UsersController userController = new UsersController(userServiceTest);

            var result = userController.Put(1,new UserModelView()).Result;

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void UserControllerPutReturnNotFound()
        {
            UserServiceTest userServiceTest = new UserServiceTest();
            UsersController userController = new UsersController(userServiceTest);

            var result = userController.Put(10, new UserModelView()).Result;

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
