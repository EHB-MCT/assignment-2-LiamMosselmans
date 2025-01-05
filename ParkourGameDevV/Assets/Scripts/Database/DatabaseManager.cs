using System.Collections.Generic;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;
public class DatabaseManager : MonoBehaviour
{
    private DatabaseReference _databaseReference;
    private string _userName = "Robbe";
    private string _userID;

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

    public void SubmitUserParkourData(float pathATime, float pathBTime, Dictionary<string, float> sectionTimes, string chosenPath)
    {
        // Create a data structure to hold the parkour data
        ParkourData parkourData = new ParkourData(pathATime, pathBTime, sectionTimes, chosenPath);
        string json = JsonUtility.ToJson(parkourData);

        // Write the data to Firebase under the current user's ID
        _databaseReference.Child("users").Child(_userID).Child("parkourData").SetRawJsonValueAsync(json);
    }
}