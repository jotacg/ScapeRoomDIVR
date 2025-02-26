using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HopOnCuadriga : MonoBehaviour
{
    [SerializeField] Transform cuadrigaPosition;
    [SerializeField] Transform floorPosition;
    [SerializeField] GameObject xrOrigin;

    bool onCuadriga = false;

    private void Update()
    {
        if (onCuadriga)
        {
            xrOrigin.transform.position = cuadrigaPosition.position;
            xrOrigin.transform.rotation = cuadrigaPosition.rotation;
        }
    }
    public void OnSelectEnter()
    {
        if (onCuadriga)
        {
            xrOrigin.GetComponent<Rigidbody>().isKinematic = false;
            xrOrigin.transform.position = floorPosition.position;
            onCuadriga = false;
        }
        else
        {
            xrOrigin.GetComponent<Rigidbody>().isKinematic = true;
            xrOrigin.transform.position = cuadrigaPosition.position;
            onCuadriga = true;
        }
    }
}
