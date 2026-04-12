namespace StudentGradeManager;

public class Student
{
    public string Name { get; set; } = null!;

    public int Score1 { get; set; }

    public int Score2 { get; set; }

    public int Score3 { get; set; }

    public double Average { get; set; }

    public string LetterGrade { set; get; } = null!;

    public List<Student> StoredStudents { get; set; } = new List<Student>();

    public Student()
    {
    }

    public Student(string name, int score1, int score2, int score3, double average, string letterGrade)
    {
        Name = name;
        Score1 = score1;
        Score2 = score2;
        Score3 = score3;
        Average = average;
        LetterGrade = letterGrade;
    }

    public double CalculateAverageAndGrade(int score1, int score2, int score3)
    {
        Average = Math.Round((double)(Score1 + Score2 + Score3) / 3, 0);
        return Average;
    }

    // Add a Reset method to clear all properties
    public void Reset()
    {
        Name = string.Empty;
        Score1 = 0;
        Score2 = 0;
        Score3 = 0;
        Average = 0;
        LetterGrade = string.Empty;
    }
}
