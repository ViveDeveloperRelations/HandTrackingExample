// Copyright HTC Corporation All Rights Reserved.
#if UNITY_EDITOR

using UnityEditor;
using UnityEditor.XR.OpenXR.Features;

namespace Wave.OpenXR
{
	[OpenXRFeatureSet(
	   FeatureIds = new string[] {
			VIVEFocus3Feature.featureId,
			VIVEFocus3Profile.featureId,
			"vive.wave.openxr.feature.hand.tracking",
			"vive.wave.openxr.feature.compositionlayer",
			"vive.wave.openxr.feature.compositionlayer.cylinder",
			"vive.wave.openxr.feature.compositionlayer.colorscalebias",
			"vive.wave.openxr.feature.tracker",
			"vive.wave.openxr.feature.hand.interaction",
		   },
	   UiName = "VIVE Focus 3 Support",
	   Description = "Necessary to deploy an VIVE Focus3 compatible app.",
	   FeatureSetId = "com.htc.wave.openxr.featureset.vivefocus3",
	   SupportedBuildTargets = new BuildTargetGroup[] { BuildTargetGroup.Android }
   )]
	sealed class VIVEFocus3FeatureSet { }
}
#endif
