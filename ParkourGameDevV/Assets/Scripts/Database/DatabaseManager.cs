using System.Collections.Generic;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using TMPro;
using UnityEngine;
public class DatabaseManager : MonoBehaviour
{
    private DatabaseReference _databaseReference;
    private string _userName = "Admin";
    private string _userID;
    private PathChoiceCounts _pathChoiceCounts = new PathChoiceCounts(0, 0);
    private List<TimeEntry> _topTimes = new List<TimeEntry>();

    public GameObject leaderboardEntryPrefab;
    public Transform leaderboardContent;

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
                LoadLeaderboard();
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

        TrackPathChoice(chosenPath, totalTime);
        SubmitGlobalData();
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

        UpdateTopTimes(totalTime, chosenPath);
    }

    public void UpdateTopTimes(float totalTime, string chosenPath)
    {
        TimeEntry newEntry = new TimeEntry(totalTime, chosenPath);

        // Check if the entry already exists in the list
        bool entryExists = _topTimes.Exists(entry => 
            Mathf.Approximately(entry.Time, newEntry.Time) && entry.ChosenPath == newEntry.ChosenPath);

        if (entryExists)
        {
            Debug.Log("Entry already exists in the top times. Skipping addition.");
            return;
        }

        _topTimes.Add(newEntry);

        // Sort the list by time in ascending order
        _topTimes.Sort((entry1, entry2) => entry1.Time.CompareTo(entry2.Time));

        // Limit the list to 10 entries
        if (_topTimes.Count > 10)
        {
            _topTimes.RemoveAt(_topTimes.Count - 1);
        }
    }

    public void SubmitGlobalData()
    {
        GlobalData globalData = new GlobalData(_topTimes, _pathChoiceCounts);

        _databaseReference.Child("globalData").SetRawJsonValueAsync(JsonUtility.ToJson(globalData)).ContinueWith(task =>
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

    public void LoadLeaderboard()
    {
        _databaseReference.Child("globalData").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                var pathChoiceCountsData = snapshot.Child("PathChoiceCounts");
                _pathChoiceCounts.PathACount = int.Parse(pathChoiceCountsData.Child("PathACount").Value.ToString());
                _pathChoiceCounts.PathBCount = int.Parse(pathChoiceCountsData.Child("PathBCount").Value.ToString());

                _topTimes.Clear();
                foreach (var timeData in snapshot.Child("TopTimes").Children)
                {
                    float time = float.Parse(timeData.Child("Time").Value.ToString());
                    string path = timeData.Child("ChosenPath").Value.ToString();
                    _topTimes.Add(new TimeEntry(time, path));
                }
                DisplayLeaderboard();
            }
            else
            {
                Debug.LogError("Failed to load global data: " + task.Exception);
            }
        });
    }

    private void DisplayLeaderboard()
    {
        foreach (Transform child in leaderboardContent)
        {
            Destroy(child.gameObject);
        }

        foreach (var entry in _topTimes)
        {
            AddLeaderboardEntry(entry);
        }
    }

    private void AddLeaderboardEntry(TimeEntry entry)
    {
        GameObject newEntry = Instantiate(leaderboardEntryPrefab, leaderboardContent);
        TMP_Text entryText = newEntry.GetComponent<TMP_Text>();

        entryText.text = $"{entry.Time} seconds - {entry.ChosenPath}";
    }
}