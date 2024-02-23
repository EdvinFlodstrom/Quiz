namespace Web_App.Server.Models;

public class PlayerStatisticsModel
{
    public string PlayerName { get; set; }
    
    public int CorrectAnswers { get; set; }
    
    public int NumberOfCurrentQuestion { get; set; }
    
    public string ListOfQuestionIds { get; set; }
}
