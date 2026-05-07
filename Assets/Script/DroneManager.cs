using UnityEngine;
using UnityEngine.XR.Hands;
using UnityEngine.SubsystemsImplementation;
using NUnit.Framework;
using System.Collections.Generic;

public class DroneManager : MonoBehaviour
{

    XRHandSubsystem handSubsystem;
    [SerializeField]
    private float defaultSpeed = 20f;
    public GameManager GM;
    private Rigidbody rb;
    public bool ShouldMove = false;

    private SoundManager soundManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var subsystems = new List<XRHandSubsystem>();
        SubsystemManager.GetSubsystems(subsystems);

        if (subsystems.Count > 0)
        {
            handSubsystem = subsystems[0];
        }

        GM = gameObject.GetComponent<GameManager>();
        rb = gameObject.GetComponent<Rigidbody>();
        if (rb == null) Debug.Log("RB NULL");

        soundManager = gameObject.GetComponent<SoundManager>();
    }

    private void FixedUpdate()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (handSubsystem == null) return; //safety check

        XRHand Lefthand = handSubsystem.leftHand;
        if (Lefthand.isTracked)
        {
            //Debug.Log("WE GOT THE LEFT HAND WORKING!!!!!!!!!!");
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
            //Debug.Log("WE GOT THE PALM POSE WORKING!!!!!!!!!!");
            MoveDrone();
        }



            //Right hand:

            XRHand Righthand = handSubsystem.rightHand;
        if (Righthand.isTracked)
        {
            //Debug.Log("WE GOT THE RIGHT HAND WORKING!!!!!!!!!!");
        }

        // May make these global variables:
        XRHandJoint RightPalm = Righthand.GetJoint(XRHandJointID.Palm);
        XRHandJoint RightThumb = Righthand.GetJoint(XRHandJointID.ThumbTip);
        XRHandJoint RightIndex = Righthand.GetJoint(XRHandJointID.IndexTip);
        XRHandJoint RightMiddle = Righthand.GetJoint(XRHandJointID.MiddleTip);
        XRHandJoint RightRing = Righthand.GetJoint(XRHandJointID.RingTip);
        XRHandJoint RightPinky = Righthand.GetJoint(XRHandJointID.LittleTip);


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
            RightIndexToPalm > 0.1f &&
            RightMiddleToPalm < 0.05f &&
            RightRingToPalm < 0.05f &&
            RightPinkyToPalm < 0.05f && Time.timeScale > 0f
            )
        {
            //Debug.Log("WE GOT THE POINTING POSE WORKING!!!!!!!!!!");
            DirectDrone();
        }





    }


    private void MoveDrone()
    {
        if (Time.timeScale > 0f)
        {
            ShouldMove = true;
            Vector3 newPosition = rb.position + transform.forward * defaultSpeed * Time.fixedDeltaTime;
            rb.MovePosition(newPosition);
        }
        else ShouldMove = false;
        //transform.position += transform.forward * defaultSpeed * Time.deltaTime;

    }

    private void DirectDrone()
    {
        XRHand Righthand = handSubsystem.rightHand;
        XRHandJoint RightIndexTip = Righthand.GetJoint(XRHandJointID.IndexTip);
        XRHandJoint RightIndexBase = Righthand.GetJoint(XRHandJointID.IndexProximal);
 
        if (RightIndexTip.TryGetPose(out Pose RightIndexTipPose) &&
            RightIndexBase.TryGetPose(out Pose RightIndexBasePose))
        {
            //gets finger direction
            Vector3 fingerDirection =  (RightIndexTipPose.position - RightIndexBasePose.position).normalized;


            float yawInput = fingerDirection.x; //left, right direction goes here
            float pitchInput = -fingerDirection.y; //up,down direction goes here

            float yawSpeed = 90f; //horizontal turning speed 
            float pitchSpeed = 60f; //vertical turning speed 

            transform.Rotate(
                Vector3.up,
                yawInput * yawSpeed * Time.deltaTime,
                Space.World
            );
            
            transform.Rotate(
                Vector3.right,
                pitchInput * pitchSpeed * Time.deltaTime,
                Space.Self
            );
       
        }

    }
}
