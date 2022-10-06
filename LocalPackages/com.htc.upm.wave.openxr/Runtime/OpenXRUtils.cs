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
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

namespace Wave.OpenXR
{
    public enum XrObjectType
    {
        XR_OBJECT_TYPE_UNKNOWN = 0,
        XR_OBJECT_TYPE_INSTANCE = 1,
        XR_OBJECT_TYPE_SESSION = 2,
        XR_OBJECT_TYPE_SWAPCHAIN = 3,
        XR_OBJECT_TYPE_SPACE = 4,
        XR_OBJECT_TYPE_ACTION_SET = 5,
        XR_OBJECT_TYPE_ACTION = 6,
        XR_OBJECT_TYPE_DEBUG_UTILS_MESSENGER_EXT = 1000019000,
        XR_OBJECT_TYPE_SPATIAL_ANCHOR_MSFT = 1000039000,
        XR_OBJECT_TYPE_HAND_TRACKER_EXT = 1000051000,
        XR_OBJECT_TYPE_SCENE_OBSERVER_MSFT = 1000097000,
        XR_OBJECT_TYPE_SCENE_MSFT = 1000097001,
        XR_OBJECT_TYPE_FACIAL_TRACKER_HTC = 1000104000,
        XR_OBJECT_TYPE_FOVEATION_PROFILE_FB = 1000114000,
        XR_OBJECT_TYPE_TRIANGLE_MESH_FB = 1000117000,
        XR_OBJECT_TYPE_PASSTHROUGH_FB = 1000118000,
        XR_OBJECT_TYPE_PASSTHROUGH_LAYER_FB = 1000118002,
        XR_OBJECT_TYPE_GEOMETRY_INSTANCE_FB = 1000118004,
        XR_OBJECT_TYPE_SPATIAL_ANCHOR_STORE_CONNECTION_MSFT = 1000142000,
        XR_OBJECT_TYPE_MAX_ENUM = 0x7FFFFFFF
    }
    public enum XrResult
    {
        XR_SUCCESS = 0,
        XR_TIMEOUT_EXPIRED = 1,
        XR_SESSION_LOSS_PENDING = 3,
        XR_EVENT_UNAVAILABLE = 4,
        XR_SPACE_BOUNDS_UNAVAILABLE = 7,
        XR_SESSION_NOT_FOCUSED = 8,
        XR_FRAME_DISCARDED = 9,
        XR_ERROR_VALIDATION_FAILURE = -1,
        XR_ERROR_RUNTIME_FAILURE = -2,
        XR_ERROR_OUT_OF_MEMORY = -3,
        XR_ERROR_API_VERSION_UNSUPPORTED = -4,
        XR_ERROR_INITIALIZATION_FAILED = -6,
        XR_ERROR_FUNCTION_UNSUPPORTED = -7,
        XR_ERROR_FEATURE_UNSUPPORTED = -8,
        XR_ERROR_EXTENSION_NOT_PRESENT = -9,
        XR_ERROR_LIMIT_REACHED = -10,
        XR_ERROR_SIZE_INSUFFICIENT = -11,
        XR_ERROR_HANDLE_INVALID = -12,
        XR_ERROR_INSTANCE_LOST = -13,
        XR_ERROR_SESSION_RUNNING = -14,
        XR_ERROR_SESSION_NOT_RUNNING = -16,
        XR_ERROR_SESSION_LOST = -17,
        XR_ERROR_SYSTEM_INVALID = -18,
        XR_ERROR_PATH_INVALID = -19,
        XR_ERROR_PATH_COUNT_EXCEEDED = -20,
        XR_ERROR_PATH_FORMAT_INVALID = -21,
        XR_ERROR_PATH_UNSUPPORTED = -22,
        XR_ERROR_LAYER_INVALID = -23,
        XR_ERROR_LAYER_LIMIT_EXCEEDED = -24,
        XR_ERROR_SWAPCHAIN_RECT_INVALID = -25,
        XR_ERROR_SWAPCHAIN_FORMAT_UNSUPPORTED = -26,
        XR_ERROR_ACTION_TYPE_MISMATCH = -27,
        XR_ERROR_SESSION_NOT_READY = -28,
        XR_ERROR_SESSION_NOT_STOPPING = -29,
        XR_ERROR_TIME_INVALID = -30,
        XR_ERROR_REFERENCE_SPACE_UNSUPPORTED = -31,
        XR_ERROR_FILE_ACCESS_ERROR = -32,
        XR_ERROR_FILE_CONTENTS_INVALID = -33,
        XR_ERROR_FORM_FACTOR_UNSUPPORTED = -34,
        XR_ERROR_FORM_FACTOR_UNAVAILABLE = -35,
        XR_ERROR_API_LAYER_NOT_PRESENT = -36,
        XR_ERROR_CALL_ORDER_INVALID = -37,
        XR_ERROR_GRAPHICS_DEVICE_INVALID = -38,
        XR_ERROR_POSE_INVALID = -39,
        XR_ERROR_INDEX_OUT_OF_RANGE = -40,
        XR_ERROR_VIEW_CONFIGURATION_TYPE_UNSUPPORTED = -41,
        XR_ERROR_ENVIRONMENT_BLEND_MODE_UNSUPPORTED = -42,
        XR_ERROR_NAME_DUPLICATED = -44,
        XR_ERROR_NAME_INVALID = -45,
        XR_ERROR_ACTIONSET_NOT_ATTACHED = -46,
        XR_ERROR_ACTIONSETS_ALREADY_ATTACHED = -47,
        XR_ERROR_LOCALIZED_NAME_DUPLICATED = -48,
        XR_ERROR_LOCALIZED_NAME_INVALID = -49,
        XR_ERROR_GRAPHICS_REQUIREMENTS_CALL_MISSING = -50,
        XR_ERROR_RUNTIME_UNAVAILABLE = -51,
        XR_ERROR_ANDROID_THREAD_SETTINGS_ID_INVALID_KHR = -1000003000,
        XR_ERROR_ANDROID_THREAD_SETTINGS_FAILURE_KHR = -1000003001,
        XR_ERROR_CREATE_SPATIAL_ANCHOR_FAILED_MSFT = -1000039001,
        XR_ERROR_SECONDARY_VIEW_CONFIGURATION_TYPE_NOT_ENABLED_MSFT = -1000053000,
        XR_ERROR_CONTROLLER_MODEL_KEY_INVALID_MSFT = -1000055000,
        XR_ERROR_REPROJECTION_MODE_UNSUPPORTED_MSFT = -1000066000,
        XR_ERROR_COMPUTE_NEW_SCENE_NOT_COMPLETED_MSFT = -1000097000,
        XR_ERROR_SCENE_COMPONENT_ID_INVALID_MSFT = -1000097001,
        XR_ERROR_SCENE_COMPONENT_TYPE_MISMATCH_MSFT = -1000097002,
        XR_ERROR_SCENE_MESH_BUFFER_ID_INVALID_MSFT = -1000097003,
        XR_ERROR_SCENE_COMPUTE_FEATURE_INCOMPATIBLE_MSFT = -1000097004,
        XR_ERROR_SCENE_COMPUTE_CONSISTENCY_MISMATCH_MSFT = -1000097005,
        XR_ERROR_DISPLAY_REFRESH_RATE_UNSUPPORTED_FB = -1000101000,
        XR_ERROR_COLOR_SPACE_UNSUPPORTED_FB = -1000108000,
        XR_ERROR_SPATIAL_ANCHOR_NAME_NOT_FOUND_MSFT = -1000142001,
        XR_ERROR_SPATIAL_ANCHOR_NAME_INVALID_MSFT = -1000142002,
        XR_RESULT_MAX_ENUM = 0x7FFFFFFF
    }
    public enum XrStructureType
    {
        XR_TYPE_UNKNOWN = 0,
        XR_TYPE_API_LAYER_PROPERTIES = 1,
        XR_TYPE_EXTENSION_PROPERTIES = 2,
        XR_TYPE_INSTANCE_CREATE_INFO = 3,
        XR_TYPE_SYSTEM_GET_INFO = 4,
        XR_TYPE_SYSTEM_PROPERTIES = 5,
        XR_TYPE_VIEW_LOCATE_INFO = 6,
        XR_TYPE_VIEW = 7,
        XR_TYPE_SESSION_CREATE_INFO = 8,
        XR_TYPE_SWAPCHAIN_CREATE_INFO = 9,
        XR_TYPE_SESSION_BEGIN_INFO = 10,
        XR_TYPE_VIEW_STATE = 11,
        XR_TYPE_FRAME_END_INFO = 12,
        XR_TYPE_HAPTIC_VIBRATION = 13,
        XR_TYPE_EVENT_DATA_BUFFER = 16,
        XR_TYPE_EVENT_DATA_INSTANCE_LOSS_PENDING = 17,
        XR_TYPE_EVENT_DATA_SESSION_STATE_CHANGED = 18,
        XR_TYPE_ACTION_STATE_BOOLEAN = 23,
        XR_TYPE_ACTION_STATE_FLOAT = 24,
        XR_TYPE_ACTION_STATE_VECTOR2F = 25,
        XR_TYPE_ACTION_STATE_POSE = 27,
        XR_TYPE_ACTION_SET_CREATE_INFO = 28,
        XR_TYPE_ACTION_CREATE_INFO = 29,
        XR_TYPE_INSTANCE_PROPERTIES = 32,
        XR_TYPE_FRAME_WAIT_INFO = 33,
        XR_TYPE_COMPOSITION_LAYER_PROJECTION = 35,
        XR_TYPE_COMPOSITION_LAYER_QUAD = 36,
        XR_TYPE_REFERENCE_SPACE_CREATE_INFO = 37,
        XR_TYPE_ACTION_SPACE_CREATE_INFO = 38,
        XR_TYPE_EVENT_DATA_REFERENCE_SPACE_CHANGE_PENDING = 40,
        XR_TYPE_VIEW_CONFIGURATION_VIEW = 41,
        XR_TYPE_SPACE_LOCATION = 42,
        XR_TYPE_SPACE_VELOCITY = 43,
        XR_TYPE_FRAME_STATE = 44,
        XR_TYPE_VIEW_CONFIGURATION_PROPERTIES = 45,
        XR_TYPE_FRAME_BEGIN_INFO = 46,
        XR_TYPE_COMPOSITION_LAYER_PROJECTION_VIEW = 48,
        XR_TYPE_EVENT_DATA_EVENTS_LOST = 49,
        XR_TYPE_INTERACTION_PROFILE_SUGGESTED_BINDING = 51,
        XR_TYPE_EVENT_DATA_INTERACTION_PROFILE_CHANGED = 52,
        XR_TYPE_INTERACTION_PROFILE_STATE = 53,
        XR_TYPE_SWAPCHAIN_IMAGE_ACQUIRE_INFO = 55,
        XR_TYPE_SWAPCHAIN_IMAGE_WAIT_INFO = 56,
        XR_TYPE_SWAPCHAIN_IMAGE_RELEASE_INFO = 57,
        XR_TYPE_ACTION_STATE_GET_INFO = 58,
        XR_TYPE_HAPTIC_ACTION_INFO = 59,
        XR_TYPE_SESSION_ACTION_SETS_ATTACH_INFO = 60,
        XR_TYPE_ACTIONS_SYNC_INFO = 61,
        XR_TYPE_BOUND_SOURCES_FOR_ACTION_ENUMERATE_INFO = 62,
        XR_TYPE_INPUT_SOURCE_LOCALIZED_NAME_GET_INFO = 63,
        XR_TYPE_COMPOSITION_LAYER_CUBE_KHR = 1000006000,
        XR_TYPE_INSTANCE_CREATE_INFO_ANDROID_KHR = 1000008000,
        XR_TYPE_COMPOSITION_LAYER_DEPTH_INFO_KHR = 1000010000,
        XR_TYPE_VULKAN_SWAPCHAIN_FORMAT_LIST_CREATE_INFO_KHR = 1000014000,
        XR_TYPE_EVENT_DATA_PERF_SETTINGS_EXT = 1000015000,
        XR_TYPE_COMPOSITION_LAYER_CYLINDER_KHR = 1000017000,
        XR_TYPE_COMPOSITION_LAYER_EQUIRECT_KHR = 1000018000,
        XR_TYPE_DEBUG_UTILS_OBJECT_NAME_INFO_EXT = 1000019000,
        XR_TYPE_DEBUG_UTILS_MESSENGER_CALLBACK_DATA_EXT = 1000019001,
        XR_TYPE_DEBUG_UTILS_MESSENGER_CREATE_INFO_EXT = 1000019002,
        XR_TYPE_DEBUG_UTILS_LABEL_EXT = 1000019003,
        XR_TYPE_GRAPHICS_BINDING_OPENGL_WIN32_KHR = 1000023000,
        XR_TYPE_GRAPHICS_BINDING_OPENGL_XLIB_KHR = 1000023001,
        XR_TYPE_GRAPHICS_BINDING_OPENGL_XCB_KHR = 1000023002,
        XR_TYPE_GRAPHICS_BINDING_OPENGL_WAYLAND_KHR = 1000023003,
        XR_TYPE_SWAPCHAIN_IMAGE_OPENGL_KHR = 1000023004,
        XR_TYPE_GRAPHICS_REQUIREMENTS_OPENGL_KHR = 1000023005,
        XR_TYPE_GRAPHICS_BINDING_OPENGL_ES_ANDROID_KHR = 1000024001,
        XR_TYPE_SWAPCHAIN_IMAGE_OPENGL_ES_KHR = 1000024002,
        XR_TYPE_GRAPHICS_REQUIREMENTS_OPENGL_ES_KHR = 1000024003,
        XR_TYPE_GRAPHICS_BINDING_VULKAN_KHR = 1000025000,
        XR_TYPE_SWAPCHAIN_IMAGE_VULKAN_KHR = 1000025001,
        XR_TYPE_GRAPHICS_REQUIREMENTS_VULKAN_KHR = 1000025002,
        XR_TYPE_GRAPHICS_BINDING_D3D11_KHR = 1000027000,
        XR_TYPE_SWAPCHAIN_IMAGE_D3D11_KHR = 1000027001,
        XR_TYPE_GRAPHICS_REQUIREMENTS_D3D11_KHR = 1000027002,
        XR_TYPE_GRAPHICS_BINDING_D3D12_KHR = 1000028000,
        XR_TYPE_SWAPCHAIN_IMAGE_D3D12_KHR = 1000028001,
        XR_TYPE_GRAPHICS_REQUIREMENTS_D3D12_KHR = 1000028002,
        XR_TYPE_SYSTEM_EYE_GAZE_INTERACTION_PROPERTIES_EXT = 1000030000,
        XR_TYPE_EYE_GAZE_SAMPLE_TIME_EXT = 1000030001,
        XR_TYPE_VISIBILITY_MASK_KHR = 1000031000,
        XR_TYPE_EVENT_DATA_VISIBILITY_MASK_CHANGED_KHR = 1000031001,
        XR_TYPE_SESSION_CREATE_INFO_OVERLAY_EXTX = 1000033000,
        XR_TYPE_EVENT_DATA_MAIN_SESSION_VISIBILITY_CHANGED_EXTX = 1000033003,
        XR_TYPE_COMPOSITION_LAYER_COLOR_SCALE_BIAS_KHR = 1000034000,
        XR_TYPE_SPATIAL_ANCHOR_CREATE_INFO_MSFT = 1000039000,
        XR_TYPE_SPATIAL_ANCHOR_SPACE_CREATE_INFO_MSFT = 1000039001,
        XR_TYPE_COMPOSITION_LAYER_IMAGE_LAYOUT_FB = 1000040000,
        XR_TYPE_COMPOSITION_LAYER_ALPHA_BLEND_FB = 1000041001,
        XR_TYPE_VIEW_CONFIGURATION_DEPTH_RANGE_EXT = 1000046000,
        XR_TYPE_GRAPHICS_BINDING_EGL_MNDX = 1000048004,
        XR_TYPE_SPATIAL_GRAPH_NODE_SPACE_CREATE_INFO_MSFT = 1000049000,
        XR_TYPE_SYSTEM_HAND_TRACKING_PROPERTIES_EXT = 1000051000,
        XR_TYPE_HAND_TRACKER_CREATE_INFO_EXT = 1000051001,
        XR_TYPE_HAND_JOINTS_LOCATE_INFO_EXT = 1000051002,
        XR_TYPE_HAND_JOINT_LOCATIONS_EXT = 1000051003,
        XR_TYPE_HAND_JOINT_VELOCITIES_EXT = 1000051004,
        XR_TYPE_SYSTEM_HAND_TRACKING_MESH_PROPERTIES_MSFT = 1000052000,
        XR_TYPE_HAND_MESH_SPACE_CREATE_INFO_MSFT = 1000052001,
        XR_TYPE_HAND_MESH_UPDATE_INFO_MSFT = 1000052002,
        XR_TYPE_HAND_MESH_MSFT = 1000052003,
        XR_TYPE_HAND_POSE_TYPE_INFO_MSFT = 1000052004,
        XR_TYPE_SECONDARY_VIEW_CONFIGURATION_SESSION_BEGIN_INFO_MSFT = 1000053000,
        XR_TYPE_SECONDARY_VIEW_CONFIGURATION_STATE_MSFT = 1000053001,
        XR_TYPE_SECONDARY_VIEW_CONFIGURATION_FRAME_STATE_MSFT = 1000053002,
        XR_TYPE_SECONDARY_VIEW_CONFIGURATION_FRAME_END_INFO_MSFT = 1000053003,
        XR_TYPE_SECONDARY_VIEW_CONFIGURATION_LAYER_INFO_MSFT = 1000053004,
        XR_TYPE_SECONDARY_VIEW_CONFIGURATION_SWAPCHAIN_CREATE_INFO_MSFT = 1000053005,
        XR_TYPE_CONTROLLER_MODEL_KEY_STATE_MSFT = 1000055000,
        XR_TYPE_CONTROLLER_MODEL_NODE_PROPERTIES_MSFT = 1000055001,
        XR_TYPE_CONTROLLER_MODEL_PROPERTIES_MSFT = 1000055002,
        XR_TYPE_CONTROLLER_MODEL_NODE_STATE_MSFT = 1000055003,
        XR_TYPE_CONTROLLER_MODEL_STATE_MSFT = 1000055004,
        XR_TYPE_VIEW_CONFIGURATION_VIEW_FOV_EPIC = 1000059000,
        XR_TYPE_HOLOGRAPHIC_WINDOW_ATTACHMENT_MSFT = 1000063000,
        XR_TYPE_COMPOSITION_LAYER_REPROJECTION_INFO_MSFT = 1000066000,
        XR_TYPE_COMPOSITION_LAYER_REPROJECTION_PLANE_OVERRIDE_MSFT = 1000066001,
        XR_TYPE_ANDROID_SURFACE_SWAPCHAIN_CREATE_INFO_FB = 1000070000,
        XR_TYPE_COMPOSITION_LAYER_SECURE_CONTENT_FB = 1000072000,
        XR_TYPE_INTERACTION_PROFILE_ANALOG_THRESHOLD_VALVE = 1000079000,
        XR_TYPE_HAND_JOINTS_MOTION_RANGE_INFO_EXT = 1000080000,
        XR_TYPE_LOADER_INIT_INFO_ANDROID_KHR = 1000089000,
        XR_TYPE_VULKAN_INSTANCE_CREATE_INFO_KHR = 1000090000,
        XR_TYPE_VULKAN_DEVICE_CREATE_INFO_KHR = 1000090001,
        XR_TYPE_VULKAN_GRAPHICS_DEVICE_GET_INFO_KHR = 1000090003,
        XR_TYPE_COMPOSITION_LAYER_EQUIRECT2_KHR = 1000091000,
        XR_TYPE_SCENE_OBSERVER_CREATE_INFO_MSFT = 1000097000,
        XR_TYPE_SCENE_CREATE_INFO_MSFT = 1000097001,
        XR_TYPE_NEW_SCENE_COMPUTE_INFO_MSFT = 1000097002,
        XR_TYPE_VISUAL_MESH_COMPUTE_LOD_INFO_MSFT = 1000097003,
        XR_TYPE_SCENE_COMPONENTS_MSFT = 1000097004,
        XR_TYPE_SCENE_COMPONENTS_GET_INFO_MSFT = 1000097005,
        XR_TYPE_SCENE_COMPONENT_LOCATIONS_MSFT = 1000097006,
        XR_TYPE_SCENE_COMPONENTS_LOCATE_INFO_MSFT = 1000097007,
        XR_TYPE_SCENE_OBJECTS_MSFT = 1000097008,
        XR_TYPE_SCENE_COMPONENT_PARENT_FILTER_INFO_MSFT = 1000097009,
        XR_TYPE_SCENE_OBJECT_TYPES_FILTER_INFO_MSFT = 1000097010,
        XR_TYPE_SCENE_PLANES_MSFT = 1000097011,
        XR_TYPE_SCENE_PLANE_ALIGNMENT_FILTER_INFO_MSFT = 1000097012,
        XR_TYPE_SCENE_MESHES_MSFT = 1000097013,
        XR_TYPE_SCENE_MESH_BUFFERS_GET_INFO_MSFT = 1000097014,
        XR_TYPE_SCENE_MESH_BUFFERS_MSFT = 1000097015,
        XR_TYPE_SCENE_MESH_VERTEX_BUFFER_MSFT = 1000097016,
        XR_TYPE_SCENE_MESH_INDICES_UINT32_MSFT = 1000097017,
        XR_TYPE_SCENE_MESH_INDICES_UINT16_MSFT = 1000097018,
        XR_TYPE_SERIALIZED_SCENE_FRAGMENT_DATA_GET_INFO_MSFT = 1000098000,
        XR_TYPE_SCENE_DESERIALIZE_INFO_MSFT = 1000098001,
        XR_TYPE_EVENT_DATA_DISPLAY_REFRESH_RATE_CHANGED_FB = 1000101000,
        XR_TYPE_SYSTEM_FACIAL_TRACKING_PROPERTIES_HTC = 1000104000,
        XR_TYPE_FACIAL_TRACKER_CREATE_INFO_HTC = 1000104001,
        XR_TYPE_FACIAL_EXPRESSIONS_HTC = 1000104002,
        XR_TYPE_SYSTEM_COLOR_SPACE_PROPERTIES_FB = 1000108000,
        XR_TYPE_FOVEATION_PROFILE_CREATE_INFO_FB = 1000114000,
        XR_TYPE_SWAPCHAIN_CREATE_INFO_FOVEATION_FB = 1000114001,
        XR_TYPE_SWAPCHAIN_STATE_FOVEATION_FB = 1000114002,
        XR_TYPE_FOVEATION_LEVEL_PROFILE_CREATE_INFO_FB = 1000115000,
        XR_TYPE_BINDING_MODIFICATIONS_KHR = 1000120000,
        XR_TYPE_VIEW_LOCATE_FOVEATED_RENDERING_VARJO = 1000121000,
        XR_TYPE_FOVEATED_VIEW_CONFIGURATION_VIEW_VARJO = 1000121001,
        XR_TYPE_SYSTEM_FOVEATED_RENDERING_PROPERTIES_VARJO = 1000121002,
        XR_TYPE_COMPOSITION_LAYER_DEPTH_TEST_VARJO = 1000122000,
        XR_TYPE_SPATIAL_ANCHOR_PERSISTENCE_INFO_MSFT = 1000142000,
        XR_TYPE_SPATIAL_ANCHOR_FROM_PERSISTED_ANCHOR_CREATE_INFO_MSFT = 1000142001,
        XR_TYPE_SWAPCHAIN_IMAGE_FOVEATION_VULKAN_FB = 1000160000,
        XR_TYPE_SWAPCHAIN_STATE_ANDROID_SURFACE_DIMENSIONS_FB = 1000161000,
        XR_TYPE_SWAPCHAIN_STATE_SAMPLER_OPENGL_ES_FB = 1000162000,
        XR_TYPE_SWAPCHAIN_STATE_SAMPLER_VULKAN_FB = 1000163000,
        XR_TYPE_GRAPHICS_BINDING_VULKAN2_KHR = XR_TYPE_GRAPHICS_BINDING_VULKAN_KHR,
        XR_TYPE_SWAPCHAIN_IMAGE_VULKAN2_KHR = XR_TYPE_SWAPCHAIN_IMAGE_VULKAN_KHR,
        XR_TYPE_GRAPHICS_REQUIREMENTS_VULKAN2_KHR = XR_TYPE_GRAPHICS_REQUIREMENTS_VULKAN_KHR,
        XR_STRUCTURE_TYPE_MAX_ENUM = 0x7FFFFFFF
    }
    public enum XrReferenceSpaceType
    {
        XR_REFERENCE_SPACE_TYPE_VIEW = 1,
        XR_REFERENCE_SPACE_TYPE_LOCAL = 2,
        XR_REFERENCE_SPACE_TYPE_STAGE = 3,
        XR_REFERENCE_SPACE_TYPE_UNBOUNDED_MSFT = 1000038000,
        XR_REFERENCE_SPACE_TYPE_COMBINED_EYE_VARJO = 1000121000,
        XR_REFERENCE_SPACE_TYPE_MAX_ENUM = 0x7FFFFFFF
    }
    public enum XrEyeVisibility
    {
        XR_EYE_VISIBILITY_BOTH = 0,
        XR_EYE_VISIBILITY_LEFT = 1,
        XR_EYE_VISIBILITY_RIGHT = 2,
        XR_EYE_VISIBILITY_MAX_ENUM = 0x7FFFFFFF
    }
    public enum XrEnvironmentBlendMode
    {
        XR_ENVIRONMENT_BLEND_MODE_OPAQUE = 1,
        XR_ENVIRONMENT_BLEND_MODE_ADDITIVE = 2,
        XR_ENVIRONMENT_BLEND_MODE_ALPHA_BLEND = 3,
        XR_ENVIRONMENT_BLEND_MODE_MAX_ENUM = 0x7FFFFFFF
    }
    public enum XrSessionState
    {
        XR_SESSION_STATE_UNKNOWN = 0,
        XR_SESSION_STATE_IDLE = 1,
        XR_SESSION_STATE_READY = 2,
        XR_SESSION_STATE_SYNCHRONIZED = 3,
        XR_SESSION_STATE_VISIBLE = 4,
        XR_SESSION_STATE_FOCUSED = 5,
        XR_SESSION_STATE_STOPPING = 6,
        XR_SESSION_STATE_LOSS_PENDING = 7,
        XR_SESSION_STATE_EXITING = 8,
        XR_SESSION_STATE_MAX_ENUM = 0x7FFFFFFF
    }
    public struct XrVector3f
    {
        public float x;
        public float y;
        public float z;
        public XrVector3f(float in_w, float in_y, float in_z)
        {
            x = in_w;
            y = in_y;
            z = in_z;
        }
    }
    public struct XrQuaternionf
    {
        public float x;
        public float y;
        public float z;
        public float w;
        public XrQuaternionf(float in_x, float in_y, float in_z, float in_w)
        {
            x = in_x;
            y = in_y;
            z = in_z;
            w = in_w;
        }
    }
    public struct XrColor4f
    {
        public float r;
        public float g;
        public float b;
        public float a;
        public XrColor4f(float in_r, float in_g, float in_b, float in_a)
        {
            r = in_r;
            g = in_g;
            b = in_b;
            a = in_a;
        }
    }
    public struct XrExtent2Df
    {
        public float width;
        public float height;
        public XrExtent2Df(float in_width, float in_height)
        {
            width = in_width;
            height = in_height;
        }
    }
    public struct XrRect2Di
    {
        public XrOffset2Di offset;
        public XrExtent2Di extent;
        public XrRect2Di(XrOffset2Di in_offset, XrExtent2Di in_extent)
        {
            offset = in_offset;
            extent = in_extent;
        }
    }
    public struct XrExtent2Di
    {
        public int width;
        public int height;
        public XrExtent2Di(int in_width, int in_height)
        {
            width = in_width;
            height = in_height;
        }
    }
    public struct XrOffset2Di
    {
        public int x;
        public int y;
        public XrOffset2Di(int in_x, int in_y)
        {
            x = in_x;
            y = in_y;
        }
    }
    public struct XrPosef
    {
        public XrQuaternionf orientation;
        public XrVector3f position;
    }

