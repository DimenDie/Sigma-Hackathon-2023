using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SkipBlink : MonoBehaviour
{
    CanvasGroup canvasGroup;
    [SerializeField]float blinkSpeed;
    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void Update()
    {
        canvasGroup.alpha = Mathf.Abs(Mathf.Sin(Time.time * blinkSpeed));
    }
}
