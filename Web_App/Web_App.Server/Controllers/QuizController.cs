using Microsoft.AspNetCore.Mvc;
using Web_App.Server.Models;
using Web_App.Server.Handlers.Quiz;
using MediatR;
using Web_App.Server.Handlers.Questions;

namespace Web_App.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuizController : ControllerBase
    {
        private readonly IMediator mediator;

        public QuizController(IMediator mediator)
        {
            this.mediator = mediator;
        }

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
                    ? Ok($"Quiz has been initialized successfully for player {playerName}.")
                    : StatusCode(500, $"{quizInitializedSuccessfully.ErrorMessage} Player was not added and no quiz was initialized. " +
                    "Please make sure that the player name is correctly formatted.");
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
        [HttpGet("{questionId:int}")]
        public async Task<ActionResult<QuestionModel>> GetQuestionWithoutAnswerById(int questionId)
        {
            try
            {
                var request = new GetQuestionWithOrWithoutAnswerByIdCommand()
                {
                    QuestionId = questionId,
                    IncludeAnswer = false
                };

                GetQuestionWithOrWithoutAnswerByIdCommandReponse question = await mediator.Send(request);
                
                return question.Success == true 
                    ? Ok(question.Question)
                    : StatusCode(500, question.ErrorMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex);
            }
        }        
        [HttpGet("{playerName}/{questionId:int}/{playerAnswer}")]
        public async Task<ActionResult<QuestionModel>> CheckQuestionAnswer(string playerName, int questionId, string playerAnswer)
        {
            var request = new GetQuestionWithOrWithoutAnswerByIdCommand()
            {
                QuestionId = questionId,
                IncludeAnswer = true
            };

            GetQuestionWithOrWithoutAnswerByIdCommandReponse question = await mediator.Send(request);

            if (question.Question.CheckQuestionAnswer(playerAnswer) != 0)
            {
                return Ok("Correct!");
            }
            else
            {
                return Ok("Incorrect.");
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
