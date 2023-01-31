using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteAfterUse : MonoBehaviour
{
    [SerializeField] float x, y, z, speed;
    Vector3 defaultPos;
    private void Start()
    {
        defaultPos = transform.position;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
