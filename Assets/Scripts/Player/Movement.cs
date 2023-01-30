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

    private void Start()
    {
        playerRigidbody = sphereTransform.GetComponent<Rigidbody>();
        directionalPoint.rotation = Quaternion.Euler( 0, camera.rotation.eulerAngles.y, 0);
    }

    void Update()
    {
        Move();
        CameraTracking();
    }

    void Move()
    {   
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


}