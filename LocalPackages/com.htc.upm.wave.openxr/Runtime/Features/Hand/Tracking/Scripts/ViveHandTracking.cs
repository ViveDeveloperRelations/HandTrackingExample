// "Wave SDK 
// © 2020 HTC Corporation. All Rights Reserved.
//
// Unless otherwise required by copyright law and practice,
// upon the execution of HTC SDK license agreement,
// HTC grants you access to and use of the Wave SDK(s).
// You shall fully comply with all of HTC’s SDK license agreement terms and
// conditions signed by you and all SDK and API requirements,
// specifications, and documentation provided by HTC to You."

using UnityEngine.XR.OpenXR;
using UnityEngine.XR.OpenXR.Features;
using UnityEngine;
using System.Runtime.InteropServices;
using System;
using System.Linq;
using UnityEngine.XR;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.XR.OpenXR.Features;
#endif

namespace Wave.OpenXR.Hand
{
#if UNITY_EDITOR
    [OpenXRFeature(UiName = "VIVE Focus 3 Hand Tracking",
        BuildTargetGroups = new[] { BuildTargetGroup.Android },
        Company = "HTC",
        Desc = "VIVE Focus 3 Hand Tracking",
        DocumentationLink = "..\\Documentation",
        OpenxrExtensionStrings = kOpenxrExtensionString,
        Version = "4.0.0",
        FeatureId = featureId)]
#endif
    public class ViveHandTracking : OpenXRFeature
    {
        const string LOG_TAG = "Wave.OpenXR.Hand.ViveHandTracking";
        void DEBUG(string msg) { Debug.Log(LOG_TAG + " " + msg); }
        void WARNING(string msg) { Debug.LogWarning(LOG_TAG + " " + msg); }
        void ERROR(string msg) { Debug.LogError(LOG_TAG + " " + msg); }

        public const string kOpenxrExtensionString = "XR_EXT_hand_tracking";
        public const string featureId = "vive.wave.openxr.feature.hand.tracking";

        #region OpenXR Life Cycle
        private bool m_XrInstanceCreated = false;
        private XrInstance m_XrInstance = 0;
        protected override bool OnInstanceCreate(ulong xrInstance)
        {
            if (!OpenXRRuntime.IsExtensionEnabled(kOpenxrExtensionString))
            {
                WARNING("OnInstanceCreate() " + kOpenxrExtensionString + " is NOT enabled.");
                return false;
            }

            m_XrInstanceCreated = true;
            m_XrInstance = xrInstance;
            DEBUG("OnInstanceCreate() " + m_XrInstance);

            return GetXrFunctionDelegates(m_XrInstance);
        }

        private XrSystemId m_XrSystemId = 0;
        protected override void OnSystemChange(ulong xrSystem)
        {
            m_XrSystemId = xrSystem;
            DEBUG("OnSystemChange() " + m_XrSystemId);
        }

        private bool m_XrSessionCreated = false;
        private XrSession m_XrSession = 0;
        private bool hasReferenceSpaceLocal = false, hasReferenceSpaceStage = false;
        private XrSpace m_ReferenceSpaceLocal = 0, m_ReferenceSpaceStage = 0;

