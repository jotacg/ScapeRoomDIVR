using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LeashBehaviour : MonoBehaviour
{
    [SerializeField] XRBaseControllerInteractor rightHand;
    [SerializeField] XRBaseControllerInteractor leftHand;
    [SerializeField] XRGrabInteractable xRGrab;
    [SerializeField] float moveTime;
    [SerializeField] float arreHeight;
    [SerializeField] float soHeight;

    [Header("Cuadriga")]
    [SerializeField] float moveSpeed;
    [SerializeField] Rigidbody cuadriga;
    [SerializeField] float maxSpeed;
    [SerializeField] float rotSpeed = 2;
   

    [Header("Caballos")]
    [SerializeField] Animator[] horses;
   

    Vector3 originalPosition;
    Quaternion originalRotation;
    float restRadious = 0.1f;
    bool waiting = false;
    bool arreChecked = false;
    bool soChecked = false;

    int currentSpeed = 0;

    private void Awake()
    {
        originalPosition = transform.localPosition;
        originalRotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
       
        
        if (xRGrab.isSelected && rightHand.isSelectActive && leftHand.isSelectActive)
        {
            
            if(currentSpeed > 0)
            {
                cuadriga.transform.position -= cuadriga.transform.forward *moveSpeed * currentSpeed * Time.deltaTime;
                cuadriga.transform.rotation *= Quaternion.Euler(-Vector3.up * (180+ cuadriga.transform.rotation.eulerAngles.y - transform.rotation.eulerAngles.y) *Time.deltaTime * rotSpeed);
                
            }

            if (!waiting)
            {
                if(transform.localPosition.y - originalPosition.y > arreHeight && !arreChecked)
                {
                    arreChecked = true;
                    StartCoroutine(CheckArre());
                }
                else
                {
                    arreChecked = false;
                }
            }

            if (!soChecked)
            {
                if(transform.localPosition.y - originalPosition.y > soHeight)
                {
                   if(currentSpeed > 0)
                    {
                        currentSpeed--;
                    }
                   if(currentSpeed == 0)
                    {
                        foreach(Animator a in horses)
                        {
                            a.SetTrigger("Stop");
                            a.speed = 1;
                        }
                    }
                    soChecked = true;
                }
            }else if((Mathf.Abs(transform.localPosition.y - originalPosition.y) < restRadious)){
                soChecked = false;
            }
        }
        else
        {
            currentSpeed = 0;
            transform.localPosition = originalPosition;
            transform.localRotation = originalRotation;
            foreach (Animator a in horses)
            {
                a.SetTrigger("Stop");
                a.speed = 1;
            }
        }
    }

    IEnumerator CheckArre()
    {
        waiting = true;
        yield return new WaitForSeconds(moveTime);
        if (Mathf.Abs(transform.localPosition.y - originalPosition.y) < restRadious)
        {
          //  texto.text = "ARRE" + arreCount++;
          if(currentSpeed < maxSpeed)
           {
                currentSpeed++;
                
           }
            foreach (Animator a in horses)
            {
                if(currentSpeed == 1)
                {
                    a.SetTrigger("Run");
                }
                
                a.speed = currentSpeed;
            }

        }
        waiting = false;
    }

    public void OnSelectExit()
    {
        transform.localPosition = originalPosition;
        transform.localRotation = originalRotation;
    }
}
