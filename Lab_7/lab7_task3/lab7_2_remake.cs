using System;

class Person
{
    private static int nextID = 1;

    private string firstName;
    private string lastName;
    private string patronymic;
    private int id;

    public string FirstName => firstName;
    public string LastName => lastName;
    public string Patronymic => patronymic;
    public int ID => id;

    public Person(string firstName, string lastName, string patronymic)
    {
        this.firstName = firstName;
        this.lastName = lastName;
        this.patronymic = patronymic;
        this.id = nextID++;
    }

    public virtual void GetPersonInfo()
    {
        Console.WriteLine($"ФИО: {LastName} {FirstName} {Patronymic} | ID: {ID}");
    }
}

class Sportsman : Person
{
    private double result;

    public double Result => result;

    public Sportsman(string firstName, string lastName, string patronymic, double result) : base(firstName, lastName, patronymic)
    {
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
        sportiks[0] = new Sportsman("Павел", "Попов", "Александрович", 5.5);
        sportiks[1] = new Sportsman("Михаил", "Оганезов", "Алексеевич", 6);
        sportiks[2] = new Sportsman("Тигран", "Мачкалян", "Норайрович", 4.5);
        sportiks[3] = new Sportsman("Федор", "Ершов", "Дмитриевич", 2.5);
        sportiks[4] = new Sportsman("Роман", "Александров", "Сергеевич", 10);

        foreach (var sportsman in sportiks)
        {
            sportsman.GetPersonInfo();
        }
    }
}
