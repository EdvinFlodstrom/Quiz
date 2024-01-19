using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.Arm;
using Web_App.Server.Data;
using Web_App.Server.Models;

namespace Web_App.Server.Services
{
    public class QuizService
    {
        private Random rnd = new Random();
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
        public bool InitializeQuiz(string playerName, int numberOfQuestions)
        {
            try
            {
                var playerExists = quizContext.PlayerStatistics.FirstOrDefault(p => p.PlayerName == playerName);

                var questions = GetAllQuestions();
                string stringOfSpaceSeparatedQuestionIds = "";
                List<int> listOfQuestionIdsOfQuestions = [];
                foreach (var item in questions)
                {
                    listOfQuestionIdsOfQuestions.Add(item.QuestionId);
                }

                for (int i = 0; i < numberOfQuestions; i++)
                {
                    int randomIndex = rnd.Next(0, listOfQuestionIdsOfQuestions.Count - 1);
                    stringOfSpaceSeparatedQuestionIds += listOfQuestionIdsOfQuestions[randomIndex].ToString() + " "; 
                    listOfQuestionIdsOfQuestions.RemoveAt(randomIndex);                     
                }

                if (playerExists != null)
                {                    
                    playerExists.ListOfQuestionIds = stringOfSpaceSeparatedQuestionIds;
                    
                    quizContext.SaveChanges();
                }
                else
                {
                    var newPlayer = new PlayerStatisticsModel
                    {
                        PlayerName = playerName,
                        NumberOfCurrentQuestion = 0,
                        CorrectAnswers = 0,
                        ListOfQuestionIds = stringOfSpaceSeparatedQuestionIds
                    };

                    quizContext.PlayerStatistics.Add(newPlayer);
                    quizContext.SaveChanges();
                }
                
                return true;
            }
            catch 
            {
                return false;
            }
        }
    }
}
