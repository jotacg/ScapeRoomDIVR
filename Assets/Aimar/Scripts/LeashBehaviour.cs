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
    [SerializeField] GameObject riendaLocalPos;

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
    float currentAngle = 0;
    void Update()
    {
        

        if (xRGrab.isSelected && rightHand.isSelectActive && leftHand.isSelectActive)
        {
            
            if (currentSpeed > 0)
            {
                cuadriga.transform.position -= cuadriga.transform.forward * moveSpeed * currentSpeed * Time.deltaTime;

                float angleDifference = riendaLocalPos.transform.localPosition.x;
                currentAngle += angleDifference * Time.deltaTime * rotSpeed;
                cuadriga.transform.rotation = Quaternion.Euler(-Vector3.up * currentAngle);


            }

            if (!waiting)
            {
                if (transform.localPosition.y - originalPosition.y > arreHeight && !arreChecked)
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
                if (transform.localPosition.y - originalPosition.y > soHeight)
                {
                    if (currentSpeed > 0)
                    {
                        currentSpeed--;
                    }
                    if (currentSpeed == 0)
                    {
                        foreach (Animator a in horses)
                        {
                            a.SetBool("Stop", true);
                            a.speed = 1;
                        }
                    }
                    soChecked = true;
                }
            }
            else if ((Mathf.Abs(transform.localPosition.y - originalPosition.y) < restRadious))
            {
                soChecked = false;
            }
        }
        else
        {
           
            StopHorses();
            
            transform.localPosition = originalPosition;
            transform.localRotation = originalRotation;
            
        }
    }

    IEnumerator CheckArre()
    {
        waiting = true;
        yield return new WaitForSeconds(moveTime);
        if (Mathf.Abs(transform.localPosition.y - originalPosition.y) < restRadious)
        {
            //  texto.text = "ARRE" + arreCount++;
            if (currentSpeed < maxSpeed)
            {
                currentSpeed++;

            }
            foreach (Animator a in horses)
            {
                if (currentSpeed == 1)
                {
                    a.SetBool("Stop", false);
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

    public void StopHorses()
    {
        currentSpeed = 0;
        foreach (Animator a in horses)
        {
            a.SetBool("Stop", true);
            a.speed = 1;
        }
    }
}
