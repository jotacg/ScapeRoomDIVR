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
            xrOrigin.GetComponent<Collider>().enabled = true;
            xrOrigin.transform.position = floorPosition.position;
            onCuadriga = false;
        }
        else
        {
            xrOrigin.GetComponent<Collider>().enabled = false;
            xrOrigin.transform.position = cuadrigaPosition.position;
            onCuadriga = true;
        }
    }
}
