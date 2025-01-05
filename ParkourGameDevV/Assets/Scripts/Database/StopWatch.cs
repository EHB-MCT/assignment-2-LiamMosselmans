using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StopWatch : MonoBehaviour
{
    private float _startTime;
    public float TotalTime;
    // public Dictionary<string, float> SectionTimes = new Dictionary<string, float>();

    public void StartTracking()
    {
        _startTime = Time.time;
    }

    public void StopTracking()
    {
        TotalTime = Time.time - _startTime;

        // SectionTimes.Clear();
        _startTime = 0f;
    }

    // public void TrackSectionTime(string sectionName)
    // {
    //     float sectionTime = Time.time - _startTime;
    //     SectionTimes[sectionName] = sectionTime;

    //     // Reset stopwatch for the next section
    //     _startTime = Time.time;
    // }
}
