using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPointLight : MonoBehaviour
{
    Light light;
    [SerializeField] float pulseSpeed, pulsePower, defaultIntensity;
    private void Start()
    {
        light = GetComponent<Light>();
    }

    private void Update()
    {
        light.intensity = defaultIntensity + Mathf.Sin(Time.time * pulseSpeed) * pulsePower;
    }

}
