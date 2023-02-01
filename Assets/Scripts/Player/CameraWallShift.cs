using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWallShift : MonoBehaviour
{
    [SerializeField] Transform camera;
    [SerializeField] float raycastLength;
    RaycastHit hit;

    Vector3 defaultPos, targetPos;

    void Update()
    {
        RaycastInit();
    }

    void RaycastInit()
    {

        Vector3 raycastBackDirection = -camera.forward;
        Vector3 raycastSideDirection = -camera.right;
        Vector3 raycastUpDirection = camera.up;

        if(Physics.Raycast(camera.position, raycastBackDirection, out hit, raycastLength))
        {
            print("BackObj = " + hit.transform.gameObject.name);
        }

        if (Physics.Raycast(camera.position, raycastSideDirection, out hit, raycastLength))
        {
            print("LeftObj = " + hit.transform.gameObject.name);
        }

        if (Physics.Raycast(camera.position, -raycastSideDirection, out hit, raycastLength))
        {
            print("RightObj = " + hit.transform.gameObject.name);
        }

        if (Physics.Raycast(camera.position, raycastUpDirection, out hit, raycastLength))
        {
            print("UpObj = " + hit.transform.gameObject.name);
        }

        if (Physics.Raycast(camera.position, -raycastUpDirection, out hit, raycastLength))
        {
            print("DownObj = " + hit.transform.gameObject.name);
        }
    }

}
