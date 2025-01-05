using System.Collections.Generic;

public class ParkourData
{
    public float TotalTime;
    public List<SectionTimeEntry> SectionTimes;
    public string ChosenPath;

    public ParkourData(float totalTime, Dictionary<string, float> sectionTimes, string chosenPath)
    {
        TotalTime = totalTime;
        SectionTimes =  ConvertDictionaryToList(sectionTimes);
        ChosenPath = chosenPath;
    }
    public ParkourData(float totalTime, string chosenPath)
    {
        TotalTime = totalTime;
        ChosenPath = chosenPath;
    }

    private List<SectionTimeEntry> ConvertDictionaryToList(Dictionary<string, float> dictionary)
    {
        var list = new List<SectionTimeEntry>();
        foreach (var kvp in dictionary)
        {
            list.Add(new SectionTimeEntry(kvp.Key, kvp.Value));
        }
        return list;
    }
}

public class SectionTimeEntry
{
    public string Key;
    public float Value;

    public SectionTimeEntry(string key, float value)
    {
        Key = key;
        Value = value;
    }
}