    public struct XrBool32 : IEquatable<UInt32>
    {
        private readonly UInt32 value;

        public XrBool32(UInt32 u)
        {
            value = u;
        }

        public static implicit operator UInt32(XrBool32 equatable)
        {
            return equatable.value;
        }
        public static implicit operator XrBool32(UInt32 u)
        {
            return new XrBool32(u);
        }
        public static implicit operator bool(XrBool32 equatable)
		{
            return equatable.value > 0;
		}
        public static implicit operator XrBool32(bool b)
		{
            return b ? new XrBool32(1) : new XrBool32(0);
		}

        public bool Equals(XrBool32 other)
        {
            return value == other.value;
        }
        public bool Equals(UInt32 other)
        {
            return value == other;
        }
        public override bool Equals(object obj)
        {
            return obj is XrBool32 && Equals((XrBool32)obj);
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public override string ToString()
        {
            return value.ToString();
        }

        public static bool operator ==(XrBool32 a, XrBool32 b) { return a.Equals(b); }
        public static bool operator !=(XrBool32 a, XrBool32 b) { return !a.Equals(b); }
        public static bool operator >=(XrBool32 a, XrBool32 b) { return a.value >= b.value; }
        public static bool operator <=(XrBool32 a, XrBool32 b) { return a.value <= b.value; }
        public static bool operator >(XrBool32 a, XrBool32 b) { return a.value > b.value; }
        public static bool operator <(XrBool32 a, XrBool32 b) { return a.value < b.value; }
        public static XrBool32 operator +(XrBool32 a, XrBool32 b) { return a.value + b.value; }
        public static XrBool32 operator -(XrBool32 a, XrBool32 b) { return a.value - b.value; }
        public static XrBool32 operator *(XrBool32 a, XrBool32 b) { return a.value * b.value; }
        public static XrBool32 operator /(XrBool32 a, XrBool32 b)
        {
            if (b.value == 0)
            {
                throw new DivideByZeroException();
            }
            return a.value / b.value;
        }

    }
    public struct XrFlags64 : IEquatable<UInt64>
    {
        private readonly UInt64 value;

