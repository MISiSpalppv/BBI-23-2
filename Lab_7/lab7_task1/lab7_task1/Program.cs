using System;

class Athlete
{
    private string lastName;
    private string club;
    private double firstAttempt;
    private double secondAttempt;
    private bool disqualified;

    public string LastName => lastName;
    public string Club => club;
    public double FirstAttempt => firstAttempt;
    public double SecondAttempt => secondAttempt;
    public double TotalDistance => firstAttempt + secondAttempt;
    public bool Disqualified => disqualified;

    public Athlete(string lastName, string club, double firstAttempt, double secondAttempt)
    {
        this.lastName = lastName;
        this.club = club;
        this.firstAttempt = firstAttempt;
        this.secondAttempt = secondAttempt;
        this.disqualified = false;
    }

    public void Disqualify()
    {
        disqualified = true;
    }

    public string GetAthleteInfo(int place)
    {
        if (disqualified)
        {
            return $"Участник {lastName} дисквалифицирован\n";
        }
        else
        {
            return $"Место {place} | Фамилия {lastName}\n";
        }
    }
}

class Program
{
    static void Main()
    {
        Athlete[] athletes = new Athlete[5];

        athletes[0] = new Athlete("Иванов", "Спартак", 5.6, 6.2);
        athletes[1] = new Athlete("Петров", "ЦСК", 6.0, 5.8);
        athletes[2] = new Athlete("Сидоров", "Локомотив", 6.5, 6.4);
        athletes[3] = new Athlete("Волчков", "Зенит", 6.6, 6.9);
        athletes[4] = new Athlete("Джугашвили", "СССР", 2.1, 3.4);

        athletes[1].Disqualify();
        athletes[4].Disqualify();

        Array.Sort(athletes, (x, y) =>
        {
            if (x.Disqualified && !y.Disqualified)
                return 1;
            else if (!x.Disqualified && y.Disqualified)
                return -1;
            else
                return x.TotalDistance.CompareTo(y.TotalDistance);
        });

        for (int i = 0; i < athletes.Length; i++)
        {
            Console.Write(athletes[i].GetAthleteInfo(i + 1));
        }

        Console.WriteLine();
    }
}
