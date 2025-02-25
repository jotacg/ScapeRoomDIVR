using UnityEngine;

public class ActivateOnFirstGem : MonoBehaviour
{
    public AudioSource backgroundAudio;  // Assign background loop (stops when activated)
    public AudioSource activationAudio;  // Assign activation sound (plays when activated)
    public bool deactivateOnStart = true;  // Editable in Unity Inspector

    private Renderer[] renderers;  // We'll store all renderers so we can enable/disable them

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
        // Gather all Renderer components on this object and its children
        renderers = GetComponentsInChildren<Renderer>();

        // If the user wants to "disable" the object at start, just hide its renderers
        if (deactivateOnStart)
        {
            foreach (Renderer r in renderers)
            {
                r.enabled = false;
            }
        }
    }

    private void ActivateObject()
    {
        Debug.Log("✨ First Gem inserted! Activating object: " + gameObject.name);

        // Re-enable the renderers, so the object becomes visible again
        foreach (Renderer r in renderers)
        {
            r.enabled = true;
        }

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
