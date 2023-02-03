using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoyTapor : MonoBehaviour
{
    [SerializeField] float amplitude, cycleTime, cycleDelay, handleSize;
    [SerializeField] Transform axePoint, axeObject, axeHandle;
    [SerializeField] AnimationCurve curve;

    private void Start()
    {
        axeHandle.transform.localScale = new Vector3(
            axeHandle.transform.localScale.x,
            axeHandle.transform.localScale.y * handleSize,
            axeHandle.transform.localScale.z);

        StartCoroutine(AxeSwing());

    }

    void Update()
    {
        axeObject.transform.rotation = axeHandle.transform.rotation;
        axeObject.position = axePoint.position;
    }

    IEnumerator AxeSwing()
    {
        float t;
        float currentAngle = amplitude, targetAngle = -amplitude;
        while (true)
        {
            t = 0;
            float xRot = currentAngle;
            while (t <= 1)
            {
                xRot = Mathf.Lerp(currentAngle, targetAngle, curve.Evaluate(t));
                axeHandle.transform.rotation = Quaternion.Euler(xRot, 0,0);
                t += Time.deltaTime / cycleTime;
                yield return null;
            }

            axeHandle.transform.rotation = Quaternion.Euler(targetAngle, 0, 0);

            float savedTargetRot = targetAngle;
            targetAngle = currentAngle;
            currentAngle = savedTargetRot;

            yield return new WaitForSeconds(cycleDelay);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            print("Death");
    }

}
