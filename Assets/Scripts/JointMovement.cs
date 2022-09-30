using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wave.OpenXR.Toolkit;
using Wave.OpenXR.Toolkit.Hand;

public class JointMovement : MonoBehaviour
{
    public int jointNum;
    public bool isLeft;

    // Update is called once per frame
    void Update()
    {
        HandJoint joint = HandTracking.GetHandJointLocations(isLeft ? HandFlag.Left : HandFlag.Right)[jointNum];
        if (joint.isValid)
        {
            transform.localPosition = joint.position;
            transform.rotation = joint.rotation;
        }
    }
}
