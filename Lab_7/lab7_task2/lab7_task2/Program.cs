using System;

class Person
{
    private string firstName;
    private string lastName;
    private string patronymic;

    public string FirstName => firstName;
    public string LastName => lastName;
    public string Patronymic => patronymic;

    public Person(string firstName, string lastName, string patronymic)
    {
        this.firstName = firstName;
        this.lastName = lastName;
        this.patronymic = patronymic;
    }

    public virtual void GetPersonInfo()
    {
        Console.WriteLine($"ФИО: {LastName} {FirstName} {Patronymic}");
    }
}

class Sportsman : Person
{
    private int id;
    private double result;

    public int ID => id;
    public double Result => result;

    public Sportsman(string firstName, string lastName, string patronymic, int id, double result) : base(firstName, lastName, patronymic)
    {
        this.id = id;
        this.result = result;
    }

    public override void GetPersonInfo()
    {
        Console.WriteLine($"ФИО: {LastName} {FirstName} {Patronymic} | ID: {ID} | Баллы: {Result}");
    }
}

class Program
{
    static void Main()
    {
        Sportsman[] sportiks = new Sportsman[5];
        sportiks[0] = new Sportsman("Павел", "Попов", "Александрович", 1, 5.5);
        sportiks[1] = new Sportsman("Михаил", "Оганезов", "Алексеевич", 2, 6);
        sportiks[2] = new Sportsman("Тигран", "Мачкалян", "Норайрович", 3, 4.5);
        sportiks[3] = new Sportsman("Федор", "Ершов", "Дмитриевич", 4, 2.5);
        sportiks[4] = new Sportsman("Роман", "Александров", "Сергеевич", 5, 10);

        Array.Sort(sportiks, (x, y) => y.Result.CompareTo(x.Result));

        foreach (var sportsman in sportiks)
        {
            sportsman.GetPersonInfo();
        }
    }
}
