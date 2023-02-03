using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swinging : MonoBehaviour
{
    public KeyCode swingKey = KeyCode.Mouse0;

    public Transform shootCenter, camera, player;
    public LayerMask isGrappleable;
    public float rootRotationSpeed;

    public float maxSwingDistance;
    private Vector3 swingPoint;
    private SpringJoint joint;

    private Vector3 currentGrapplePosition;

    public Movement playerMovement;


    GameObject hookObj;

    GraplePoint currentGrapplePoint, previousGrapplePoint;

    [Space(10)]
    [Header("RootPrefab")]
    [SerializeField] GameObject HookSpritePrefab;

    [Space(10)]
    [Header("RootOriginReference")]
    [SerializeField] Transform rootOrigin;

    [Space(10)]
    [Header("Hook Values")]
    [SerializeField] float grappleTime;

    private void Update()
    {
        rootOrigin.position = player.position;
        shootCenter.eulerAngles = Vector3.zero;

        GrapplePointCheck();

        if (Input.GetKeyDown(swingKey)) StartSwing();
        if (Input.GetKeyUp(swingKey)) StopSwing();
    }

    void GrapplePointCheck()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, maxSwingDistance, isGrappleable))
        {
            if (currentGrapplePoint != null && currentGrapplePoint.transform.position != hit.transform.position)
                currentGrapplePoint.ReturnColor();

            currentGrapplePoint = hit.transform.GetComponent<GraplePoint>();
            currentGrapplePoint.SetColor();
        }
        else
        {
            if(currentGrapplePoint != null)
                currentGrapplePoint.ReturnColor();

            currentGrapplePoint = null;
        }
    }

    void StartSwing()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, maxSwingDistance, isGrappleable))
        {
            playerMovement.activeSwing = true;

            swingPoint = hit.transform.GetComponent<GraplePoint>().hookPivo.position;

            StartCoroutine(StartGrappleAnimation());

        }
    }

    IEnumerator StartGrappleAnimation()
    {
        hookObj = Instantiate(HookSpritePrefab, rootOrigin.position, Quaternion.identity);
        float t = 0;
        Vector3 defautScale = hookObj.transform.localScale;
        defautScale.x = 0;
        while (t <= 1)
        {

            if (hookObj == null) yield break;

            hookObj.transform.rotation = Quaternion.Euler(0, DetectAngleY() + 90, DetectAngleZ() + 90);
            hookObj.transform.position = Vector3.Lerp(rootOrigin.position, (rootOrigin.position + swingPoint) / 2, t);
            hookObj.transform.localScale = new Vector3
                (Mathf.Lerp(defautScale.x, Vector3.Distance(rootOrigin.position, swingPoint) / 2, t),
                defautScale.y,
                defautScale.z);

            t += Time.deltaTime / grappleTime;
            yield return null;
        }

        CreateJoint();

        while (joint)
        {
            hookObj.transform.rotation = Quaternion.Euler(0, DetectAngleY() + 90, DetectAngleZ() + 90);
            hookObj.transform.position = (rootOrigin.position + swingPoint) / 2;
            hookObj.transform.localScale = new Vector3
                (Vector3.Distance(rootOrigin.position, swingPoint) / 2,
                defautScale.y,
                defautScale.z);

            yield return null;
        }

    }

    void CreateJoint()
    {
        joint = player.gameObject.AddComponent<SpringJoint>();
        joint.autoConfigureConnectedAnchor = false;
        joint.connectedAnchor = swingPoint;

        float distanceFromPoint = Vector3.Distance(player.position, swingPoint);

        joint.maxDistance = distanceFromPoint * 0.8f;
        joint.minDistance = distanceFromPoint * 0.25f;

        joint.spring = 4.5f;
        joint.damper = 7f;
        joint.massScale = 4.5f;

        currentGrapplePosition = shootCenter.position;
    }


    void StopSwing()
    {
        playerMovement.activeSwing = false;
        Destroy(hookObj);
        Destroy(joint);
    }

    float DetectAngleY()
    {
        Vector3 playerDirection = rootOrigin.forward;
        playerDirection.y = 0;
        Vector3 targetDirection = rootOrigin.position - swingPoint;
        targetDirection.y = 0;

        return Vector3.SignedAngle(playerDirection, targetDirection, Vector3.up) + gameObject.transform.rotation.eulerAngles.y;
    }


    float DetectAngleZ()
    {
        Vector3 playerDirection = rootOrigin.up;
        Vector3 targetDirection = rootOrigin.position - swingPoint;

        return Vector3.SignedAngle(playerDirection, targetDirection, Vector3.Cross(playerDirection, targetDirection)) + gameObject.transform.rotation.eulerAngles.z;
    }


}
