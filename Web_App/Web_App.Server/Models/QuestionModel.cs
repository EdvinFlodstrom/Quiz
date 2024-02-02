namespace Web_App.Server.Models
{
    public class QuestionModel
    {
        public int QuestionId { get; set; }
        public string? QuestionText { get; set; }
        public string? QuestionType { get; set; }

        public virtual string GetCorrectAnswer()
        {
            return "";
        }
        public int CheckQuestionAnswer(string answer)
        {
            string[] splitStr = GetCorrectAnswer().Split(' ');

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
