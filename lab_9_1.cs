using System;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Newtonsoft.Json;
using ProtoBuf;

[ProtoContract]
[DataContract]
public class Athlete
{
    [ProtoMember(1)]
    [DataMember]
    private string lastName;
    [ProtoMember(2)]
    [DataMember]
    private string club;
    [ProtoMember(3)]
    [DataMember]
    private double firstAttempt;
    [ProtoMember(4)]
    [DataMember]
    private double secondAttempt;
    [ProtoMember(5)]
    [DataMember]
    private bool disqualified;

    [XmlElement("LastName")]
    public string LastName
    {
        get { return lastName; }
        set { lastName = value; }
    }

    [XmlElement("Club")]
    public string Club
    {
        get { return club; }
        set { club = value; }
    }

    [XmlElement("FirstAttempt")]
    public double FirstAttempt
    {
        get { return firstAttempt; }
        set { firstAttempt = value; }
    }

    [XmlElement("SecondAttempt")]
    public double SecondAttempt
    {
        get { return secondAttempt; }
        set { secondAttempt = value; }
    }

    [XmlElement("Disqualified")]
    public bool Disqualified
    {
        get { return disqualified; }
        set { disqualified = value; }
    }

    public double TotalDistance => firstAttempt + secondAttempt;

    public Athlete() { }

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

public abstract class SerializerBase<T>
{
    public abstract void Serialize(T obj, string filePath);
    public abstract T Deserialize(string filePath);
}

public class JsonSerializer<T> : SerializerBase<T>
{
    public override void Serialize(T obj, string filePath)
    {
        string json = Newtonsoft.Json.JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented);
        File.WriteAllText(filePath, json);
    }

    public override T Deserialize(string filePath)
    {
        string json = File.ReadAllText(filePath);
        return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
    }
}

public class XmlSerializer<T> : SerializerBase<T>
{
    public override void Serialize(T obj, string filePath)
    {
        var serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
        using (var writer = new StreamWriter(filePath))
        {
            serializer.Serialize(writer, obj);
        }
    }

    public override T Deserialize(string filePath)
    {
        var serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
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
        using (var file = File.Create(filePath))
        {
            Serializer.Serialize(file, obj);
        }
    }

    public override T Deserialize(string filePath)
    {
        using (var file = File.OpenRead(filePath))
        {
            return Serializer.Deserialize<T>(file);
        }
    }
}

class Program
{
    static void Main()
    {
        string folderPath = "/Users/palppv/Desktop/AthleteData";
        Directory.CreateDirectory(folderPath);

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
                return y.TotalDistance.CompareTo(x.TotalDistance);
        });

        for (int i = 0; i < athletes.Length; i++)
        {
            Console.Write(athletes[i].GetAthleteInfo(i + 1));
        }

        var jsonSerializer = new JsonSerializer<Athlete[]>();
        var xmlSerializer = new XmlSerializer<Athlete[]>();
        var binarySerializer = new BinarySerializer<Athlete[]>();

        string jsonFilePath = Path.Combine(folderPath, "athletes.json");
        string xmlFilePath = Path.Combine(folderPath, "athletes.xml");
        string binaryFilePath = Path.Combine(folderPath, "athletes.bin");

        jsonSerializer.Serialize(athletes, jsonFilePath);
        xmlSerializer.Serialize(athletes, xmlFilePath);
        binarySerializer.Serialize(athletes, binaryFilePath);

        Athlete[] deserializedFromJson = jsonSerializer.Deserialize(jsonFilePath);
        Athlete[] deserializedFromXml = xmlSerializer.Deserialize(xmlFilePath);
        Athlete[] deserializedFromBinary = binarySerializer.Deserialize(binaryFilePath);

        Console.WriteLine("\nDeserialized from JSON:");
        foreach (var athlete in deserializedFromJson)
        {
            Console.WriteLine(athlete.GetAthleteInfo(Array.IndexOf(deserializedFromJson, athlete) + 1));
        }

        Console.WriteLine("\nDeserialized from XML:");
        foreach (var athlete in deserializedFromXml)
        {
            Console.WriteLine(athlete.GetAthleteInfo(Array.IndexOf(deserializedFromXml, athlete) + 1));
        }

        Console.WriteLine("\nDeserialized from Binary:");
        foreach (var athlete in deserializedFromBinary)
        {
            Console.WriteLine(athlete.GetAthleteInfo(Array.IndexOf(deserializedFromBinary, athlete) + 1));
        }
    }
}
