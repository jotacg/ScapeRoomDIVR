using UnityEngine;

public class ActivateOnSecondGem : MonoBehaviour
{
    public AudioSource backgroundAudio;  // Assign background loop (stops when activated)
    public AudioSource activationAudio;  // Assign activation sound (plays when activated)
    public bool deactivateOnStart = true;  // Editable in Unity Inspector

    private Renderer[] renderers;  // We'll store all renderers to enable/disable them

    private void OnEnable()
    {
        CauldronUnlocker.OnSecondGemInserted += ActivateObject;
    }

    private void OnDisable()
    {
        CauldronUnlocker.OnSecondGemInserted -= ActivateObject;
    }

    private void Start()
    {
        // Gather all Renderer components on this object (and children)
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
        Debug.Log("🔆 Second Gem inserted! Activating object: " + gameObject.name);

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
