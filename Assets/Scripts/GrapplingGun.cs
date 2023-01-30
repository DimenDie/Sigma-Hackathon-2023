using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class GrapplingGun : MonoBehaviour
{
    public LayerMask isGrappleable;
    public Transform grappleBase, player;
    public Camera mainCamera;
    private float maxDistance = 100f;
    private float force = 1f;


    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) //Should change to newer input system in unity
        {
            StartGrapple();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopGrapple();
        }
    }

    private void StopGrapple()
    {

    }

    private void StartGrapple()
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, maxDistance, isGrappleable))
        {
            Vector3 direction = player.transform.position - hit.transform.gameObject.transform.position;


            print(direction * force);

            player.GetComponent<Rigidbody>().AddForce(direction * -force);
            
        }
    }
}
