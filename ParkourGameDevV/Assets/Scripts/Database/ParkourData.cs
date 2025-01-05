using System.Collections.Generic;

public class ParkourData
{
    public float PathATime;
    public float PathBTime;
    public Dictionary<string, float> SectionTimes;
    public string ChosenPath;

    public ParkourData(float pathATime, float pathBTime, Dictionary<string, float> sectionTimes, string chosenPath)
    {
        PathATime = pathATime;
        PathBTime = pathBTime;
        SectionTimes = sectionTimes;
        ChosenPath = chosenPath;
    }
}