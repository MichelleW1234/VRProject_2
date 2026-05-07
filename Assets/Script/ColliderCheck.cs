using UnityEngine;

public class DroneCollisionForwarder : MonoBehaviour
{
    private GameManager GM;

    private void Start()
    {
        GM = GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (GM == null) return;

        if (other.CompareTag("MAP") || other.transform.root.CompareTag("MAP"))
        {
            GM.ResetDrone();
            return;
        }

        if (other.CompareTag("CP"))
        {
            GM.CheckPointReached(other);
            return;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (GM == null) return;

        if (other.CompareTag("MAP") || other.transform.root.CompareTag("MAP"))
        {
            GM.ResetDrone();
        }
    }
}