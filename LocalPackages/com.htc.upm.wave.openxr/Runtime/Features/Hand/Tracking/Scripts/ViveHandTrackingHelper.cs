// "Wave SDK 
// © 2020 HTC Corporation. All Rights Reserved.
//
// Unless otherwise required by copyright law and practice,
// upon the execution of HTC SDK license agreement,
// HTC grants you access to and use of the Wave SDK(s).
// You shall fully comply with all of HTC’s SDK license agreement terms and
// conditions signed by you and all SDK and API requirements,
// specifications, and documentation provided by HTC to You."

using System;

namespace Wave.OpenXR.Hand
{
    public struct XrHandTrackerEXT : IEquatable<ulong>
    {
        private readonly ulong value;

        public XrHandTrackerEXT(ulong u)
        {
            value = u;
        }

        public static implicit operator ulong(XrHandTrackerEXT xrInst)
        {
            return xrInst.value;
        }
        public static implicit operator XrHandTrackerEXT(ulong u)
        {
            return new XrHandTrackerEXT(u);
        }

        public bool Equals(XrHandTrackerEXT other)
        {
            return value == other.value;
        }
        public bool Equals(ulong other)
        {
            return value == other;
        }
        public override bool Equals(object obj)
        {
            return obj is XrHandTrackerEXT && Equals((XrHandTrackerEXT)obj);
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public override string ToString()
        {
            return value.ToString();
        }

        public static bool operator ==(XrHandTrackerEXT a, XrHandTrackerEXT b) { return a.Equals(b); }
        public static bool operator !=(XrHandTrackerEXT a, XrHandTrackerEXT b) { return !a.Equals(b); }
        public static bool operator >=(XrHandTrackerEXT a, XrHandTrackerEXT b) { return a.value >= b.value; }
        public static bool operator <=(XrHandTrackerEXT a, XrHandTrackerEXT b) { return a.value <= b.value; }
        public static bool operator >(XrHandTrackerEXT a, XrHandTrackerEXT b) { return a.value > b.value; }
        public static bool operator <(XrHandTrackerEXT a, XrHandTrackerEXT b) { return a.value < b.value; }
        public static XrHandTrackerEXT operator +(XrHandTrackerEXT a, XrHandTrackerEXT b) { return a.value + b.value; }
        public static XrHandTrackerEXT operator -(XrHandTrackerEXT a, XrHandTrackerEXT b) { return a.value - b.value; }
        public static XrHandTrackerEXT operator *(XrHandTrackerEXT a, XrHandTrackerEXT b) { return a.value * b.value; }
        public static XrHandTrackerEXT operator /(XrHandTrackerEXT a, XrHandTrackerEXT b)
        {
            if (b.value == 0)
            {
                throw new DivideByZeroException();
            }
            return a.value / b.value;
        }

    }

