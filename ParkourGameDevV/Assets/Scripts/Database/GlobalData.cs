using System.Collections.Generic;

public class GlobalData
{
    public List<TimeEntry> TopTimes; // List to store the top 10 times
    public PathChoiceCounts PathChoiceCounts; // Store the path choice counts

    public GlobalData(List<TimeEntry> topTimes, PathChoiceCounts pathChoiceCounts)
    {
        TopTimes = topTimes;
        PathChoiceCounts = pathChoiceCounts;
    }
}

[System.Serializable]
public class TimeEntry
{
    public float Time;
    public string ChosenPath;

    public TimeEntry(float time, string chosenPath)
    {
        Time = time;
        ChosenPath = chosenPath;
    }
}

[System.Serializable]
public class PathChoiceCounts
{
    public int PathACount;
    public int PathBCount;

    public PathChoiceCounts(int pathACount, int pathBCount)
    {
        PathACount = pathACount;
        PathBCount = pathBCount;
    }
}