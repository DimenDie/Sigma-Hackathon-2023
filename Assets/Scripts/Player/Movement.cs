using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Movement Values")]
    public float playerSpeed;
    [SerializeField] float playerDeceleration;
    [SerializeField] float playerAcceleration;
    public float swingSpeed;
    public bool isGrounded;
    [SerializeField] float groundDistance;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float landingTimeCap;


    [Space(10)]

    [Header("Camera Values")]
    [SerializeField] float cameraMinSpeed;
    [SerializeField] float cameraMaxSpeed;
    [SerializeField] float cameraSwingMaxSpeed;
    [SerializeField] float cameraMaxDistance;
    [SerializeField] float cameraSmoothTime;
    [SerializeField] public float mouseSensivity;
    [SerializeField] float cameraMaxAngle;
    [SerializeField] float cameraMinAngle;

    float Xinput = 0;

    [Space(10)]

    [Header("Transform References")]
    [SerializeField] Transform centralPoint;
    [SerializeField] Transform playerPrefab;
    [SerializeField] Transform sphereTransform;
    [SerializeField] Transform directionalPoint;
    [SerializeField] Transform camera;
    [SerializeField] Transform cameraXZPivot;
    [SerializeField] Transform groundCheck;

    //Other
    Vector3 cameraVelocity;
    [HideInInspector] public Rigidbody playerRigidbody;
    [HideInInspector] public bool grappleFreeze;
    [HideInInspector] public bool activeGrapple;
    public bool activeSwing;
    private Vector3 velocityToSet;

    bool landingMemory;
    [SerializeField]float notGroundedTime;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        SetXZPivot();
        playerRigidbody = sphereTransform.GetComponent<Rigidbody>();
        directionalPoint.rotation = Quaternion.Euler(0, camera.rotation.eulerAngles.y, 0);
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        LandingCheck();

        if (grappleFreeze) //Currently instead of grapple swinging is used
        {
            playerRigidbody.velocity = Vector3.zero;
        }



        if (FindObjectOfType<Death>().isDead || FindObjectOfType<Level>().levelFinished)
        {
            playerRigidbody.velocity = Vector3.zero;
            return;
        }
            

        Move();
        CameraTracking();
        CameraRotation();

    }

    void LandingCheck()
    {
        if (isGrounded && !landingMemory && notGroundedTime >= landingTimeCap)
            FindObjectOfType<AudioManager>().PlaySound("Landing");

        landingMemory = isGrounded;

        notGroundedTime = isGrounded ? 0 : notGroundedTime + Time.deltaTime;
    }

    void SetXZPivot()
    {
        Vector3 XZforward = cameraXZPivot.forward;
        Vector3 XZtoCamDirection = cameraXZPivot.position - camera.position;
        XZtoCamDirection.y = 0;

        float Yoffset = Vector3.SignedAngle(XZforward, XZtoCamDirection, Vector3.Cross(XZforward, XZtoCamDirection));


        cameraXZPivot.rotation = Quaternion.Euler(0, playerPrefab.rotation.eulerAngles.y + Yoffset, 0);
        camera.parent = cameraXZPivot;
    }

    void Move()
    {
        if (activeGrapple) return;

        Vector3 input =
                (Input.GetAxisRaw("Vertical") * directionalPoint.forward +
                Input.GetAxisRaw("Horizontal") * directionalPoint.right);

        if (playerRigidbody.velocity.magnitude < 0.35f && isGrounded)
            playerRigidbody.angularVelocity = Vector3.zero;

        
        if (input.magnitude == 0)
        {
            playerRigidbody.velocity = Vector3.MoveTowards(playerRigidbody.velocity, new Vector3(0, playerRigidbody.velocity.y, 0), playerDeceleration * Time.deltaTime);
            if(isGrounded)
                playerRigidbody.angularVelocity = Vector3.MoveTowards(playerRigidbody.angularVelocity, Vector3.zero, playerDeceleration * Time.deltaTime);
        }
        else
        {
            Vector3 velocityDirection = Vector3.MoveTowards(playerRigidbody.velocity, input.normalized * (activeSwing ? swingSpeed : playerSpeed), playerAcceleration * Time.deltaTime);
            playerRigidbody.velocity = new Vector3(velocityDirection.x, playerRigidbody.velocity.y, velocityDirection.z);
        }

    }

    void CameraTracking()
    {
        float pointAndCamDistance = (centralPoint.position - sphereTransform.position).magnitude;
        float cameraSpeed = Mathf.Lerp(cameraMinSpeed, activeSwing ? cameraSwingMaxSpeed : cameraMaxSpeed, pointAndCamDistance / cameraMaxDistance);
        centralPoint.transform.position = Vector3.MoveTowards(centralPoint.transform.position, sphereTransform.position, cameraSpeed * Time.deltaTime);
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

    void CameraRotation()
    {
        centralPoint.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * mouseSensivity * Time.deltaTime, 0));

        Xinput += Input.GetAxis("Mouse Y") * mouseSensivity * Time.deltaTime;


        Xinput = Mathf.Clamp(Xinput, cameraMinAngle - 30, cameraMaxAngle - 30);

        cameraXZPivot.rotation = Quaternion.Euler(-Xinput, cameraXZPivot.rotation.eulerAngles.y, cameraXZPivot.rotation.eulerAngles.z);
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.transform.position, groundDistance);
    }
}