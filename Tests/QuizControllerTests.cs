using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Web_App.Server.Controllers;
using Web_App.Server.Handlers.Questions;
using Web_App.Server.Handlers.Quiz;
using Web_App.Server.Models;

namespace Tests;

[TestClass]
public class QuizControllerTests
{
    private static readonly Mock<IMediator> mediatorMock = new();

    [TestMethod]
    public async Task InitializeQuizSuccessTest()
    {
        //Arrange        
        var command = new InitializeQuizCommand
        {
            PlayerName = "John Doe",
            NumberOfQuestions = 10,
        };
        string successfulInitializationString = "Quiz has been initialized successfully for player " + command.PlayerName + ".";

        var controller = new QuizController(mediatorMock.Object);

        mediatorMock.Setup(m => m.Send(
            It.Is<InitializeQuizCommand>(x =>
            x.PlayerName == command.PlayerName && x.NumberOfQuestions == command.NumberOfQuestions),
            CancellationToken.None))
            .ReturnsAsync(new InitializeQuizCommandResponse
            {
                QuizInitializedDetails = [successfulInitializationString],
                Success = true,
                ErrorMessage = null,
            });

        //Act
        var result = await controller.InitializeQuiz(command.PlayerName, command.NumberOfQuestions);
        ObjectResult objectResult = (ObjectResult)result;

        mediatorMock.Verify(m => m.Send(
            It.Is<InitializeQuizCommand>(x =>
            x.PlayerName == command.PlayerName && x.NumberOfQuestions == command.NumberOfQuestions),
            CancellationToken.None));

        //Assert
        Assert.IsNotNull(result);
        Assert.IsTrue(objectResult.StatusCode == 200);
        Assert.AreEqual(
            successfulInitializationString,
            (objectResult.Value as List<string>)?[0]);
    }

    [TestMethod]
    public async Task InitializeQuizNoNameTest()
    {
        //Arrange
        var command = new InitializeQuizCommand
        {
            PlayerName = null,
            NumberOfQuestions = 1,
        };

        var controller = new QuizController(mediatorMock.Object);

        mediatorMock.Setup(m => m.Send(
            It.Is<InitializeQuizCommand>(x =>
            x.PlayerName == command.PlayerName && x.NumberOfQuestions == command.NumberOfQuestions),
            CancellationToken.None))
            .ReturnsAsync(new InitializeQuizCommandResponse
            {
                QuizInitializedDetails = new List<string>(),
                Success = false,
                ErrorMessage = null,
            });

        //Act
        var result = await controller.InitializeQuiz(command.PlayerName, command.NumberOfQuestions);
        ObjectResult objectResult = (ObjectResult)result;

        mediatorMock.Verify(m => m.Send(
            It.Is<InitializeQuizCommand>(x =>
            x.PlayerName == command.PlayerName && x.NumberOfQuestions == command.NumberOfQuestions),
            CancellationToken.None));

        //Assert
        Assert.IsNotNull(objectResult);
        Assert.AreEqual(objectResult.StatusCode, 500);
        Assert.AreEqual(
            null,
            (objectResult.Value as List<string>)?[0]);
    }

    [TestMethod]
    public async Task InitializeQuizNoNumberOfQuestionsTest()
    {
        //Arrange
        var command = new InitializeQuizCommand
        {
            PlayerName = "John Doe",
            NumberOfQuestions = 0,
        };

        var controller = new QuizController(mediatorMock.Object);

        mediatorMock.Setup(m => m.Send(
            It.Is<InitializeQuizCommand>(x =>
            x.PlayerName == command.PlayerName && x.NumberOfQuestions == command.NumberOfQuestions),
            CancellationToken.None))
            .ReturnsAsync(new InitializeQuizCommandResponse
            {
                QuizInitializedDetails = new List<string>(),
                Success = false,
                ErrorMessage = null,
            });

        //Act
        var result = await controller.InitializeQuiz(command.PlayerName, command.NumberOfQuestions);
        ObjectResult objectResult = (ObjectResult)result;

        mediatorMock.Verify(m => m.Send(
            It.Is<InitializeQuizCommand>(x =>
            x.PlayerName == command.PlayerName && x.NumberOfQuestions == command.NumberOfQuestions),
            CancellationToken.None));

        //Assert
        Assert.IsNotNull(objectResult);
        Assert.IsTrue(objectResult.StatusCode == 500);
        Assert.AreEqual(
            null,
            (objectResult.Value as List<string>)?[0]);
    }

    [TestMethod]
    public async Task GetAllQuestionsSuccessTest()
    {
        //Arrange
        var query = new GetAllQuestionsQuery();

        var controller = new QuizController(mediatorMock.Object);

        var questionCardMock = new QuestionCardModel
        {
            QuestionId = 1,
            QuestionType = "QuestionCard",
            QuestionText = "Test",
            RequiredWords = "Test",
        };

        var mcsaCardMock = new MCSACardModel
        {
            QuestionId = 2,
            QuestionType = "MCSACard",
            QuestionText = "Test",
            Option1 = "Test1",
            Option2 = "Test2",
            Option3 = "Test3",
            Option4 = "Test4",
            Option5 = "Test5",
            CorrectOptionNumber = 1,
        };

        mediatorMock.Setup(m => m.Send(
            It.IsAny<GetAllQuestionsQuery>(), CancellationToken.None))
            .ReturnsAsync(new GetAllQuestionsQueryResponse
            {
                Questions =
                [
                    questionCardMock,
                    mcsaCardMock,
                ],
                Success = true,
                ErrorMessage = null,
            });

        //Act
        var result = await controller.GetAllQuestions();
        var objectResult = result.Result as ObjectResult;

        var questionCard = (objectResult.Value as List<QuestionModel>).OfType<QuestionCardModel>().ToList()[0];
        var mcsaCard = (objectResult.Value as List<QuestionModel>).OfType<MCSACardModel>().ToList()[0];

        mediatorMock.Verify(m => m.Send(
            It.IsAny<GetAllQuestionsQuery>(), CancellationToken.None));

        //Assert
        Assert.AreEqual(200, objectResult.StatusCode);
        Assert.IsNotNull(objectResult);
        Assert.IsNotNull(questionCard);
        Assert.IsNotNull(mcsaCard);

        Assert.AreEqual(questionCard.QuestionId, questionCardMock.QuestionId);
        Assert.AreEqual(questionCard.QuestionType, questionCardMock.QuestionType);
        Assert.AreEqual(questionCard.QuestionText, questionCardMock.QuestionText);
        Assert.AreEqual(questionCard.RequiredWords, questionCardMock.RequiredWords);

        Assert.AreEqual(mcsaCard.QuestionId, mcsaCardMock.QuestionId);
        Assert.AreEqual(mcsaCard.QuestionType, mcsaCardMock.QuestionType);
        Assert.AreEqual(mcsaCard.Option1, mcsaCardMock.Option1);
        Assert.AreEqual(mcsaCard.Option2, mcsaCardMock.Option2);
        Assert.AreEqual(mcsaCard.Option3, mcsaCardMock.Option3);
        Assert.AreEqual(mcsaCard.Option4, mcsaCardMock.Option4);
        Assert.AreEqual(mcsaCard.Option5, mcsaCardMock.Option5);
        Assert.AreEqual(mcsaCard.CorrectOptionNumber, mcsaCardMock.CorrectOptionNumber);
    }
}