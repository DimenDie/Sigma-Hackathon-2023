using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swinging : MonoBehaviour
{
    public KeyCode swingKey = KeyCode.Mouse0;

    public LineRenderer lineRenderer;
    public Transform shootCenter, camera, player;
    public LayerMask isGrappleable;

    private float maxSwingDistance = 30f;
    private Vector3 swingPoint;
    private SpringJoint joint;

    private void Update()
    {
        if (Input.GetKeyDown(swingKey)) StartSwing();
        if (Input.GetKeyUp(swingKey)) StopSwing();
    }
    void StartSwing()
    {
        RaycastHit hit;
        if(Physics.Raycast(camera.position, camera.forward, out hit, maxSwingDistance, isGrappleable))
        {
            swingPoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = swingPoint;

            float distanceFromPoint = Vector3.Distance(player.position, swingPoint);

            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

        }
    }
    void StopSwing()
    {
        lineRenderer.positionCount = 0;
        Destroy(joint);
    }
}
