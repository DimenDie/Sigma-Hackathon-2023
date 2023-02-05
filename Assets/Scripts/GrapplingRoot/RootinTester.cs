using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootinTester : MonoBehaviour
{
    [Header("Transform References")]
    [SerializeField] Transform player;
    [SerializeField] Transform target;

    [Space(10)]
    [Header("SpritePrefab")]
    [SerializeField] GameObject HookSpritePrefab;

    [Space(10)]
    [Header("Hook Values")]
    [SerializeField] float grappleTime;

    private void Start()
    {
        HookSpritePrefab.transform.position = player.position;
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.O))
        //    StartCoroutine(StartGrapple());
    }


    IEnumerator StartGrapple()
    {
        GameObject hookObj = Instantiate(HookSpritePrefab, player.position,Quaternion.identity);
        float t = 0;
        Vector3 defautScale = hookObj.transform.localScale;
        defautScale.x = 0;

        while (t<=1)
        {
            hookObj.transform.rotation = Quaternion.Euler(0, DetectAngleY() + 90, DetectAngleZ() + 90);
            hookObj.transform.position = Vector3.Lerp(player.position, (player.position + target.position)/2, t);
            hookObj.transform.localScale = new Vector3 
                (Mathf.Lerp(defautScale.x, Vector3.Distance(player.position, target.position)/2, t), 
                defautScale.y, 
                defautScale.z);
            
            t += Time.deltaTime / grappleTime;
            yield return null;
        }

        while(true)
        {
            hookObj.transform.rotation = Quaternion.Euler(0, DetectAngleY() + 90, DetectAngleZ() + 90);
            hookObj.transform.position = (player.position + target.position) / 2;
            hookObj.transform.localScale = new Vector3
                (Vector3.Distance(player.position, target.position) / 2,
                defautScale.y,
                defautScale.z);
            yield return null;
        }

    }


    float DetectAngleY()
    {
        Vector3 playerDirection = player.forward;
        playerDirection.y = 0;
        Vector3 targetDirection = player.position - target.position;
        targetDirection.y = 0;

        return Vector3.SignedAngle(playerDirection, targetDirection, Vector3.up) + player.rotation.eulerAngles.y;
    }


    float DetectAngleZ()
    {
        Vector3 playerDirection = player.up;
        Vector3 targetDirection = player.position - target.position;

        return Vector3.SignedAngle(playerDirection, targetDirection, Vector3.Cross(playerDirection, targetDirection)) + player.rotation.eulerAngles.z;
    }

}
