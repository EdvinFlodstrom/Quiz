using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web_App.Server.Handlers.Questions;
using Web_App.Server.Handlers.Quiz;
using Web_App.Server.Models;

namespace Web_App.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuizController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator mediator = mediator;

        [HttpGet("initquiz/{playerName}/{numberOfQuestions:int}")]
        public async Task<ActionResult> InitializeQuiz(string playerName, int numberOfQuestions)
        {
            try
            {
                var command = new InitializeQuizCommand
                {
                    PlayerName = playerName,
                    NumberOfQuestions = numberOfQuestions
                };

                InitializeQuizCommandResponse quizInitializedSuccessfully = await mediator.Send(command);

                return quizInitializedSuccessfully.Success == true
                    ? Ok(quizInitializedSuccessfully.QuizInitializedDetails)
                    : StatusCode(500, quizInitializedSuccessfully.QuizInitializedDetails + quizInitializedSuccessfully.ErrorMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex);
            }
        }
        [HttpGet]
        public async Task<ActionResult<List<QuestionModel>>> GetAllQuestions()
        {
            try
            {
                var query = new GetAllQuestionsQuery();
                GetAllQuestionsQueryResponse questions = await mediator.Send(query);

                return questions.Success == true
                    ? Ok(questions.Questions)
                    : StatusCode(500, questions.ErrorMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex);
            }
        }
        [HttpGet("takequiz/{playerName}")]
        public async Task<ActionResult<QuestionModel>> GetQuestionWithoutAnswer(string playerName)
        {
            try
            {
                var request = new GetQuestionCommand()
                {
                    PlayerName = playerName
                };

                GetQuestionCommandResponse question = await mediator.Send(request);

                Response.Headers.Append("Content-Type", "application/json");

                return question.Success == true
                    ? Ok(question.Question)
                    : question.ErrorMessage is null
                    ? Ok(false)
                    : StatusCode(500, question.AnswerMessage + question.ErrorMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex);
            }
        }
        [HttpGet("takequiz/{playerName}/{playerAnswer}")]
        public async Task<ActionResult<QuestionModel>> CheckQuestionAnswer(string playerName, string playerAnswer)
        {
            try
            {
                var request = new CheckAnswerCommand()
                {
                    PlayerName = playerName,
                    PlayerAnswer = playerAnswer
                };

                CheckAnswerCommandReponse question = await mediator.Send(request);

                Response.Headers.Append("Content-Type", "application/json");

                return question.Success == true
                    ? Ok(question.AnswerMessage)
                    : StatusCode(500, question.AnswerMessage + question.ErrorMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex);
            }
        }
        [HttpGet("instructions")]
        public async Task<ActionResult<List<string>>> GetInitialInstructions()
        {
            try
            {
                var query = new GetInitialInstructionsQuery();
                GetInitialInstructionsQueryResponse instructions = await mediator.Send(query);

                return instructions.Success == true
                    ? Ok(instructions.Instructions)
                    : StatusCode(500, instructions.ErrorMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex);
            }
        }
        [HttpGet("{*path}")]
        public ActionResult<string> HandleUnknownRoute()
        {
            return NotFound("Resource not found.");
        }
    }
}
