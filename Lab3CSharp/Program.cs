using System;
using System.Collections.Generic;
using System.Linq;

// task 1.2
class Triangle
{
    protected int a, b, c;
    protected int color;
    public Triangle(int a, int b, int c, int color)
    {
        this.a = a;
        this.b = b;
        this.c = c;
        this.color = color;
    }

    public int A
    {
        get => a;
        set
        {
            if (IsValidTriangle(value, b, c))
                a = value;
            else
                Console.WriteLine("Невірне значення сторони A — трикутник не існуватиме.");
        }
    }

    public int B
    {
        get => b;
        set
        {
            if (IsValidTriangle(a, value, c))
                b = value;
            else
                Console.WriteLine("Невірне значення сторони B — трикутник не існуватиме.");
        }
    }

    public int C
    {
        get => c;
        set
        {
            if (IsValidTriangle(a, b, value))
                c = value;
            else
                Console.WriteLine("Невірне значення сторони C — трикутник не існуватиме.");
        }
    }

    public int Color => color;

    public void PrintSides()
    {
        Console.WriteLine($"  Сторони: a={a}, b={b}, c={c}, колір={color}");
    }

    public int Perimeter() => a + b + c;

    public double Area()
    {
        double s = Perimeter() / 2.0;
        return Math.Sqrt(s * (s - a) * (s - b) * (s - c));
    }

    private bool IsValidTriangle(int a, int b, int c)
    {
        return a > 0 && b > 0 && c > 0
            && a + b > c
            && a + c > b
            && b + c > a;
    }

    public override string ToString() =>
        $"Triangle(a={a}, b={b}, c={c}, колір={color}, " +
        $"P={Perimeter()}, S={Area():F2})";
} 

// ЗАВДАННЯ 2.7
class Trial
{
    public string Subject { get; set; }
    public string Date { get; set; }

    public Trial(string subject, string date)
    {
        Subject = subject;
        Date = date;
    }

    public virtual void Show()
    {
        Console.WriteLine($"[Trial] Предмет: {Subject}, Дата: {Date}");
    }
}

// Тест
class Test : Trial
{
    public int Questions { get; set; }
    public int PassingScore { get; set; }

    public Test(string subject, string date, int questions, int passingScore)
        : base(subject, date)
    {
        Questions = questions;
        PassingScore = passingScore;
    }

    public override void Show()
    {
        Console.WriteLine($"[Test] Предмет: {Subject}, Дата: {Date}, " +
                          $"Питань: {Questions}, Прохідний бал: {PassingScore}");
    }
}

class Exam : Trial
{
    public string Examiner { get; set; }
    public int MaxGrade { get; set; }

    public Exam(string subject, string date, string examiner, int maxGrade)
        : base(subject, date)
    {
        Examiner = examiner;
        MaxGrade = maxGrade;
    }

    public override void Show()
    {
        Console.WriteLine($"[Exam] Предмет: {Subject}, Дата: {Date}, " +
                          $"Екзаменатор: {Examiner}, Макс. оцінка: {MaxGrade}");
    }
}

class FinalExam : Exam
{
    public string Commission { get; set; }

    public FinalExam(string subject, string date, string examiner,
                     int maxGrade, string commission)
        : base(subject, date, examiner, maxGrade)
    {
        Commission = commission;
    }

    public override void Show()
    {
        Console.WriteLine($"[FinalExam] Предмет: {Subject}, Дата: {Date}, " +
                          $"Екзаменатор: {Examiner}, Макс. оцінка: {MaxGrade}, " +
                          $"Комісія: {Commission}");
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // ЗАВДАННЯ 1.2    
        Console.WriteLine("   Завдання 1.2: Масив трикутників   \n");

        Triangle[] triangles =
        {
            new Triangle(3, 4, 5, 2),
            new Triangle(5, 5, 5, 1),
            new Triangle(6, 8, 10, 3),
            new Triangle(7, 7, 5, 2),
            new Triangle(9, 12, 15, 1),
            new Triangle(4, 4, 6, 3),
        };

        Console.WriteLine("-- За кольором --");
        foreach (var t in triangles.OrderBy(t => t.Color))
            t.PrintSides();

        Console.WriteLine("\n-- За площею --");
        foreach (var t in triangles.OrderBy(t => t.Area()))
            Console.WriteLine($"  S={t.Area():F2}  →  {t}");

        Console.WriteLine("\n-- За периметром --");
        foreach (var t in triangles.OrderBy(t => t.Perimeter()))
            Console.WriteLine($"  P={t.Perimeter()}  →  {t}");

        Console.WriteLine("\n-- Демонстрація властивостей --");
        Triangle demo = triangles[0];
        Console.WriteLine($"Колір (тільки читання): {demo.Color}");
        Console.Write("Змінюємо сторону A=100 (неможливо): ");
        demo.A = 100; // Порушує нерівність трикутника
        Console.Write("Змінюємо сторону A=6 (допустимо): ");
        demo.A = 6;
        demo.PrintSides();

        // ЗАВДАННЯ 2.7    
        Console.WriteLine("\n   Завдання 2.7: Ієрархія (Тест, Іспит, Випускний іспит, Випробування)   \n");

        Trial[] trials = FillTrials();

        Console.WriteLine("-- Впорядковано за типами --");
        var sorted = trials
            .OrderBy(t => t.GetType().Name)
            .ThenBy(t => t.Subject);

        foreach (var item in sorted)
            item.Show();
    }

    static Trial[] FillTrials()
    {
        return new Trial[]
        {
            new Test("Математика", "10.03.2026", 30, 60),
            new Exam("Фізика", "15.03.2026", "Іванов І.І.", 100),
            new FinalExam("Програмування", "20.06.2026", "Петров П.П.", 100, "Комісія №3"),
            new Trial("Психологічне тестування", "05.04.2026"),
            new Test("Англійська мова", "12.03.2026", 50, 70),
            new Exam("Хімія", "18.03.2026", "Сидорова С.С.", 100),
            new FinalExam("Алгоритми", "25.06.2026", "Коваль К.К.", 100, "Комісія №1"),
            new Trial("Медичний огляд", "01.03.2026"),
        };
    }
}
