using Application.Features.Auths.Commands;
using Core.Security.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RegisterLoginWebApi.UnitTest.ControllerTest.AuthController;

namespace RegisterLoginWebApi.UnitTest.ControllerTest
{
    public class AuthControllerTest : BaseControllerTest
    {
        //Register metotu beklenen giriş parametreleriyle çağrılıyor mu
        //doğru sonucu dönüyor mu
        //sonuç beklenen sonuçla eşleşiyor mu test edelim:
        [Test]
        public async Task CreateUser_ReturnsCreated_WhenRegister()
        {
            // Arrange//Hazırlık
            var registerCommand = new RegisterCommand()
            {
                NameSurname = String.Empty,
                Username = String.Empty,
                Email = string.Empty,
                Password = String.Empty,
                PhoneNumber = String.Empty,
            };
            UserForRegisterDto createdUser = null;

            //mock ile IMediatr interfaceinden sahte bir nesne oluştururz
            //Moq kütüphanesinin It classındaki IsAny() metotunu çağırırız
            //"IsAny" metotuna herhangi bir "RegisterCommand" parametresi girilidiğinde Returns içine yazılan dönecek. 

            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(x => x.Send(It.IsAny<RegisterCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((RegisterCommand command, CancellationToken cancellationToken) =>
                    createdUser = new UserForRegisterDto()
                    {
                        NameSurname = command.NameSurname,
                        Username = command.Username,
                        Email = command.Email,
                        Password = command.Password,
                        PhoneNumber = command.PhoneNumber
                    });

            var controller = new WebApi.Controller.AuthController(mockMediator.Object);

            //Act//Eylem
            var result = await controller.Register(registerCommand) as CreatedResult;

            //Assert//Doğrulama
            mockMediator.Verify(x =>
                x.Send(It.Is<RegisterCommand>(r => r == registerCommand), default(CancellationToken)), Times.Once);
            Assert.NotNull(result);
            Assert.IsAssignableFrom<CreatedResult>(result);
            Assert.AreEqual(createdUser, result?.Value);
            
        }
    }
}


//
// [SetUp]
// public void Setup(object mockMediator)
// {
//     _mediatorMock =new Mock<IMediator>();
//     _authController = new WebApi.Controller.AuthController(_mediatorMock.Object);
// }
//
// [Test]
// public async Task Register_ShouldCreatedOk()
// {
//     // Arrange
//    
//     var registerCommand = new RegisterCommand()
//     {
//         NameSurname =" Esra Ya��n",
//         Username = "esra",
//         Email = "yasnesraa@gmail.com",
//         Password = "Esra123",
//         PhoneNumber ="5343337788"
//     };
//
//     var expectedUserForRegisterDto = new UserForRegisterDto()
//     {
//         NameSurname = " Esra Ya��n",
//         Username = "esra",
//         Email = "yasnesraa@gmail.com",
//         Password = "Esra123",
//         PhoneNumber = "5343337788"
//     };
//
//     // IMediator mock nesnesini kullanarak MediatR i�lemini ayarlay�n
//    // _authController.Mediator() = new Mock<IMediator>().Object;
//         _authController.Mediator
//         .Setup(x => x.Send(It.IsAny<RegisterCommand>(), default(CancellationToken)))
//         .ReturnsAsync(expectedUserForRegisterDto);
//
//     // Act
//
//     var result = await _authController.Register(registerCommand) as CreatedResult;
//
//
//     // Assert
//     Assert.IsNotNull(result);
//     Assert.AreEqual(201, result.StatusCode);
//     Assert.AreEqual("", result.Location); // Update with the expected location header value
//     Assert.AreSame(expectedUserForRegisterDto, result.Value);
//
// }