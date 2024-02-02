using System.Globalization;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace QuizLibrary
{
    public class InterfaceHandler
    {
        private readonly HttpClient httpClient;

        private IManager manager;
        private Quiz quiz;
        private List<QuestionCard> questionCards;
        private int correctAnswers = 0;
        private int totalAnswers = 0;
        private int deckLength = 0;
        int totalNumberOfquestions;
        string answerIsNullString = ("Please verify that you entered a valid answer."
                    + Environment.NewLine);
        string apiUrl = "https://localhost:7140/api/quiz/";
        public string AnswerIsNullString
        {
            get
            {
                return answerIsNullString;
            }
        }
        public int TotalNumberOfQuestions
        {
            get
            {
                return totalNumberOfquestions;
            }
        }
        public InterfaceHandler(HttpClient httpClient)
        {
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }
        public string AddQuestionToFileOrReturnQuestionAsString(string question, string questionType,
            string questionAnswer, List<string> questionMcsaOptions, bool modifyAQuestion)
        {
            string combinedString = questionType + "|" + question.Trim() + "|" + questionAnswer.Trim();

            if (questionType == "MCSACard")
            {
                string combinedMCSAOptionsString = "";

                foreach (string item in questionMcsaOptions)
                {
                    combinedMCSAOptionsString += item;
                    combinedMCSAOptionsString += ",";
                }

                combinedMCSAOptionsString = combinedMCSAOptionsString.Remove(combinedMCSAOptionsString.Length - 1);

                combinedString += "|";
                combinedString += combinedMCSAOptionsString;
            }

            if (!modifyAQuestion)
            {
                if (!manager.AddQuestion(combinedString))
                {
                    return "";
                }
                else
                {
                    return "success";
                }
            }
            return combinedString;
        }
        private string CheckQuestionAnswer(string answer)
        {
            QuestionCard card = questionCards[totalAnswers++];

            int pointsGained = card.CheckQuestionAnswer(answer);

            correctAnswers += pointsGained;

            if (pointsGained == 0)
            {
                return "Incorrect.";
            }
            return "Correct!";
        }
        public int CheckIfQuestionIsMcsa(QuestionCard quizQuestion)
        {
            if (quizQuestion.McsaOptions != null)
            {
                return quizQuestion.McsaOptions.Count;
            }
            return 0;
        }        
        public string ConvertQuestionTypeNumberToString(string questionTypeNumber)
        {
            if (questionTypeNumber == "1") //User answers 1 or 2 above to determine the question type. This here converts from number (string) to respective type, as a string.
            {
                return "QuestionCard";
            }
            else
            {
                return "MCSACard";
            }
        }        
        public string CreateQuestion(string question, string questionType,
            string questionAnswer, List<string> questionMcsaOptions, bool modifyAQuestion = false)
        {
            string response = (AddQuestionToFileOrReturnQuestionAsString(
                question, questionType, questionAnswer,
                questionMcsaOptions, modifyAQuestion));
            if (response == "")
            {
                return "Failed to add question. Are you trying to add an already existing question?";
            }
            else if (response == "success")
            {
                return "Question was added successfully.";
            }
            else
            {
                return response;
            }
        }
        public List<string> CreateQuestionAnswer()
        {
            return new List<string> { "What are the words that the answer has to include for it to be correct?",
            "What are the five options for the question going to be?" };
        }
        public List<string> CreateQuestionInit()
        {
            List<string> listWithDetails = new List<string> {
                "What is the question going to be?",
                "What type of question would you like to add?",
                "1. Regular question.",
                "2. Multiple Choice Single Answer question." };

            return listWithDetails;
        }
        public string DisplayCurrentQuizResults(string answer)
        {
            return CheckQuestionAnswer(answer) +
                Environment.NewLine +
                Environment.NewLine +
                GetCorrectAndTotalAnswers();
        }
        private string DoWhileFunction(string answer, List<string> mcsaOptions, int highestAllowedNumber = 0)
        {
            int intAnswer = 0;

            if (answer == "")
            {
                answer = null;
            }
            if (highestAllowedNumber > 0)
            {
                if (!(int.TryParse(answer, out intAnswer)))
                {
                    intAnswer = 0;
                    answer = null;
                }

                for (int i = 1; i <= highestAllowedNumber; i++)
                {
                    if (intAnswer == i)
                    {
                        answer = intAnswer.ToString();
                        break;
                    }
                    else
                    {
                        answer = null;
                    }
                }
            }
            if (!(mcsaOptions is null))
            {
                if (mcsaOptions.Contains(answer))
                {
                    answer = null;
                }
            }
            return answer;
        }
        public List<string> GetAllQuestions()
        {
            List<string> listOfAllQuestions = new List<string>();
            int questionNumber = 0;
            foreach (QuestionCard item in questionCards)
            {
                listOfAllQuestions.Add(++questionNumber + ". " + item.Question);
            }
            return listOfAllQuestions;
        }
        public string GetAnswerFormat(int highestAllowedNumber)
        {
            string answer = "";

            string answerFormat = "Enter your answer";

            if (highestAllowedNumber > 0)
            {
                answerFormat += " in the range 1-" + highestAllowedNumber + ": ";
            }
            else
            {
                answerFormat += ": ";
            }
            return answerFormat;
        }
        public string GetCorrectAndTotalAnswers()
        {
            return "Correct answers/total answers: " + correctAnswers + "/" + totalAnswers;
        }
        public List<string> GetListWithTargetQuestionDetails()
        {
            return new List<string> { "These are the questions currently in the quiz:", "Choose the number of the question you want to target in the quiz." };
        }
        public QuestionCard GetQuestion(int indexOfQuestion)
        {
            return questionCards[indexOfQuestion];
        }
        public List<string> GetQuestionDetails(QuestionCard question)
        {
            List<string> listOfQuestionDetails = new List<string>();
            listOfQuestionDetails.Add(question.Question);
            if (question.McsaOptions != null)
            {
                for (int i = 1; i <= question.McsaOptions.Count; i++)
                {
                    listOfQuestionDetails.Add(i + ". " + question.McsaOptions[i-1]);
                }
            }
            return listOfQuestionDetails;
        }
        public string GetQuizResults()
        {
            string resultString = "";

            resultString += ("That's all the cards. Thanks for playing!" + Environment.NewLine +
                "Your final result was: " + correctAnswers + "/" + totalAnswers + "." +
                Environment.NewLine);
            resultString += (correctAnswers == totalAnswers ? "Perfect score!" :
                correctAnswers == 0 ? "All incorrect..." : "Not bad!");

            return resultString;
        }
        public string InitializeQuizInstructions()
        {
            return "Time to initialize the quiz! You may either create a new user, or choose an existing one. " +
                "Please enter a name, and the amount of questions you would like.";
        }
        public async Task<string> InitializeQuiz(string playerName, int numberOfQuestions)
        {
            apiUrl += "initquiz" + "/" + playerName + "/" + numberOfQuestions.ToString();

            using (httpClient)
            {
                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadFromJsonAsync<string>();
                    }
                    else
                    {
                        Console.WriteLine($"Error: {response.StatusCode}");
                        return response.StatusCode.ToString();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                    return ex.Message;
                }
            }


            return null;
        }
        public async Task<List<string>> LogInstructions()
        {
            apiUrl += "instructions";

            using (httpClient)
            {
                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadFromJsonAsync<List<string>>();
                    }
                    else
                    {
                        Console.WriteLine($"Error: {response.StatusCode}");
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                    return null;
                }
            }
        }
        public List<List<string>> PerformAction(string answer)
        {
            //TODO fix this
            List<List<string>> listOfInstructions = new List<List<string>>();
            //PrepareQuiz();
            //totalNumberOfquestions = questionCards.Count;
            if (answer == "1")
            {
                return new List<List<string>> { new List<string> { "Welcome to the quiz! You will be presented with a question, for which you may submit an answer." } };
            }
            else if (answer == "2")
            {
                listOfInstructions.Add(CreateQuestionInit());
                listOfInstructions.Add(CreateQuestionAnswer());

                return listOfInstructions;
            }
            
            else if (answer == "3")
            {                
                listOfInstructions.Add(new List<string> { questionCards.Count.ToString() });
                listOfInstructions.Add(GetAllQuestions());

                return listOfInstructions;
            }

            else if (answer == "4")
            {
                listOfInstructions.Add(new List<string> { questionCards.Count.ToString() });
                listOfInstructions.Add(GetAllQuestions());
                listOfInstructions.Add(CreateQuestionInit());
                listOfInstructions.Add(CreateQuestionAnswer());

                return listOfInstructions;
            }

            else if (answer == "5")
            {
                return new List<List<string>> { new List<string> { "" } };
            } 
            
            else
            {
                return (new List<List<string>> { new List<string> { "Please enter a number in the range 1-5." } });
            }
        }
        /*
        private void PrepareQuiz()
        {
            //TODO fix quiz
            questionCards = quiz.Run();

            correctAnswers = 0;
            totalAnswers = 0;
            deckLength = questionCards.Count;
        }
        */
        public void RemoveOrModifyQuestion(string numberOfQuestion, string modifiedQuestion = "")
        {
            int intNumberOfQuestion = Convert.ToInt32(numberOfQuestion);
            intNumberOfQuestion--;
            numberOfQuestion = intNumberOfQuestion.ToString();

            foreach (QuestionCard item in quiz.ListOfSortedQuestionCards())
            {
                if (item.Question == questionCards[Convert.ToInt32(numberOfQuestion)].Question)
                {
                    numberOfQuestion = quiz.ListOfSortedQuestionCards().IndexOf(item).ToString();
                    break;
                }
            }
            manager.RemoveOrModifyQuestion(Convert.ToInt32(numberOfQuestion), modifiedQuestion);
        }     
        public string VerifyAnswer(string answer, int highestAllowedNumber = 0, List<string>? mcsaOptions = null)
        {
            answer = DoWhileFunction(answer, mcsaOptions, highestAllowedNumber);
            if (answer == null)
            {
                return answerIsNullString;
            }
            return answer;
        }        
    }
}
