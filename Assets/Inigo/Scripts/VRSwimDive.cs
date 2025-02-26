using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR;

public class VRSwimDive : MonoBehaviour
{
    [Header("Referencias")]
    public Rigidbody playerRigidbody;
    public Transform leftController;
    public Transform rightController;

    [Header("Ajustes")]
    public float forceMultiplier = 1f;
    public float gripThreshold = 0.1f;

    private Vector3 leftLastPos;
    private Vector3 rightLastPos;


    public InputActionProperty grabActionRight; // Asignar en el Inspector
    public InputActionProperty grabActionLeft; // Asignar en el Inspector


    private void Start()
    {
        RenderSettings.fog = true;
    }

    private void FixedUpdate()
    {


        bool leftGripPressed = grabActionLeft.action.ReadValue<float>()>gripThreshold;
        bool rightGripPressed = grabActionRight.action.ReadValue<float>() > gripThreshold;


        // Determinar si se considera que el agarre está presionado


        // Si ambos agarres están presionados, calcular la fuerza a aplicar
        if (leftGripPressed && rightGripPressed)
        {
            // Calcula la diferencia de posición entre frames para cada controlador
            Vector3 leftDelta = leftController.position - leftLastPos;
            Vector3 rightDelta = rightController.position - rightLastPos;

            // Promedia el movimiento de ambos controladores
            Vector3 averageDelta = (leftDelta + rightDelta) * 0.5f;

            // Aplica la fuerza en dirección opuesta al movimiento para simular el "jala" (buceo/natación)
            Vector3 force = -averageDelta * forceMultiplier;
            playerRigidbody.AddForce(force, ForceMode.VelocityChange);
        }

        // Actualizar las posiciones para el siguiente frame
        leftLastPos = leftController.position;
        rightLastPos = rightController.position;
    }
}
