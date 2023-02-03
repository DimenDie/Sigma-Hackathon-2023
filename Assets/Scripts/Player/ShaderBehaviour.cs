using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderBehaviour : MonoBehaviour
{
    [SerializeField] Material sphereMaterial, rootsMaterial;
    [SerializeField] float dissolveSpeed;
    [SerializeField] float defaultThiccness;
    [SerializeField] AnimationCurve curve;
    bool duringDissolve, onoff;
    

    private void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O) && !duringDissolve)
        {
            StartCoroutine(DissolveSwitch(onoff));
            onoff = !onoff;
        }
    }

    IEnumerator DissolveSwitch( bool on )
    {
        float t = 0;
        float startValue = on ? 0 : 1f;
        float targetValue = on ? 1f : 0;
        float value;

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
            t += Time.deltaTime / dissolveSpeed;
            yield return null;
        }

        sphereMaterial.SetFloat("_DissolveStatus", targetValue);
        rootsMaterial.SetFloat("_DissolveStatus", targetValue);

        t = 0;

        while(t<=1)
        {
            value = Mathf.Lerp(defaultThiccness, 0, curve.Evaluate(t));
            sphereMaterial.SetFloat("_Thiccness", value);
            rootsMaterial.SetFloat("_Thiccness", value);
            t += Time.deltaTime / (dissolveSpeed / 2);
            yield return null;
        }

        sphereMaterial.SetFloat("_Thiccness", 0);
        rootsMaterial.SetFloat("_Thiccness", 0);

        duringDissolve = false;
    }

}
