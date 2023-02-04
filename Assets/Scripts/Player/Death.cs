using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    public string tag;
    [SerializeField] ShaderBehaviour spawnShader;
    [HideInInspector] public bool isDead;

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag(tag) && !isDead)
        {
            StartCoroutine(spawnShader.DissolveSwitch(true));
            isDead = true;            
        }
    }
}
