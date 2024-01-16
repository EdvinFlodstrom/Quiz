namespace Web_App.Server.Models
{
    public class MCSACardModel : QuestionModel
    {
        public string? Option1 { get; set; }
        public string? Option2 { get; set; }
        public string? Option3 { get; set; }
        public string? Option4 { get; set; }
        public string? Option5 { get; set; }
        public int CorrectOptionNumber { get; set; }

        public override string GetCorrectAnswer()
        {
            return CorrectOptionNumber.ToString();  
        }
    }
}
