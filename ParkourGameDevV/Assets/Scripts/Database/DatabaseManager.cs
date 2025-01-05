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

                // Example: Writing data to Firebase under a node called "exampleNode"
                CreateUser();
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

    // private void WriteToDatabase(string nodeName, string data)
    // {
    //     // Set a value under the node with the name 'nodeName'
    //     _databaseReference.Child(nodeName).SetValueAsync(data).ContinueWithOnMainThread(writeTask =>
    //     {
    //         if (writeTask.IsCompleted)
    //         {
    //             Debug.Log($"Data written to {nodeName}: {data}");
    //         }
    //         else
    //         {
    //             Debug.LogError("Failed to write data to Firebase.");
    //         }
    //     });
    // }
}