        private bool hasLeftHandTracker = false, hasRightHandTracker = false;
        private XrHandTrackerEXT leftHandTracker = 0, rightHandTracker = 0;
        protected override void OnSessionCreate(ulong xrSession)
        {
            m_XrSession = xrSession;
            m_XrSessionCreated = true;
            DEBUG("OnSessionCreate() " + m_XrSession);

            // Enumerate supported reference space types and create the XrSpace.
            XrReferenceSpaceType[] spaces = new XrReferenceSpaceType[Enum.GetNames(typeof(XrReferenceSpaceType)).Count()];
            UInt32 spaceCountOutput;
            if (EnumerateReferenceSpaces(
                spaceCapacityInput: 0,
                spaceCountOutput: out spaceCountOutput,
                spaces: out spaces[0]) == XrResult.XR_SUCCESS)
            {
                DEBUG("OnSessionCreate() spaceCountOutput: " + spaceCountOutput);

                Array.Resize(ref spaces, (int)spaceCountOutput);
                if (EnumerateReferenceSpaces(
                    spaceCapacityInput: spaceCountOutput,
                    spaceCountOutput: out spaceCountOutput,
                    spaces: out spaces[0]) == XrResult.XR_SUCCESS)
                {
                    XrReferenceSpaceCreateInfo createInfo;

                    /// Create m_ReferenceSpaceLocal
                    if (IsReferenceSpaceTypeSupported(spaceCountOutput, spaces, XrReferenceSpaceType.XR_REFERENCE_SPACE_TYPE_LOCAL))
                    {
                        createInfo.type = XrStructureType.XR_TYPE_REFERENCE_SPACE_CREATE_INFO;
                        createInfo.next = IntPtr.Zero;
                        createInfo.referenceSpaceType = XrReferenceSpaceType.XR_REFERENCE_SPACE_TYPE_LOCAL;//referenceSpaceType;
                        createInfo.poseInReferenceSpace.orientation = new XrQuaternionf(0, 0, 0, 1);
                        createInfo.poseInReferenceSpace.position = new XrVector3f(0, 0, 0);

                        if (CreateReferenceSpace(
                            createInfo: ref createInfo,
                            space: out m_ReferenceSpaceLocal) == XrResult.XR_SUCCESS)
                        {
                            hasReferenceSpaceLocal = true;
                            DEBUG("OnSessionCreate() CreateReferenceSpace LOCAL: " + m_ReferenceSpaceLocal);
                        }
                        else
                        {
                            ERROR("OnSessionCreate() CreateReferenceSpace LOCAL failed.");
                        }
                    }

                    /// Create m_ReferenceSpaceStage
                    if (IsReferenceSpaceTypeSupported(spaceCountOutput, spaces, XrReferenceSpaceType.XR_REFERENCE_SPACE_TYPE_STAGE))
                    {
                        createInfo.type = XrStructureType.XR_TYPE_REFERENCE_SPACE_CREATE_INFO;
                        createInfo.next = IntPtr.Zero;
                        createInfo.referenceSpaceType = XrReferenceSpaceType.XR_REFERENCE_SPACE_TYPE_STAGE;
                        createInfo.poseInReferenceSpace.orientation = new XrQuaternionf(0, 0, 0, 1);
                        createInfo.poseInReferenceSpace.position = new XrVector3f(0, 0, 0);

                        if (CreateReferenceSpace(
                            createInfo: ref createInfo,
                            space: out m_ReferenceSpaceStage) == XrResult.XR_SUCCESS)
                        {
                            hasReferenceSpaceStage = true;
                            DEBUG("OnSessionCreate() CreateReferenceSpace STAGE: " + m_ReferenceSpaceStage);
                        }
                        else
                        {
                            ERROR("OnSessionCreate() CreateReferenceSpace STAGE failed.");
                        }
                    }
                }
                else
                {
                    ERROR("OnSessionCreate() EnumerateReferenceSpaces(" + spaceCountOutput + ") failed.");
                }
            }
            else
            {
                ERROR("OnSessionCreate() EnumerateReferenceSpaces(0) failed.");
            }

            { // left hand tracker
                if (CreateHandTrackers(true, out XrHandTrackerEXT value))
                {
                    hasLeftHandTracker = true;
                    leftHandTracker = value;
                    DEBUG("OnSessionCreate() leftHandTracker " + leftHandTracker);
                }
            }
            { // right hand tracker
                if (CreateHandTrackers(false, out XrHandTrackerEXT value))
                {
                    hasRightHandTracker = true;
                    rightHandTracker = value;
                    DEBUG("OnSessionCreate() rightHandTracker " + rightHandTracker);
                }
            }
        }

