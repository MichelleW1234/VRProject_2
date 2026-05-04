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
        XRHandJoint thumb = Lefthand.GetJoint(XRHandJointID.ThumbTip);
        XRHandJoint index = Lefthand.GetJoint(XRHandJointID.IndexTip);
        XRHandJoint middle = Lefthand.GetJoint(XRHandJointID.MiddleTip);
        XRHandJoint ring = Lefthand.GetJoint(XRHandJointID.RingTip);
        XRHandJoint pinky = Lefthand.GetJoint(XRHandJointID.LittleTip);


        float thumbToPalm = 0;
        float indexToPalm = 0;
        float middleToPalm = 0;
        float ringToPalm = 0;
        float pinkyToPalm = 0;




        if (palm.TryGetPose(out Pose palmPose)) {


            if (thumb.TryGetPose(out Pose thumbPose)) {

                thumbToPalm = Vector3.Distance(thumbPose.position, palmPose.position);

            }

            if (index.TryGetPose(out Pose indexPose)) {

                indexToPalm = Vector3.Distance(indexPose.position, palmPose.position);

            }

            if (middle.TryGetPose(out Pose middlePose))
            {

                middleToPalm = Vector3.Distance(middlePose.position, palmPose.position);

            }

            if (ring.TryGetPose(out Pose ringPose))
            {

                ringToPalm = Vector3.Distance(ringPose.position, palmPose.position);

            }

            if (pinky.TryGetPose(out Pose pinkyPose))
            {

                pinkyToPalm = Vector3.Distance(pinkyPose.position, palmPose.position);

            }

        }

        if (thumbToPalm < 0.1f &&
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
