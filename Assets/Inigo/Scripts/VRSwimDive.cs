using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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

    private InputDevice leftDevice;
    private InputDevice rightDevice;

    private void OnEnable()
    {
        InputDevices.deviceConnected += OnDeviceConnected;
    }

    private void OnDisable()
    {
        InputDevices.deviceConnected -= OnDeviceConnected;
    }


    private void TryInitializeDevices()
    {
        List<InputDevice> devices = new List<InputDevice>();

        InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, devices);
        if (devices.Count > 0) leftDevice = devices[0];
        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, devices);
        if (devices.Count > 0)rightDevice = devices[0];
    }
    private void OnDeviceConnected(InputDevice device)
    {
        Debug.Log("Dispositivo conectado: " + device.name);
        TryInitializeDevices();
    }

    private void Start()
    {
        RenderSettings.fog = true;
        leftLastPos = leftController.position;
        rightLastPos = rightController.position;
    }

    private void FixedUpdate()
    {
        if (!leftDevice.isValid || !rightDevice.isValid)
        {
            TryInitializeDevices();
            return;
        }

        // Obtener el valor del agarre (entre 0 y 1) para cada dispositivo
        float leftGripValue = 0f;
        float rightGripValue = 0f;
        leftDevice.TryGetFeatureValue(CommonUsages.grip, out leftGripValue);
        rightDevice.TryGetFeatureValue(CommonUsages.grip, out rightGripValue);

        // Determinar si se considera que el agarre está presionado
        bool leftGripPressed = leftGripValue > gripThreshold;
        bool rightGripPressed = rightGripValue > gripThreshold;

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
[System.Serializable]
public class PrimaryButtonEvent : UnityEvent<bool> { }

public class PrimaryButtonWatcher : MonoBehaviour
{
    public PrimaryButtonEvent primaryButtonPress;

    private bool lastButtonState = false;
    private List<InputDevice> devicesWithPrimaryButton;

    private void Awake()
    {
        if (primaryButtonPress == null)
        {
            primaryButtonPress = new PrimaryButtonEvent();
        }

        devicesWithPrimaryButton = new List<InputDevice>();
    }

    void OnEnable()
    {
        List<InputDevice> allDevices = new List<InputDevice>();
        InputDevices.GetDevices(allDevices);
        foreach (InputDevice device in allDevices)
            InputDevices_deviceConnected(device);

        InputDevices.deviceConnected += InputDevices_deviceConnected;
        InputDevices.deviceDisconnected += InputDevices_deviceDisconnected;
    }

    private void OnDisable()
    {
        InputDevices.deviceConnected -= InputDevices_deviceConnected;
        InputDevices.deviceDisconnected -= InputDevices_deviceDisconnected;
        devicesWithPrimaryButton.Clear();
    }

    private void InputDevices_deviceConnected(InputDevice device)
    {
        bool discardedValue;
        if (device.TryGetFeatureValue(CommonUsages.primaryButton, out discardedValue))
        {
            devicesWithPrimaryButton.Add(device); // Add any devices that have a primary button.
        }
    }

    private void InputDevices_deviceDisconnected(InputDevice device)
    {
        if (devicesWithPrimaryButton.Contains(device))
            devicesWithPrimaryButton.Remove(device);
    }

    void Update()
    {
        bool tempState = false;
        foreach (var device in devicesWithPrimaryButton)
        {
            bool primaryButtonState = false;
            tempState = device.TryGetFeatureValue(CommonUsages.primaryButton, out primaryButtonState) // did get a value
                        && primaryButtonState // the value we got
                        || tempState; // cumulative result from other controllers
        }

        if (tempState != lastButtonState) // Button state changed since last frame
        {
            primaryButtonPress.Invoke(tempState);
            lastButtonState = tempState;
        }
    }
}



public class PrimaryReactor : MonoBehaviour
{
    public PrimaryButtonWatcher watcher;
    public bool IsPressed = false; // used to display button state in the Unity Inspector window
    public Vector3 rotationAngle = new Vector3(45, 45, 45);
    public float rotationDuration = 0.25f; // seconds
    private Quaternion offRotation;
    private Quaternion onRotation;
    private Coroutine rotator;

    void Start()
    {
        watcher.primaryButtonPress.AddListener(onPrimaryButtonEvent);
        offRotation = this.transform.rotation;
        onRotation = Quaternion.Euler(rotationAngle) * offRotation;
    }

    public void onPrimaryButtonEvent(bool pressed)
    {
        IsPressed = pressed;
        if (rotator != null)
            StopCoroutine(rotator);
        if (pressed)
            rotator = StartCoroutine(AnimateRotation(this.transform.rotation, onRotation));
        else
            rotator = StartCoroutine(AnimateRotation(this.transform.rotation, offRotation));
    }

    private IEnumerator AnimateRotation(Quaternion fromRotation, Quaternion toRotation)
    {
        float t = 0;
        while (t < rotationDuration)
        {
            transform.rotation = Quaternion.Lerp(fromRotation, toRotation, t / rotationDuration);
            t += Time.deltaTime;
            yield return null;
        }
    }
}