        protected override void OnSessionDestroy(ulong xrSession)
        {
            DEBUG("OnSessionDestroy() " + xrSession);

            if (hasReferenceSpaceLocal)
            {
                if (DestroySpace(m_ReferenceSpaceLocal) == XrResult.XR_SUCCESS)
                {
                    DEBUG("OnSessionDestroy() DestroySpace LOCAL " + m_ReferenceSpaceLocal);
                    m_ReferenceSpaceLocal = 0;
                }
                else
                {
                    ERROR("OnSessionDestroy() DestroySpace LOCAL " + m_ReferenceSpaceLocal + " failed.");
                }
                hasReferenceSpaceLocal = false;
            }
            if (hasReferenceSpaceStage)
            {
                if (DestroySpace(m_ReferenceSpaceStage) == XrResult.XR_SUCCESS)
                {
                    DEBUG("OnSessionDestroy() DestroySpace STAGE " + m_ReferenceSpaceStage);
                    m_ReferenceSpaceStage = 0;
                }
                else
                {
                    ERROR("OnSessionDestroy() DestroySpace STAGE " + m_ReferenceSpaceStage + " failed.");
                }
                hasReferenceSpaceStage = false;
            }

            if (hasLeftHandTracker)
            {
                if (DestroyHandTrackerEXT(leftHandTracker) == XrResult.XR_SUCCESS)
                {
                    DEBUG("OnSessionDestroy() Left DestroyHandTrackerEXT " + leftHandTracker);
                }
                else
                {
                    ERROR("OnSessionDestroy() Left DestroyHandTrackerEXT " + leftHandTracker + " failed.");
                }
                hasLeftHandTracker = false;
            }
            if (hasRightHandTracker)
            {
                if (DestroyHandTrackerEXT(rightHandTracker) == XrResult.XR_SUCCESS)
                {
                    DEBUG("OnSessionDestroy() Right DestroyHandTrackerEXT " + rightHandTracker);
                }
                else
                {
                    ERROR("OnSessionDestroy() Right DestroyHandTrackerEXT " + rightHandTracker + " failed.");
                }
                hasRightHandTracker = false;
            }
            m_XrSessionCreated = false;
        }
        #endregion

        #region OpenXR function delegates
        /// xrGetInstanceProcAddr
        OpenXRHelper.xrGetInstanceProcAddrDelegate XrGetInstanceProcAddr;

        /// xrGetSystemProperties
        OpenXRHelper.xrGetSystemPropertiesDelegate xrGetSystemProperties;
        public XrResult GetSystemProperties(ref XrSystemProperties properties)
        {
            if (m_XrInstanceCreated)
            {
                return xrGetSystemProperties(m_XrInstance, m_XrSystemId, ref properties);
            }

            return XrResult.XR_ERROR_INSTANCE_LOST;
        }

        /// xrEnumerateReferenceSpaces
        OpenXRHelper.xrEnumerateReferenceSpacesDelegate xrEnumerateReferenceSpaces;
        public XrResult EnumerateReferenceSpaces(UInt32 spaceCapacityInput, out UInt32 spaceCountOutput, out XrReferenceSpaceType spaces)
        {
            if (!m_XrSessionCreated)
            {
                spaceCountOutput = 0;
                spaces = XrReferenceSpaceType.XR_REFERENCE_SPACE_TYPE_UNBOUNDED_MSFT;
                return XrResult.XR_ERROR_SESSION_NOT_RUNNING;
            }

            return xrEnumerateReferenceSpaces(m_XrSession, spaceCapacityInput, out spaceCountOutput, out spaces);
        }

        /// xrCreateReferenceSpace
        OpenXRHelper.xrCreateReferenceSpaceDelegate xrCreateReferenceSpace;
        public XrResult CreateReferenceSpace(ref XrReferenceSpaceCreateInfo createInfo, out XrSpace space)
        {
            if (!m_XrSessionCreated)
            {
                space = 0;
                return XrResult.XR_ERROR_SESSION_NOT_RUNNING;
            }

            return xrCreateReferenceSpace(m_XrSession, ref createInfo, out space);
        }

        /// xrDestroySpace
        OpenXRHelper.xrDestroySpaceDelegate xrDestroySpace;
        public XrResult DestroySpace(XrSpace space)
        {
            return xrDestroySpace(space);
        }

