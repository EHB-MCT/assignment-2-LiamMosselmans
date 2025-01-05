using System.Collections.Generic;
using UnityEngine;

public class DataTrackingManager : MonoBehaviour
{
    private StopWatch _stopWatch;
    // private Dictionary<string, float> _sectionTimes;
    private string _chosenPath;
    private DatabaseManager _databaseManager;

    private void Start()
    {
        _stopWatch = GetComponent<StopWatch>();
        // _sectionTimes = new Dictionary<string, float>();
        _databaseManager = FindObjectOfType<DatabaseManager>();
    }

    // Call this method to start tracking a new path (A or B)
    public void StartTrackingPath(string chosenPath)
    {
        _chosenPath = chosenPath;
        _stopWatch.StartTracking();
    }

    // This method is called when the player enters the finish trigger for a path
    public void StopTrackingPath()
    {
        // Stop the stopwatch and get the total time
        _stopWatch.StopTracking();
        float totalTime = _stopWatch.TotalTime;
        
        // Log or save the path and total time
        Debug.Log($"Path: {_chosenPath}, Time: {totalTime}");

        // Send data to the DatabaseManager
        // _databaseManager.SubmitUserParkourData(totalTime, _chosenPath);
    }

    // Call this method when the player completes a section
    // public void TrackSectionTime()
    // {
    //     _stopWatch.TrackSectionTime();
    //     _sectionTimes[sectionName] = _stopWatch.SectionTimes;
    // }

    // Call this method when the player finishes the entire parkour run
    // public void SubmitData()
    // {
    //     // Get total path time
    //     float totalTime = _stopWatch.StopTracking();

    //     // Create and submit parkour data to DatabaseManager
    //     _databaseManager.SubmitUserParkourData(totalTime, _chosenPath, _sectionTimes);
    // }
}