    public enum XrHandEXT
    {
        XR_HAND_LEFT_EXT = 1,
        XR_HAND_RIGHT_EXT = 2,
        XR_HAND_MAX_ENUM_EXT = 3
    }
    public enum XrHandJointEXT
    {
        XR_HAND_JOINT_PALM_EXT = 0,
        XR_HAND_JOINT_WRIST_EXT = 1,
        XR_HAND_JOINT_THUMB_METACARPAL_EXT = 2,
        XR_HAND_JOINT_THUMB_PROXIMAL_EXT = 3,
        XR_HAND_JOINT_THUMB_DISTAL_EXT = 4,
        XR_HAND_JOINT_THUMB_TIP_EXT = 5,
        XR_HAND_JOINT_INDEX_METACARPAL_EXT = 6,
        XR_HAND_JOINT_INDEX_PROXIMAL_EXT = 7,
        XR_HAND_JOINT_INDEX_INTERMEDIATE_EXT = 8,
        XR_HAND_JOINT_INDEX_DISTAL_EXT = 9,
        XR_HAND_JOINT_INDEX_TIP_EXT = 10,
        XR_HAND_JOINT_MIDDLE_METACARPAL_EXT = 11,
        XR_HAND_JOINT_MIDDLE_PROXIMAL_EXT = 12,
        XR_HAND_JOINT_MIDDLE_INTERMEDIATE_EXT = 13,
        XR_HAND_JOINT_MIDDLE_DISTAL_EXT = 14,
        XR_HAND_JOINT_MIDDLE_TIP_EXT = 15,
        XR_HAND_JOINT_RING_METACARPAL_EXT = 16,
        XR_HAND_JOINT_RING_PROXIMAL_EXT = 17,
        XR_HAND_JOINT_RING_INTERMEDIATE_EXT = 18,
        XR_HAND_JOINT_RING_DISTAL_EXT = 19,
        XR_HAND_JOINT_RING_TIP_EXT = 20,
        XR_HAND_JOINT_LITTLE_METACARPAL_EXT = 21,
        XR_HAND_JOINT_LITTLE_PROXIMAL_EXT = 22,
        XR_HAND_JOINT_LITTLE_INTERMEDIATE_EXT = 23,
        XR_HAND_JOINT_LITTLE_DISTAL_EXT = 24,
        XR_HAND_JOINT_LITTLE_TIP_EXT = 25,
        XR_HAND_JOINT_MAX_ENUM_EXT = 26
    }
    public enum XrHandJointSetEXT
    {
        XR_HAND_JOINT_SET_DEFAULT_EXT = 0,
        XR_HAND_JOINT_SET_MAX_ENUM_EXT = 1
    }

    public struct XrSystemHandTrackingPropertiesEXT
    {
        public XrStructureType type;
        public IntPtr next;
        public XrBool32 supportsHandTracking;
    };
    public struct XrHandTrackerCreateInfoEXT
    {
        public XrStructureType type;
        public IntPtr next;
        public XrHandEXT hand;
        public XrHandJointSetEXT handJointSet;
        public XrHandTrackerCreateInfoEXT(XrStructureType in_type, IntPtr in_next, XrHandEXT in_hand, XrHandJointSetEXT in_handJointSet)
        {
            type = in_type;
            next = in_next;
            hand = in_hand;
            handJointSet = in_handJointSet;
        }
    }
    public struct XrHandJointsLocateInfoEXT
    {
        public XrStructureType type;
        public IntPtr next;
        public XrSpace baseSpace;
        public XrTime time;
        public XrHandJointsLocateInfoEXT(XrStructureType in_type, IntPtr in_next, XrSpace in_baseSpace, XrTime in_time)
        {
            type = in_type;
            next = in_next;
            baseSpace = in_baseSpace;
            time = in_time;
        }
    };
    public struct XrHandJointLocationEXT
    {
        public XrSpaceLocationFlags locationFlags;
        public XrPosef pose;
        public float radius;
    }
    public struct XrHandJointLocationsEXT
    {
        public XrStructureType type;
        public IntPtr next;
        public XrBool32 isActive;
        public UInt32 jointCount;
        public IntPtr jointLocations;  //XrHandJointLocationEXT*
        public XrHandJointLocationsEXT(XrStructureType in_type, IntPtr in_next, XrBool32 in_isActive, UInt32 in_jointCount, IntPtr in_jointLocations)
        {
            type = in_type;
            next = in_next;
            isActive = in_isActive;
            jointCount = in_jointCount;
            jointLocations = in_jointLocations;
        }
    }

    public static class ViveHandTrackingHelper
    {
        public const int XR_HAND_JOINT_COUNT_EXT = 26;

        public delegate XrResult xrCreateHandTrackerEXTDelegate(
            XrSession session,
            ref XrHandTrackerCreateInfoEXT createInfo,
            out XrHandTrackerEXT handTracker);
        public delegate XrResult xrDestroyHandTrackerEXTDelegate(
            XrHandTrackerEXT handTracker);
        public delegate XrResult xrLocateHandJointsEXTDelegate(
            XrHandTrackerEXT handTracker,
            XrHandJointsLocateInfoEXT locateInfo,
            ref XrHandJointLocationsEXT locations);
    }
}
