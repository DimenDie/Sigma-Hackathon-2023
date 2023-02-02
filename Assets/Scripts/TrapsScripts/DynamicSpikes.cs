using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicSpikes : MonoBehaviour
{
    [SerializeField] float inOutTime, delayTime, downShiftValue;
    [SerializeField] AnimationCurve curve;
    public bool spikeCanKill;
    Vector3 defaultPos, newPos;
    void Start()
    {
        defaultPos = transform.position;
        newPos = defaultPos + (Vector3.down * downShiftValue);
        StartCoroutine(InitSpikeAnim());
    }

    IEnumerator InitSpikeAnim()
    {
        float t;
        Vector3 currentPos = defaultPos, targetPos = newPos;
        while(true)
        {
            t = 0;

            while (t <= 1)
            {
                transform.position = Vector3.Lerp(currentPos, targetPos, curve.Evaluate(t));
                t += Time.deltaTime / inOutTime;
                yield return null;
            }
            transform.position = targetPos;

            Vector3 savedTargetPos = targetPos;
            targetPos = currentPos;
            currentPos = savedTargetPos;

            yield return new WaitForSeconds(delayTime);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            print("Death");
    }

}
