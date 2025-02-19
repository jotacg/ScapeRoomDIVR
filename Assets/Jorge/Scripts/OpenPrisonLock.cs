using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPrisonLock : MonoBehaviour
{
    private bool _doorOpen = false;
    private Animator _animator;
    [SerializeField] private GameObject _door;  // Go que se abrirá al introducir la llave

    // Start is called before the first frame update
    void Start()
    {
        _animator = _door.GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other) {
        if(!_doorOpen && other.transform.gameObject.layer == 11)
        {
            _animator.SetTrigger("TrOpenPrison");
            _doorOpen = true;
        }
    }
}
