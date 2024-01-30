using System.Diagnostics;
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
        public Task<List<string>> GetInitialInstructions()
        {
            return Task.FromResult(new List<string>
            {
                "Welcome to the quiz UI! Your options are as follow:",
                "1. Take the quiz.",
                "2. Add a question to the quiz.",
                "3. Remove a question from the quiz.",
                "4. Modify a question in the quiz.",
                "5. Close the application."
            });
        }
        public Task<List<QuestionModel>> GetAllQuestions()
        {
            try
            {
                var questions = quizContext.Questions.ToList();

                return Task.FromResult(questions);
            }
            catch (Exception ex) 
            {
                Debug.WriteLine(ex);
                return null;
            }
        }
        public async Task<bool> InitializeQuiz(string playerName, int numberOfQuestions)
        {
            try
            {
                var playerExists = quizContext.PlayerStatistics.FirstOrDefault(p => p.PlayerName == playerName);

                var questions = GetAllQuestions();
                string stringOfSpaceSeparatedQuestionIds = "";
                List<int> listOfQuestionIdsOfQuestions = [];
                foreach (var item in await questions)
                {
                    listOfQuestionIdsOfQuestions.Add(item.QuestionId);
                }

                for (int i = 0; i < numberOfQuestions; i++)
                {
                    int randomIndex = rnd.Next(0, listOfQuestionIdsOfQuestions.Count - 1);
                    stringOfSpaceSeparatedQuestionIds += listOfQuestionIdsOfQuestions[randomIndex].ToString() + " ";
                    listOfQuestionIdsOfQuestions.RemoveAt(randomIndex);
                }
                
                List<int> listOfQuestionIds = stringOfSpaceSeparatedQuestionIds
                    .Trim()
                    .Split(' ')
                    .Select(int.Parse)
                    .ToList();

                if (playerExists != null)
                {
                    playerExists.ListOfQuestionIds = stringOfSpaceSeparatedQuestionIds;
                    playerExists.NumberOfCurrentQuestion = listOfQuestionIds[0];
                    playerExists.CorrectAnswers = 0;

                    quizContext.SaveChanges();
                }
                else
                {
                    var newPlayer = new PlayerStatisticsModel
                    {
                        PlayerName = playerName,
                        NumberOfCurrentQuestion = listOfQuestionIds[0],
                        CorrectAnswers = 0,
                        ListOfQuestionIds = stringOfSpaceSeparatedQuestionIds
                    };

                    quizContext.PlayerStatistics.Add(newPlayer);
                    quizContext.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }
        private Task<(PlayerStatisticsModel player, QuestionModel question)> GetPlayerAndQuestion(string playerName)
        {
            var player = quizContext.PlayerStatistics.FirstOrDefault(p => p.PlayerName == playerName);

            if (player == null)
            {
                return Task.FromResult<(PlayerStatisticsModel player, QuestionModel question)>((null, null));
            }

            var question = quizContext.Questions.FirstOrDefault(q => q.QuestionId == player.NumberOfCurrentQuestion);

            return Task.FromResult<(PlayerStatisticsModel player, QuestionModel question)>((player, question));
        }
        public async Task<(bool, string, QuestionModel)> GetQuestion(string playerName)
        {
            try
            {
                var (player, question) = await GetPlayerAndQuestion(playerName);

                if (player == null)
                {
                    return (false, "Player does not exist in the database.", null);
                }
                
                if (question is QuestionCardModel questionCard)
                {
                    questionCard.RequiredWords = null;
                }
                else if (question is MCSACardModel mcsaCard)
                {
                    mcsaCard.CorrectOptionNumber = 0;
                }

                return (true, "Success.", question);                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return (false, ex.Message, null);
            }
        }
        public async Task<(bool, string)> CheckAnswer(string playerName, string playerAnswer)
        {
            try
            {
                var (player, question) = await GetPlayerAndQuestion(playerName);

                if (player == null)
                {
                    return (false, "Player does not exist in the database.");
                }

                string correctOrIncorrect = "";

                if (question.CheckQuestionAnswer(playerAnswer) != 0)
                {
                    player.CorrectAnswers++;
                    correctOrIncorrect = "Correct! ";
                }
                else
                {
                    correctOrIncorrect = "Incorrect. ";
                }

                List<int> listOfQuestionIds = player.ListOfQuestionIds
                    .Trim()
                    .Split(' ')
                    .Select(int.Parse)
                    .ToList();

                int index = listOfQuestionIds.IndexOf(player.NumberOfCurrentQuestion) + 1;

                if (index < listOfQuestionIds.Count)
                {
                    player.NumberOfCurrentQuestion = listOfQuestionIds[index];
                }
                else
                {
                    return (true, correctOrIncorrect
                        + "You have now answered all the questions. Thanks for playing!" 
                        + Environment.NewLine
                        + $"Your final result was: {player.CorrectAnswers} / {listOfQuestionIds.Count}");
                }

                quizContext.SaveChanges();

                return (false, correctOrIncorrect);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return (false, ex.Message);
            }
        }
    }
}