        /// xrCreateHandTrackerEXT
        ViveHandTrackingHelper.xrCreateHandTrackerEXTDelegate xrCreateHandTrackerEXT;
        public XrResult CreateHandTrackerEXT(ref XrHandTrackerCreateInfoEXT createInfo, out XrHandTrackerEXT handTracker)
        {
            if (!m_XrSessionCreated)
            {
                handTracker = 0;
                return XrResult.XR_ERROR_SESSION_NOT_RUNNING;
            }

            return xrCreateHandTrackerEXT(m_XrSession, ref createInfo, out handTracker);
        }

        /// xrDestroyHandTrackerEXT
        ViveHandTrackingHelper.xrDestroyHandTrackerEXTDelegate xrDestroyHandTrackerEXT;
        public XrResult DestroyHandTrackerEXT(XrHandTrackerEXT handTracker)
            => xrDestroyHandTrackerEXT(handTracker);

        /// xrLocateHandJointsEXT
        ViveHandTrackingHelper.xrLocateHandJointsEXTDelegate xrLocateHandJointsEXT;
        public XrResult LocateHandJointsEXT(XrHandTrackerEXT handTracker, XrHandJointsLocateInfoEXT locateInfo, ref XrHandJointLocationsEXT locations)
            => xrLocateHandJointsEXT(handTracker, locateInfo, ref locations);

