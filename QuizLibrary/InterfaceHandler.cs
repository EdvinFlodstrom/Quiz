using System.Globalization;
using System.Reflection.Metadata.Ecma335;

namespace QuizLibrary
{
    public class InterfaceHandler
    {
        FileManager fm = new FileManager();
        ActionHandler handler = new ActionHandler();
        List<string> listOfAllQuestions = new List<string>();
        int totalNumberOfquestions;
        string optionsString = "Your options are as follow:";
        string answerIsNullString = ("Please verify that you entered a valid answer."
                    + Environment.NewLine);
        public string AnswerIsNullString
        {
            get
            {
                return answerIsNullString;
            }
        }
        public string OptionsString
        {
            get
            {
                return optionsString;
            }
        }
        public int TotalNumberOfQuestions
        {
            get
            {
                return totalNumberOfquestions;
            }
        }
        public InterfaceHandler() { }
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
                if (!fm.AddQuestionToFile(combinedString))
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
        public List<string> CreateQuestionAnswer()
        {
            return new List<string> { "What are the words that the answer has to include for it to be correct?",
            "What are the five options for the question going to be?" };
        }        
        public List<string> CreateQuestionInit()
        {
            List<string> listWithDetails = new List<string> {
                "What is the question going to be?",
                "What type of question would you like to add?" + 
                Environment.NewLine +
                "1. Regular question." + 
                Environment.NewLine +
                "2. Multiple Choice Single Answer question." };
          
            return listWithDetails;
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
        public string DisplayCurrentQuizResults(string answer)
        {
            return handler.CheckQuestionAnswer(answer) +
                Environment.NewLine +
                Environment.NewLine +
                handler.GetCorrectAndTotalAnswers();
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
        public List<string> GetListWithTargetQuestionDetails()
        {
            return new List<string> { "These are the questions currently in the quiz:", "Choose the number of the question you want to target in the quiz." };
        }
        public string GetQuestion(int indexOfQuestion)
        {
            return listOfAllQuestions[indexOfQuestion];
        }       
        public string GetQuizResults()
        {
            return handler.GetQuizResults();
        }        
        public string LogInstructions()
        {
            return "Welcome to the quiz UI! Your options are as follow:" + Environment.NewLine +
                "1. Take the quiz." + Environment.NewLine +
                "2. Add a question to the quiz." + Environment.NewLine +
                "3. Remove a question from the quiz." + Environment.NewLine +
                "4. Modify a question in the quiz." + Environment.NewLine +
                "5. Close the application." + Environment.NewLine;
        }
        public List<List<string>> PerformAction(string answer)
        {
            List<List<string>> listOfInstructions = new List<List<string>>();
            handler.PrepareQuiz();
            listOfAllQuestions.Clear();
            RunQuiz();
            totalNumberOfquestions = listOfAllQuestions.Count;
            if (answer == "1")
            {
                RunQuiz();
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
                listOfInstructions.Add(new List<string> { listOfAllQuestions.Count.ToString() });
                listOfInstructions.Add(handler.GetAllQuestions());

                return listOfInstructions;
            }

            else if (answer == "4")
            {
                listOfInstructions.Add(new List<string> { listOfAllQuestions.Count.ToString() });
                listOfInstructions.Add(handler.GetAllQuestions());
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
        public void RemoveOrModifyQuestion(string numberOfQuestion, string modifiedQuestion = "")
        {
            fm.RemoveOrModifyQuestion(Convert.ToInt32(numberOfQuestion), modifiedQuestion);
        }
        private void RunQuiz()
        {            
            int indexOfCurrentQuestion = 0;

            for (List<string> i = handler.GetNewQuestion(indexOfCurrentQuestion); i[0] != null; i = handler.GetNewQuestion(indexOfCurrentQuestion)) //Until i[0] is null, which happens when there are no more questions, keep calling handler.GetNewQuestion.
            {
                if (i.Count > 1)
                {
                    string mcsaString = i[0];
                    mcsaString += (Environment.NewLine
                        + optionsString + Environment.NewLine);
                    mcsaString += (i[1]);
                    listOfAllQuestions.Add(mcsaString);
                }
                else
                {
                    listOfAllQuestions.Add(i[0]);
                }
                indexOfCurrentQuestion++;
            }
        }        
        public string VerifyAnswer(string answer, int highestAllowedNumber = 0, List<string> mcsaOptions = null)
        {
            answer = handler.DoWhileFunction(answer, mcsaOptions, highestAllowedNumber);
            if (answer == null)
            {
                return answerIsNullString;
            }
            return answer;
        }        
    }
}
