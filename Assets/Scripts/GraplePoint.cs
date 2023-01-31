using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraplePoint : MonoBehaviour
{
    private Color startColor;
    void OnMouseEnter()
    {
        startColor = gameObject.GetComponent<Renderer>().material.color;
        gameObject.GetComponent<Renderer>().material.color = Color.red;
    }
    void OnMouseExit()
    {
        gameObject.GetComponent<Renderer>().material.color = startColor;
    }
}
