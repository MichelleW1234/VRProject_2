using UnityEngine;
using TMPro;

public class DistanceIndicator : MonoBehaviour
{
    private TextMeshPro textMesh;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textMesh = GetComponent<TextMeshPro>();

        UpdateDistance();
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDistance();

    }

    private void UpdateDistance()
    {
        
        Transform head = Camera.main.transform;

        GameObject drone = GameObject.Find("[BuildingBlock] Camera Rig");
        GameManager GM = drone.GetComponent<GameManager>();

        Vector3 droneHeadCoordPosition = head.InverseTransformPoint(drone.transform.position);
        Vector3 checkPointHeadCoordPosition = head.InverseTransformPoint(GM.positions[GM.checkpoints_reached]);

        float distance = Vector3.Distance(droneHeadCoordPosition, checkPointHeadCoordPosition);

        textMesh.text = "Distance To Checkpoint: " + distance;

    }
}
