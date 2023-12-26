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
        public ActionResult<List<Question>>? Get()
        {
            try
            {
                List<Question>? questions = quizService.GetAllQuestions();

                return questions == null ? NotFound() : Ok(questions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex);
            }
        }
        [HttpGet("{id}")]
        public ActionResult<Question>? GetById(int id)
        {
            try
            {
                Question? question = quizService.GetQuestionById(id);

                return question == null ? NotFound() : Ok(question);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex);
            }
        }
    }
}
