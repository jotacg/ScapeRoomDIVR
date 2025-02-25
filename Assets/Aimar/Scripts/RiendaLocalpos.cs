using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiendaLocalpos : MonoBehaviour
{
    [SerializeField] Transform riendas;

    private void Update()
    {
        transform.position = riendas.transform.position;
        transform.rotation = riendas.transform.rotation;
    }
}
