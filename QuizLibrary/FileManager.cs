namespace QuizLibrary
{
    public class FileManager : IManager
    {
        private string pathAndFileName;
        public FileManager()
        {
            string docPath =
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            pathAndFileName = Path.Combine(docPath, "quiz_questions.txt");
    }
        public bool AddQuestion(string questionCardString)
        {
            foreach (QuestionCard item in Read())
            {
                if (questionCardString.ToLower().Contains(item.Question.ToLower()))
                {
                    return false; //Quiz does not allow you to add a question that already exists.
                }
            }
            using (StreamWriter outputFile = new StreamWriter(pathAndFileName, true))
            {
                outputFile.WriteLine(questionCardString);
            }            
            return true;
        }
        public List<QuestionCard> Read()
        {
            using (var sr = new StreamReader(pathAndFileName))
            {
                string row = sr.ReadLine();

                List<List<string>> lines = new List<List<string>>();

                while (row != null)
                {
                    List<string> listOfRow = row.Split('|').ToList(); //Splits into list: "QuestionCard", "[question]" etc.
                    lines.Add(listOfRow);
                    row = sr.ReadLine();
                }

                List<QuestionCard> listOfAllCards = new List<QuestionCard>();
                foreach (List<string> line in lines)
                {
                    if (line[0] == "QuestionCard")
                    {
                        listOfAllCards.Add(new QuestionCard(line[1], line[2]));
                    }
                    else if (line[0] == "MCSACard")
                    {
                        listOfAllCards.Add(new MCSACard(line[1], line[2], line[3].Split(',').ToList()));
                    }
                }
                return listOfAllCards;
            }
        }
        public void RemoveOrModifyQuestion(int numberOfQuestion, string modifiedQuestion = "")
        {
            string newStringOfQuestions = "";
            int indexOfCurrentItem = 0;

            using (var sr = new StreamReader(pathAndFileName))
            {
                string row = sr.ReadLine();

                List<string> lines = new List<string>();

                while (row != null)
                {                                        
                    lines.Add(row);
                    row = sr.ReadLine();
                }

                foreach (string item in lines)
                {                    
                    if (numberOfQuestion != indexOfCurrentItem++)
                    {
                        newStringOfQuestions += item + Environment.NewLine;
                    }
                    else
                    {
                        if (modifiedQuestion != "")
                        {
                            newStringOfQuestions += modifiedQuestion + Environment.NewLine;
                        }                        
                    }                    
                }
            }
            File.WriteAllText(pathAndFileName, newStringOfQuestions);
        }
    }
}