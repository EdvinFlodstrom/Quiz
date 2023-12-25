using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_App.Server.Data;
using Web_App.Server.Models;

namespace Web_App.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuizController : ControllerBase
    {
        private readonly QuizContext quizContext;

        public QuizController(QuizContext quizContext)
        {
            this.quizContext = quizContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {                
                var q = quizContext.Questions.ToList();

                return Ok(q);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex);
            }
        }
    }
}
