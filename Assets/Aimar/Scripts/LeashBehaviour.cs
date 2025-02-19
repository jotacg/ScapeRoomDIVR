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
    

    [SerializeField] GameObject debugCube;
    [SerializeField] TextMeshProUGUI texto;
    float soCount = 0;
    float arreCount = 0;

    Vector3 originalPosition;
    float restRadious = 0.1f;
    bool waiting = false;
    bool arreChecked = false;
    bool soChecked = false;

    private void Awake()
    {
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (xRGrab.isSelected && rightHand.isSelectActive && leftHand.isSelectActive)
        {
            texto.text = ""+transform.localRotation.eulerAngles.y;
            debugCube.transform.rotation = Quaternion.Euler(Vector3.up * transform.localRotation.eulerAngles.y);
            if (!waiting)
            {
                if(transform.position.y - originalPosition.y > arreHeight && !arreChecked)
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
                if(transform.position.y - originalPosition.y > soHeight)
                {
                   // texto.text = "SOOO" + soCount++;
                    soChecked = true;
                }
            }else if((Mathf.Abs(transform.position.y - originalPosition.y) < restRadious)){
                soChecked = false;
            }
        }
    }

    IEnumerator CheckArre()
    {
        waiting = true;
        yield return new WaitForSeconds(moveTime);
        if (Mathf.Abs(transform.position.y - originalPosition.y) < restRadious)
        {
          //  texto.text = "ARRE" + arreCount++;
            
        }
        waiting = false;
    }
}