        public XrFlags64(UInt64 u)
        {
            value = u;
        }

        public static implicit operator UInt64(XrFlags64 equatable)
        {
            return equatable.value;
        }
        public static implicit operator XrFlags64(UInt64 u)
        {
            return new XrFlags64(u);
        }

        public bool Equals(XrFlags64 other)
        {
            return value == other.value;
        }
        public bool Equals(UInt64 other)
        {
            return value == other;
        }
        public override bool Equals(object obj)
        {
            return obj is XrFlags64 && Equals((XrFlags64)obj);
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public override string ToString()
        {
            return value.ToString();
        }

        public static bool operator ==(XrFlags64 a, XrFlags64 b) { return a.Equals(b); }
        public static bool operator !=(XrFlags64 a, XrFlags64 b) { return !a.Equals(b); }
        public static bool operator >=(XrFlags64 a, XrFlags64 b) { return a.value >= b.value; }
        public static bool operator <=(XrFlags64 a, XrFlags64 b) { return a.value <= b.value; }
        public static bool operator >(XrFlags64 a, XrFlags64 b) { return a.value > b.value; }
        public static bool operator <(XrFlags64 a, XrFlags64 b) { return a.value < b.value; }
        public static XrFlags64 operator +(XrFlags64 a, XrFlags64 b) { return a.value + b.value; }
        public static XrFlags64 operator -(XrFlags64 a, XrFlags64 b) { return a.value - b.value; }
        public static XrFlags64 operator *(XrFlags64 a, XrFlags64 b) { return a.value * b.value; }
        public static XrFlags64 operator /(XrFlags64 a, XrFlags64 b)
        {
            if (b.value == 0)
            {
                throw new DivideByZeroException();
            }
            return a.value / b.value;
        }

    }
    public struct XrInstance : IEquatable<ulong>
    {
        private readonly ulong value;

