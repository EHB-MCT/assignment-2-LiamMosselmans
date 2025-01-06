using System.Collections.Generic;
using UnityEngine;

public class StopWatch : MonoBehaviour
{
    private float _startTime;
    private float _currentSectionTime;
    public float TotalTime;
    public Dictionary<string, float> SectionTimes = new Dictionary<string, float>();

    public void StartTracking()
    {
        _startTime = Time.time;
        _currentSectionTime = Time.time;
    }

    public void StopTracking()
    {
        TotalTime = Time.time - _startTime;
        _startTime = 0f;
    }

    public void TrackSectionTime(string chosenPath, string sectionName)
    {
        string fullKey = $"{chosenPath}_{sectionName}";
        
        float sectionFinalTime = Time.time - _currentSectionTime;
        SectionTimes[fullKey] = sectionFinalTime;

        _currentSectionTime = Time.time;
    }
}
