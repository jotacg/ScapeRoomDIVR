using UnityEngine;

public class ActivateOnSecondGem : MonoBehaviour
{
    private AudioSource audioSource;

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
        audioSource = GetComponent<AudioSource>();
        gameObject.SetActive(false); // Start disabled
    }

    private void ActivateObject()
    {
        Debug.Log("🔆 Second Gem inserted! Activating object: " + gameObject.name);
        gameObject.SetActive(true);

        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}
