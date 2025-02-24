using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandadoBarco : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Llave")
        {
            
        }
    }
}
