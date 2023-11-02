using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class ControlSky : MonoBehaviour
{
    public Material dayMat;
    public Material nightMat;

    public Light directionalLight;
    private Quaternion originalRotation;

    private void Start()
    {
        originalRotation = directionalLight.transform.rotation;
    }

    private void Update()
    {
        float currentTime = Time.time;
        float rotation = currentTime * 1f % 120f; // rotation is always between 0 and 360.
        UnityEngine.RenderSettings.skybox.SetFloat("_Rotation", rotation);

        

        if (rotation <= 60f)
        {
            UnityEngine.RenderSettings.skybox = dayMat;
            directionalLight.enabled = true;
            float rotationSpeed = 2f; // Define the rotation speed.
            directionalLight.transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
        }
        else
        {
            UnityEngine.RenderSettings.skybox = nightMat;
            directionalLight.enabled = false;

            directionalLight.transform.rotation = originalRotation;
        }

    }
}