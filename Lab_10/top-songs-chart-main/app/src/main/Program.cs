using app.main.charts;
using app.main.serialize;
using app.main.songs;


namespace app.main
{
    static class Program
    {
        private static void PrintCharts(MyJsonSerializer<Chart[]> myJsonSerializer, string path, string file)
        {
            Chart[] charts = myJsonSerializer.Read(path + file);
            Console.WriteLine(file);
            foreach (Chart chart in charts)
            {
                chart.DisplayChart();
                Console.WriteLine();
            }
        }

        private static void PrintCharts(MyXmlSerializer<Chart[]> myXmlSerializer, string path, string file)
        {
            Chart[] charts = myXmlSerializer.Read(path + file);
            Console.WriteLine(file);
            foreach (Chart chart in charts)
            {
                chart.DisplayChart();
                Console.WriteLine();
            }
        }

        private static void DeleteFile(string path, string file)
        {
            if (File.Exists(path + file))
            {
                File.Delete(path + file);
            }
        }

        public static void Main(string[] args)
        {
            Chart[] charts =
            {
                new Chart("RussiaChart"),
                new Chart("AmericanChart"),
                new Chart("WorldChart")
            };

            var songData = new (string Title, string Artist, int Year, int Listens, Genre Genre)[]
            {
                ("Bohemian Rhapsody", "Queen", 1975, 150000000, Genre.Rock),
                ("Like a Rolling Stone", "Bob Dylan", 1965, 130000000, Genre.Rock),
                ("Billie Jean", "Michael Jackson", 1982, 200000000, Genre.Pop),
                ("Imagine", "John Lennon", 1971, 140000000, Genre.Rock),
                ("Smells Like Teen Spirit", "Nirvana", 1991, 120000000, Genre.Rock),
                ("What's Going On", "Marvin Gaye", 1971, 90000000, Genre.Soul),
                ("Respect", "Aretha Franklin", 1967, 110000000, Genre.Soul),
                ("Good Vibrations", "The Beach Boys", 1966, 95000000, Genre.Rock),
                ("Hey Jude", "The Beatles", 1968, 180000000, Genre.Rock),
                ("Superstition", "Stevie Wonder", 1972, 85000000, Genre.Funk),
                ("Lose Yourself", "Eminem", 2002, 250000000, Genre.HipHop),
                ("I Will Always Love You", "Whitney Houston", 1992, 210000000, Genre.Pop),
                ("Hotel California", "Eagles", 1976, 190000000, Genre.Rock),
                ("Sweet Child o' Mine", "Guns N' Roses", 1987, 170000000, Genre.Rock),
                ("Rolling in the Deep", "Adele", 2010, 300000000, Genre.Pop),
                ("Purple Rain", "Prince", 1984, 100000000, Genre.Rock),
                ("One", "U2", 1991, 90000000, Genre.Rock),
                ("Crazy in Love", "Beyoncé", 2003, 200000000, Genre.Pop),
                ("Royals", "Lorde", 2013, 220000000, Genre.Pop),
                ("Back in Black", "AC/DC", 1980, 130000000, Genre.Rock),
                ("Uptown Funk", "Mark Ronson ft. Bruno Mars", 2014, 300000000, Genre.Funk),
                ("Rolling Stone", "The Weeknd", 2011, 180000000, Genre.Pop),
                ("Stayin' Alive", "Bee Gees", 1977, 160000000, Genre.Disco),
                ("Shake It Off", "Taylor Swift", 2014, 250000000, Genre.Pop),
                ("Old Town Road", "Lil Nas X", 2019, 350000000, Genre.Country)
            };

            ChartSong[] songs = new ChartSong[songData.Length];
            for (int i = 0; i < songData.Length; i++)
            {
                songs[i] = new ChartSong(
                    songData[i].Title,
                    songData[i].Artist,
                    songData[i].Year,
                    songData[i].Listens,
                    songData[i].Genre
                );
            }

            for (int i = 0; i < 3; i++)
            {
                HashSet<int> set = new HashSet<int>();
                for (int j = 0; j < 10; j++)
                {
                    int num = new Random().Next(0, songData.Length);
                    while (set.Contains(num))
                    {
                        num = new Random().Next(0, songData.Length);
                    }

                    charts[i].AddSong(songs[num]);
                    set.Add(num);
                }
            }

            string path = "/Users/palppv/Lab_10/top-songs-chart-main/app/src/resources/JSON/";
            string file1 = "raw_data.json";
            string file2 = "data.json";
            string file3 = "sort_data.json";
            DeleteFile(path, file1);
            DeleteFile(path, file2);
            DeleteFile(path, file3);
            MyJsonSerializer<Chart[]> myJsonSerializer = new MyJsonSerializer<Chart[]>();
            myJsonSerializer.Write(charts, path + file1);
            for (int i = 0; i < 3; i++)
            {
                ChartSorted.BubbleSortByYearAsc(charts[i].Songs.ToArray());
            }

            myJsonSerializer.Write(charts, path + file2);

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    int num = new Random().Next(0, songData.Length);
                    charts[i].AddSong(
                        new ChartSong(
                            songData[num].Title,
                            songData[num].Artist,
                            songData[num].Year,
                            songData[num].Listens,
                            songData[num].Genre
                        )
                    );
                }

