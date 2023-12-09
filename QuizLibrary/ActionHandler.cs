namespace QuizLibrary
{
    internal class ActionHandler
    {
        FileManager fm = new FileManager();
        Quiz quiz = new Quiz();

        List<QuestionCard> questionCards;
        private int correctAnswers = 0;
        private int totalAnswers = 0;
        private int deckLength = 0;
        
        public ActionHandler() { }
        public string CheckQuestionAnswer(string answer)
        {
            QuestionCard card = questionCards[totalAnswers++];            

            string[] splitStr = card.CorrectAnswer.Split(' ');

            bool answerTrue = false;

            foreach (string item in splitStr)
            {
                if (answer.ToLower().Contains(item.ToLower()))
                {
                    answerTrue = true;
                }
                else
                {
                    answerTrue = false;
                    return "Incorrect.";
                }
            }
            if (answerTrue)
            {
                correctAnswers++;
                return "Correct!";
            }
            return "Incorrect.";
        }
        public string DoWhileFunction(string answer, List<string> mcsaOptions, int highestAllowedNumber = 0)
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
            foreach (List<string> item in fm.ReadFile())
            {
                listOfAllQuestions.Add(++questionNumber + ". " + item[1]);
            }
            return listOfAllQuestions;
        }
        public string GetCorrectAndTotalAnswers()
        {
            return "Correct answers/total answers: " + correctAnswers + "/" + totalAnswers;
        }
        public List<string> GetNewQuestion(int indexOfCurrentQuestion)
        {
            List<string> stringOfQuestionDetails = new List<string>();
            
            if (indexOfCurrentQuestion >= deckLength)
            {
                stringOfQuestionDetails.Add(null);

                return stringOfQuestionDetails;
            }

            QuestionCard card = questionCards[indexOfCurrentQuestion];

            stringOfQuestionDetails.Add(card.Question);

            if (!(card.McsaOptions is null))
            {
                string stringOfMcsaOptions = "";
                for (int i = 1; i < 6; i++)
                {
                    stringOfMcsaOptions += i + ". " + card.McsaOptions[i - 1];
                    if (i < 5)
                    {
                        stringOfMcsaOptions += Environment.NewLine;
                    }
                }
                stringOfQuestionDetails.Add(stringOfMcsaOptions);
            }

            return stringOfQuestionDetails;
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
        public void PrepareQuiz()
        {
            questionCards = quiz.Run();

            correctAnswers = 0;
            totalAnswers = 0;
            deckLength = questionCards.Count;            
        }
    }
}