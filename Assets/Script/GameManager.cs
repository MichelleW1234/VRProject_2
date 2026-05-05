using Meta.XR.MRUtilityKit;
using System.Collections.Generic;
using UnityEngine;



public class GameManager : MonoBehaviour
{
    public TextAsset file;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreateCheckPoints(ParseFile());
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
            //GameObject checkpoint = Instantiate(checkpointPrefab, positions[i], Quaternion.identity)
        }
    }
}