                ChartSorted.BubbleSortByTitleAsc(charts[i].Songs.ToArray());
            }

            myJsonSerializer.Write(charts, path + file3);

            PrintCharts(myJsonSerializer, path, file1);
            PrintCharts(myJsonSerializer, path, file2);
            PrintCharts(myJsonSerializer, path, file3);

            string file4 = "raw_data.xml";
            string file5 = "data.xml";
            string path2 = "/Users/palppv/Lab_10/top-songs-chart-main/app/src/resources/XML/";
            DeleteFile(path2, file4);
            DeleteFile(path2, file5);
            MyXmlSerializer<Chart[]> myXmlSerializer = new MyXmlSerializer<Chart[]>();
            myXmlSerializer.Write(charts, path2 + file4);

            for (int i = 0; i < 3; i++)
            {
                List<ChartSong> songsToRemove = new List<ChartSong>();
                foreach (ChartSong song in charts[i].SelectedByYear(0, 2019))
                {
                    songsToRemove.Add(song);
                }
                foreach (ChartSong song in songsToRemove)
                {
                    charts[i].RemoveSong(song);
                }
            }

            myXmlSerializer.Write(charts, path2 + file5);

            LiveSingle[] liveSingles = new LiveSingle[10];
            for (int i = 0; i < 10; i++)
            {
                int num = new Random().Next(0, songData.Length);
                liveSingles[i] = new LiveSingle(
                    songData[num].Title,
                    songData[num].Artist,
                    songData[num].Year,
                    songData[num].Listens / 1000,
                    new Random().Next(100, 1000)
                );
            }

            ChartSorted.LiveSingleSorter.SortByRevenueDesc(liveSingles);

            Console.WriteLine("LiveSingles");
            for (int i = 0; i < liveSingles.Length; i++)
            {
                Console.WriteLine(i + 1 + " " + liveSingles[i]);
            }

            Console.WriteLine();

            ChartSong[] chartSongs = new ChartSong[3];
            for (int i = 0; i < 3; i++)
            {
                int num = new Random().Next(0, songData.Length);
                chartSongs[i] = new ChartSong(
                    songData[num].Title,
                    songData[num].Artist,
                    songData[num].Year,
                    songData[num].Listens,
                    songData[num].Genre
                );
            }

            for (int i = 0; i < 3; i++)
            {
                int num = new Random().Next(0, songData.Length);
                charts[i].AddSong(
                    new ChartSong(
                        songData[num].Title,
                        songData[num].Artist,
                        songData[num].Year,
                        songData[num].Listens,
                        songData[num].Genre
                    )
                );
                foreach (var song in chartSongs)
                {
                    charts[i].AddSong(song);
                }
                ChartSorted.BubbleSortByTitleAsc(charts[i].Songs.ToArray());
            }

            myXmlSerializer.Write(charts, path2 + file5);

            PrintCharts(myXmlSerializer, path2, file4);
            PrintCharts(myXmlSerializer, path2, file5);
        }
    }
}