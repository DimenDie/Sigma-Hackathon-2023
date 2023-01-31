using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class GrapplingGun : MonoBehaviour
{
    public LayerMask isGrappleable;
    public Transform player;
    private Camera mainCamera;
    private float maxDistance = 100f;
    [SerializeField]private float force = 25f;

    private void Start()
    {
        mainCamera = Camera.main;
    }
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
            direction = Vector3.Normalize(direction);
            print(direction * -force);

            player.GetComponent<Rigidbody>().velocity += direction * -force;
            
        }
    }
}
