using Newtonsoft.Json;

namespace TriviaBot.App.Models;

public class ResultModel
{
    public string Category { get; set; }
    public string Question { get; set; }
    public string Difficulty { get; set; }
    public string Correct_Answer { get; set; }
    public List<string> Incorrect_Answers { get; set; }
    public List<string> AllAnswers
    {
        get
        {
            var allAnswers = new List<string>();
            allAnswers.Add(Correct_Answer);
            allAnswers.AddRange(Incorrect_Answers);
            
            return allAnswers;
        }
    }
}