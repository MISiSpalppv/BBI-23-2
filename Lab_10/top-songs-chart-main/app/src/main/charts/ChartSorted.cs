using app.main.songs;

namespace app.main.charts;

public static class ChartSorted
{
    public static void BubbleSortByYearAsc(ChartSong[] songs)
    {
        for (int i = 0; i < songs.Length - 1; i++)
        {
            for (int j = 0; j < songs.Length - i - 1; j++)
            {
                if (songs[j].Year > songs[j + 1].Year)
                {
                    var temp = songs[j];
                    songs[j] = songs[j + 1];
                    songs[j + 1] = temp;
                }
            }
        }
    }

    public static void BubbleSortByTitleAsc(ChartSong[] songs)
    {
        for (int i = 0; i < songs.Length - 1; i++)
        {
            for (int j = 0; j < songs.Length - i - 1; j++)
            {
                if (string.Compare(songs[j].Title, songs[j + 1].Title, StringComparison.Ordinal) > 0)
                {
                    var temp = songs[j];
                    songs[j] = songs[j + 1];
                    songs[j + 1] = temp;
                }
            }
        }
    }

    public static class LiveSingleSorter
    {
        public static void SortByRevenueDesc(LiveSingle[] liveSingles)
        {
            for (int i = 0; i < liveSingles.Length; i++)
            {
                int max = i;
                for (int j = i + 1; j < liveSingles.Length; j++)
                {
                    if (liveSingles[j].GetRevenue() > liveSingles[max].GetRevenue())
                    {
                        max = j;
                    }
                }
                LiveSingle temp = liveSingles[i];
                liveSingles[i] = liveSingles[max];
                liveSingles[max] = temp;
            }
        }
    }


}
