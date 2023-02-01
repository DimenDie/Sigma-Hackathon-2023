using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappling : MonoBehaviour
{
    private Movement playerMovement;
    public Transform camera;
    public Transform shootPoint;
    public LayerMask isGrappable;
    public LineRenderer lineRenderer;

    public float maxGrappleDistance;
    public float grappleDelay;
    public float rootDelay = 1f;
    public float overshootYAxis;

    public float velocityMultiplier = 1.5f;

    private Vector3 grapplePoint;

    public float grapplingCooldown;
    private float grapplingCooldownTimer;

    public KeyCode grappleKey = KeyCode.Mouse0;

    private bool grappling;

    private void Start()
    {
        playerMovement = GetComponent<Movement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(grappleKey))
        {
            StartGrapple();
        }

        if(grapplingCooldownTimer > 0)
        {
            grapplingCooldownTimer -= Time.deltaTime;
        }
    }

    private void LateUpdate()
    {
        if (grappling)
            lineRenderer.SetPosition(0, shootPoint.position);
    }

    private void StartGrapple()
    {
        if (grapplingCooldownTimer > 0) return;

        grappling = true;

        playerMovement.grappleFreeze = true;

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit, maxGrappleDistance, isGrappable))
        {
            grapplePoint = hit.transform.position;
            Invoke(nameof(ExecuteGrapple), grappleDelay);
        }
        else
        {
            grapplePoint = ray.GetPoint(maxGrappleDistance);

            Invoke(nameof(StopGrapple), grappleDelay);
        }

        lineRenderer.enabled = true;
        lineRenderer.SetPosition(1, grapplePoint);
    }

    private void ExecuteGrapple()
    {
        playerMovement.grappleFreeze = false;

        Vector3 lowestPoint = new Vector3(shootPoint.transform.position.x, shootPoint.transform.position.y - 1f, shootPoint.transform.position.z);
        float grapplePointRelativeYPos = grapplePoint.y - lowestPoint.y;
        float highestPointOnArc = grapplePointRelativeYPos + overshootYAxis;
        if (grapplePointRelativeYPos < 0) highestPointOnArc = overshootYAxis;


        playerMovement.GrappleToPosition(grapplePoint, highestPointOnArc, velocityMultiplier);

        Invoke(nameof(StopGrapple), rootDelay);

    }

    private void StopGrapple()
    {
        playerMovement.grappleFreeze = false;

        playerMovement.activeGrapple = false;

        grappling = false;

        grapplingCooldownTimer = grapplingCooldown;

        lineRenderer.enabled = false;

    }
}
