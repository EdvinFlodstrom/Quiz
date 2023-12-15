using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using QuizLibrary;

namespace WPF_GUI_Question
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>  
    public partial class MainWindow : Window
    {        
        InterfaceHandler handler = new InterfaceHandler();
        List<Button> buttons = new List<Button>();
        QuestionCard quizQuestion;
        List<List<string>> listWithDetailsOfQuestionActions;        
        int indexOfCurrentQuestion = 0;
        bool isTakingQuiz = false;
        bool hasFinishedQuiz = false;
        bool isCreatingQuestion = false; //TODO: Set ALL of the following, until //, to false/1 when question creation/modification/removal is done...
        bool isModifyingQuestion = false;
        bool isRemovingQuestion = false;
        bool hasCreatedQuestion = false;
        bool hasTargetedQuestion = false;
        string typeOfQuestionToCreate = "";
        int numberOfCreatedOption = 1;
        bool hasCreatedAnswer = false; //

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
        private void CreateQuestionInit(bool isCreatingQuestion, bool isRemovingQuestion, bool isModifyingQuestion, string numberOfActionToPerform)
        {            
            this.isCreatingQuestion = isCreatingQuestion;
            this.isModifyingQuestion = isRemovingQuestion;
            this.isRemovingQuestion = isModifyingQuestion;
            hasCreatedQuestion = false;
            hasTargetedQuestion = false;
            typeOfQuestionToCreate = "";
            numberOfCreatedOption = 1;
            hasCreatedAnswer = false;

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
        private void InitialInstructions()
        {
            InstructionsAndAnswer.IsEnabled = true;

            List<string> listOfInstructions = handler.LogInstructions();

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
            QuestionAnswer.IsEnabled = false;
            QuestionAnswer.Text = "";
            SubmitAnswerButton.IsEnabled = false;
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
                        QuestionAnswer.IsEnabled = true;
                        SubmitAnswerButton.IsEnabled = true;
                    }
                    indexOfCurrentQuestion++;
                    ContinueButton.IsEnabled = false;
                }
            }
            else if (!isTakingQuiz && hasFinishedQuiz)
            {
                hasFinishedQuiz = false;
                InitialInstructions();
            }
            else if (isCreatingQuestion)
            {
                Instructions.Text = listWithDetailsOfQuestionActions[!isModifyingQuestion ? 0 : 2][0]; //Check this.
            }
        }
        private void SubmitAnswer_Click(object sender, RoutedEventArgs e)
        {
            if (QuestionAnswer.Text != "")
            {
                CheckAnswerAndPrepareNextAction(QuestionAnswer.Text);
            }
        }
        private void Option1_Click(object sender, RoutedEventArgs e)
        {
            if (isTakingQuiz)
            {
                CheckAnswerAndPrepareNextAction("1");
                return;
            }            
            RunQuiz();
        }
        private void Option2_Click(object sender, RoutedEventArgs e)
        {
            if (isTakingQuiz)
            {
                CheckAnswerAndPrepareNextAction("2");
                return;
            }
            else if (isCreatingQuestion && !hasTargetedQuestion)
            {

            }




            //If user wants to create question.
            CreateQuestionInit(true, false, false, "2");
        }
        private void Option3_Click(object sender, RoutedEventArgs e)
        {
            if (isTakingQuiz)
            {
                CheckAnswerAndPrepareNextAction("3");
                return;
            }



            CreateQuestionInit(false, true, false, "3");
        }
        private void Option4_Click(object sender, RoutedEventArgs e)
        {
            if (isTakingQuiz)
            {
                CheckAnswerAndPrepareNextAction("4");
                return;
            }




            CreateQuestionInit(false, false, true, "4");
        }
        private void Option5_Click(object sender, RoutedEventArgs e)
        {
            if (isTakingQuiz)
            {
                CheckAnswerAndPrepareNextAction("5");
                return;
            }
            Application.Current.Shutdown();
        }
    }
}