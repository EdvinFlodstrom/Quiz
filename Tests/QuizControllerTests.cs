using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Web_App.Server.Controllers;
using Web_App.Server.DTOs;
using Web_App.Server.Handlers.Questions;
using Web_App.Server.Handlers.Quiz;
using Web_App.Server.Models;

namespace Tests;

[TestClass]
public class QuizControllerTests
{
    private static readonly Mock<IMediator> mediatorMock = new();

    [TestMethod]
    public async Task InitializeQuizTest_Successful()
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
    public async Task InitializeQuizTest_NoName()
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
    public async Task InitializeQuizTest_NoNumberOfQuestions()
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
    public async Task GetAllQuestionsTest_Successful()
    {
        //Arrange
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
            It.IsAny<GetAllQuestionsQuery>(),
            CancellationToken.None))
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

    [TestMethod]
    public async Task GetAllQuestionsTest_Fail()
    {
        //Arrange
        var controller = new QuizController(mediatorMock.Object);
        mediatorMock.Setup(m => m.Send(
            It.IsAny<GetAllQuestionsQuery>(),
            CancellationToken.None)
            ).ReturnsAsync(new GetAllQuestionsQueryResponse
            {
                Questions = null,
                Success = false,
                ErrorMessage = null,
            });

        //Act
        var response = await controller.GetAllQuestions();
        var objectResult = response.Result as ObjectResult;

        //Assert
        Assert.IsNotNull(objectResult);
        Assert.AreEqual(500, objectResult.StatusCode);
    }

    [TestMethod]
    public async Task GetQuestionWithoutAnswerTest_QuestionsLeft_Successful()
    {
        //Arrange
        var questionCardMock = new QuestionDto
        {
            QuestionType = "QuestionCard",
            QuestionText = "Test",
        };

        var request = new GetQuestionCommand
        {
            PlayerName = "John Doe",
        };

        var controller = new QuizController(mediatorMock.Object);

        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext()
        };

        mediatorMock.Setup(m => m.Send(
            It.IsAny<GetQuestionCommand>(),
            CancellationToken.None))
            .ReturnsAsync(new GetQuestionCommandResponse
            {
                Question = new QuestionDto
                {
                    QuestionType = "QuestionCard",
                    QuestionText = "Test",
                },
                AnswerMessage = null,
                Success = true,
                ErrorMessage = null,
            });

        //Act
        var response = await controller.GetQuestionWithoutAnswer(request.PlayerName);
        var objectResult = response.Result as ObjectResult;
        var questionCard = objectResult.Value as QuestionDto;

        //Assert
        Assert.IsNotNull(objectResult);
        Assert.AreEqual(200, objectResult.StatusCode);
        Assert.IsTrue(questionCard is QuestionDto);
        Assert.AreEqual(questionCardMock.QuestionType, questionCard.QuestionType);
        Assert.AreEqual(questionCardMock.QuestionText, questionCard.QuestionText);
    }

    [TestMethod]
    public async Task GetQuestionWithoutAnswerTest_NoQuestionsLeft_Success()
    {
        //Arrange
        var request = new GetQuestionCommand
        {
            PlayerName = "John Doe",
        };

        var controller = new QuizController(mediatorMock.Object);

        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext()
        };

        mediatorMock.Setup(m => m.Send(
            It.IsAny<GetQuestionCommand>(),
            CancellationToken.None))
            .ReturnsAsync(new GetQuestionCommandResponse
            {
                Question = null,
                AnswerMessage = "You have answered all your questions.",
                Success = false,
                ErrorMessage = null,
            });

        //Act
        var response = await controller.GetQuestionWithoutAnswer(request.PlayerName);
        var objectResult = response.Result as ObjectResult;

        //Assert
        Assert.IsNotNull(objectResult);
        Assert.AreEqual(200, objectResult.StatusCode);
        Assert.IsFalse((bool)objectResult.Value);
    }

    [TestMethod]
    public async Task GetQuestionWithoutAnswerTest_Fail()
    {
        //Arrange
        var GetQuestionCommand = new GetQuestionCommand
        {
            PlayerName = "John Doe",
        };

        var controller = new QuizController(mediatorMock.Object);

        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext()
        };

        mediatorMock.Setup(m => m.Send(
            It.IsAny<GetQuestionCommand>(),
            CancellationToken.None))
            .ReturnsAsync(new GetQuestionCommandResponse
            {
                Question = null,
                AnswerMessage = null,
                Success = false,
                ErrorMessage = "Internal server error",
            });

        //Act
        var response = await controller.GetQuestionWithoutAnswer(GetQuestionCommand.PlayerName);
        var objectResult = response.Result as ObjectResult;
        var questionMessage = objectResult.Value as string;

        //Assert
        Assert.IsNotNull(objectResult);
        Assert.AreEqual(500, objectResult.StatusCode);
        Assert.AreEqual("Internal server error", questionMessage);
    }

    [TestMethod]
    public async Task CheckQuestionAnswerTest_Correct_Successful()
    {
        //Arrange
        var request = new CheckAnswerCommand
        {
            PlayerName = "John Doe",
            PlayerAnswer = "Test",
        };

        var controller = new QuizController(mediatorMock.Object);

        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext()
        };

        mediatorMock.Setup(m => m.Send(
            It.IsAny<CheckAnswerCommand>(),
            CancellationToken.None))
            .ReturnsAsync(new CheckAnswerCommandReponse
            {
                AnswerMessage = "Correct",
                Success = true,
                ErrorMessage = null,
            });

        //Act
        var response = await controller.CheckQuestionAnswer(request.PlayerName, request.PlayerAnswer);
        var objectResult = response.Result as ObjectResult;
        var answerWasCorrectOrIncorrectString = objectResult.Value as string;

        //Assert
        Assert.IsNotNull(objectResult);
        Assert.AreEqual(200, objectResult.StatusCode);
        Assert.AreEqual("Correct", answerWasCorrectOrIncorrectString);
    }

    [TestMethod]
    public async Task CheckQuestionAnswerTest_Incorrect_Successful()
    {
        //Arrange
        var request = new CheckAnswerCommand
        {
            PlayerName = "John Doe",
            PlayerAnswer = "Test",
        };

        var controller = new QuizController(mediatorMock.Object);

        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext()
        };

        mediatorMock.Setup(m => m.Send(
            It.IsAny<CheckAnswerCommand>(),
            CancellationToken.None))
            .ReturnsAsync(new CheckAnswerCommandReponse
            {
                AnswerMessage = "Incorrect",
                Success = true,
                ErrorMessage = null,
            });

        //Act
        var response = await controller.CheckQuestionAnswer(request.PlayerName, request.PlayerAnswer);
        var objectResult = response.Result as ObjectResult;
        var answerWasCorrectOrIncorrectString = objectResult.Value as string;

        //Assert
        Assert.IsNotNull(objectResult);
        Assert.AreEqual(200, objectResult.StatusCode);
        Assert.AreEqual("Incorrect", answerWasCorrectOrIncorrectString);
    }

    [TestMethod]
    public async Task CheckQuestionAnswerTest_NoName_Fail()
    {
        //Arrange
        var request = new CheckAnswerCommand
        {
            PlayerName = "John Doe",
            PlayerAnswer = "Test",
        };

        var controller = new QuizController(mediatorMock.Object);

        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext()
        };

        mediatorMock.Setup(m => m.Send(
            It.IsAny<CheckAnswerCommand>(),
            CancellationToken.None))
            .ReturnsAsync(new CheckAnswerCommandReponse
            {
                AnswerMessage = null,
                Success = false,
                ErrorMessage = "No name was entered.",
            });

        //Act
        var response = await controller.CheckQuestionAnswer(request.PlayerName, request.PlayerAnswer);
        var objectResult = response.Result as ObjectResult;

        //Assert
        Assert.IsNotNull(objectResult);
        Assert.AreEqual(500, objectResult.StatusCode);
        Assert.AreEqual("No name was entered.", objectResult.Value);
    }

    [TestMethod]
    public async Task CheckQuestionAnswerTest_NoAnswer_Fail()
    {
        //Arrange
        var request = new CheckAnswerCommand
        {
            PlayerName = "John Doe",
            PlayerAnswer = "Test",
        };

        var controller = new QuizController(mediatorMock.Object);

        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext()
        };

        mediatorMock.Setup(m => m.Send(
            It.IsAny<CheckAnswerCommand>(),
            CancellationToken.None))
            .ReturnsAsync(new CheckAnswerCommandReponse
            {
                AnswerMessage = null,
                Success = false,
                ErrorMessage = "No answer was entered.",
            });

        //Act
        var response = await controller.CheckQuestionAnswer(request.PlayerName, request.PlayerAnswer);
        var objectResult = response.Result as ObjectResult;

        //Assert
        Assert.IsNotNull(objectResult);
        Assert.AreEqual(500, objectResult.StatusCode);
        Assert.AreEqual("No answer was entered.", objectResult.Value);
    }

    [TestMethod]
    public async Task GetInitialInstructionsTest_Successful()
    {
        //Arrange
        var query = new GetInitialInstructionsQuery();

        var controller = new QuizController(mediatorMock.Object);

        mediatorMock.Setup(m => m.Send(
            It.IsAny<GetInitialInstructionsQuery>(),
            CancellationToken.None))
            .ReturnsAsync(new GetInitialInstructionsQueryResponse
            {
                Instructions = new List<string> { "Test" },
                Success = true,
                ErrorMessage = null,
            });

        //Act
        var response = await controller.GetInitialInstructions();
        var objectResult = response.Result as ObjectResult;
        var listOfInstructions = objectResult.Value as List<string>;

        //Assert
        Assert.IsNotNull(objectResult);
        Assert.AreEqual(200, objectResult.StatusCode);
        Assert.AreEqual("Test", listOfInstructions[0]);
    }

    [TestMethod]
    public async Task GetInitialInstructionsTest_Fail()
    {
        //Arrange
        var query = new GetInitialInstructionsQuery();

        var controller = new QuizController(mediatorMock.Object);

        mediatorMock.Setup(m => m.Send(
            It.IsAny<GetInitialInstructionsQuery>(),
            CancellationToken.None))
            .ReturnsAsync(new GetInitialInstructionsQueryResponse
            {
                Instructions = null,
                Success = false,
                ErrorMessage = "Internal server error",
            });

        //Act
        var response = await controller.GetInitialInstructions();
        var objectResult = response.Result as ObjectResult;
        var errorMessage = objectResult.Value as string;

        //Assert
        Assert.IsNotNull(objectResult);
        Assert.AreEqual(500, objectResult.StatusCode);
        Assert.AreEqual("Internal server error", errorMessage);
    }

    [TestMethod]
    public async Task HandleUnknownRoute_Returns_NotFound()
    {
        //Arrange
        var controller = new QuizController(mediatorMock.Object);

        //Act
        var response = controller.HandleUnknownRoute();
        var objectResult = response.Result as ObjectResult;

        //Assert
        Assert.IsNotNull(response);
        Assert.AreEqual(404, objectResult.StatusCode);
    }
}