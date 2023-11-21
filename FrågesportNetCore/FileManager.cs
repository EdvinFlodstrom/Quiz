using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrågesportNetCore
{
    internal class FileManager
    {
        private string pathAndFileName;
        public FileManager()
        {
            string docPath =
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            pathAndFileName = Path.Combine(docPath, "quiz_questions.txt");
    }
        public void AddQuestionToFile(string questionCardString)
        {
            using (StreamWriter outputFile = new StreamWriter(pathAndFileName, true))
            {
                outputFile.WriteLine(questionCardString);
            }
        }
        public List<List<string>> ReadFile()
        {
            using (var sr = new StreamReader(pathAndFileName))
            {
                string row = sr.ReadLine();

                List<List<string>> lines = new List<List<string>>();

                while (row != null)
                {
                    //row == QuestionCard|What is Eyjafjallajökull, and where is it located?|volcano iceland
                    List<string> listOfRow = row.Split('|').ToList(); //Splits into list: "QuestionCard", "[q]" etc.                 
                    lines.Add(listOfRow);
                    row = sr.ReadLine();
                }
                return lines;
            }
        }
    }
}
