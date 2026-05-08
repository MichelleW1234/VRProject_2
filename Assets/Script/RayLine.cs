using UnityEngine;

public class RayLine : MonoBehaviour
{
    public LineRenderer line;
    private GameManager GM;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        GM = GameObject.Find("UFO").GetComponent<GameManager>();
        line.material = new Material(Shader.Find("Sprites/Default"));
        UpdateRay();
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRay();
        
    }

    private void UpdateRay()
    {

        Vector3 rayDirectionEndpoint = GM.positions[GM.checkpoints_reached];
        Ray ray = new Ray(transform.position, (rayDirectionEndpoint - transform.position).normalized);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {

            line.SetPosition(0, transform.position);
            line.SetPosition(1, rayDirectionEndpoint);

        }

    }
}
