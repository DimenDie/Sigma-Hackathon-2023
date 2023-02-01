using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWallShift : MonoBehaviour
{
    [SerializeField] Transform camera, target;
    [SerializeField] float raycastLength;
    RaycastHit hit;

    void Update()
    {
        RaycastInit();
    }

    void RaycastInit()
    {

        Vector3 raycastDirection = target.position - camera.position;


        //if(Physics.Raycast(camera.position, raycastDirection, raycastLength, ref hit,))
    }

}
