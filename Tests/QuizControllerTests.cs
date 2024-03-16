using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Web_App.Server.Controllers;
using Web_App.Server.Handlers.Quiz;

namespace Tests;

[TestClass]
public class QuizControllerTests
{
    private static readonly Mock<IMediator> mediatorMock = new();

    [TestMethod]
    public async Task TestMethod1()
    {
        //Arrange        
        var initializeQuizCommand = new InitializeQuizCommand
        {
            PlayerName = "John Doe",
            NumberOfQuestions = 10,
        };        

        mediatorMock.Setup(m => m.Send(
            It.Is<InitializeQuizCommand>(x => 
            x.PlayerName == "John Doe" && x.NumberOfQuestions == 10),
            CancellationToken.None))
            .ReturnsAsync(new InitializeQuizCommandResponse
            {
                QuizInitializedDetails = ["Quiz has been initialized successfully for player John Doe."],
                Success = true,
                ErrorMessage = null,
            });

        var controller = new QuizController(mediatorMock.Object);

        //Act
        var result = await controller.InitializeQuiz("John Doe", 10);
        mediatorMock.Verify(m => m.Send(
            It.Is<InitializeQuizCommand>(x =>
            x.PlayerName == "John Doe" && x.NumberOfQuestions == 10),
            CancellationToken.None));

        ObjectResult objectResult = (ObjectResult)result;

        //Assert
        Assert.IsNotNull(result);
        Assert.IsTrue(objectResult.StatusCode == 200);
        Assert.AreEqual(
            "Quiz has been initialized successfully for player John Doe.",
            (objectResult.Value as List<string>)?[0]);
    }
}