using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowOrb : MonoBehaviour
{
    Vector3 dir;
    Vector3 prevDir;

    [SerializeField]
    float launchForce;

    Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        dir = prevDir - transform.position;
        prevDir = transform.position;
    }

   
    public void OnSelectExit()
    {
        rb.constraints = RigidbodyConstraints.None;
        rb.AddForce(dir * launchForce);
    }

    private void OnCollisionEnter(Collision collision)
    {
        rb.constraints = RigidbodyConstraints.FreezePosition;
    }

}
