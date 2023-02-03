using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPointLight : MonoBehaviour
{
    Light light;
    [SerializeField] float pulseSpeed, pulsePower;
    public float defaultIntensity;
    float targetIntensity;
    [HideInInspector] public float timer;
    public bool canPulse = false;
    private void Start()
    {
        timer = 0;
        light = GetComponent<Light>();
    }

    private void Update()
    {
        if(canPulse)
        {
            timer += Time.deltaTime;
            targetIntensity = defaultIntensity + Mathf.Sin(Time.time * pulseSpeed) * pulsePower;
            light.intensity = Mathf.MoveTowards(light.intensity, targetIntensity, 0.05f);
        }
            
    }

}