        public XrInstance(ulong u)
        {
            value = u;
        }

        public static implicit operator ulong(XrInstance equatable)
        {
            return equatable.value;
        }
        public static implicit operator XrInstance(ulong u)
        {
            return new XrInstance(u);
        }

        public bool Equals(XrInstance other)
        {
            return value == other.value;
        }
        public bool Equals(ulong other)
        {
            return value == other;
        }
        public override bool Equals(object obj)
        {
            return obj is XrInstance && Equals((XrInstance)obj);
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public override string ToString()
        {
            return value.ToString();
        }

        public static bool operator ==(XrInstance a, XrInstance b) { return a.Equals(b); }
        public static bool operator !=(XrInstance a, XrInstance b) { return !a.Equals(b); }
        public static bool operator >=(XrInstance a, XrInstance b) { return a.value >= b.value; }
        public static bool operator <=(XrInstance a, XrInstance b) { return a.value <= b.value; }
        public static bool operator >(XrInstance a, XrInstance b) { return a.value > b.value; }
        public static bool operator <(XrInstance a, XrInstance b) { return a.value < b.value; }
        public static XrInstance operator +(XrInstance a, XrInstance b) { return a.value + b.value; }
        public static XrInstance operator -(XrInstance a, XrInstance b) { return a.value - b.value; }
        public static XrInstance operator *(XrInstance a, XrInstance b) { return a.value * b.value; }
        public static XrInstance operator /(XrInstance a, XrInstance b)
        {
            if (b.value == 0)
            {
                throw new DivideByZeroException();
            }
            return a.value / b.value;
        }

    }
    public struct XrSession : IEquatable<ulong>
    {
        private readonly ulong value;

