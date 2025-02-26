using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    [SerializeField] GameObject openObject;
    [SerializeField] float openForce = 100;
    [SerializeField] AudioSource audioSource;
    Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == openObject)
        {
            rb.constraints = RigidbodyConstraints.None;
            rb.AddForce(transform.forward * openForce, ForceMode.Impulse);
            rb.tag = "Gravity";
            audioSource.Play();
        }
    }
}
