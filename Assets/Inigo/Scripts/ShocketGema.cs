using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ShocketGema : MonoBehaviour
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
            Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
            rb.isKinematic = true;
        }
    }
}