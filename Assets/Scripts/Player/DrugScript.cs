using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrugScript : MonoBehaviour
{
    [SerializeField] Transform drugPivot, drugTransform;
    [SerializeField] float pivoRadius, pivoSpeed, friendMaxSpeed, friendMaxDistance, drugSmoothTime;
    float friendSpeed, pivotAndDrugDistance, friendMinSpeed;
    Vector3 defaultPos, drugVelocity;

    private void Start()
    {
        defaultPos = drugPivot.localPosition;
    }

    void Update()
    {
        pivotAndDrugDistance = Vector3.Distance(drugPivot.position, drugTransform.position);
        friendSpeed = Mathf.Lerp(friendMinSpeed, friendMaxSpeed, pivotAndDrugDistance / friendMaxDistance);

        drugPivot.localPosition = defaultPos + new Vector3(Mathf.Sin(Time.time * pivoSpeed) * pivoRadius, 0 , Mathf.Cos(Time.time * pivoSpeed) * pivoRadius);
        drugTransform.transform.position = Vector3.SmoothDamp(drugTransform.transform.position, drugPivot.transform.position, ref drugVelocity, drugSmoothTime, friendSpeed);
    }

}
