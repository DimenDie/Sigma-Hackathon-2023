using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingAudio : MonoBehaviour
{
    BallAudioSourceHolder ball;

    AudioSource rollingSource;
    AudioSource flyingSource;
    
    Movement movement;

    [SerializeField] float rollingVolumeDecreaseSpeed;
    [SerializeField] float flyingVolumeDecreaseSpeed;
    [Range(0.0f, 1.0f)]
    [SerializeField]float maximumRollingVolume;
    [Range(0.0f, 1.0f)]
    [SerializeField]float maximumFlingVolume;

    private void Start()
    {
        ball = FindObjectOfType<BallAudioSourceHolder>();
        rollingSource = ball.rollingSource;
        flyingSource = ball.flyingSource;

        movement = GetComponent<Movement>();
    }

    void Update()
    {
        SoundRegulator();
    }

    void SoundRegulator()
    {
        if (movement.isGrounded && movement.playerRigidbody.velocity.magnitude > 0)
            rollingSource.volume = Mathf.Lerp(0, maximumRollingVolume, movement.playerRigidbody.velocity.magnitude / movement.playerSpeed);
        else
            rollingSource.volume = Mathf.MoveTowards(rollingSource.volume, 0, rollingVolumeDecreaseSpeed * Time.deltaTime);

        if(movement.activeSwing && movement.playerRigidbody.velocity.magnitude > 0)
            flyingSource.volume = Mathf.Lerp(0, maximumFlingVolume, movement.playerRigidbody.velocity.magnitude / movement.swingSpeed);
        else
            flyingSource.volume = Mathf.MoveTowards(flyingSource.volume, 0, flyingVolumeDecreaseSpeed * Time.deltaTime);
    }
}
