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

        [HttpGet]
        public async Task<ActionResult<List<QuestionModel>>> GetAllQuestions()
        {
            try
            {
                var query = new GetAllQuestionsQuery();
                List<QuestionModel> questions = await mediator.Send(query);

                return questions == null 
                    ? NotFound() 
                    : Ok(questions);
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
                var request = new GetQuestionWithoutAnswerByIdCommand()
                {
                    QuestionId = questionId
                };
                QuestionModel? question = await mediator.Send(request);

                //int antal_rätt = question.CheckQuestionAnswer("hej");                
                
                return question == null 
                    ? NotFound() 
                    : Ok(question);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex);
            }
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

                bool quizInitializedSuccessfully = await mediator.Send(command);

                return quizInitializedSuccessfully == true 
                    ? Ok($"Quiz has been initialized successfully for player {playerName}.")
                    : BadRequest("Player was not added and no quiz was initialized. " +
                    "Please make sure that the player name is correctly formatted.");
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
                List<string> instructions = await mediator.Send(query);

                return instructions == null 
                    ? NotFound() 
                    : Ok(instructions);
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
