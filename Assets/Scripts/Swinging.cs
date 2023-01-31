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

    private Vector3 currentGrapplePosition;

    private void Update()
    {
        shootCenter.eulerAngles = Vector3.zero;
        if (Input.GetKeyDown(swingKey)) StartSwing();
        if (Input.GetKeyUp(swingKey)) StopSwing();
    }
    private void LateUpdate()
    {
        DrawRope();
    }
    void StartSwing()
    {
        print("start swing called");
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, maxSwingDistance, isGrappleable))
        {
            print("raycasted");
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

            lineRenderer.positionCount = 2;
            currentGrapplePosition = shootCenter.position;

        }
    }
    void StopSwing()
    {
        print("stop swing called");
        lineRenderer.positionCount = 0;
        Destroy(joint);
    }

    void DrawRope()
    {
        if (!joint) return;

        lineRenderer.SetPosition(0, shootCenter.position);
        lineRenderer.SetPosition(1, swingPoint);
    }
}
