using System.Runtime.Intrinsics.Arm;
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
        public List<string> GetInitialInstructions()
        {
            return new List<string>
            {
                "Welcome to the quiz UI! Your options are as follow:",
                "1. Take the quiz.",
                "2. Add a question to the quiz.",
                "3. Remove a question from the quiz.",
                "4. Modify a question in the quiz.",
                "5. Close the application."
            };
        }
        public List<QuestionModel>? GetAllQuestions()
        {
            try
            {
                var questions = quizContext.Questions.ToList();

                return questions;
            }
            catch
            { 
                return null;
            }
        }
        public QuestionModel? GetQuestionWithoutAnswerById(int id)
        {
            try
            {
                var question = quizContext.QuestionCards.FirstOrDefault(q => q.QuestionId == id) as QuestionModel
                    ?? quizContext.MCSACards.FirstOrDefault(q => q.QuestionId == id);

                if (question is QuestionCardModel questionCard)
                {
                    questionCard.RequiredWords = null;
                }
                else if (question is MCSACardModel mcsaCard)
                {
                    mcsaCard.CorrectOptionNumber = 0;
                }

                return question;
            }
            catch
            {
                return null;
            }
        }
    }
}
