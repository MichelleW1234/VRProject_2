using UnityEngine;

public class DroneCollisionForwarder : MonoBehaviour
{
    public DroneManager droneParent;

    private void OnTriggerEnter(Collider other)
    {
        droneParent.HandleCollision(other);
    }
}