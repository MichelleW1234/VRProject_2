using UnityEngine;
using TMPro;
using Unity.XR.CoreUtils;

public class DistanceIndicator : MonoBehaviour
{

    private TextMeshPro textMesh;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textMesh = GetComponent<TextMeshPro>();
        if (textMesh != null) Debug.Log("text assigned");
        UpdateDistance();
    }

    // Update is called once per frame
    void Update()
    {
         UpdateDistance();
        
    }

    private void UpdateDistance()
    {
        /**
        Transform head = Camera.main.transform;

        if (head == null)
        {
            Debug.LogError("No camera tagged MainCamera found!");
            return;
        }*/


        GameObject drone = GameObject.Find("UFO");
        GameManager GM = drone.GetComponent<GameManager>();
        GameObject head = GameObject.Find("[BuildingBlock] Camera Rig");
        Transform headCoord = head.transform;
        Vector3 droneHeadCoordPosition = headCoord.InverseTransformPoint(drone.transform.position); //UFO
        Vector3 checkPointHeadCoordPosition = headCoord.InverseTransformPoint(GM.positions[GM.checkpoints_reached]); //checkpoint

        float distance = Vector3.Distance(droneHeadCoordPosition, checkPointHeadCoordPosition);

        textMesh.text = "Distance To Checkpoint: " + distance;

    }
}
