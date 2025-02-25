using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource audioSource; // Assign AudioSource in Inspector
    public AudioClip trackStart; // Track for game start
    public AudioClip trackFirstGem; // Track for first gem
    public AudioClip trackSecondGem; // Track for second gem
    public AudioClip trackUnlock; // Track for cauldron unlock

    private void Start()
    {
        // Ensure AudioSource is set up
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        // Play the first track on game start
        PlayMusic(trackStart);
    }

    private void OnEnable()
    {
        CauldronUnlocker.OnFirstGemInserted += PlayFirstGemTrack;
        CauldronUnlocker.OnSecondGemInserted += PlaySecondGemTrack;
        CauldronUnlocker.OnCauldronUnlocked += PlayUnlockTrack;
    }

    private void OnDisable()
    {
        CauldronUnlocker.OnFirstGemInserted -= PlayFirstGemTrack;
        CauldronUnlocker.OnSecondGemInserted -= PlaySecondGemTrack;
        CauldronUnlocker.OnCauldronUnlocked -= PlayUnlockTrack;
    }

    private void PlayFirstGemTrack()
    {
        PlayMusic(trackFirstGem);
    }

    private void PlaySecondGemTrack()
    {
        PlayMusic(trackSecondGem);
    }

    private void PlayUnlockTrack()
    {
        PlayMusic(trackUnlock);
    }

    private void PlayMusic(AudioClip clip)
    {
        if (audioSource.clip != clip)
        {
            audioSource.Stop();
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}
