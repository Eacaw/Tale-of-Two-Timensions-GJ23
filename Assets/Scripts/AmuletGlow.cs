using System.Collections;

using System.Collections.Generic;

using UnityEngine;

public class AmuletGlow : MonoBehaviour
{
    public Light glowLight;
    private float lightIntensity;
    private float lightRange;
    private float lightSpeed = 2f;
    private float lightMaxIntensity = 4f;

    private void Start()
    {
        lightIntensity = glowLight.intensity;
        lightRange = glowLight.range;
    }

    private void Update()
    {
            glowLight.intensity = Mathf.PingPong(Time.time * lightSpeed, lightMaxIntensity);
    }
}