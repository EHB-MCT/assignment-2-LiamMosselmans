using System.IO;
using UnityEngine;

public class PathTrigger : MonoBehaviour
{
    public string PathName;
    [SerializeField] private DataTrackingManager _dataTrackingManager;

    private void Start()
    {
        if (_dataTrackingManager == null)
        {
            _dataTrackingManager = FindObjectOfType<DataTrackingManager>();
            Debug.Log(_dataTrackingManager);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerObject")) // Ensure the player enters the trigger
        {
            if (this.CompareTag("StartTrigger_PathA"))
            {
                Debug.Log("Hello there, path A");
                PathName = "PathA";
                _dataTrackingManager.StartTrackingPath(PathName);
                GetComponent<Collider>().enabled = false;
            }
            else if(this.CompareTag("StartTrigger_PathB"))
            {
                Debug.Log("Hello there, path B");
                PathName = "PathB";
                _dataTrackingManager.StartTrackingPath(PathName);
                GetComponent<Collider>().enabled = false;
            }
            else if (this.CompareTag("FinishTrigger"))
            {
                _dataTrackingManager.StopTrackingPath();
                GetComponent<Collider>().enabled = false;
            }
        }
    }
}