using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class camaraSocket : MonoBehaviour
{
    // Start is called before the first frame update
    private XRSocketInteractor socketInteractor;

    void Start()
    {
        socketInteractor = GetComponent<XRSocketInteractor>();
        socketInteractor.selectEntered.AddListener(LockObject);
    }

    public void LockObject(SelectEnterEventArgs args)
    {
        XRGrabInteractable grabbedObject = args.interactableObject as XRGrabInteractable;
        if (grabbedObject != null)
        {
            grabbedObject.gameObject.layer = 7;
        }
    }
}
