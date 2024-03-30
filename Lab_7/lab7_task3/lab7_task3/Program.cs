using System;
using System.Linq;

class Sportsman
{
    protected string surname;
    protected int result;

    public int Result => result;

    public Sportsman(string surname, int result)
    {
        this.surname = surname;
        this.result = result;
    }

    public virtual string GetTable()
    {
        return $"{surname,-15} {result,-10}\n";
    }
}

class SkierWoman : Sportsman
{
    public SkierWoman(string surname, int result) : base(surname, result)
    {
    }
}

class SkierMan : Sportsman
{
    public SkierMan(string surname, int result) : base(surname, result)
    {
    }
}

class Program
{
    static Sportsman[] MergeArrays(Sportsman[] first, Sportsman[] second)
    {
        int totalLength = first.Length + second.Length;
        Sportsman[] merged = new Sportsman[totalLength];

        int i = 0, j = 0, k = 0;

        while (i < first.Length && j < second.Length)
        {
            if (first[i].Result <= second[j].Result)
            {
                merged[k++] = first[i++];
            }
            else
            {
                merged[k++] = second[j++];
            }
        }

        while (i < first.Length)
        {
            merged[k++] = first[i++];
        }

        while (j < second.Length)
        {
            merged[k++] = second[j++];
        }

        return merged;
    }

    static void Main()
    {
        SkierWoman[] first_group_women = new SkierWoman[5];
        first_group_women[0] = new SkierWoman("Горбачева", 4);
        first_group_women[1] = new SkierWoman("Щербакова", 6);
        first_group_women[2] = new SkierWoman("Маковская", 2);
        first_group_women[3] = new SkierWoman("Ермакова", 8);
        first_group_women[4] = new SkierWoman("Семиохина", 12);

        Array.Sort(first_group_women, (x, y) => x.Result.CompareTo(y.Result));

        SkierWoman[] second_group_women = new SkierWoman[5];
        second_group_women[0] = new SkierWoman("Леванова", 4);
        second_group_women[1] = new SkierWoman("Левина", 6);
        second_group_women[2] = new SkierWoman("Шашкина", 12);
        second_group_women[3] = new SkierWoman("Шадрина", 8);
        second_group_women[4] = new SkierWoman("Немова", 2);

        Array.Sort(second_group_women, (x, y) => x.Result.CompareTo(y.Result));

        Sportsman[] all_group_women = MergeArrays(first_group_women, second_group_women);

        Console.WriteLine("Лыжницы - Группа 1");
        foreach (var woman in first_group_women)
        {
            Console.WriteLine(woman.GetTable());
        }

        Console.WriteLine("\nЛыжницы - Группа 2");
        foreach (var woman in second_group_women)
        {
            Console.WriteLine(woman.GetTable());
        }

        Console.WriteLine("\nОбщая таблица для лыжниц");
        foreach (var woman in all_group_women)
        {
            Console.WriteLine(woman.GetTable());
        }

        SkierMan[] first_group_men = new SkierMan[5];
        first_group_men[0] = new SkierMan("Иванов", 4);
        first_group_men[1] = new SkierMan("Петров", 6);
        first_group_men[2] = new SkierMan("Сидоров", 2);
        first_group_men[3] = new SkierMan("Орлов", 8);
        first_group_men[4] = new SkierMan("Клестов", 12);

        Array.Sort(first_group_men, (x, y) => x.Result.CompareTo(y.Result));

        SkierMan[] second_group_men = new SkierMan[5];
        second_group_men[0] = new SkierMan("Ирвинг", 4);
        second_group_men[1] = new SkierMan("Зубастик", 6);
        second_group_men[2] = new SkierMan("Волков", 2);
        second_group_men[3] = new SkierMan("Дзюба", 8);
        second_group_men[4] = new SkierMan("Пеле", 12);

        Array.Sort(second_group_men, (x, y) => x.Result.CompareTo(y.Result));

        Sportsman[] all_group_men = MergeArrays(first_group_men, second_group_men);

        Console.WriteLine("\n\nЛыжники - Группа 1");
        foreach (var man in first_group_men)
        {
            Console.WriteLine(man.GetTable());
        }

        Console.WriteLine("\nЛыжники - Группа 2");
        foreach (var man in second_group_men)
        {
            Console.WriteLine(man.GetTable());
        }

        Console.WriteLine("\nОбщая таблица для лыжников");
        foreach (var man in all_group_men)
        {
            Console.WriteLine(man.GetTable());
        }

        Console.WriteLine("\n\nОбщая таблица для всех участников");
        Sportsman[] all_group = MergeArrays(all_group_women, all_group_men);
        foreach (var participant in all_group)
        {
            Console.WriteLine(participant.GetTable());
        }
    }
}
