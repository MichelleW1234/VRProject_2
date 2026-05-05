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
        XRHandJoint LeftPalm = Lefthand.GetJoint(XRHandJointID.Palm);
        XRHandJoint LeftThumb = Lefthand.GetJoint(XRHandJointID.ThumbTip);
        XRHandJoint LeftIndex = Lefthand.GetJoint(XRHandJointID.IndexTip);
        XRHandJoint LeftMiddle = Lefthand.GetJoint(XRHandJointID.MiddleTip);
        XRHandJoint LeftRing = Lefthand.GetJoint(XRHandJointID.RingTip);
        XRHandJoint LeftPinky = Lefthand.GetJoint(XRHandJointID.LittleTip);


        float LeftThumbToPalm = Mathf.Infinity;
        float LeftIndexToPalm = Mathf.Infinity;
        float LeftMiddleToPalm = Mathf.Infinity;
        float LeftRingToPalm = Mathf.Infinity;
        float LeftPinkyToPalm = Mathf.Infinity;

        if (LeftPalm.TryGetPose(out Pose LeftPalmPose)) {


            if (LeftThumb.TryGetPose(out Pose LeftThumbPose)) {

                LeftThumbToPalm = Vector3.Distance(LeftThumbPose.position, LeftPalmPose.position);

            }

            if (LeftIndex.TryGetPose(out Pose LeftIndexPose)) {

                LeftIndexToPalm = Vector3.Distance(LeftIndexPose.position, LeftPalmPose.position);

            }

            if (LeftMiddle.TryGetPose(out Pose LeftMiddlePose))
            {

                LeftMiddleToPalm = Vector3.Distance(LeftMiddlePose.position, LeftPalmPose.position);

            }

            if (LeftRing.TryGetPose(out Pose LeftRingPose))
            {

                LeftRingToPalm = Vector3.Distance(LeftRingPose.position, LeftPalmPose.position);

            }

            if (LeftPinky.TryGetPose(out Pose LeftPinkyPose))
            {

                LeftPinkyToPalm = Vector3.Distance(LeftPinkyPose.position, LeftPalmPose.position);

            }

        }

        if (LeftThumbToPalm < 0.1f &&
            LeftIndexToPalm < 0.05f &&
            LeftMiddleToPalm < 0.05f &&
            LeftRingToPalm < 0.05f &&
            LeftPinkyToPalm < 0.05f
            )
        {
            Debug.Log("WE GOT THE PALM POSE WORKING!!!!!!!!!!");
            MoveDrone();
        }



        //Right hand:

        XRHand Righthand = handSubsystem.rightHand;
        if (RightHand.isTracked)
        {
            Debug.Log("WE GOT THE LEFT HAND WORKING!!!!!!!!!!");
        }

        // May make these global variables:
        XRHandJoint RightPalm = RightHand.GetJoint(XRHandJointID.Palm);
        XRHandJoint RightThumb = RightHand.GetJoint(XRHandJointID.ThumbTip);
        XRHandJoint RightIndex = RightHand.GetJoint(XRHandJointID.IndexTip);
        XRHandJoint RightMiddle = RightHand.GetJoint(XRHandJointID.MiddleTip);
        XRHandJoint RightRing = RightHand.GetJoint(XRHandJointID.RingTip);
        XRHandJoint RightPinky = RightHand.GetJoint(XRHandJointID.LittleTip);


        float RightThumbToPalm = Mathf.Infinity;
        float RightIndexToPalm = Mathf.Infinity;
        float RightMiddleToPalm = Mathf.Infinity;
        float RightRingToPalm = Mathf.Infinity;
        float RightPinkyToPalm = Mathf.Infinity;

        if (RightPalm.TryGetPose(out Pose RightPalmPose)) {


            if (RightThumb.TryGetPose(out Pose RightThumbPose)) {

                RightThumbToPalm = Vector3.Distance(RightThumbPose.position, RightPalmPose.position);

            }

            if (RightIndex.TryGetPose(out Pose RightIndexPose)) {

                RightIndexToPalm = Vector3.Distance(RightIndexPose.position, RightPalmPose.position);

            }

            if (RightMiddle.TryGetPose(out Pose RightMiddlePose))
            {

                RightMiddleToPalm = Vector3.Distance(RightMiddlePose.position, RightPalmPose.position);

            }

            if (RightRing.TryGetPose(out Pose RightRingPose))
            {

                RightRingToPalm = Vector3.Distance(RightRingPose.position, RightPalmPose.position);

            }

            if (RightPinky.TryGetPose(out Pose RightPinkyPose))
            {

                RightPinkyToPalm = Vector3.Distance(RightPinkyPose.position, RightPalmPose.position);

            }

        }

        if (RightThumbToPalm < 0.1f &&
            RightIndexToPalm > 1f &&
            RightMiddleToPalm < 0.05f &&
            RightRingToPalm < 0.05f &&
            RightPinkyToPalm < 0.05f
            )
        {
            Debug.Log("WE GOT THE PALM POSE WORKING!!!!!!!!!!");
            DirectDrone();
        }





    }


    private void MoveDrone()
    {
        
        transform.position += transform.forward * defaultSpeed * Time.deltaTime;

    }

    private void DirectDrone()
    {
        
        XRHandJoint RightIndex = RightHand.GetJoint(XRHandJointID.IndexTip);
        XRHandJoint RightIndexBase = leftHand.GetJoint(XRHandJointID.IndexProximal);

        if (RightIndexTip.TryGetPose(out Pose RightIndexTipPose) &&
            RightIndexBase.TryGetPose(out Pose RightIndexBasePose))
        {
            Vector3 newDirection = (RightIndexTipPose.position - RightIndexBasePose.position).normalized;
            transform.rotation = Quaternion.LookRotation(newDirection);

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("DRONE COLLIDED WITH ", other);
    }
}
