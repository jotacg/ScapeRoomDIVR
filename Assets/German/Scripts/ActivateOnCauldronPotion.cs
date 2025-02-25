using UnityEngine;

public class ActivateOnCauldronPotion : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource backgroundAudio;   // Optional background loop (stop on activate)
    public AudioSource activationAudio;   // Optional one-shot activation sound

    [Header("Visual Settings")]
    [Tooltip("If true, this script will hide all renderers at Start, but keep the object active.")]
    public bool hideRenderersOnStart = true; 

    private Renderer[] renderers;

    private void OnEnable()
    {
        // Subscribe to the puzzle-complete event
        CauldronPuzzleManager.OnCauldronPotionComplete += ActivateObject;
    }

    private void OnDisable()
    {
        // Unsubscribe to avoid memory leaks
        CauldronPuzzleManager.OnCauldronPotionComplete -= ActivateObject;
    }

    private void Start()
    {
        // Cache all Renderers (including children)
        renderers = GetComponentsInChildren<Renderer>();

        // Hide them if needed
        if (hideRenderersOnStart)
        {
            foreach (Renderer r in renderers)
            {
                r.enabled = false;
            }
        }
    }

    private void ActivateObject()
    {
        Debug.Log("🍵 Cauldron potion puzzle complete! Activating object: " + gameObject.name);

        // Re-enable all renderers so the object becomes visible
        if (renderers != null)
        {
            foreach (Renderer r in renderers)
            {
                r.enabled = true;
            }
        }

        // Stop background audio if present
        if (backgroundAudio != null)
        {
            backgroundAudio.Stop();
        }

        // Play activation audio if present
        if (activationAudio != null)
        {
            activationAudio.Play();
        }
    }
}
