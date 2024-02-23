namespace QuizLibrary
{
    internal class MCSACard : QuestionCard
    {
        private string question;
        private string correctAnswer;
        private List<string> options = new List<string>();
        public MCSACard(string question, string correctAnswer, List<string> options) :
            base(question, correctAnswer, options)
        {
            this.question = question;
            this.correctAnswer = correctAnswer;
            this.options = options;
        }
    }
}