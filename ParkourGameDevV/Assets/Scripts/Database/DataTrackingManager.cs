using System.Collections.Generic;
using UnityEngine;

public class DataTrackingManager : MonoBehaviour
{
    private StopWatch _stopWatch;
    // private Dictionary<string, float> _sectionTimes;
    public string ChosenPath;
    private float _totalTime;
    private DatabaseManager _databaseManager;

    private void Start()
    {
        _stopWatch = GetComponent<StopWatch>();
        _databaseManager = FindObjectOfType<DatabaseManager>();
    }

    public void StartTrackingPath(string chosenPath)
    {
        ChosenPath = chosenPath;
        _stopWatch.StartTracking();
    }

    public void StopTrackingPath()
    {
        _stopWatch.StopTracking();
        _totalTime = _stopWatch.TotalTime;
        _databaseManager.SubmitUserParkourData(_totalTime, _stopWatch.SectionTimes,ChosenPath);
    }

    public void TrackSectionTime(string sectionName)
    {
        _stopWatch.TrackSectionTime(ChosenPath, sectionName);
    }
}