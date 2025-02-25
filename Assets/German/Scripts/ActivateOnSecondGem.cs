using UnityEngine;

public class ActivateOnSecondGem : MonoBehaviour
{
    public AudioSource backgroundAudio;
    public AudioSource activationAudio;
    public bool deactivateOnStart = true; // Editable in Unity Inspector

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
        if (deactivateOnStart)
        {
            gameObject.SetActive(false);
        }
    }

    private void ActivateObject()
    {
        Debug.Log("🔆 Second Gem inserted! Activating object: " + gameObject.name);
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
