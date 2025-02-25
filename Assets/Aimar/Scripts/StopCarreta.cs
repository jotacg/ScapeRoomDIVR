using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopCarreta : MonoBehaviour
{
    [SerializeField] LeashBehaviour lb;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Valla"))
        {
            lb.StopHorses();
        }
    }
}