        private bool GetXrFunctionDelegates(XrInstance xrInstance)
        {
            /// xrGetInstanceProcAddr
            if (xrGetInstanceProcAddr != null && xrGetInstanceProcAddr != IntPtr.Zero)
            {
                DEBUG("Get function pointer of xrGetInstanceProcAddr.");
                XrGetInstanceProcAddr = Marshal.GetDelegateForFunctionPointer(
                    xrGetInstanceProcAddr,
                    typeof(OpenXRHelper.xrGetInstanceProcAddrDelegate)) as OpenXRHelper.xrGetInstanceProcAddrDelegate;
            }
            else
            {
                ERROR("xrGetInstanceProcAddr");
                return false;
            }

            IntPtr funcPtr = IntPtr.Zero;
            /// xrGetSystemProperties
            if (XrGetInstanceProcAddr(xrInstance, "xrGetSystemProperties", out funcPtr) == XrResult.XR_SUCCESS)
            {
                if (funcPtr != IntPtr.Zero)
                {
                    DEBUG("Get function pointer of xrGetSystemProperties.");
                    xrGetSystemProperties = Marshal.GetDelegateForFunctionPointer(
                        funcPtr,
                        typeof(OpenXRHelper.xrGetSystemPropertiesDelegate)) as OpenXRHelper.xrGetSystemPropertiesDelegate;
                }
            }
            else
            {
                ERROR("xrGetSystemProperties");
                return false;
            }
            /// xrEnumerateReferenceSpaces
            if (XrGetInstanceProcAddr(xrInstance, "xrEnumerateReferenceSpaces", out funcPtr) == XrResult.XR_SUCCESS)
            {
                if (funcPtr != IntPtr.Zero)
                {
                    DEBUG("Get function pointer of xrEnumerateReferenceSpaces.");
                    xrEnumerateReferenceSpaces = Marshal.GetDelegateForFunctionPointer(
                        funcPtr,
                        typeof(OpenXRHelper.xrEnumerateReferenceSpacesDelegate)) as OpenXRHelper.xrEnumerateReferenceSpacesDelegate;
                }
            }
            else
            {
                ERROR("xrEnumerateReferenceSpaces");
                return false;
            }
            /// xrCreateReferenceSpace
            if (XrGetInstanceProcAddr(xrInstance, "xrCreateReferenceSpace", out funcPtr) == XrResult.XR_SUCCESS)
            {
                if (funcPtr != IntPtr.Zero)
                {
                    DEBUG("Get function pointer of xrCreateReferenceSpace.");
                    xrCreateReferenceSpace = Marshal.GetDelegateForFunctionPointer(
                        funcPtr,
                        typeof(OpenXRHelper.xrCreateReferenceSpaceDelegate)) as OpenXRHelper.xrCreateReferenceSpaceDelegate;
                }
            }
            else
            {
                ERROR("xrCreateReferenceSpace");
                return false;
            }
            /// xrDestroySpace
            if (XrGetInstanceProcAddr(xrInstance, "xrDestroySpace", out funcPtr) == XrResult.XR_SUCCESS)
            {
                if (funcPtr != IntPtr.Zero)
                {
                    DEBUG("Get function pointer of xrDestroySpace.");
                    xrDestroySpace = Marshal.GetDelegateForFunctionPointer(
                        funcPtr,
                        typeof(OpenXRHelper.xrDestroySpaceDelegate)) as OpenXRHelper.xrDestroySpaceDelegate;
                }
            }
            else
            {
                ERROR("xrDestroySpace");
                return false;
            }

            /// xrCreateHandTrackerEXT
            if (XrGetInstanceProcAddr(xrInstance, "xrCreateHandTrackerEXT", out funcPtr) == XrResult.XR_SUCCESS)
            {
                if (funcPtr != IntPtr.Zero)
                {
                    DEBUG("Get function pointer of xrCreateHandTrackerEXT.");
                    xrCreateHandTrackerEXT = Marshal.GetDelegateForFunctionPointer(
                        funcPtr,
                        typeof(ViveHandTrackingHelper.xrCreateHandTrackerEXTDelegate)) as ViveHandTrackingHelper.xrCreateHandTrackerEXTDelegate;
                }
            }
            else
            {
                ERROR("xrCreateHandTrackerEXT");
                return false;
            }
            /// xrDestroyHandTrackerEXT
            if (XrGetInstanceProcAddr(xrInstance, "xrDestroyHandTrackerEXT", out funcPtr) == XrResult.XR_SUCCESS)
            {
                if (funcPtr != IntPtr.Zero)
                {
                    DEBUG("Get function pointer of xrDestroyHandTrackerEXT.");
                    xrDestroyHandTrackerEXT = Marshal.GetDelegateForFunctionPointer(
                        funcPtr,
                        typeof(ViveHandTrackingHelper.xrDestroyHandTrackerEXTDelegate)) as ViveHandTrackingHelper.xrDestroyHandTrackerEXTDelegate;
                }
            }
            else
            {
                ERROR("xrDestroyHandTrackerEXT");
                return false;
            }
            /// xrLocateHandJointsEXT
            if (XrGetInstanceProcAddr(xrInstance, "xrLocateHandJointsEXT", out funcPtr) == XrResult.XR_SUCCESS)
            {
                if (funcPtr != IntPtr.Zero)
                {
                    DEBUG("Get function pointer of xrLocateHandJointsEXT.");
                    xrLocateHandJointsEXT = Marshal.GetDelegateForFunctionPointer(
                        funcPtr,
                        typeof(ViveHandTrackingHelper.xrLocateHandJointsEXTDelegate)) as ViveHandTrackingHelper.xrLocateHandJointsEXTDelegate;
                }
            }
            else
            {
                ERROR("xrLocateHandJointsEXT");
                return false;
            }

            return true;
        }
        #endregion

        static List<XRInputSubsystem> s_InputSubsystems = new List<XRInputSubsystem>();
        public TrackingOriginModeFlags GetTrackingOriginMode()
        {
            XRInputSubsystem subsystem = null;

            SubsystemManager.GetInstances(s_InputSubsystems);
            if (s_InputSubsystems.Count > 0)
            {
                subsystem = s_InputSubsystems[0];
            }

            if (subsystem != null)
            {
                return subsystem.GetTrackingOriginMode();
            }

            return TrackingOriginModeFlags.Unknown;
        }
        private bool IsReferenceSpaceTypeSupported(UInt32 spaceCountOutput, XrReferenceSpaceType[] spaces, XrReferenceSpaceType space)
        {
            bool support = false;
            for (int i = 0; i < spaceCountOutput; i++)
            {
                DEBUG("IsReferenceSpaceTypeSupported() supported space[" + i + "]: " + spaces[i]);
                if (spaces[i] == space) { support = true; }
            }

            return support;
        }

