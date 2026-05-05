using UnityEngine;

public class DroneCollisionForwarder : MonoBehaviour
{
    public Drone droneParent;

    private void OnTriggerEnter(Collider other)
    {
        droneParent.HandleCollision(other);
    }
}