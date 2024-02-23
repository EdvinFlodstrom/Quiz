namespace QuizLibrary
{
    public interface IManager
    {
        bool AddQuestion(string questionCardString);
        List<QuestionCard> Read();
        void RemoveOrModifyQuestion(int numberOfQuestion, string modifiedQuestion = "");
    }
}