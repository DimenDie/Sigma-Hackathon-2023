using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    public string tag;
    [HideInInspector] public bool isDead;

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag(tag))
        {
            isDead = true;
            FindObjectOfType<UI>().TogglePause();
        }
    }
}
