using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPointLight : MonoBehaviour
{
    Light light;
    [SerializeField] float pulseSpeed, pulsePower;
    public float defaultIntensity;
    float targetIntensity;
    public bool canPulse = false;
    private void Start()
    {
        light = GetComponent<Light>();
    }

    private void Update()
    {
        if(canPulse)
        {
            targetIntensity = defaultIntensity + Mathf.Sin(Time.time * pulseSpeed) * pulsePower;
            light.intensity = Mathf.MoveTowards(light.intensity, targetIntensity, 0.1f);
        }
            
    }

}
