using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;

abstract class Task
{
    public abstract string Process(string input);
}

class LetterFrequencyTask : Task
{
    public override string Process(string input)
    {
        Dictionary<char, double> frequencyMap = new Dictionary<char, double>();
        int totalLetters = 0;

        foreach (char c in input)
        {
            if (char.IsLetter(c))
            {
                totalLetters++;
                char lowerCaseChar = char.ToLower(c);
                if (frequencyMap.ContainsKey(lowerCaseChar))
                {
                    frequencyMap[lowerCaseChar]++;
                }
                else
                {
                    frequencyMap.Add(lowerCaseChar, 1);
                }
            }
        }

        foreach (var key in frequencyMap.Keys.ToList())
        {
            frequencyMap[key] /= totalLetters;
        }

        var sortedMap = frequencyMap.OrderByDescending(pair => pair.Value);

        StringBuilder result = new StringBuilder();
        foreach (var pair in sortedMap)
        {
            result.AppendLine($"Буква '{pair.Key}': {pair.Value:P2}");
        }

        return result.ToString();
    }
}

class LineBreakTask : Task
{
    public override string Process(string input)
    {
        const int maxLineLength = 50;

        string[] words = input.Split(' ');
        StringBuilder formattedText = new StringBuilder();
        int currentLineLength = 0;

        foreach (string word in words)
        {
            if (currentLineLength + word.Length + 1 <= maxLineLength)
            {
                formattedText.Append(word + " ");
                currentLineLength += word.Length + 1;
            }
            else
            {
                formattedText.AppendLine();
                formattedText.Append(word + " ");
                currentLineLength = word.Length + 1;
            }
        }

        return formattedText.ToString();
    }
}
class StartingLettersFrequencyTask : Task
{
    public override string Process(string input)
    {
        Dictionary<char, int> frequencyMap = new Dictionary<char, int>();

        string[] words = input.Split(' ');
        foreach (string word in words)
        {
            if (!string.IsNullOrEmpty(word))
            {
                char firstLetter = char.ToLower(word[0]);
                if (char.IsLetter(firstLetter))
                {
                    if (frequencyMap.ContainsKey(firstLetter))
                    {
                        frequencyMap[firstLetter]++;
                    }
                    else
                    {
                        frequencyMap.Add(firstLetter, 1);
                    }
                }
            }
        }

        var sortedMap = frequencyMap.OrderByDescending(pair => pair.Value);

        StringBuilder result = new StringBuilder();
        foreach (var pair in sortedMap)
        {
            result.AppendLine($"Буква '{pair.Key}': {pair.Value}");
        }

        return result.ToString();
    }
}

class WordSequenceTask : Task
{
    public override string Process(string input)
    {
        Console.WriteLine("Введите последовательность букв:");
        string sequence = Console.ReadLine().ToLower();

        string[] words = input.Split(' ');
        List<string> matchedWords = new List<string>();

        foreach (string word in words)
        {
            if (word.ToLower().Contains(sequence))
            {
                matchedWords.Add(word);
            }
        }

        return string.Join(", ", matchedWords);
    }
}

class SurnameSortingTask : Task
{
    public override string Process(string input)
    {
        string[] surnames = input.Split(',').Select(s => s.Trim()).OrderBy(s => s).ToArray();
        return string.Join(", ", surnames);
    }
}

class NumberSumTask : Task
{
    public override string Process(string input)
    {
        int sum = 0;

        string[] words = input.Split(' ');
        foreach (string word in words)
        {
            if (int.TryParse(word, out int number))
            {
                if (number >= 1 && number <= 10)
                {
                    sum += number;
                }
            }
        }

        return $"Сумма чисел от 1 до 10 в тексте: {sum}";
    }
}
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Выберите задание: ");
        Console.WriteLine("1) Определить частоту букв в тексте.");
        Console.WriteLine("2) Разбить исходный текст на строки длиной не более 50 символов.");
        Console.WriteLine("3) Вывести буквы, на которые начинаются слова в тексте, в порядке убывания их употребления.");
        Console.WriteLine("4) Вывести слова, содержащие заданную последовательность букв.");
        Console.WriteLine("5) Упорядочить список фамилий по алфавиту.");
        Console.WriteLine("6) Найти сумму чисел от 1 до 10 в тексте.");
        Console.Write("Ваш выбор: ");

        int choice = int.Parse(Console.ReadLine());

        Task task;
        switch (choice)
        {
            case 1:
                task = new LetterFrequencyTask();
                break;
            case 2:
                task = new LineBreakTask();
                break;
            case 3:
                task = new StartingLettersFrequencyTask();
                break;
            case 4:
                task = new WordSequenceTask();
                break;
            case 5:
                task = new SurnameSortingTask();
                break;
            case 6:
                task = new NumberSumTask();
                break;
            default:
                Console.WriteLine("Недопустимый выбор.");
                return;
        }

        Console.Write("Введите текст: ");
        string input = Console.ReadLine();

        string result = task.Process(input);
        Console.WriteLine($"Результат:\n{result}");
    }
}

