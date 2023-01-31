using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Movement Values")]
    [SerializeField] float playerSpeed;
    [SerializeField] float playerDeceleration;
    [SerializeField] float playerAcceleration;

    [Space(10)]

    [Header("Camera Tracking Values")]
    [SerializeField] float cameraMinSpeed;
    [SerializeField] float cameraMaxSpeed;
    [SerializeField] float cameraMaxDistance;
    [SerializeField] float cameraSmoothTime;

    [Space(10)]

    [Header("Transform References")]
    [SerializeField] Transform centralPoint;
    [SerializeField] Transform sphereTransform;
    [SerializeField] Transform directionalPoint;
    [SerializeField] Transform camera;

    //Other
    Vector3 cameraVelocity;
    Rigidbody playerRigidbody;
    public bool freeze;
    public bool activeGrapple;
    private Vector3 velocityToSet;


    private void Start()
    {
        playerRigidbody = sphereTransform.GetComponent<Rigidbody>();
        directionalPoint.rotation = Quaternion.Euler(0, camera.rotation.eulerAngles.y, 0);
    }

    void Update()
    {
        if (freeze)
        {
            playerRigidbody.velocity = Vector3.zero;
        }
        Move();
        CameraTracking();
    }

    void Move()
    {
        if (activeGrapple) return;

        Vector3 input =
                (Input.GetAxisRaw("Vertical") * directionalPoint.forward +
                Input.GetAxisRaw("Horizontal") * directionalPoint.right);

        if (playerRigidbody.velocity.magnitude < 0.35f)
            playerRigidbody.angularVelocity = Vector3.zero;

        
        if (input.magnitude == 0)
        {
            playerRigidbody.velocity = Vector3.MoveTowards(playerRigidbody.velocity, new Vector3(0, playerRigidbody.velocity.y, 0), playerDeceleration * Time.deltaTime);
            playerRigidbody.angularVelocity = Vector3.MoveTowards(playerRigidbody.angularVelocity, Vector3.zero, playerDeceleration * Time.deltaTime);
        }
        else
        {
            Vector3 velocityDirection = Vector3.MoveTowards(playerRigidbody.velocity, input.normalized * playerSpeed, playerAcceleration * Time.deltaTime);
            playerRigidbody.velocity = new Vector3(velocityDirection.x, playerRigidbody.velocity.y, velocityDirection.z);
        }

    }

    void CameraTracking()
    {
        float pointAndCamDistance = (centralPoint.position - sphereTransform.position).magnitude;
        float cameraSpeed = Mathf.Lerp(cameraMinSpeed, cameraMaxSpeed, pointAndCamDistance / cameraMaxDistance);
        centralPoint.transform.position = Vector3.SmoothDamp(centralPoint.transform.position, sphereTransform.position, ref cameraVelocity, cameraSmoothTime * Time.deltaTime, cameraSpeed);
    }

    public Vector3 CalculateGrappleVelocity(Vector3 startPoint, Vector3 endPoint, float trajectoryHeight)
    {
        float gravity = Physics.gravity.y;
        float displacementY = endPoint.y - startPoint.y;
        Vector3 displacementXZ = new Vector3(endPoint.x - startPoint.x, 0f, endPoint.z - startPoint.z);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * trajectoryHeight);
        Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * trajectoryHeight / gravity)
            + Mathf.Sqrt(2 * (displacementY - trajectoryHeight) / gravity));

        return velocityXZ + velocityY;
    }

    public void GrappleToPosition(Vector3 targetPosition, float trajectoryHeight, float velocityMultiplier)
    {
        activeGrapple = true;

        velocityToSet = CalculateGrappleVelocity(sphereTransform.transform.position, targetPosition, trajectoryHeight) * velocityMultiplier;
        Invoke(nameof(SetVelocity), 0.1f);
    }

    private void SetVelocity()
    {
        playerRigidbody.velocity = velocityToSet;
    }
}