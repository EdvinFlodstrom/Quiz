namespace QuizLibrary
{
    public class QuestionCard
    {
        private string question;
        private string correctAnswer;
        private List<string> mcsaOptions;
        public string CorrectAnswer
        {
            get
            {
                return correctAnswer;
            }
        }
        public string Question
        {
            get
            {
                return question;
            }
        }
        public List<string> McsaOptions
        {
            get
            {
                return mcsaOptions;
            }
        }
        public QuestionCard(string question, string correctAnswer, List<string> mcsaOptions = null)
        {
            this.question = question;
            this.correctAnswer = correctAnswer;
            this.mcsaOptions = mcsaOptions;
        }
    }
}