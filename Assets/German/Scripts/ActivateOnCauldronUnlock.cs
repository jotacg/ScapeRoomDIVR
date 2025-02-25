using UnityEngine;

public class ActivateOnCauldronUnlock : MonoBehaviour
{
    private AudioSource audioSource;

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
        audioSource = GetComponent<AudioSource>();
        gameObject.SetActive(false); // Start disabled
    }

    private void ActivateObject()
    {
        Debug.Log("🔓 Cauldron unlocked! Activating object: " + gameObject.name);
        gameObject.SetActive(true);

        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}
