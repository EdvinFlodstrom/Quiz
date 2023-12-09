using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
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
        //ActionHandler handler = new ActionHandler();
        public MainWindow()
        {
            InitializeComponent();
            InstructionsAndAnswer.IsEnabled = true;
            //Instructions.Text = handler.LogInstructions();
        }
        private void SubmitAnswer_Click(object sender, RoutedEventArgs e)
        {
            SubmitAnswerButton.IsEnabled = false;
            QuestionAnswer.Text = "";
        }
    }
}