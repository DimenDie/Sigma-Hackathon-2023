using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderBehaviour : MonoBehaviour
{
    [SerializeField] Material sphereMaterial, rootsMaterial;
    [SerializeField] float dissolveSpeed;
    [SerializeField] float defaultThiccness;
    [SerializeField] AnimationCurve curve;
    [HideInInspector] public bool duringDissolve;

    float defaultLightIntensity;

    PlayerPointLight pointLightPulse;
    Light light;

    private void Start()
    {
        pointLightPulse = FindObjectOfType<PlayerPointLight>();
        light = pointLightPulse.GetComponent<Light>();
        defaultLightIntensity = pointLightPulse.defaultIntensity;

        StartCoroutine(DissolveSwitch(false));

    }

    public IEnumerator DissolveSwitch( bool on )
    {
        float t = 0;
        float startValue = on ? 0 : 1;
        float targetValue = on ? 1 : 0;
        
        float value;

        float startLightIntensity = on ? defaultLightIntensity : 0;
        float targetLightIntensity = on ? 0 : defaultLightIntensity;


        sphereMaterial.SetFloat("_Thiccness", defaultThiccness);
        rootsMaterial.SetFloat("_Thiccness", defaultThiccness);

        sphereMaterial.SetInt("_Dead", System.Convert.ToInt32(targetValue));
        rootsMaterial.SetInt("_Dead", System.Convert.ToInt32(targetValue));

        duringDissolve = true;

        while(t<=1)
        {
            value = Mathf.Lerp(startValue, targetValue, curve.Evaluate(t));
            sphereMaterial.SetFloat("_DissolveStatus", value);
            rootsMaterial.SetFloat("_DissolveStatus", value);

            light.intensity = Mathf.Lerp(startLightIntensity, targetLightIntensity, curve.Evaluate(t));

            t += Time.deltaTime / dissolveSpeed;
            yield return null;
        }

        pointLightPulse.canPulse = !on;

        sphereMaterial.SetFloat("_DissolveStatus", targetValue);
        rootsMaterial.SetFloat("_DissolveStatus", targetValue);
        light.intensity = targetLightIntensity;

        if (on)
            FindObjectOfType<UI>().TogglePause();

        t = 0;

        while(t<=1)
        {
            value = Mathf.Lerp(defaultThiccness, 0, curve.Evaluate(t));
            sphereMaterial.SetFloat("_Thiccness", value);
            rootsMaterial.SetFloat("_Thiccness", value);
            t += Time.deltaTime / (dissolveSpeed / 2);
            yield return null;
        }


        pointLightPulse.timer = 0;
        sphereMaterial.SetFloat("_Thiccness", 0);
        rootsMaterial.SetFloat("_Thiccness", 0);

        duringDissolve = false;
        


    }
}
