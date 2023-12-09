namespace QuizLibrary
{
    public class FileManager
    {
        private string pathAndFileName;
        public FileManager()
        {
            string docPath =
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            pathAndFileName = Path.Combine(docPath, "quiz_questions.txt");
    }
        public bool AddQuestionToFile(string questionCardString)
        {
            foreach (List<string> item in ReadFile())
            {
                if (questionCardString.ToLower().Contains(item[1].ToLower()))
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
        public List<List<string>> ReadFile()
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
                return lines;
            }
        }
        public void RemoveOrModifyQuestion(int numberOfQuestion, string modifiedQuestion = "")
        {
            numberOfQuestion--;
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