        XrSystemHandTrackingPropertiesEXT handTrackingSystemProperties;
        XrSystemProperties systemProperties;
        private bool IsHandTrackingSupported()
        {
            bool ret = false;
            if (!m_XrSessionCreated)
            {
                ERROR("IsHandTrackingSupported() session is not created.");
                return ret;
            }

            handTrackingSystemProperties.type = XrStructureType.XR_TYPE_SYSTEM_HAND_TRACKING_PROPERTIES_EXT;
            systemProperties.type = XrStructureType.XR_TYPE_SYSTEM_PROPERTIES;
            systemProperties.next = Marshal.AllocHGlobal(Marshal.SizeOf(handTrackingSystemProperties));

            long offset = 0;
            if (IntPtr.Size == 4)
                offset = systemProperties.next.ToInt32();
            else
                offset = systemProperties.next.ToInt64();

            IntPtr sys_hand_tracking_prop_ptr = new IntPtr(offset);
            Marshal.StructureToPtr(handTrackingSystemProperties, sys_hand_tracking_prop_ptr, false);

            if (GetSystemProperties(ref systemProperties) == XrResult.XR_SUCCESS)
            {
                if (IntPtr.Size == 4)
                    offset = systemProperties.next.ToInt32();
                else
                    offset = systemProperties.next.ToInt64();

                sys_hand_tracking_prop_ptr = new IntPtr(offset);
                handTrackingSystemProperties = (XrSystemHandTrackingPropertiesEXT)Marshal.PtrToStructure(sys_hand_tracking_prop_ptr, typeof(XrSystemHandTrackingPropertiesEXT));

                DEBUG("IsHandTrackingSupported() XrSystemHandTrackingPropertiesEXT.supportsHandTracking: " + handTrackingSystemProperties.supportsHandTracking);
                ret = handTrackingSystemProperties.supportsHandTracking > 0;
            }
            else
            {
                ERROR("IsHandTrackingSupported() GetSystemProperties failed.");
            }

            Marshal.FreeHGlobal(systemProperties.next);
            return ret;
        }

        private bool CreateHandTrackers(bool isLeft, out XrHandTrackerEXT handTracker)
        {
            if (!IsHandTrackingSupported())
            {
                ERROR("CreateHandTrackers() " + (isLeft ? "Left" : "Right") + " hand tracking is NOT supported.");
                handTracker = 0;
                return false;
            }

            XrHandTrackerCreateInfoEXT createInfo;
            createInfo.type = XrStructureType.XR_TYPE_HAND_TRACKER_CREATE_INFO_EXT;
            createInfo.next = IntPtr.Zero;
            createInfo.hand = isLeft ? XrHandEXT.XR_HAND_LEFT_EXT : XrHandEXT.XR_HAND_RIGHT_EXT;
            createInfo.handJointSet = XrHandJointSetEXT.XR_HAND_JOINT_SET_DEFAULT_EXT;

            var ret = CreateHandTrackerEXT(ref createInfo, out handTracker);
            DEBUG("CreateHandTrackers() " + (isLeft ? "Left" : "Right") + " CreateHandTrackerEXT = " + ret);

            return ret == XrResult.XR_SUCCESS;
        }

        private XrHandJointLocationEXT[] jointLocationsL = new XrHandJointLocationEXT[(int)XrHandJointEXT.XR_HAND_JOINT_MAX_ENUM_EXT];
        private XrHandJointLocationEXT[] jointLocationsR = new XrHandJointLocationEXT[(int)XrHandJointEXT.XR_HAND_JOINT_MAX_ENUM_EXT];
        private XrHandJointLocationsEXT locations;

