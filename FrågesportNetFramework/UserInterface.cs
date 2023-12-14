using QuizLibrary;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;
using System.Net.NetworkInformation;

namespace FrågesportNetFramework
{
    public class UserInterface
    {
        InterfaceHandler handler = new InterfaceHandler();
        public UserInterface() { }
        private string DoWhileMethod(int highestAllowedNumber)
        {
            string answer = "";
            do
            {
                if (answer == handler.AnswerIsNullString)
                {
                    Console.WriteLine(answer);
                }
                Console.Write(handler.GetAnswerFormat(highestAllowedNumber));
                answer = Console.ReadLine();
                answer = handler.VerifyAnswer(answer, highestAllowedNumber);
            } while (answer == handler.AnswerIsNullString);

            return answer;
        }
        public void Run()
        {
            string answer;
            string ifEmptyBreakLook = "";

            do
            {
                foreach (string item in handler.LogInstructions())
                {
                    Console.WriteLine(item);
                }
                answer = DoWhileMethod(5);
                ifEmptyBreakLook = handler.PerformAction(answer)[0][0];
                if (answer == "1")
                {
                    Console.WriteLine(ifEmptyBreakLook + Environment.NewLine);                    
                    for (int i = 0; i < handler.TotalNumberOfQuestions; i++)
                    {                        
                        QuestionCard quizQuestion = handler.GetQuestion(i);
                        List<string> quizQuestionDetails = handler.GetQuestionDetails(quizQuestion);
                        foreach (string item in quizQuestionDetails)
                        {
                            Console.WriteLine(item);
                        }
                        do
                        {
                            int highestAllowedNumber = handler.CheckIfQuestionIsMcsa(quizQuestion);
                            Console.Write(handler.GetAnswerFormat(highestAllowedNumber));
                            answer = Console.ReadLine();
                            answer = handler.VerifyAnswer(answer, highestAllowedNumber);
                            if (answer != handler.AnswerIsNullString)
                            {
                                Console.WriteLine(handler.DisplayCurrentQuizResults(answer) + Environment.NewLine);
                            }
                            else
                            {
                                Console.WriteLine(answer + Environment.NewLine); //Writes out that you entered an invalid answer.
                            }
                        } while(answer == handler.AnswerIsNullString);
                    }
                    Console.WriteLine(handler.GetQuizResults());
                }
                else if (answer != "5")
                {
                    Console.WriteLine();

                    List<List<string>> instructions = handler.PerformAction(answer);
                    ifEmptyBreakLook = instructions[0][0];

                    string questionType = "";
                    string question = "";
                    string questionAnswer = "";                    
                    int numberOfQuestions;
                    string numberOfTargetedQuestion = "";
                    bool modifyAQuestion = false;
                    bool removeAQuestion = false;
                    bool hasTargetedAQuestion = false;
                    List<string> questionMcsaOptions = new List<string>();

                    foreach (List<string> item in instructions)
                    {
                        if (int.TryParse(item[0], out numberOfQuestions)) //Evaluates to true ONLY if user wants to remove or modify a question.
                        {                            
                            if (instructions.Count > 2) //Happens if user chose to modify a question. More List<string> are returned to instructions if this is the case.
                            {
                                modifyAQuestion = true;
                            }
                            else
                            {
                                removeAQuestion = true;
                            }
                            continue; //The above are only for checking if the user wants to add/modify a question. These values are not meant to be used for anything else.
                        }
                        if ((modifyAQuestion || removeAQuestion) && !hasTargetedAQuestion)
                        {
                            List<string> listWithQuestionTargetingDetails = 
                                handler.GetListWithTargetQuestionDetails();
                            Console.WriteLine(listWithQuestionTargetingDetails[0]);
                            for (int i = 1; i <= item.Count; i++)
                            {
                                Console.WriteLine(item[i - 1]);
                            }                            
                            Console.WriteLine(Environment.NewLine + listWithQuestionTargetingDetails[1]);

                            numberOfQuestions = handler.TotalNumberOfQuestions;
                            numberOfTargetedQuestion = DoWhileMethod(numberOfQuestions);
                            hasTargetedAQuestion = true;

                            if (removeAQuestion)
                            {
                                handler.RemoveOrModifyQuestion(numberOfTargetedQuestion);
                                Console.WriteLine("Question was removed successfully.");                                
                            }
                            else
                            {
                                Console.WriteLine("Please create the new question that will replace the previous one." +
                                    Environment.NewLine);
                            }
                            continue;
                        }
                        
                        //Here is where question creation ensues.
                        int highestAllowedNumber = 0;
                        if (questionType == "")
                        {                           
                            string readAnswer = "";
                            foreach (string subItem in item)
                            {
                                highestAllowedNumber = 0;
                                if (item.IndexOf(subItem) == 1)
                                {
                                    highestAllowedNumber = 2;
                                }
                                question = readAnswer;
                                Console.WriteLine(subItem);
                                readAnswer = DoWhileMethod(highestAllowedNumber);
                                questionType = readAnswer;
                                Console.WriteLine();
                            }
                            questionType = handler.ConvertQuestionTypeNumberToString(questionType);
                        }
                        else
                        {
                            if (questionType == "QuestionCard")
                            {
                                highestAllowedNumber = 0;
                            }
                            else
                            {
                                highestAllowedNumber = 5;
                            }
                            Console.WriteLine(highestAllowedNumber == 0 ? item[0] : item[1]);

                            for (int i = 1; i < 6; i++)
                            {
                                string optionOrFullAnswer = "";
                                do
                                {
                                    if (optionOrFullAnswer == handler.AnswerIsNullString)
                                    {
                                        Console.WriteLine("Please enter a valid answer, and remember not to enter the same option twice." + 
                                            Environment.NewLine);
                                    }
                                    if (highestAllowedNumber != 0)
                                    {
                                        Console.Write(i + ". ");
                                    }
                                    Console.Write(handler.GetAnswerFormat(0));
                                    optionOrFullAnswer = "";                                    
                                    optionOrFullAnswer = Console.ReadLine();
                                    optionOrFullAnswer = handler.VerifyAnswer(optionOrFullAnswer, mcsaOptions: questionMcsaOptions);
                                } while (optionOrFullAnswer == handler.AnswerIsNullString);
                                if (highestAllowedNumber == 0)
                                {
                                    questionAnswer = optionOrFullAnswer;
                                    break;
                                }
                                questionMcsaOptions.Add(optionOrFullAnswer);
                            }
                            
                            if (highestAllowedNumber != 0)
                            {
                                Console.Write(Environment.NewLine + "Please enter the correct answer for the question. " +
                                    Environment.NewLine);
                                questionAnswer = DoWhileMethod(highestAllowedNumber);
                            }
                            answer = handler.CreateQuestion(
                                question, questionType, questionAnswer,
                                questionMcsaOptions, modifyAQuestion);
                            if (modifyAQuestion)
                            {
                                handler.RemoveOrModifyQuestion(numberOfTargetedQuestion, answer);
                                Console.WriteLine(Environment.NewLine + "Question was modified successfully.");
                            }
                            else
                            {
                                Console.WriteLine(Environment.NewLine + "Question was added successfully.");
                            }
                        }
                    }
                }
                Console.WriteLine();
            } while (ifEmptyBreakLook != "");
            Console.Write("Closing application. Press any key.");
        }        
    }
}