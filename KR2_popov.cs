using System;
using System.Runtime.CompilerServices;

class Task1
{
    private string text_1;

    public Task1(string text_1)
    {
        this.text_1 = text_1;
        FindMaxFollowingChars(text_1);
    }
    public void FindMaxFollowingChars(string text_1)
    {
        int maxcount = 0;
        int count = 0;
        int length = text_1.Length;
        for (int i = 0; i<length; i++)
        {
            for (int j = 0; j<length; j++)
            {
                if (text_1[i] == text_1[j])
                {
                    count++;

                    if (count > maxcount) maxcount = count;
                }
                else count = 0;
            }
        }
        Console.WriteLine(maxcount);
    }
}
class Task2
{
    private string text_2;

    public Task2(string text_2)
    {
        this.text_2 = text_2;
        PrintLastWordsInText(text_2);
    }
    public void PrintLastWordsInText(string text_2)
    {
        int lengthcount = 0;
        int length2count = 0;
        for (int i = 0; i < text_2.Length; i++)
        {
            if (text_2[i] == '.')
            {
                lengthcount++;
            }
            if (text_2[i] == ' ')
            {
                length2count++;
            }
        }
        string[] lastWordList = new string[lengthcount];


        string[] splitedText = new string[length2count];
        for (int i = 0; i < length2count; i++)
        {
            splitedText[i] = text_2.Split(' ')[i];
        }
        int index = 0;
        for (int i = 0; i<length2count; i++)
        {
            if (splitedText[i].EndsWith('.'))
            {
                lastWordList[index] = splitedText[i];
            }
        }
        foreach (var elem in lastWordList)
        {
            Console.WriteLine(elem + " ");
        }
        Console.WriteLine();
    }
}



class Program
{
    static void Main()
    {
        Task1 task1 = new Task1("abbcaaaff");
        Task2 task2 = new Task2("В девять часов из Парижа выходит специальный поезд, отвозящий в Гавр пассажиров Нормандии. Поезд идет без остановок и через три часа вкатывается в здание гаврского морского вокзала. Пассажиры выходят на закрытый перрон, подымаются на верхний этаж вокзала по эскалатору, проходят несколько зал, идут по закрытым со всех сторон сходням и оказываются в большом вестибюле. Здесь они садятся в лифты и разъезжаются по своим этажам. Это уже Нормандия. Каков ее внешний вид – пассажирам неизвестно, потому что парохода они так и не увидели.");
        //я не знаю почему у меня выводит только одно слово из текста(( 
    }

}
