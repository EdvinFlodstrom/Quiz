using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_App.Server.Services;
using Web_App.Server.Models;

namespace Web_App.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuizController : ControllerBase
    {
        private readonly QuizService quizService;

        public QuizController(QuizService quizService)
        {
            this.quizService = quizService;
        }

        [HttpGet]
        public ActionResult<List<QuestionModel>>? GetAllQuestions()
        {
            try
            {
                List<QuestionModel>? questions = quizService.GetAllQuestions();

                return questions == null ? NotFound() : Ok(questions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex);
            }
        }
        [HttpGet("{id:int}")]
        public ActionResult<QuestionModel>? GetQuestionWithoutAnswerById(int id)
        {
            try
            {
                QuestionModel? question = quizService.GetQuestionWithoutAnswerById(id);
                //int antal_rätt = question.CheckQuestionAnswer("hej");
                return question == null ? NotFound() : Ok(question);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex);
            }
        }
        [HttpGet("initquiz/{playerName}/{numberOfQuestions:int}")]
        public ActionResult InitializeQuiz(string playerName, int numberOfQuestions)
        {
            try
            {                
                bool quizInitializedSuccessfully = quizService.InitializeQuiz(playerName, numberOfQuestions);

                return quizInitializedSuccessfully == true ?
                    Ok($"Quiz has been initialized successfully for player {playerName}.")
                    : BadRequest("Player was not added and no quiz was initialized. " +
                    "Please make sure that the player name is correctly formatted.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex);
            }                
        }
        [HttpGet("instructions")]
        public ActionResult<List<string>> GetInstructions()
        {
            try
            {
                List<string> instructions = quizService.GetInitialInstructions();

                return instructions == null ? NotFound() : Ok(instructions);
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
