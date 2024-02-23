namespace QuizLibrary
{
    public class QuestionCard
    {
        private string question;
        private string correctAnswer;
        private List<string> mcsaOptions;
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
        public QuestionCard(string question, string correctAnswer, List<string>? mcsaOptions = null)
        {
            this.question = question;
            this.correctAnswer = correctAnswer;
            this.mcsaOptions = mcsaOptions;
        }
        public int CheckQuestionAnswer(string answer)
        {
            int pointsGained = 0;

            string[] splitStr = correctAnswer.Split(' ');

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
                    return 0;
                }
            }
            if (answerTrue)
            {
                return 1;
            }
            return 0;
        }
    }
}