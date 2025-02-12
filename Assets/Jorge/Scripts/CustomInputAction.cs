using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CustomInputAction : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    private Vector3 _grapplePoint;
    public LayerMask _grappleableMask;
    public Transform _tip, _cam, _player; 
    private float _maxDistance = 200f;
    private SpringJoint _joint;

    void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    public InputActionReference customButton;

    // Start is called before the first frame update
    void Start()
    {
        customButton.action.started += ButtonWasPressed;
        customButton.action.canceled += ButtonWasReleased;
    }
    
    // Update is called once per frame
    void LateUpdate()
    {
        DrawRope();
    }

    private void ButtonWasPressed(InputAction.CallbackContext context)
    {
        Debug.Log("Presionado!!");
        StartGrapple();
    }

    private void ButtonWasReleased(InputAction.CallbackContext context)
    {
        Debug.Log("Soltado!!");
        StopGrapple();
    }

    private void StartGrapple()
    {
        RaycastHit hit;
        if(Physics.Raycast(_cam.position, _cam.forward, out hit, _maxDistance, _grappleableMask))
        {
            _grapplePoint = hit.point;
            _joint = _player.gameObject.AddComponent<SpringJoint>();
            _joint.autoConfigureConnectedAnchor = false;
            _joint.connectedAnchor = _grapplePoint;

            float distanceFromPoint = Vector3.Distance(_player.position, _grapplePoint);

            // The distance grapple will try  to keep from grapple point
            _joint.maxDistance = distanceFromPoint * 0.4f;
            _joint.minDistance = distanceFromPoint * 0.25f;

            // Probar diferentes valores
            _joint.spring = 4.5f;
            _joint.damper = 7f;
            _joint.massScale = 4.5f;

            _lineRenderer.positionCount = 2;
        }
    }

    private void StopGrapple()
    {
        _lineRenderer.positionCount = 0;
        Destroy(_joint);
    }

    void DrawRope()
    {
        // Si no se ha enganchado a nada  no se dibuja 
        if(!_joint) return;

        _lineRenderer.SetPosition(0, _tip.position);
        _lineRenderer.SetPosition(1, _grapplePoint);
    }
}
