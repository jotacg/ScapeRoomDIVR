using UnityEngine;

public class ActivateOnFirstGem : MonoBehaviour
{
    public AudioSource backgroundAudio; // Assign background loop (stops when activated)
    public AudioSource activationAudio; // Assign activation sound (plays when activated)
    public bool deactivateOnStart = true; // Editable in Unity Inspector

    private void OnEnable()
    {
        CauldronUnlocker.OnFirstGemInserted += ActivateObject;
    }

    private void OnDisable()
    {
        CauldronUnlocker.OnFirstGemInserted -= ActivateObject;
    }

    private void Start()
    {
        if (deactivateOnStart)
        {
            gameObject.SetActive(false); // Start disabled if set in editor
        }
    }

    private void ActivateObject()
    {
        Debug.Log("✨ First Gem inserted! Activating object: " + gameObject.name);
        gameObject.SetActive(true);

        if (backgroundAudio != null)
        {
            backgroundAudio.Stop(); // Stop background audio permanently
        }

        if (activationAudio != null)
        {
            activationAudio.Play(); // Play activation sound effect
        }
    }
}
