using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetrieveOrb : MonoBehaviour
{
    [SerializeField] GameObject orb;
    [SerializeField] Transform socketPosition;


    public void OnActivatedEnter()
    {
        orb.GetComponent<Rigidbody>().velocity = Vector3.zero;
        orb.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        orb.transform.position = socketPosition.position;
    }
}
