using Meta.XR.MRUtilityKit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public TextAsset file;

    [SerializeField]
    private GameObject checkpointPrefab;
    private List<Vector3> positions;
    public int checkpoints_reached = 0;
    private int total_checkpoint;
    public CheckPoint currentCP; //stores the last checkpoint reached 
    
    [SerializeField]
    private UI UIS;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        positions = ParseFile();
        CreateCheckPoints(positions);
        SetStartPos(positions);
        total_checkpoint = positions.Count;
        checkpoints_reached = 1;

        if (UIS == null) Debug.Log("UI script not assigned");
    }

    // Update is called once per frame
    void Update()
    {

    }

    //given code for parsing the input file
    List<Vector3> ParseFile()
    {
        float ScaleFactor = 1.0f / 39.37f;
        List<Vector3> positions = new List<Vector3>();
        string content = file.ToString();
        string[] lines = content.Split('\n');
        for (int i = 0; i < lines.Length; i++)
        {
            string[] coords = lines[i].Split(' ');
            Vector3 pos = new Vector3(float.Parse(coords[0]), float.Parse(coords[1]), float.Parse(coords[2]));
            positions.Add(pos * ScaleFactor);
        }
        return positions;
    }

    //take the positions created by ParseFile and instantiate checkpoint prefabs
    void CreateCheckPoints(List<Vector3> positions)
    {
        for (int i = 0; i < positions.Count; i++)
        {

            GameObject checkpoint = Instantiate(checkpointPrefab, positions[i], Quaternion.identity);
            checkpoint.name = "Checkpoint_" + i; //name the checkpoints with index number

            CheckPoint script = checkpoint.GetComponent<CheckPoint>(); //get script of new checkpoint
            if (script != null)
            {
                //set the checkpoint script values for later use
                script.checkpointIndex = i; 
                script.checkpointPosition = positions[i];

                //sets checkpoint 0 (starting point) initially
                if (i == 0)
                {
                    currentCP = script;
                }
            }
        }
    }

    void SetStartPos(List<Vector3> positions)
    {
        transform.position = positions[0];
        if (positions.Count < 2)
        {
            return;
        }
        Vector3 directionToNext = positions[1] - positions[0];
        transform.rotation = Quaternion.LookRotation(directionToNext.normalized, Vector3.up);

    }

    //called from DroneManage when Drone collider collides with the MAP
    public void ResetDrone()
    {
        StartCoroutine(ResetDroneRoutine());
    }

    private IEnumerator ResetDroneRoutine()
    {
        // reset drone here
        if (currentCP != null)
        {
            //sets position to last checkpoint and rotation to the next checkpoint
            transform.position = currentCP.checkpointPosition;
            //did not check currentCP.checkpointIndex + 1 gets out of size because if we reached all checkpoints
            //game should have stopped not make drone reset
            Vector3 directionToNext = positions[currentCP.checkpointIndex + 1]
                - positions[currentCP.checkpointIndex];
            transform.rotation = Quaternion.LookRotation(directionToNext.normalized, Vector3.up);
        }


        // pause everything affected by Time.deltaTime / physics
        Time.timeScale = 0f;

        Debug.Log("Paused for 3 seconds");

        // wait 3 real-world seconds
        yield return new WaitForSecondsRealtime(3f);

        // resume
        Time.timeScale = 1f;

        Debug.Log("Game resumed");
    }

    //called from DroneManager when Drone collider collides with checkpoint
    public void CheckPointReached(Collider other)
    {
        if (other == null) return; //safety

        if (other.CompareTag("CP")) {
            CheckPoint CPtoCheck = other.GetComponent<CheckPoint>(); //updates last checkpoint

            //if this checkpoint is the valid next checkpoint
            if (CPtoCheck != null && CPtoCheck.checkpointIndex == checkpoints_reached)
            {
                checkpoints_reached++; //increment the checkpoint count
                currentCP = CPtoCheck; //assign new last checkpoint reached
                UIS.UpdateCheckPoint(checkpoints_reached);
                //Final checkpoint reached
                if (checkpoints_reached == total_checkpoint)
                {
                    Debug.Log("Final checkpoint reached");
                    UIS.StopTimer();
                    Time.timeScale = 0f; 
                }

            }
            else
            {
                Debug.Log("NOT THE NEXT CHECKPOINT");
            }
        }
    }

}