        public XrSession(ulong u)
        {
            value = u;
        }

        public static implicit operator ulong(XrSession equatable)
        {
            return equatable.value;
        }
        public static implicit operator XrSession(ulong u)
        {
            return new XrSession(u);
        }

        public bool Equals(XrSession other)
        {
            return value == other.value;
        }
        public bool Equals(ulong other)
        {
            return value == other;
        }
        public override bool Equals(object obj)
        {
            return obj is XrSession && Equals((XrSession)obj);
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public override string ToString()
        {
            return value.ToString();
        }

        public static bool operator ==(XrSession a, XrSession b) { return a.Equals(b); }
        public static bool operator !=(XrSession a, XrSession b) { return !a.Equals(b); }
        public static bool operator >=(XrSession a, XrSession b) { return a.value >= b.value; }
        public static bool operator <=(XrSession a, XrSession b) { return a.value <= b.value; }
        public static bool operator >(XrSession a, XrSession b) { return a.value > b.value; }
        public static bool operator <(XrSession a, XrSession b) { return a.value < b.value; }
        public static XrSession operator +(XrSession a, XrSession b) { return a.value + b.value; }
        public static XrSession operator -(XrSession a, XrSession b) { return a.value - b.value; }
        public static XrSession operator *(XrSession a, XrSession b) { return a.value * b.value; }
        public static XrSession operator /(XrSession a, XrSession b)
        {
            if (b.value == 0)
            {
                throw new DivideByZeroException();
            }
            return a.value / b.value;
        }

    }
    public struct XrSpace : IEquatable<ulong>
    {
        private readonly ulong value;

