using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandadoBarco : MonoBehaviour
{
    [SerializeField] Animator animPuertaDer, animPuertaIzq, animCandado;
    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    public void abrir()
    {
        animCandado.Play("candado");
        StartCoroutine(abrirelresto());

    }

    IEnumerator abrirelresto()
    {
        yield return new WaitForSeconds(0.8f);
        rb.isKinematic = false;
        animPuertaDer.Play("puertaDer");
        animPuertaIzq.Play("puertaIzq");
        ;
    }
}
