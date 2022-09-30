using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wave.OpenXR.Toolkit;
using Wave.OpenXR.Toolkit.Hand;

public class SampleHandViewer : MonoBehaviour
{
    public class HandVisualizer
    {
        public readonly VisualizedJoint[] Joints;
        public readonly HandFlag HandFlag;
        public readonly GameObject HandParent;
        public HandVisualizer(HandFlag handFlag)
        {
            HandFlag = handFlag;
            var handJoints = HandTracking.GetHandJointLocations(HandFlag);
            Joints = new VisualizedJoint[handJoints.Length];

            HandParent = new GameObject(handFlag.ToString());
            
            for (var i = 0; i < handJoints.Length; i++)
            {
                var joint = handJoints[i];
                
                GameObject jointObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                jointObject.transform.parent = HandParent.transform;
                jointObject.transform.position = joint.position;
                jointObject.transform.localScale = Vector3.one * 0.01f;
                Joints[i] = new VisualizedJoint()
                {
                    Joint = joint,
                    JointObject = jointObject
                };
            }
        }

        public void UpdatePositions()
        {
            var handJoints = HandTracking.GetHandJointLocations(HandFlag);
            for (var i = 0; i < handJoints.Length; i++)
            {
                var visualizedJoint = Joints[i];
                var joint = handJoints[i];
                visualizedJoint.Joint = joint;
                //a user could destroy it for some reason
                if (visualizedJoint.JointObject != null)
                {
                    var isValid = joint.isValid;
                    //visualizedJoint.JointObject.SetActive(isValid);
                    Debug.Log($"Joint {i} is valid {isValid} on hand {HandFlag}");
                    if (isValid)
                    {
                        visualizedJoint.JointObject.transform.position = joint.position;
                    }
                }
            }
        }

        public class VisualizedJoint
        {
            public HandJoint Joint;
            public GameObject JointObject;
        }
        
    }
    HandVisualizer m_LeftHandVisualizer;
    HandVisualizer m_RightHandVisualizer;
    public IEnumerator Start()
    {
        m_LeftHandVisualizer = new HandVisualizer(HandFlag.Left);
        m_RightHandVisualizer = new HandVisualizer(HandFlag.Right);
        
        while (true)
        {
            m_LeftHandVisualizer.UpdatePositions();
            m_RightHandVisualizer.UpdatePositions();

            yield return null;
        }
    }
    

}
