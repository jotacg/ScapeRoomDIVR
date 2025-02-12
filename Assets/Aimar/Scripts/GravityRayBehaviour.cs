using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityRayBehaviour : MonoBehaviour
{
    GameObject objective = null;
    bool objInPlace = false;
    [SerializeField] Transform rayPoint;
    [SerializeField] float launchForce;
    public void OnActivateEnter()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(rayPoint.position, transform.TransformDirection(rayPoint.up), out hit, Mathf.Infinity))
        {
            if (hit.transform.CompareTag("Gravity"))
            {

            }
     
        }
    }

    public void OnActivateExit()
    {
        percentagePlace = 0;
        objInPlace = false;
        objective.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        if (objInPlace)
        {
            
            objective.GetComponent<Rigidbody>().AddForce(rayPoint.up * launchForce);
        }
        objective = null;
    }

    float percentagePlace = 0;
    void Update()
    {
        if(objective != null && !objInPlace)
        {
            percentagePlace += Time.deltaTime;
            objective.transform.position = Vector3.Lerp(transform.position, rayPoint.position, percentagePlace);
            if (Vector3.Distance(objective.transform.position, rayPoint.position) < 0.1f)
            {
                objective.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                objInPlace = true;
            }
        }
        if (objInPlace)
        {
            objective.transform.position = rayPoint.position;
        }
    }
}
