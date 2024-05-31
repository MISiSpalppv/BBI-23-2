using System;
using System.IO;
using System.Text.Json;
using System.Xml.Serialization;
using ProtoBuf;

public abstract class Serializer<T>
{
    public abstract void Serialize(T obj, string filePath);
    public abstract T Deserialize(string filePath);
}

public class JSONSerializer<T> : Serializer<T>
{
    public override void Serialize(T obj, string filePath)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        var json = JsonSerializer.Serialize(obj, options);
        File.WriteAllText(filePath, json);
    }

    public override T Deserialize(string filePath)
    {
        var json = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<T>(json);
    }
}

public class XMLSerializer<T> : Serializer<T>
{
    public override void Serialize(T obj, string filePath)
    {
        var serializer = new XmlSerializer(typeof(T));
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            serializer.Serialize(stream, obj);
        }
    }

    public override T Deserialize(string filePath)
    {
        var serializer = new XmlSerializer(typeof(T));
        using (var stream = new FileStream(filePath, FileMode.Open))
        {
            return (T)serializer.Deserialize(stream);
        }
    }
}

public class BinarySerializer<T>
{
    public void Serialize(T obj, string filePath)
    {
        using (var file = File.Create(filePath))
        {
            ProtoBuf.Serializer.Serialize(file, obj);
        }
    }

    public T Deserialize(string filePath)
    {
        using (var file = File.OpenRead(filePath))
        {
            return ProtoBuf.Serializer.Deserialize<T>(file);
        }
    }
}

[ProtoContract]
[ProtoInclude(100, typeof(SkierWoman))]
[ProtoInclude(200, typeof(SkierMan))]
[XmlInclude(typeof(SkierWoman))]
[XmlInclude(typeof(SkierMan))]
public class Sportsman
{
    [ProtoMember(1)]
    [XmlElement("Surname")]
    public string Surname { get; set; }

    [ProtoMember(2)]
    [XmlElement("Result")]
    public int Result { get; set; }

    public Sportsman() { }

    public Sportsman(string surname, int result)
    {
        this.Surname = surname;
        this.Result = result;
    }

    public virtual string GetTable()
    {
        return $"{Surname,-15} {Result,-10}\n";
    }
}

[ProtoContract]
public class SkierWoman : Sportsman
{
    public SkierWoman() { }

    public SkierWoman(string surname, int result) : base(surname, result) { }
}

[ProtoContract]
public class SkierMan : Sportsman
{
    public SkierMan() { }

    public SkierMan(string surname, int result) : base(surname, result) { }
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

        string basePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "SportsmanData");

        if (!Directory.Exists(basePath))
        {
            Directory.CreateDirectory(basePath);
        }

        string jsonFilePath = Path.Combine(basePath, "sportsmen.json");
        string xmlFilePath = Path.Combine(basePath, "sportsmen.xml");
        string binaryFilePath = Path.Combine(basePath, "sportsmen.bin");

        var allGroup = MergeArrays(all_group_women, all_group_men);

        var jsonSerializer = new JSONSerializer<Sportsman[]>();
        jsonSerializer.Serialize(allGroup, jsonFilePath);

        var xmlSerializer = new XMLSerializer<Sportsman[]>();
        xmlSerializer.Serialize(allGroup, xmlFilePath);

        var binarySerializer = new BinarySerializer<Sportsman[]>();
        binarySerializer.Serialize(allGroup, binaryFilePath);

        var jsonDeserialized = jsonSerializer.Deserialize(jsonFilePath);
        Console.WriteLine("\nJSON Deserialized data:");
        foreach (var person in jsonDeserialized)
        {
            Console.WriteLine(person.GetTable());
        }

        var xmlDeserialized = xmlSerializer.Deserialize(xmlFilePath);
        Console.WriteLine("\nXML Deserialized data:");
        foreach (var person in xmlDeserialized)
        {
            Console.WriteLine(person.GetTable());
        }

        var binaryDeserialized = binarySerializer.Deserialize(binaryFilePath);
        Console.WriteLine("\nBinary Deserialized data:");
        foreach (var person in binaryDeserialized)
        {
            Console.WriteLine(person.GetTable());
        }
    }
}
