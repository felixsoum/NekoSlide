using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampLight : MonoBehaviour
{
    new Light light;
    float initialIntensity;
    const float FlickerSpeed = 20;
    float timer;

    void Awake()
    {
        light = GetComponent<Light>();
        initialIntensity = light.intensity;
    }

    void Update()
    {
        timer += Random.value * Time.deltaTime * FlickerSpeed;
        light.intensity = initialIntensity + 0.001f * Mathf.Sin(timer);
    }


}
