using UnityEngine;
using UnityEngine.XR.Hands;
using UnityEngine.SubsystemsImplementation;
using NUnit.Framework;
using System.Collections.Generic;

public class Drone : MonoBehaviour
{

    XRHandSubsystem handSubsystem;
    float defaultSpeed = 5f;



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

        // May make these global variables:
        XRHandJoint palm = Lefthand.GetJoint(XRHandJointID.Palm);
        XRHandJoint thumb = leftHand.GetJoint(XRHandJointID.ThumbTip);
        XRHandJoint index = leftHand.GetJoint(XRHandJointID.IndexTip);
        XRHandJoint middle = leftHand.GetJoint(XRHandJointID.MiddleTip);
        XRHandJoint ring = leftHand.GetJoint(XRHandJointID.RingTip);
        XRHandJoint pinky = leftHand.GetJoint(XRHandJointID.LittleTip);

        float thumbToPalm = Vector3.Distance(thumb.pose.position, palm.pose.position);
        float indexToPalm = Vector3.Distance(index.pose.position, palm.pose.position);
        float middleToPalm = Vector3.Distance(middle.pose.position, palm.pose.position);
        float ringToPalm = Vector3.Distance(ring.pose.position, palm.pose.position);
        float pinkyToPalm = Vector3.Distance(ring.pose.position, palm.pose.position);

        if (palm.TryGetPose(out Pose palmPose) &&
            thumbToPalm < 0.1f &&
            indexToPalm < 0.05f &&
            middleToPalm < 0.05f &&
            ringToPalm < 0.05f && 
            pinkyToPalm < 0.05f 
            )
        {
            Debug.Log("WE GOT THE PALM POSE WORKING!!!!!!!!!!");
            MoveDrone();
        }

    }


    private void MoveDrone()
    {
        
        transform.position += transform.forward * defaultSpeed * Time.deltaTime;

    }

    private void RotateDrone()
    {
        

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("DRONE COLLIDED WITH ", other);
    }
}
