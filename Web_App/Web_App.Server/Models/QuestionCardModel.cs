namespace Web_App.Server.Models;

public class QuestionCardModel : QuestionModel
{
    // TODO remove ? make not nullable
    public string? RequiredWords { get; set; }

    public override string GetCorrectAnswer()
    {
        return RequiredWords;
    }
}
