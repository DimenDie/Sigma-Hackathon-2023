using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    [SerializeField] float amplitude;
    [SerializeField] float speed;
    Vector3 defaultPosition;
    private void Start()
    {
        defaultPosition = transform.position;
    }
    private void Update()
    {
        transform.position = defaultPosition + new Vector3(0, Mathf.Sin(Time.time * speed) * amplitude, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Daun");
        }
    }
}
