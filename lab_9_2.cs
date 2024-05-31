using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using System.Xml.Serialization;
using Newtonsoft.Json;
using ProtoBuf;

[Serializable]
[ProtoContract]
[DataContract]
public class Person
{
    private static int nextID = 1;

    [ProtoMember(1)]
    [DataMember]
    private string firstName;

    [ProtoMember(2)]
    [DataMember]
    private string lastName;

    [ProtoMember(3)]
    [DataMember]
    private string patronymic;

    [ProtoMember(4)]
    [DataMember]
    private int id;

    [XmlElement("FirstName")]
    public string FirstName
    {
        get { return firstName; }
        set { firstName = value; }
    }

    [XmlElement("LastName")]
    public string LastName
    {
        get { return lastName; }
        set { lastName = value; }
    }

    [XmlElement("Patronymic")]
    public string Patronymic
    {
        get { return patronymic; }
        set { patronymic = value; }
    }

    [XmlElement("ID")]
    public int ID
    {
        get { return id; }
        set { id = value; }
    }

    public Person()
    {
        this.id = nextID++;
    }

    public Person(string firstName, string lastName, string patronymic) : this()
    {
        this.firstName = firstName;
        this.lastName = lastName;
        this.patronymic = patronymic;
    }

    public virtual void GetPersonInfo()
    {
        Console.WriteLine($"ФИО: {LastName} {FirstName} {Patronymic} | ID: {ID}");
    }
}

[Serializable]
[ProtoContract]
[DataContract]
public class Sportsman : Person
{
    [ProtoMember(5)]
    [DataMember]
    private double result;

    [XmlElement("Result")]
    public double Result
    {
        get { return result; }
        set { result = value; }
    }

    public Sportsman() { }

    public Sportsman(string firstName, string lastName, string patronymic, double result) : base(firstName, lastName, patronymic)
    {
        this.result = result;
    }

    public override void GetPersonInfo()
    {
        Console.WriteLine($"ФИО: {LastName} {FirstName} {Patronymic} | ID: {ID} | Баллы: {Result}");
    }
}

public abstract class SerializerBase<T>
{
    public abstract void Serialize(T obj, string filePath);
    public abstract T Deserialize(string filePath);
}

public class JsonSerializer<T> : SerializerBase<T>
{
    public override void Serialize(T obj, string filePath)
    {
        string json = JsonConvert.SerializeObject(obj, Formatting.Indented);
        File.WriteAllText(filePath, json);
    }

    public override T Deserialize(string filePath)
    {
        string json = File.ReadAllText(filePath);
        return JsonConvert.DeserializeObject<T>(json);
    }
}

public class XmlSerializer<T> : SerializerBase<T>
{
    public override void Serialize(T obj, string filePath)
    {
        var serializer = new XmlSerializer(typeof(T));
        using (var writer = new StreamWriter(filePath))
        {
            serializer.Serialize(writer, obj);
        }
    }

    public override T Deserialize(string filePath)
    {
        var serializer = new XmlSerializer(typeof(T));
        using (var reader = new StreamReader(filePath))
        {
            return (T)serializer.Deserialize(reader);
        }
    }
}

public class BinarySerializer<T> : SerializerBase<T>
{
    public override void Serialize(T obj, string filePath)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };
        string json = System.Text.Json.JsonSerializer.Serialize(obj, options);
        File.WriteAllText(filePath, json);
    }

    public override T Deserialize(string filePath)
    {
        string json = File.ReadAllText(filePath);
        return System.Text.Json.JsonSerializer.Deserialize<T>(json);
    }
}
class Program
{
    static void Main()
    {
        string folderPath = "/Users/palppv/Desktop/PersonData";
        Directory.CreateDirectory(folderPath);

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

        var jsonSerializer = new JsonSerializer<Sportsman[]>();
        var xmlSerializer = new XmlSerializer<Sportsman[]>();
        var binarySerializer = new BinarySerializer<Sportsman[]>();

        string jsonFilePath = Path.Combine(folderPath, "sportsmen.json");
        string xmlFilePath = Path.Combine(folderPath, "sportsmen.xml");
        string binaryFilePath = Path.Combine(folderPath, "sportsmen.bin");

        jsonSerializer.Serialize(sportiks, jsonFilePath);
        xmlSerializer.Serialize(sportiks, xmlFilePath);
        binarySerializer.Serialize(sportiks, binaryFilePath);

        Sportsman[] deserializedFromJson = jsonSerializer.Deserialize(jsonFilePath);
        Sportsman[] deserializedFromXml = xmlSerializer.Deserialize(xmlFilePath);
        Sportsman[] deserializedFromBinary = binarySerializer.Deserialize(binaryFilePath);

        Console.WriteLine("\nDeserialized from JSON:");
        foreach (var sportsman in deserializedFromJson)
        {
            sportsman.GetPersonInfo();
        }

        Console.WriteLine("\nDeserialized from XML:");
        foreach (var sportsman in deserializedFromXml)
        {
            sportsman.GetPersonInfo();
        }

        Console.WriteLine("\nDeserialized from Binary:");
        foreach (var sportsman in deserializedFromBinary)
        {
            sportsman.GetPersonInfo();
        }
    }
}
