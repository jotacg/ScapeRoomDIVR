using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MantainCuadriga : MonoBehaviour
{
    [SerializeField] GameObject xrOrigin;
    [SerializeField] Transform cuadriga;


    public void OnSelectEnter()
    {
        xrOrigin.transform.SetParent(cuadriga);
    }

    public void OnSelectExit()
    {
        xrOrigin.transform.SetParent(null);
    }
}
