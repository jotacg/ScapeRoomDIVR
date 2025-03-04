using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRInteractablePlayAudio : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource; // Drag the audio source here in the Inspector

    // Method to play audio
    public void onHoverEnter()
    {

        // Play audio
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
            Debug.Log("Audio is now playing.");
        }
        else
        {
            Debug.LogWarning("No AudioSource assigned or audio was playing already.");
        }
    }
}
