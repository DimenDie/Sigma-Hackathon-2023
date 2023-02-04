using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrugScript : MonoBehaviour
{
    [SerializeField] Transform drugPivot, drugTransform;
    [SerializeField] float radius, speed;
    Vector3 defaultPos;

    private void Start()
    {
        defaultPos = drugPivot.localPosition;
    }

    void Update()
    {
        drugPivot.localPosition = defaultPos + new Vector3(Mathf.Sin(Time.time * speed) * radius, 0 , Mathf.Cos(Time.time * speed) * radius);
    }

}
