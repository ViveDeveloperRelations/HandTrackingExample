using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class OpenxrEyebufferbug : MonoBehaviour
{
    bool isResume = false;

    int counter = 0;

    void OnApplicationPause(bool pause)
    {
        Debug.Log("OnApplicationPause " + pause);
        if (pause == false)
        {
            counter = 0;
            isResume = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(isResume)
        {
            counter++;
            if(counter >= 10)
            {
                XRSettings.eyeTextureResolutionScale = XRSettings.occlusionMaskScale = 0.0f;
                isResume = false;
            }
        }
    }
}
