using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System;

public class CauldronUnlocker : MonoBehaviour
{
    public XRSocketInteractor socket1;
    public XRSocketInteractor socket2;
    public XRSocketInteractor socket3;

    public GameObject unlockEffect; // Assign particle effect for final unlock

    private bool socket1Filled = false;
    private bool socket2Filled = false;
    private bool socket3Filled = false;

    public static event Action OnFirstGemInserted;
    public static event Action OnSecondGemInserted;
    public static event Action OnCauldronUnlocked;

    private void Start()
    {
        if (unlockEffect != null)
        {
            unlockEffect.SetActive(false);
        }

        socket1.selectEntered.AddListener(OnSocket1Filled);
        socket2.selectEntered.AddListener(OnSocket2Filled);
        socket3.selectEntered.AddListener(OnSocket3Filled);

        socket1.selectExited.AddListener(OnSocket1Emptied);
        socket2.selectExited.AddListener(OnSocket2Emptied);
        socket3.selectExited.AddListener(OnSocket3Emptied);
    }

    private void OnSocket1Filled(SelectEnterEventArgs args)
    {
        if (!socket1Filled)
        {
            socket1Filled = true;
            Debug.Log("🔹 First gem inserted!");
            OnFirstGemInserted?.Invoke();
        }
        CheckUnlockCondition();
    }

    private void OnSocket2Filled(SelectEnterEventArgs args)
    {
        if (socket1Filled && !socket2Filled) // Only trigger if first gem is in
        {
            socket2Filled = true;
            Debug.Log("🔸 Second gem inserted!");
            OnSecondGemInserted?.Invoke();
        }
        CheckUnlockCondition();
    }

    private void OnSocket3Filled(SelectEnterEventArgs args)
    {
        socket3Filled = true;
        CheckUnlockCondition();
    }

    private void OnSocket1Emptied(SelectExitEventArgs args)
    {
        socket1Filled = false;
    }

    private void OnSocket2Emptied(SelectExitEventArgs args)
    {
        socket2Filled = false;
    }

    private void OnSocket3Emptied(SelectExitEventArgs args)
    {
        socket3Filled = false;
    }

    private void CheckUnlockCondition()
    {
        if (socket1Filled && socket2Filled && socket3Filled)
        {
            Debug.Log("🔓 Cauldron Unlocked! Emitting event...");
            OnCauldronUnlocked?.Invoke();

            if (unlockEffect != null)
            {
                unlockEffect.SetActive(true);
            }
        }
    }

    private void OnDestroy()
    {
        socket1.selectEntered.RemoveListener(OnSocket1Filled);
        socket2.selectEntered.RemoveListener(OnSocket2Filled);
        socket3.selectEntered.RemoveListener(OnSocket3Filled);

        socket1.selectExited.RemoveListener(OnSocket1Emptied);
        socket2.selectExited.RemoveListener(OnSocket2Emptied);
        socket3.selectExited.RemoveListener(OnSocket3Emptied);
    }
}
