using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;
using WebApi.Controller;

namespace RegisterLoginWebApi.UnitTest.ControllerTest.AuthController
{
    public abstract class BaseControllerTest
    {
        //BaseControllerTests sınıfını miras alan diğer test sınıfları içinde kullanılabilen metot
        //Test senaryolarında kimlik doğrulama işlemkerini simüle etmek için kullanılan metot

        // private TestController _testController;
        // private Mock<IMediator> _mediatorMock;
        //
        // [SetUp]
        // public void Setup()
        // {
        //     _mediatorMock = new Mock<IMediator>();
        //     _testController = new TestController(_mediatorMock.Object);
        // }

        // BaseControllerTests sınıfını miras alan diğer test sınıfları içinde kullanılabilen metot
        // Test senaryolarında kimlik doğrulama işlemlerini simüle etmek için kullanılan metot
        protected void SetAuthenticationContext(BaseController baseController)
        {
            //Claimden kullanıcı oluşturulur
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(
                    type:"userid",
                    value: Guid.NewGuid().ToString())
            }));

            // HttpContext, kimlik doğrulama ile BaseController nesnesi tarafından kullanılacak olan kimlik bilgisi sağlanmış olur.
            var httpContext = new DefaultHttpContext()
            {
                User = user,
                RequestServices = baseController.HttpContext.RequestServices 
            };

            // ControllerContext'i ayarlayın
            baseController.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };
        }
    }
}
