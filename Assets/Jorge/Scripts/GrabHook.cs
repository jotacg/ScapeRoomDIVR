using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabHook : MonoBehaviour
{
    private bool _doorOpen = false;
    private Animator _animator;
    [SerializeField] private GameObject _door;  // Go que se abrirá cuando se coja el gancho
    [SerializeField] private GameObject _rightController;

    // Start is called before the first frame update
    void Start()
    {
        _animator = _door.GetComponent<Animator>();
    }

    public void GrabHookAction()
    {
        Destroy(this.gameObject);
        if(!_doorOpen)
        {
            _doorOpen = true;

            _rightController.SetActive(true);
        }

        
    }
}
