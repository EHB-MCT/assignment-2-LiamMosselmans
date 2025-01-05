using System.Collections.Generic;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;
public class DatabaseManager : MonoBehaviour
{
    private DatabaseReference _databaseReference;
    private string _userName = "Admin";
    private string _userID;
    private PathChoiceCounts _pathChoiceCounts = new PathChoiceCounts(0, 0);
    private List<TimeEntry> _topTimes = new List<TimeEntry>();

    private void Start()
    {
        _userID = SystemInfo.deviceUniqueIdentifier;
        InitializeFirebase();
    }

    private void InitializeFirebase()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                _databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
                Debug.Log("Firebase initialized successfully.");
            }
            else
            {
                Debug.LogError($"Could not resolve Firebase dependencies: {task.Result}");
            }
        });
    }

    public void CreateUser()
    {
        User newUser = new User(_userName);
        string json = JsonUtility.ToJson(newUser);

        _databaseReference.Child("users").Child(_userID).SetRawJsonValueAsync(json);
    }

    public void SubmitUserParkourData(float totalTime, Dictionary<string, float> sectionTimes, string chosenPath)
    {
        if (string.IsNullOrEmpty(chosenPath))
        {
            Debug.LogError("No path chosen. Cannot submit parkour data.");
            return;
        }
    
        ParkourData parkourData = new ParkourData(
            totalTime,     
            chosenPath 
        );

        string json = JsonUtility.ToJson(parkourData);

        _databaseReference.Child("users").Child(_userID).Child("parkourData").SetRawJsonValueAsync(JsonUtility.ToJson(parkourData)).ContinueWith(task =>
        {
            if (task.IsCompletedSuccessfully)
            {
                Debug.Log("Parkour data submitted successfully.");
            }
            else
            {
                Debug.LogError($"Failed to submit parkour data: {task.Exception}");
            }
        });

        _databaseReference.Child("globalData").SetRawJsonValueAsync(JsonUtility.ToJson(parkourData)).ContinueWith(task =>
        {
            if (task.IsCompletedSuccessfully)
            {
                Debug.Log("Parkour data submitted successfully.");
            }
            else
            {
                Debug.LogError($"Failed to submit parkour data: {task.Exception}");
            }
        });
    }

    public void TrackPathChoice(string chosenPath, float totalTime)
    {
        if (chosenPath == "PathA")
        {
            _pathChoiceCounts.PathACount++;
        }
        else if (chosenPath == "PathB")
        {
            _pathChoiceCounts.PathBCount++;
        }

        _topTimes.Add(new TimeEntry(totalTime, chosenPath));
        _topTimes.Sort((entry1, entry2) => entry1.Time.CompareTo(entry2.Time));

        if (_topTimes.Count > 10)
        {
            _topTimes.RemoveAt(_topTimes.Count - 1);
        }
    }

    public void SubmitGlobalData()
    {
        GlobalData globalData = new GlobalData(_topTimes, _pathChoiceCounts);

        DatabaseReference databaseReference = FirebaseDatabase.DefaultInstance.GetReference("globalData");

        databaseReference.SetRawJsonValueAsync(JsonUtility.ToJson(globalData)).ContinueWith(task =>
        {
            if (task.IsCompletedSuccessfully)
            {
                Debug.Log("Global data submitted successfully.");
            }
            else
            {
                Debug.LogError($"Failed to submit global data: {task.Exception}");
            }
        });
    }
}