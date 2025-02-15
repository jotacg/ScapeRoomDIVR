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
    public void OnActivateEnter()
    {
        pressing = true;
    }

    public void OnActivateExit()
    {
      
        objInPlace = false;
        objRB.constraints = RigidbodyConstraints.None;
        if (objInPlace)
        {

            objRB.AddForce(rayPoint.up * launchForce);
        }
        objective = null;
        objRB = null;
        pressing = false;
    }

    private void Update()
    {
        
        line.GetPosition(0).Set(transform.position.x, transform.position.y, transform.position.z);
        if (objective != null && !objInPlace)
        {

            line.GetPosition(1).Set(objective.transform.position.x, objective.transform.position.y, objective.transform.position.z);
        }
        else
        {
            line.GetPosition(1).Set(transform.position.x, transform.position.y, transform.position.z);
        }
    }

    void FixedUpdate()
    {
        if (pressing)
        {
            RaycastHit hit;
           
            if (Physics.Raycast(rayPoint.position, rayPoint.right, out hit, Mathf.Infinity))
            {
    
                if (hit.transform.CompareTag("Gravity"))
                {
                    objective = hit.transform.gameObject;
                    objRB = objective.GetComponent<Rigidbody>();
                }

            }
            pressing = false;
        }
        if(objective != null && !objInPlace)
        {

            line.GetPosition(1).Set(objective.transform.position.x, objective.transform.position.y, objective.transform.position.z);
            objRB.AddForce((objective.transform.position - rayPoint.position)*rayForce);
            if (Vector3.Distance(objective.transform.position, rayPoint.position) < 0.1f)
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
