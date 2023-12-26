using Web_App.Server.Data;
using Web_App.Server.Models;

namespace Web_App.Server.Services
{
    public class QuizService
    {
        private readonly QuizContext quizContext;

        public QuizService(QuizContext quizContext)
        {
            this.quizContext = quizContext;            
        }

        public List<Question>? GetAllQuestions()
        {
            try
            {
                var question = quizContext.Questions.ToList();

                return question;
            }
            catch
            { 
                return null;
            }
        }
        public Question? GetQuestionById(int id)
        {
            try
            {
                var question = quizContext.Questions.FirstOrDefault(q => q.QuestionId == id);

                return question;
            }
            catch
            {
                return null;
            }
        }
    }
}
