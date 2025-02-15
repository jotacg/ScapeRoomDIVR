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
   

   
    public void OnSelectExit()
    {
        rb.constraints = RigidbodyConstraints.None;
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        rb.constraints = RigidbodyConstraints.FreezePosition;
    }

}
