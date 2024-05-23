using System.Text.Json.Serialization;
using app.main.songs;

namespace app.main.charts;

[Serializable]
public partial class Chart : IChart
{
    private string name;
    private List<ChartSong> songs;

    public Chart() { }

    public Chart(string name)
    {
        this.name = name ?? throw new ArgumentNullException(nameof(name));
        songs = new List<ChartSong>();
    }

    [JsonConstructor]
    public Chart(string name, List<ChartSong> songs)
    {
        this.name = name ?? throw new ArgumentNullException(nameof(name));
        this.songs = songs ?? throw new ArgumentNullException(nameof(songs));
    }

    public List<ChartSong> Songs
    {
        get => songs;
        set => songs = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Name
    {
        get => name;
        set => name = value ?? throw new ArgumentNullException(nameof(value));
    }

    public void AddSong(Song song)
    {
        if (song == null)
            throw new ArgumentNullException(nameof(song));
        songs.Add((ChartSong)song);
    }

    public void RemoveSong(Song song)
    {
        if (song == null)
            throw new ArgumentNullException(nameof(song));
        songs.Remove((ChartSong)song);
    }

    public void AddSong(Song[] arraySong)
    {
        if (arraySong == null)
            throw new ArgumentNullException(nameof(arraySong));
        foreach (Song song in arraySong)
        {
            if (song == null)
                throw new ArgumentNullException(nameof(song));
            songs.Add((ChartSong)song);
        }
    }

    public void RemoveSong(int id)
    {
        if (id < 0 || id >= songs.Count)
            throw new ArgumentOutOfRangeException(nameof(id));
        songs.RemoveAt(id);
    }

    public void DisplayChart()
    {
        Console.WriteLine($"Chart: {name}");
        for (int i = 0; i < songs.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {songs[i]}");
        }
    }
}