        public XrSpace(ulong u)
        {
            value = u;
        }

        public static implicit operator ulong(XrSpace equatable)
        {
            return equatable.value;
        }
        public static implicit operator XrSpace(ulong u)
        {
            return new XrSpace(u);
        }

        public bool Equals(XrSpace other)
        {
            return value == other.value;
        }
        public bool Equals(ulong other)
        {
            return value == other;
        }
        public override bool Equals(object obj)
        {
            return obj is XrSpace && Equals((XrSpace)obj);
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public override string ToString()
        {
            return value.ToString();
        }

        public static bool operator ==(XrSpace a, XrSpace b) { return a.Equals(b); }
        public static bool operator !=(XrSpace a, XrSpace b) { return !a.Equals(b); }
        public static bool operator >=(XrSpace a, XrSpace b) { return a.value >= b.value; }
        public static bool operator <=(XrSpace a, XrSpace b) { return a.value <= b.value; }
        public static bool operator >(XrSpace a, XrSpace b) { return a.value > b.value; }
        public static bool operator <(XrSpace a, XrSpace b) { return a.value < b.value; }
        public static XrSpace operator +(XrSpace a, XrSpace b) { return a.value + b.value; }
        public static XrSpace operator -(XrSpace a, XrSpace b) { return a.value - b.value; }
        public static XrSpace operator *(XrSpace a, XrSpace b) { return a.value * b.value; }
        public static XrSpace operator /(XrSpace a, XrSpace b)
        {
            if (b.value == 0)
            {
                throw new DivideByZeroException();
            }
            return a.value / b.value;
        }

    }
    public struct XrSpaceLocationFlags : IEquatable<UInt64>
    {
        private readonly UInt64 value;

        public XrSpaceLocationFlags(UInt64 u)
        {
            value = u;
        }

