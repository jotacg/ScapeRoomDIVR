using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJump : MonoBehaviour
{
    [SerializeField] GameObject xrOrigin;
    [SerializeField] float force;
    
    float posYdiff;
    public void OnSelectEnter()
    {
        posYdiff = xrOrigin.transform.position.y;
    }
    public void OnSelectExit()
    {
        posYdiff -= xrOrigin.transform.position.y;
        if(posYdiff > 0)
        {
            xrOrigin.GetComponent<Rigidbody>().AddForce((transform.up + transform.forward) * force);
        }
    }
}
