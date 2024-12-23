using System;
using System.Collections.Generic;
using System.IO;
public abstract class Question
{
    public string Body { get; set; }
    public int Marks { get; set; }
    public string Header { get; set; }

    public Question(string body, int marks, string header)
    { 
   
        Body = body;
        Marks = marks;
        Header = header;
    }

    public abstract void DisplayQuestion();
}

public class TrueFalseQuestion : Question
{
    public bool CorrectAnswer { get; set; }

    public TrueFalseQuestion(string body, int marks, string header, bool correctAnswer)
        : base(body, marks, header)
    {
        CorrectAnswer = correctAnswer;
    }

    public override void DisplayQuestion()
    {
        Console.WriteLine($"{Header}\n{Body}\nTrue/False");
    }
}

public class ChooseOneQuestion : Question
{
    public List<string> Choices { get; set; }
    public int CorrectAnswerIndex { get; set; }

    public ChooseOneQuestion(string body, int marks, string header, List<string> choices, int correctAnswerIndex)
           : base(body, marks, header)
    {
        Choices = choices;
        CorrectAnswerIndex = correctAnswerIndex;
    }
    public override void DisplayQuestion()
    {
        Console.WriteLine($"{Header}\n{Body}");
        for (int i = 0; i < Choices.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {Choices[i]}");
        }
    }
}

public class ChooseAllQuestion : Question
{
    public List<string> Choices { get; set; }
    public List<int> CorrectAnswerIndexes { get; set; }

    public ChooseAllQuestion(string body, int marks, string header, List<string> choices, List<int> correctAnswerIndexes)
   : base(body, marks, header)
    {
        Choices = choices;
        CorrectAnswerIndexes = correctAnswerIndexes;
    }

    public override void DisplayQuestion()
    {
        Console.WriteLine($"{Header}\n{Body}");
        for (int i = 0; i < Choices.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {Choices[i]}");
        }
    }
}

public class QuestionList : List<Question>
{
    private string _fileName;

    public QuestionList(string fileName)
    {
        _fileName = fileName;
    }

    public new void Add(Question question)
    {
        base.Add(question);
        LogQuestion(question);
    }

    private void LogQuestion(Question question)
    {
        using (StreamWriter writer = new StreamWriter(_fileName, true))
        {
            writer.WriteLine($"Header: {question.Header}, Body: {question.Body}, Marks: {question.Marks}");
        }
    }
}

public class Answer
{
    public Question Question { get; set; }
    public object AnswerGiven { get; set; }

    public Answer(Question question, object answerGiven)
    {
        Question = question;
        AnswerGiven = answerGiven;
    }
}

public class AnswerList : List<Answer>
{
}

public abstract class Exam : ICloneable, IComparable<Exam>
{
    public string Subject { get; set; }
    public TimeSpan Duration { get; set; }
    public List<Question> Questions { get; set; }
    public Dictionary<Question, Answer> QuestionAnswerDict { get; set; }
    public string Mode { get; set; }

    public Exam(string subject, TimeSpan duration)
    {
        Subject = subject;
        Duration = duration;
        Questions = new List<Question>();
        QuestionAnswerDict = new Dictionary<Question, Answer>();
        Mode = "Queued";
    }

    public abstract void ShowExam();

    public object Clone()
    {
        return this.MemberwiseClone();
    }

    public int CompareTo(Exam other)
    {
        return this.Subject.CompareTo(other.Subject);
    }
}

public class PracticeExam : Exam
{
    public PracticeExam(string subject, TimeSpan duration)
        : base(subject, duration) { }

    public override void ShowExam()
    {
        foreach (var question in Questions)
        {
            question.DisplayQuestion();
            if (Mode == "Finished")
            {
                Console.WriteLine($"Answer: {QuestionAnswerDict[question].AnswerGiven}");
            }
        }
    }
}

public class FinalExam : Exam
{
    public FinalExam(string subject, TimeSpan duration)
        : base(subject, duration) { }

    public override void ShowExam()
    {
        foreach (var question in Questions)
        {
            question.DisplayQuestion();
        }
    }
}

public class Subject
{
    public string Name { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }

    public Subject(string name, string code, string description)
    {
        Name = name;
        Code = code;
        Description = description;
    }
}
public class Student
{
    public string Name { get; set; }

    public Student(string name)
    {
        Name = name;
    }

    public void OnExamStarted(object sender, EventArgs e)
    {
        Console.WriteLine($"{Name} has been notified that the exam has started.");
    }
}
public class ExamEventArgs : EventArgs
{
    public string ExamName { get; set; }

    public ExamEventArgs(string examName)
    {
        ExamName = examName;
    }
}

public class ExamNotificationSystem
{
    public event EventHandler<ExamEventArgs> ExamStarted;

    public void StartExam(string examName)
    {
        OnExamStarted(new ExamEventArgs(examName));
    }

    protected virtual void OnExamStarted(ExamEventArgs e)
    {
        ExamStarted?.Invoke(this, e);
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        var subject = new Subject("Astronomy", "AST101", "Exploring the mysteries of the universe and celestial bodies.");

        var practiceExam = new PracticeExam(subject.Name, new TimeSpan(1, 30, 0));
        var finalExam = new FinalExam(subject.Name, new TimeSpan(2, 0, 0));

        var student1 = new Student("Elara Moonstone");
        var student2 = new Student("Orion Nebula");

        var notificationSystem = new ExamNotificationSystem();
        notificationSystem.ExamStarted += student1.OnExamStarted;
        notificationSystem.ExamStarted += student2.OnExamStarted;

        practiceExam.Questions.Add(new TrueFalseQuestion("Is the Milky Way galaxy the only galaxy in the universe?", 2, "Galaxy Exploration", false));
        finalExam.Questions.Add(new ChooseOneQuestion("What is the closest planet to the Sun?", 3, "Solar System Quiz",
            new List<string> { "Earth", "Venus", "Mercury", "Mars" }, 3));

        practiceExam.Questions.Add(new ChooseAllQuestion("Select the planets with rings.", 5, "Planetary Rings",
            new List<string> { "Saturn", "Jupiter", "Earth", "Neptune" }, new List<int> { 1, 2, 4 }));


        Console.WriteLine("Choose Exam Type: 1. Practice Exam 2. Final Exam");
        var choice = Console.ReadLine();

        if (choice == "1")
        {
            practiceExam.ShowExam();
            notificationSystem.StartExam("Practice Exam");
        }
        else if (choice == "2")
        {
            finalExam.ShowExam();
            notificationSystem.StartExam("Final Exam");
        }
    }
}