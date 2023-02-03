using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraplePoint : MonoBehaviour
{
    private Color startColor;

    private void Start()
    {
        startColor = gameObject.GetComponent<Renderer>().material.color;
    }

    public void SetColor()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.red;
    }
    public void ReturnColor()
    {
        gameObject.GetComponent<Renderer>().material.color = startColor;
    }
}