        public static implicit operator UInt64(XrSpaceLocationFlags equatable)
        {
            return equatable.value;
        }
        public static implicit operator XrSpaceLocationFlags(UInt64 u)
        {
            return new XrSpaceLocationFlags(u);
        }

        public bool Equals(XrSpaceLocationFlags other)
        {
            return value == other.value;
        }
        public bool Equals(UInt64 other)
        {
            return value == other;
        }
        public override bool Equals(object obj)
        {
            return obj is XrSpaceLocationFlags && Equals((XrSpaceLocationFlags)obj);
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public override string ToString()
        {
            return value.ToString();
        }

        public static bool operator ==(XrSpaceLocationFlags a, XrSpaceLocationFlags b) { return a.Equals(b); }
        public static bool operator !=(XrSpaceLocationFlags a, XrSpaceLocationFlags b) { return !a.Equals(b); }
        public static bool operator >=(XrSpaceLocationFlags a, XrSpaceLocationFlags b) { return a.value >= b.value; }
        public static bool operator <=(XrSpaceLocationFlags a, XrSpaceLocationFlags b) { return a.value <= b.value; }
        public static bool operator >(XrSpaceLocationFlags a, XrSpaceLocationFlags b) { return a.value > b.value; }
        public static bool operator <(XrSpaceLocationFlags a, XrSpaceLocationFlags b) { return a.value < b.value; }
        public static XrSpaceLocationFlags operator +(XrSpaceLocationFlags a, XrSpaceLocationFlags b) { return a.value + b.value; }
        public static XrSpaceLocationFlags operator -(XrSpaceLocationFlags a, XrSpaceLocationFlags b) { return a.value - b.value; }
        public static XrSpaceLocationFlags operator *(XrSpaceLocationFlags a, XrSpaceLocationFlags b) { return a.value * b.value; }
        public static XrSpaceLocationFlags operator /(XrSpaceLocationFlags a, XrSpaceLocationFlags b)
        {
            if (b.value == 0)
            {
                throw new DivideByZeroException();
            }
            return a.value / b.value;
        }

    }
    public struct XrSystemId : IEquatable<ulong>
    {
        private readonly ulong value;

        public XrSystemId(ulong u)
        {
            value = u;
        }

        public static implicit operator ulong(XrSystemId equatable)
        {
            return equatable.value;
        }
        public static implicit operator XrSystemId(ulong u)
        {
            return new XrSystemId(u);
        }

        public bool Equals(XrSystemId other)
        {
            return value == other.value;
        }
        public bool Equals(ulong other)
        {
            return value == other;
        }
        public override bool Equals(object obj)
        {
            return obj is XrSystemId && Equals((XrSystemId)obj);
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public override string ToString()
        {
            return value.ToString();
        }

        public static bool operator ==(XrSystemId a, XrSystemId b) { return a.Equals(b); }
        public static bool operator !=(XrSystemId a, XrSystemId b) { return !a.Equals(b); }
        public static bool operator >=(XrSystemId a, XrSystemId b) { return a.value >= b.value; }
        public static bool operator <=(XrSystemId a, XrSystemId b) { return a.value <= b.value; }
        public static bool operator >(XrSystemId a, XrSystemId b) { return a.value > b.value; }
        public static bool operator <(XrSystemId a, XrSystemId b) { return a.value < b.value; }
        public static XrSystemId operator +(XrSystemId a, XrSystemId b) { return a.value + b.value; }
        public static XrSystemId operator -(XrSystemId a, XrSystemId b) { return a.value - b.value; }
        public static XrSystemId operator *(XrSystemId a, XrSystemId b) { return a.value * b.value; }
        public static XrSystemId operator /(XrSystemId a, XrSystemId b)
        {
            if (b.value == 0)
            {
                throw new DivideByZeroException();
            }
            return a.value / b.value;
        }

    }
    public struct XrTime : IEquatable<Int64>
    {
        private readonly Int64 value;

        public XrTime(Int64 u)
        {
            value = u;
        }

        public static implicit operator Int64(XrTime equatable)
        {
            return equatable.value;
        }
        public static implicit operator XrTime(Int64 u)
        {
            return new XrTime(u);
        }

        public bool Equals(XrTime other)
        {
            return value == other.value;
        }
        public bool Equals(Int64 other)
        {
            return value == other;
        }
        public override bool Equals(object obj)
        {
            return obj is XrTime && Equals((XrTime)obj);
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public override string ToString()
        {
            return value.ToString();
        }

        public static bool operator ==(XrTime a, XrTime b) { return a.Equals(b); }
        public static bool operator !=(XrTime a, XrTime b) { return !a.Equals(b); }
        public static bool operator >=(XrTime a, XrTime b) { return a.value >= b.value; }
        public static bool operator <=(XrTime a, XrTime b) { return a.value <= b.value; }
        public static bool operator >(XrTime a, XrTime b) { return a.value > b.value; }
        public static bool operator <(XrTime a, XrTime b) { return a.value < b.value; }
        public static XrTime operator +(XrTime a, XrTime b) { return a.value + b.value; }
        public static XrTime operator -(XrTime a, XrTime b) { return a.value - b.value; }
        public static XrTime operator *(XrTime a, XrTime b) { return a.value * b.value; }
        public static XrTime operator /(XrTime a, XrTime b)
        {
            if (b.value == 0)
            {
                throw new DivideByZeroException();
            }
            return a.value / b.value;
        }
    }
    public struct XrDuration : IEquatable<Int64>
    {
        private readonly Int64 value;

        public XrDuration(Int64 u)
        {
            value = u;
        }

        public static implicit operator Int64(XrDuration equatable)
        {
            return equatable.value;
        }
        public static implicit operator XrDuration(Int64 u)
        {
            return new XrDuration(u);
        }

