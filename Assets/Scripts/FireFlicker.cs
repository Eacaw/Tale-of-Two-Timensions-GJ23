using UnityEngine;

public class FireFlicker : MonoBehaviour
{
    // Light script to give a light the flickering effect of a fire

    public float minIntensity = 4f;
    public float maxIntensity = 5f;
    public float flickerSpeed = 5f;
    
    private Light fireLight;
    private float random;

    void Start()
    {
        fireLight = GetComponent<Light>();
        random = Random.Range(0.0f, 65535.0f);
    }

    void Update()
    {
        float noise = Mathf.PerlinNoise(random, Time.time * flickerSpeed);
        fireLight.intensity = Mathf.Lerp(minIntensity, maxIntensity, noise);
    }
}
