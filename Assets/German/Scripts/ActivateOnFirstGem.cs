using UnityEngine;

public class ActivateOnFirstGem : MonoBehaviour
{
    private AudioSource audioSource;

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
        audioSource = GetComponent<AudioSource>();
        gameObject.SetActive(false); // Start disabled
    }

    private void ActivateObject()
    {
        Debug.Log("✨ First Gem inserted! Activating object: " + gameObject.name);
        gameObject.SetActive(true);

        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}
