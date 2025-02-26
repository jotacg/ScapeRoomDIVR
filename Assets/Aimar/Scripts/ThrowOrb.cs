using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowOrb : MonoBehaviour
{

    Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public bool grounded = false;
   
    public void OnSelectExit()
    {
        rb.constraints = RigidbodyConstraints.None;
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            rb.constraints = RigidbodyConstraints.FreezePosition;
            grounded = true;
            RaycastHit hit;

            if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity))
            {
                transform.position = hit.point;
            }
        }
    }

}
