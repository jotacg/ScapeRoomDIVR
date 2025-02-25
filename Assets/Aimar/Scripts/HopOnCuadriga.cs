using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HopOnCuadriga : MonoBehaviour
{
    [SerializeField] Transform cuadrigaPosition;
    [SerializeField] Transform floorPosition;
    [SerializeField] GameObject xrOrigin;

    bool onCuadriga = false;

    public void OnSelectEnter()
    {
        if (onCuadriga)
        {
            
            xrOrigin.transform.position = floorPosition.position;
            onCuadriga = false;
        }
        else
        {
            
            xrOrigin.transform.position = cuadrigaPosition.position;
            onCuadriga = true;
        }
    }
}