        public bool Equals(XrDuration other)
        {
            return value == other.value;
        }
        public bool Equals(Int64 other)
        {
            return value == other;
        }
        public override bool Equals(object obj)
        {
            return obj is XrDuration && Equals((XrDuration)obj);
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public override string ToString()
        {
            return value.ToString();
        }

        public static bool operator ==(XrDuration a, XrDuration b) { return a.Equals(b); }
        public static bool operator !=(XrDuration a, XrDuration b) { return !a.Equals(b); }
        public static bool operator >=(XrDuration a, XrDuration b) { return a.value >= b.value; }
        public static bool operator <=(XrDuration a, XrDuration b) { return a.value <= b.value; }
        public static bool operator >(XrDuration a, XrDuration b) { return a.value > b.value; }
        public static bool operator <(XrDuration a, XrDuration b) { return a.value < b.value; }
        public static XrDuration operator +(XrDuration a, XrDuration b) { return a.value + b.value; }
        public static XrDuration operator -(XrDuration a, XrDuration b) { return a.value - b.value; }
        public static XrDuration operator *(XrDuration a, XrDuration b) { return a.value * b.value; }
        public static XrDuration operator /(XrDuration a, XrDuration b)
        {
            if (b.value == 0)
            {
                throw new DivideByZeroException();
            }
            return a.value / b.value;
        }
    }

    public struct XrReferenceSpaceCreateInfo
    {
        public XrStructureType type;
        public IntPtr next;
        public XrReferenceSpaceType referenceSpaceType;
        public XrPosef poseInReferenceSpace;
    }
    public struct XrSystemGraphicsProperties
    {
        public uint maxSwapchainImageHeight;
        public uint maxSwapchainImageWidth;
        public uint maxLayerCount;
    }
    public struct XrSystemTrackingProperties
    {
        public uint orientationTracking;
        public uint positionTracking;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct XrSystemProperties
    {
        public XrStructureType type;
        public IntPtr next;
        public ulong systemId;
        public uint vendorId;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public char[] systemName; //char systemName[XR_MAX_SYSTEM_NAME_SIZE];
        public XrSystemGraphicsProperties graphicsProperties;
        public XrSystemTrackingProperties trackingProperties;
    }

	[StructLayout(LayoutKind.Sequential)]
	public struct XrSessionCreateInfo
	{
		XrStructureType type;
		public IntPtr next;
		Int64 createFlags;
		XrSystemId systemId;
	}

    public static class OpenXRHelper
	{
        public static bool VALIDATE(InputActionReference actionReference, out string msg)
        {
            msg = "Normal";

            if (actionReference == null)
			{
                msg = "Null reference.";
                return false;
			} else if (actionReference.action == null)
			{
                msg = "Null reference action.";
                return false;
			} else if (!actionReference.action.enabled)
			{
                msg = "Reference action disabled.";
                return false;
			} else if (actionReference.action.activeControl == null)
			{
                msg = "No active control of the reference action.";
                return false;
			} else if (actionReference.action.controls.Count <= 0)
			{
                msg = "Action control count is " + actionReference.action.controls.Count;
                return false;
			}

            return true;
        }
        public static Vector3 ToUnityVector(this Vector3 xrVec)
        {
            Vector3 vec = Vector3.zero;
            vec.x = xrVec.x;
            vec.y = xrVec.y;
            vec.z = -xrVec.z;
            return vec;
        }
        public static Vector3 ToUnityVector(this XrVector3f xrVec)
        {
            Vector3 vec = Vector3.zero;
            vec.x = xrVec.x;
            vec.y = xrVec.y;
            vec.z = -xrVec.z;
            return vec;
        }
        public static Quaternion ToUnityQuaternion(this Quaternion xrQuat)
        {
            Quaternion quat = Quaternion.identity;
            quat.x = xrQuat.x;
            quat.y = xrQuat.y;
            quat.z = -xrQuat.z;
            quat.w = -xrQuat.w;
            return quat;
        }
        public static Quaternion ToUnityQuaternion(this XrQuaternionf xrQuat)
        {
            Quaternion quat = Quaternion.identity;
            quat.x = xrQuat.x;
            quat.y = xrQuat.y;
            quat.z = -xrQuat.z;
            quat.w = -xrQuat.w;
            return quat;
        }

        // Flag bits for XrSpaceLocationFlags
        public static XrSpaceLocationFlags XR_SPACE_LOCATION_ORIENTATION_VALID_BIT = 0x00000001;
        public static XrSpaceLocationFlags XR_SPACE_LOCATION_POSITION_VALID_BIT = 0x00000002;
        public static XrSpaceLocationFlags XR_SPACE_LOCATION_ORIENTATION_TRACKED_BIT = 0x00000004;
        public static XrSpaceLocationFlags XR_SPACE_LOCATION_POSITION_TRACKED_BIT = 0x00000008;

        // XrDuration definitions
        public static XrDuration XR_NO_DURATION = 0;
        public static XrDuration XR_INFINITE_DURATION = 0x7fffffffffffffff;

        public delegate XrResult xrCreateSessionDelegate(
			XrInstance instance,
			in XrSessionCreateInfo createInfo,
			XrSession session);
        public delegate XrResult xrGetInstanceProcAddrDelegate(
            XrInstance instance,
            string name,
            out IntPtr function);
        public delegate XrResult xrGetSystemPropertiesDelegate(
            XrInstance instance,
            XrSystemId systemId,
            ref XrSystemProperties properties);
        public delegate XrResult xrEnumerateReferenceSpacesDelegate(
            XrSession session,
            UInt32 spaceCapacityInput,
            out UInt32 spaceCountOutput,
            out XrReferenceSpaceType spaces);
        public delegate XrResult xrCreateReferenceSpaceDelegate(
            XrSession session,
            ref XrReferenceSpaceCreateInfo createInfo,
            out XrSpace space);
        public delegate XrResult xrDestroySpaceDelegate(
            XrSpace space);
    }

    public static class ClientInterface
    {
        public static bool IsUserPresence()
        {
#if UNITY_ANDROID
            if (ProximitySensor.current != null)
            {
                if (!ProximitySensor.current.IsActuated())
                    InputSystem.EnableDevice(ProximitySensor.current);

                return ProximitySensor.current.distance.ReadValue() < 1; // near p-sensor < 1cm
            }
            else
            {
                return false;
            }
#else
            return true;
#endif
        }

        static List<XRInputSubsystem> s_InputSubsystems = new List<XRInputSubsystem>();
        public static TrackingOriginModeFlags TrackingOrigin()
        {
            SubsystemManager.GetInstances(s_InputSubsystems);
            if (s_InputSubsystems.Count > 0)
            {
                return s_InputSubsystems[0].GetTrackingOriginMode();
            }
            return TrackingOriginModeFlags.Unknown;
        }
    }
}