        /// <summary>
        /// Retrieves the <see href = "https://www.khronos.org/registry/OpenXR/specs/1.0/html/xrspec.html#XrHandJointLocationEXT"> XrHandJointLocationEXT </see> data.
        /// </summary>
        /// <param name="isLeft">Left or right hand.</param>
        /// <param name="handJointLocation">Output parameter to retrieve <see href = "https://www.khronos.org/registry/OpenXR/specs/1.0/html/xrspec.html#XrHandJointLocationEXT"> XrHandJointLocationEXT </see> data.</param>
        /// <returns>True for valid data.</returns>
        public bool GetJointLocations(bool isLeft, out XrHandJointLocationEXT[] handJointLocation)
        {
            bool ret = false;
            handJointLocation = isLeft ? jointLocationsL : jointLocationsR;

            if (isLeft && !hasLeftHandTracker) { return ret; }
            if (!isLeft && !hasRightHandTracker) { return ret; }

            TrackingOriginModeFlags origin = GetTrackingOriginMode();
            if (origin == TrackingOriginModeFlags.Unknown || origin == TrackingOriginModeFlags.Unbounded) { return ret; }
            XrSpace baseSpace = (origin == TrackingOriginModeFlags.Device ? m_ReferenceSpaceLocal : m_ReferenceSpaceStage);

            /// Configures XrHandJointsLocateInfoEXT
            XrHandJointsLocateInfoEXT locateInfo = new XrHandJointsLocateInfoEXT(
                in_type: XrStructureType.XR_TYPE_HAND_JOINTS_LOCATE_INFO_EXT,
                in_next: IntPtr.Zero,
                in_baseSpace: baseSpace,
                in_time: 0);

            /// Configures XrHandJointLocationsEXT
            locations.type = XrStructureType.XR_TYPE_HAND_JOINT_LOCATIONS_EXT;
            locations.next = IntPtr.Zero;
            locations.isActive = false;
            locations.jointCount = (uint)XrHandJointEXT.XR_HAND_JOINT_MAX_ENUM_EXT;

            XrHandJointLocationEXT joint_location_ext_type = default(XrHandJointLocationEXT);
            int jointLocationsLength = isLeft ? jointLocationsL.Length : jointLocationsR.Length;
            locations.jointLocations = Marshal.AllocHGlobal(Marshal.SizeOf(joint_location_ext_type) * jointLocationsLength);

            long offset = 0;
            if (IntPtr.Size == 4)
                offset = locations.jointLocations.ToInt32();
            else
                offset = locations.jointLocations.ToInt64();

            for (int i = 0; i < jointLocationsLength; i++)
            {
                IntPtr joint_location_ext_ptr = new IntPtr(offset);

                if (isLeft)
                    Marshal.StructureToPtr(jointLocationsL[i], joint_location_ext_ptr, false);
                else
                    Marshal.StructureToPtr(jointLocationsR[i], joint_location_ext_ptr, false);

                offset += Marshal.SizeOf(joint_location_ext_type);
            }

            if (LocateHandJointsEXT(
                handTracker: (isLeft ? leftHandTracker : rightHandTracker),
                locateInfo: locateInfo,
                locations: ref locations) == XrResult.XR_SUCCESS)
            {
                if (locations.isActive)
                {
                    if (IntPtr.Size == 4)
                        offset = locations.jointLocations.ToInt32();
                    else
                        offset = locations.jointLocations.ToInt64();

                    for (int i = 0; i < locations.jointCount; i++)
                    {
                        IntPtr joint_location_ext_ptr = new IntPtr(offset);

                        if (isLeft)
                            jointLocationsL[i] = (XrHandJointLocationEXT)Marshal.PtrToStructure(joint_location_ext_ptr, typeof(XrHandJointLocationEXT));
                        else
                            jointLocationsR[i] = (XrHandJointLocationEXT)Marshal.PtrToStructure(joint_location_ext_ptr, typeof(XrHandJointLocationEXT));

                        offset += Marshal.SizeOf(joint_location_ext_type);
                    }

                    // ToDo: locationFlags?
                    handJointLocation = isLeft ? jointLocationsL : jointLocationsR;

                    ret = true;
                }
            }

            Marshal.FreeHGlobal(locations.jointLocations);
            return ret;
        }
    }
}
