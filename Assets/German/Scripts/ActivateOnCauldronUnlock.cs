using UnityEngine;

public class ActivateOnCauldronUnlock : MonoBehaviour
{
    public AudioSource backgroundAudio;
    public AudioSource activationAudio;
    public bool deactivateOnStart = true; // Editable in Unity Inspector

    private void OnEnable()
    {
        CauldronUnlocker.OnCauldronUnlocked += ActivateObject;
    }

    private void OnDisable()
    {
        CauldronUnlocker.OnCauldronUnlocked -= ActivateObject;
    }

    private void Start()
    {
        if (deactivateOnStart)
        {
            gameObject.SetActive(false);
        }
    }

    private void ActivateObject()
    {
        Debug.Log("🔓 Cauldron unlocked! Activating object: " + gameObject.name);
        gameObject.SetActive(true);

        if (backgroundAudio != null)
        {
            backgroundAudio.Stop();
        }

        if (activationAudio != null)
        {
            activationAudio.Play();
        }
    }
}
