using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWallShift : MonoBehaviour
{
    [SerializeField] Transform camera;
    [SerializeField] float raycastLength;
    RaycastHit hit;

    List<Vector3> raycastDirections;

    Vector3 defaultPos, targetPos;

    private void Start()
    {
        raycastDirections = new List<Vector3>();
    }

    void Update()
    {
        RaycastInit();
    }

    void RaycastInit()
    {
        print(RaycastCheck());
    }

    bool RaycastCheck() // мама застрели меня за эти макароны
    {
        bool nearObstacle = false;
        Vector3 raycastBackDirection = -camera.forward;
        Vector3 raycastSideDirection = -camera.right;
        Vector3 raycastUpDirection = camera.up;

        if (Physics.Raycast(camera.position, raycastBackDirection, out hit, raycastLength))
        {
            nearObstacle = true;
        }

        if (Physics.Raycast(camera.position, raycastSideDirection, out hit, raycastLength))
        {
            nearObstacle = true;
        }

        if (Physics.Raycast(camera.position, -raycastSideDirection, out hit, raycastLength))
        {
            nearObstacle = true;
        }

        if (Physics.Raycast(camera.position, raycastUpDirection, out hit, raycastLength))
        {
            nearObstacle = true;
        }

        if (Physics.Raycast(camera.position, -raycastUpDirection, out hit, raycastLength))
        {
            nearObstacle = true;
        }

        return nearObstacle;
    }

}
