using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenKeyBox : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] GameObject _keyBox;
    private bool _keyBoxOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        _animator = _keyBox.GetComponent<Animator>();
    }
    public void OpenKeyBoxAnim()
    {
        if(!_keyBoxOpen)
        {
            _animator.SetTrigger("TrKeyBox");
            _keyBoxOpen = true;
        }
    }
}
