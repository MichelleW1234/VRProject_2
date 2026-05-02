using UnityEngine;
using UnityEngine.XR.Hands;
using UnityEngine.SubsystemsImplementation;
using NUnit.Framework;
using System.Collections.Generic;

public class Drone : MonoBehaviour
{

    XRHandSubsystem handSubsystem;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var subsystems = new List<XRHandSubsystem>();
        SubsystemManager.GetSubsystems(subsystems);

        if (subsystems.Count > 0)
        {
            handSubsystem = subsystems[0];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (handSubsystem == null) return; //safety check

        XRHand Lefthand = handSubsystem.leftHand;
        if (Lefthand.isTracked)
        {
            Debug.Log("WE GOT THE LEFT HAND WORKING!!!!!!!!!!");
        }
        XRHandJoint palm = Lefthand.GetJoint(XRHandJointID.Palm);
        if (palm.TryGetPose(out Pose palmPose))
        {
            Debug.Log("WE GOT THE PALM POSE WORKING!!!!!!!!!!");
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("DRONE COLLIDED WITH ", other);
    }
}
