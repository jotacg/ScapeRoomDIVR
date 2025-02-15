using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ActivateTP : MonoBehaviour
{
    [SerializeField]
    Transform bottomPlace;
    [SerializeField]
    XRBaseControllerInteractor rightHand;
    [SerializeField]
    XRGrabInteractable xRGrab;
    [SerializeField]
    Transform xrOrigin;
    [SerializeField]
    Transform orb;
    [SerializeField]
    Transform baculo;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Suelo"))
        {
            if(xRGrab.isSelected && rightHand.isSelectActive) //Esta agarrando el baculo con la mano derecha
            {
                xrOrigin.position = orb.position;
                baculo.GetComponent<RetrieveOrb>().OnActivatedEnter();
            }
        }
    }

    private void Update()
    {
        transform.position = bottomPlace.transform.position;
    }
}
