using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using QuizLibrary;

namespace WPF_GUI_Question
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>  
    public partial class MainWindow : Window
    {        
        InterfaceHandler handler = new InterfaceHandler(new HttpClient());
        List<Button> buttons = new List<Button>();
        QuestionCard quizQuestion;
        List<List<string>> listWithDetailsOfQuestionActions;
        List<string> listWithTargetQuestionDetails;
        List<string> mcsaOptions;
        string question;
        string questionAnswer;
        int indexOfCurrentQuestion = 0;
        bool isTakingQuiz = false;
        bool hasFinishedQuiz = false;
        bool isCreatingQuestion = false;
        string typeOfQuestionToCreate = "";
        bool hasFinishedCreatingQuestion = false;
        bool doneWithCreatingQuestion = false;
        bool isModifyingQuestion = false;
        bool isRemovingQuestion = false;
        bool hasCreatedQuestion = false;
        bool hasTargetedQuestion = false;        
        int numberOfCreatedOption = 1;
        int numberOfTargetedQuestion;

        public MainWindow()
        {
            InitializeComponent();
            buttons.AddRange(new List<Button>
            {
                Option1Button,
                Option2Button,
                Option3Button,
                Option4Button,
                Option5Button
            });

            InitialInstructions();           
        }
        private void CheckAnswerAndPrepareNextAction(string answer)
        {
            Instructions.Text = handler.DisplayCurrentQuizResults(answer);
            PrepareNextQuestion();
        }
        private void CreateAnswerForQuestion(string numberForQuestionToCreate)
        {
            typeOfQuestionToCreate = handler.ConvertQuestionTypeNumberToString(numberForQuestionToCreate);
            EnableOrDisableOptionButtons(false, true);
            EnableOrDisableSubmitButtonAndBox(true);

            Instructions.Text = listWithDetailsOfQuestionActions[!isModifyingQuestion ? 1 : 3][typeOfQuestionToCreate == "QuestionCard" ? 0 : 1];
        }
        private void CreateQuestionInit(bool isCreatingQuestion, bool isRemovingQuestion, bool isModifyingQuestion, string numberOfActionToPerform)
        {
            EnableOrDisableOptionButtons(false, true);
            ResetQuestionCreatingRelatedBooleans(isCreatingQuestion, isRemovingQuestion, isModifyingQuestion);
            mcsaOptions = new List<string>();

            listWithDetailsOfQuestionActions = handler.PerformAction(numberOfActionToPerform);

            string instructionsString = "Adding question to the quiz";

            if (isRemovingQuestion)
            {
                instructionsString = "Removing question from the quiz";
            }
            else if (isModifyingQuestion)
            {
                instructionsString = "Modifying question in the quiz";
            }

            Instructions.Text = instructionsString;
            ContinueButton.IsEnabled = true;
        }
        private void EnableOrDisableOptionButtons(bool setState, bool clearButtonContent)
        {
            foreach (Button item in buttons)
            {
                item.IsEnabled = setState;
                if (clearButtonContent) item.Content = "";
            }
        }
        private void EnableOrDisableSubmitButtonAndBox(bool setState)
        {
            SubmitAnswerButton.IsEnabled = setState;
            QuestionAnswer.IsEnabled = setState;
            QuestionAnswer.Text = "";
        }
        private void InitialInstructions()
        {
            ResetQuestionCreatingRelatedBooleans(false, false, false);
            InstructionsAndAnswer.IsEnabled = true;

            List<string> listOfInstructions = handler.LogInstructions().Result;

            Instructions.Text = listOfInstructions[0];

            for (int i = 1; i < listOfInstructions.Count; i++)
            {
                buttons[i - 1].Content = listOfInstructions[i];
            }
            EnableOrDisableOptionButtons(true, false);
            ContinueButton.IsEnabled = false;
        }
        private void PrepareNextQuestion()
        {
            EnableOrDisableOptionButtons(false, true);
            EnableOrDisableSubmitButtonAndBox(false);
            ContinueButton.IsEnabled = true;
        }
        private void RunQuiz()
        {
            Instructions.Text = handler.PerformAction("1")[0][0];
            EnableOrDisableOptionButtons(false, true);
            ContinueButton.IsEnabled = true;
            hasFinishedQuiz = false;
            isTakingQuiz = true;
            indexOfCurrentQuestion = 0;
        }
        private void ResetQuestionCreatingRelatedBooleans(bool isCreatingQuestion, bool isRemovingQuestion, bool isModifyingQuestion)
        {
            this.isCreatingQuestion = isCreatingQuestion;
            this.isRemovingQuestion = isRemovingQuestion;
            this.isModifyingQuestion = isModifyingQuestion;
            hasFinishedCreatingQuestion = false;
            hasCreatedQuestion = false;
            hasTargetedQuestion = false;
            typeOfQuestionToCreate = "";
            numberOfCreatedOption = 1;
            doneWithCreatingQuestion = false;
        }
        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            if (isTakingQuiz && !hasFinishedQuiz)
            {
                if (indexOfCurrentQuestion >= handler.TotalNumberOfQuestions)
                {
                    Instructions.Text = handler.GetQuizResults();
                    hasFinishedQuiz = true;
                    isTakingQuiz = false;
                    ContinueButton.IsEnabled = true;
                }
                else
                {
                    quizQuestion = handler.GetQuestion(indexOfCurrentQuestion);
                    Instructions.Text = quizQuestion.Question;
                    if (quizQuestion.McsaOptions != null)
                    {
                        for (int i = 0; i < quizQuestion.McsaOptions.Count; i++)
                        {
                            buttons[i].Content = quizQuestion.McsaOptions[i];
                        }
                        EnableOrDisableOptionButtons(true, false);
                    }
                    else
                    {                        
                        EnableOrDisableSubmitButtonAndBox(true);
                    }
                    indexOfCurrentQuestion++;
                    ContinueButton.IsEnabled = false;
                }
            }
            else if (hasFinishedCreatingQuestion)
            {
                InitialInstructions();
            }
            else if (!isTakingQuiz && hasFinishedQuiz)
            {
                hasFinishedQuiz = false;
                InitialInstructions();
            }
            else if (isCreatingQuestion)
            {
                Instructions.Text = listWithDetailsOfQuestionActions[!isModifyingQuestion ? 0 : 2][0]; //"What is the question going to be?", when it's time to create question (after modifying or not).
                EnableOrDisableSubmitButtonAndBox(true);
                ContinueButton.IsEnabled = false;
            }
            else if ((isRemovingQuestion || isModifyingQuestion) && !hasTargetedQuestion)
            {
                listWithTargetQuestionDetails = handler.GetListWithTargetQuestionDetails();
                Instructions.Text = listWithTargetQuestionDetails[0];
                foreach (string item in listWithDetailsOfQuestionActions[1])
                {
                    ListOfAllQuestions.Text += item + Environment.NewLine;
                }
                ListOfAllQuestions.Text += listWithTargetQuestionDetails[1];
                ContinueButton.IsEnabled = false;                
                EnableOrDisableSubmitButtonAndBox(true);
            }
            else
            {
                InitialInstructions();
            }
        }
        private void SubmitAnswer_Click(object sender, RoutedEventArgs e)
        {
            if (QuestionAnswer.Text != "")
            {
                if (isTakingQuiz)
                {
                    CheckAnswerAndPrepareNextAction(QuestionAnswer.Text);
                }
                else if (!isCreatingQuestion)
                {
                    numberOfTargetedQuestion = Convert.ToInt32(QuestionAnswer.Text);
                    if (numberOfTargetedQuestion < 1 || numberOfTargetedQuestion > handler.TotalNumberOfQuestions)
                    {
                        return;
                    }
                    ListOfAllQuestions.Text = "";
                    hasTargetedQuestion = true;                    
                    if (isRemovingQuestion)
                    {
                        handler.RemoveOrModifyQuestion(QuestionAnswer.Text);
                        Instructions.Text = "Question has been removed successfully.";
                    }
                    else
                    {
                        Instructions.Text = "Now, please create the question that will replace the previous one";
                        isCreatingQuestion = true;
                    }                    
                    EnableOrDisableSubmitButtonAndBox(false);
                    ContinueButton.IsEnabled = true;
                }
                else if (!hasCreatedQuestion)
                {
                    question = QuestionAnswer.Text;
                    EnableOrDisableSubmitButtonAndBox(false);
                    for (int i = 2; i < listWithDetailsOfQuestionActions[!isModifyingQuestion ? 0 : 2].Count; i++)
                    {
                        buttons[i-2].Content = listWithDetailsOfQuestionActions[!isModifyingQuestion ? 0 : 2][i];
                        buttons[i-2].IsEnabled = true;
                    }
                    hasCreatedQuestion = true;

                    Instructions.Text = listWithDetailsOfQuestionActions[!isModifyingQuestion ? 0 : 2][1];
                }
                else if (typeOfQuestionToCreate == "QuestionCard" || typeOfQuestionToCreate == "MCSACard")
                {
                    if (typeOfQuestionToCreate == "QuestionCard")
                    {
                        questionAnswer = QuestionAnswer.Text;
                        doneWithCreatingQuestion = true;
                    }
                    else
                    {

                        if (numberOfCreatedOption < 6)
                        {
                            questionAnswer = handler.VerifyAnswer(QuestionAnswer.Text, 0, mcsaOptions);
                            if (questionAnswer == handler.AnswerIsNullString)
                            {
                                Instructions.Text = questionAnswer;
                                return;
                            }
                            else
                            {
                                mcsaOptions.Add(QuestionAnswer.Text);
                                numberOfCreatedOption++;
                            }                          
                        }

                        questionAnswer = QuestionAnswer.Text;
                        QuestionAnswer.Clear();

                        if (numberOfCreatedOption < 6)
                        {                            
                            Instructions.Text = "Enter your answer for option " + numberOfCreatedOption;
                            return;
                        }
                        else if (!doneWithCreatingQuestion)
                        {
                            Instructions.Text = "Choose the correct option. " + handler.GetAnswerFormat(numberOfCreatedOption-1);
                            doneWithCreatingQuestion = true;
                            return;
                        }
                        
                        questionAnswer = handler.VerifyAnswer(questionAnswer, numberOfCreatedOption - 1);
                        if (questionAnswer == handler.AnswerIsNullString)
                        {
                            Instructions.Text = questionAnswer + handler.GetAnswerFormat(numberOfCreatedOption-1);
                            return;
                        }                       
                    }

                    if (doneWithCreatingQuestion)
                    {
                        EnableOrDisableSubmitButtonAndBox(false);

                        string stringOfQuestionOrReturnMessage = handler.CreateQuestion(
                                    question, typeOfQuestionToCreate, questionAnswer,
                                    mcsaOptions, isModifyingQuestion);

                        if (isModifyingQuestion)
                        {
                            handler.RemoveOrModifyQuestion(numberOfTargetedQuestion.ToString(), stringOfQuestionOrReturnMessage);
                            Instructions.Text = "Question was modified successfully.";
                        }
                        else
                        {
                            Instructions.Text = "Question was added successfully.";
                        }

                        hasFinishedCreatingQuestion = true;
                        ContinueButton.IsEnabled = true;
                    }
                }               
            }
        }
        private void Option1_Click(object sender, RoutedEventArgs e)
        {
            if (isTakingQuiz)
            {
                CheckAnswerAndPrepareNextAction("1");
                return;
            }
            else if (isCreatingQuestion)
            {
                CreateAnswerForQuestion("1");
            }
            else
            {
                RunQuiz();
            }
        }
        private void Option2_Click(object sender, RoutedEventArgs e)
        {
            if (isTakingQuiz)
            {
                CheckAnswerAndPrepareNextAction("2");
                return;
            }
            else if (isCreatingQuestion && hasTargetedQuestion)
            {
                CreateAnswerForQuestion("2");
            }
            else
            {
                CreateQuestionInit(true, false, false, "2");
            }
        }
        private void Option3_Click(object sender, RoutedEventArgs e)
        {
            if (isTakingQuiz)
            {
                CheckAnswerAndPrepareNextAction("3");
                return;
            }
            else
            {
                CreateQuestionInit(false, true, false, "3");
            }            
        }
        private void Option4_Click(object sender, RoutedEventArgs e)
        {
            if (isTakingQuiz)
            {
                CheckAnswerAndPrepareNextAction("4");
                return;
            }
            else
            {
                CreateQuestionInit(false, false, true, "4");
            }            
        }
        private void Option5_Click(object sender, RoutedEventArgs e)
        {
            if (isTakingQuiz)
            {
                CheckAnswerAndPrepareNextAction("5");
                return;
            }
            else
            {
                Application.Current.Shutdown();
            }
        }
    }
}