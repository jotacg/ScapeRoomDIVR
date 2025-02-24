using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    [SerializeField] GameObject openObject;
    [SerializeField] float openForce = 100;
    Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == openObject)
        {
            rb.isKinematic = false;
            rb.AddForce(transform.forward * openForce, ForceMode.Impulse);
        }
    }
}
