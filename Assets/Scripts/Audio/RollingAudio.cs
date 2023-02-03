using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingAudio : MonoBehaviour
{
    
    [SerializeField]AudioSource audioSource;
    Movement movement;

    [SerializeField] float soundDecreaseSpeed;
    [Range(0.0f, 1.0f)]
    [SerializeField]float maximumVolume;

    private void Start()
    {
        movement = GetComponent<Movement>();
    }

    void Update()
    {
        SoundRegulator();
    }

    void SoundRegulator()
    {
        if (movement.isGrounded && movement.playerRigidbody.velocity.magnitude > 0)
            audioSource.volume = Mathf.Lerp(0, maximumVolume, movement.playerRigidbody.velocity.magnitude / movement.playerSpeed);
        else
            audioSource.volume = Mathf.MoveTowards(audioSource.volume, 0, soundDecreaseSpeed);

    }
}
