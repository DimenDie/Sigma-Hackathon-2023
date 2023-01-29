using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float playerSpeed;
    [SerializeField] float playerSpeedChange;
    [SerializeField] Transform centralPoint, sphereTransform, directionalPoint ,camera;
    Rigidbody playerRigidbody;


    private void Start()
    {
        playerRigidbody = sphereTransform.GetComponent<Rigidbody>();
        directionalPoint.rotation = Quaternion.Euler( 0, camera.rotation.eulerAngles.y, 0);
    }

    void Update()
    {
        Move();
        centralPoint.transform.position = sphereTransform.position;
    }

    void Move()
    {   
        Vector3 input = 
                (Input.GetAxisRaw("Vertical") * directionalPoint.forward +
                Input.GetAxisRaw("Horizontal") * directionalPoint.right)
                * Time.deltaTime;

        playerRigidbody.velocity = Vector3.MoveTowards(playerRigidbody.velocity, input.normalized * playerSpeed, playerSpeedChange);

        if (input.magnitude == 0)
        {
            playerRigidbody.velocity = Vector3.MoveTowards(playerRigidbody.velocity, Vector3.zero, playerSpeedChange);
            playerRigidbody.angularVelocity = Vector3.MoveTowards(playerRigidbody.angularVelocity, Vector3.zero, playerSpeedChange);

        }

    }
}