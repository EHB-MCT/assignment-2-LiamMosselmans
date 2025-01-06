using System.IO;
using UnityEngine;

public class PathTrigger : MonoBehaviour
{
    private string _privateName;
    [SerializeField] private DataTrackingManager _dataTrackingManager;

    private void Start()
    {
        if (_dataTrackingManager == null)
        {
            _dataTrackingManager = FindObjectOfType<DataTrackingManager>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerObject"))
        {
            if (this.CompareTag("StartTrigger_PathA"))
            {
                _privateName = "PathA";
                _dataTrackingManager.ChosenPath = _privateName;
                _dataTrackingManager.StartTrackingPath(_dataTrackingManager.ChosenPath);
                GetComponent<Collider>().enabled = false;
            }
            else if(this.CompareTag("StartTrigger_PathB"))
            {
                _privateName = "PathB";
                _dataTrackingManager.ChosenPath = _privateName;
                _dataTrackingManager.StartTrackingPath(_dataTrackingManager.ChosenPath);
                GetComponent<Collider>().enabled = false;
            }
            else if(this.CompareTag("SectionTrigger"))
            {
                _dataTrackingManager.TrackSectionTime("Section1");
                GetComponent<Collider>().enabled = false;
            }
            else if (this.CompareTag("FinishTrigger"))
            {
                _dataTrackingManager.TrackSectionTime("Section2");
                _dataTrackingManager.StopTrackingPath();
                GetComponent<Collider>().enabled = false;
            }
        }
    }
}