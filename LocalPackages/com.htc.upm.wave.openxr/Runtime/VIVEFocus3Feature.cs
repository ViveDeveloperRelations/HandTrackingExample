// Copyright HTC Corporation All Rights Reserved.
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.XR.OpenXR;
using UnityEngine.XR.OpenXR.Features;
using System.Runtime.InteropServices;
using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor.XR.OpenXR.Features;
using System.Linq;
using UnityEngine.Rendering;
#endif

namespace Wave.OpenXR
{
#if UNITY_EDITOR
    [OpenXRFeature(UiName = "VIVE Focus 3 Support",
		Desc = "Necessary to deploy an VIVE Focus3 compatible app.",
		Company = "HTC",
		DocumentationLink = "https://developer.vive.com/resources/openxr/openxr-mobile/tutorials/how-install-vive-wave-openxr-plugin/",
		OpenxrExtensionStrings = "",
		Version = "1.0.0",
		BuildTargetGroups = new[] { BuildTargetGroup.Android },
		CustomRuntimeLoaderBuildTargets = new[] { BuildTarget.Android },
        FeatureId = featureId
	)]
#endif
    public class VIVEFocus3Feature : OpenXRFeature
    {
        /// <summary>
        /// The feature id string. This is used to give the feature a well known id for reference.
        /// </summary>
        public const string featureId = "com.unity.openxr.feature.vivefocus3";

		/// <summary>
		/// Enable Hand Tracking or Not.
		/// </summary>
		//public bool enableHandTracking = false;

		/// <summary>
		/// Enable Tracker or Not.
		/// </summary>
		//public bool enableTracker = false;

		/// <inheritdoc />
		//protected override IntPtr HookGetInstanceProcAddr(IntPtr func)
		//{
		//	Debug.Log("EXT: registering our own xrGetInstanceProcAddr");
		//	return intercept_xrGetInstanceProcAddr(func);
		//}

		//private const string ExtLib = "waveopenxr";
		//[DllImport(ExtLib, EntryPoint = "intercept_xrGetInstanceProcAddr")]
		//private static extern IntPtr intercept_xrGetInstanceProcAddr(IntPtr func);

#if UNITY_EDITOR 
		protected override void GetValidationChecks(List<ValidationRule> rules, BuildTargetGroup targetGroup)
		{
			rules.Add(
				new ValidationRule(this)
				{
					message = "Only the Focus 3 Interaction Profile is supported right now.",
					checkPredicate = () =>
					{
						var settings = OpenXRSettings.GetSettingsForBuildTargetGroup(targetGroup);
						if (null == settings)
							return false;

						bool touchFeatureEnabled = false;
						foreach (var feature in settings.GetFeatures<OpenXRInteractionFeature>())
						{
							if (feature.enabled)
							{
								if (feature is VIVEFocus3Profile)
									touchFeatureEnabled = true;
							}
						}

						return touchFeatureEnabled;
					},
					fixIt = () =>
					{
						var settings = OpenXRSettings.GetSettingsForBuildTargetGroup(targetGroup);
						if (null == settings)
							return;

						foreach (var feature in settings.GetFeatures<OpenXRInteractionFeature>())
						{
							if (feature is VIVEFocus3Profile)
								feature.enabled = true;
						}
					},
					error = true,
				});
			
            rules.Add(
                new ValidationRule(this)
                {
	                message = "Enable run in background",
	                checkPredicate = () => Application.runInBackground,
	                fixIt = () =>
	                {
						Application.runInBackground = true;
	                },
                }
            );
            rules.Add(
	            new ValidationRule(this)
	            {
		            message = "Disable Auto Graphics API and ensure openGLES2x is also disabled",
		            checkPredicate = () =>
		            {
			            if (PlayerSettings.GetUseDefaultGraphicsAPIs(BuildTarget.Android))
				            return false;
			            return !PlayerSettings.GetGraphicsAPIs(BuildTarget.Android).Contains(GraphicsDeviceType.OpenGLES2) ;
		            },
		            fixIt = () =>
		            {
			            PlayerSettings.SetUseDefaultGraphicsAPIs(BuildTarget.Android, false);
			            PlayerSettings.SetGraphicsAPIs(BuildTarget.Android, new[] { GraphicsDeviceType.Vulkan, GraphicsDeviceType.OpenGLES3 });
		            },
	            }
            );
		}
#endif
    }
}
