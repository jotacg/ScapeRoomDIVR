using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityRayBehaviour : MonoBehaviour
{
    GameObject objective = null;
    Rigidbody objRB = null;
    bool objInPlace = false;
    [SerializeField] Transform rayPoint;
    [SerializeField] float launchForce;
    [SerializeField] float rayForce;
    [SerializeField] LineRenderer line;
    bool pressing = false;

    Vector3 lineHitPos;
    public void OnActivateEnter()
    {
        pressing = true;
    }

    public void OnActivateExit()
    {
      
        
        objRB.constraints = RigidbodyConstraints.None;
        if (objInPlace)
        {

            objRB.AddForce(rayPoint.forward * launchForce, ForceMode.Impulse);
        }
        objInPlace = false;
        objective = null;
        objRB = null;
        pressing = false;
    }

    void Update()
    {
        line.SetPosition(0, rayPoint.position);
        
        if (pressing)
        {
            line.SetPosition(1, lineHitPos);
        }
        else
        {
            line.SetPosition(1, rayPoint.position);
        }
    }

    void FixedUpdate()
    {
        if (pressing)
        {
            RaycastHit hit;
           
            if (Physics.Raycast(rayPoint.position, rayPoint.forward, out hit, Mathf.Infinity))
            {
                lineHitPos = hit.point;
                if (hit.transform.CompareTag("Gravity"))
                {
                    objective = hit.transform.gameObject;
                    objRB = objective.GetComponent<Rigidbody>();
                    
                    pressing = false;
                }

            }
            
        }
        if(objective != null && !objInPlace)
        {
            
            line.GetPosition(1).Set(objective.transform.position.x, objective.transform.position.y, objective.transform.position.z);
            objRB.AddForce(-(objective.transform.position - rayPoint.position)*rayForce);
            if (Vector3.Distance(objective.transform.position, rayPoint.position) < .5f)
            {
                objRB.constraints = RigidbodyConstraints.FreezePosition;
                objInPlace = true;
            }
        }
        if (objInPlace)
        {
            objective.transform.position = rayPoint.position;
        }
    }
}
