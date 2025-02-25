using UnityEngine;
using System;
using System.Collections.Generic;

public class CauldronPuzzleManager : MonoBehaviour
{
    // List of PuzzleItem references in the exact *required* order
    // In the Inspector, assign each slot with the *prefab or scene reference* 
    // that you expect the player to throw, in sequence.
    [Tooltip("Puzzle items in the correct sequence. Each entry = 1 item the player must throw.")]
    public List<PuzzleItem> puzzleOrder;

    // Index of the NEXT required item in puzzleOrder
    private int nextRequiredIndex = 0;

    // Event: fired when puzzle completes (all required items are thrown)
    public static event Action OnCauldronPotionComplete;

    [Header("Audio")]
    public AudioClip ingestionAudio; // For correct item
    public AudioClip spittedAudio;   // For wrong item (disabled if puzzleComplete=true)
    private AudioSource audioSource;

    [Header("Spit-Out Settings (Wrong Order)")]
    public float spitForce = 5f;
    public float spitTorque = 3f;

    private bool puzzleComplete = false;

    private void Start()
    {
        if (puzzleOrder == null || puzzleOrder.Count == 0)
        {
            Debug.LogError("puzzleOrder is empty! Puzzle will complete on first correct item (or do nothing).");
        }
        else
        {
            Debug.Log($"[CauldronPuzzleManager] puzzleOrder has {puzzleOrder.Count} items.");
        }

        // Ensure an AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // If puzzle is done, still spit out new items but no spit sound
        if (puzzleComplete)
        {
            SpitOutItem(other, playSound: false);
            return;
        }

        // Check for PuzzleItem
        PuzzleItem thrownItem = other.GetComponent<PuzzleItem>();
        Rigidbody rb = other.GetComponent<Rigidbody>();

        if (thrownItem == null)
        {
            // Non-puzzle => spit out
            Debug.Log($"Spitting out NON-puzzle item: {other.name}");
            SpitOutItem(other, playSound: true);
            return;
        }

        // Now we do direct matching:
        // 1) The next required puzzle item in the list
        // 2) Compare names (or references)

        if (nextRequiredIndex < puzzleOrder.Count)
        {
            // The PuzzleItem we *expect* next:
            PuzzleItem requiredItem = puzzleOrder[nextRequiredIndex];

            // Compare itemName (or check if thrownItem == requiredItem if they are scene references)
            bool isCorrect = (thrownItem.itemName == requiredItem.itemName);

            if (isCorrect)
            {
                Debug.Log($"✅ Correct item! (needed '{requiredItem.itemName}', got '{thrownItem.itemName}')");

                // Play ingestion sound
                if (ingestionAudio != null)
                {
                    audioSource.PlayOneShot(ingestionAudio);
                }

                // Destroy immediately (prevents double triggers from bounces)
                Destroy(thrownItem.gameObject);

                // Move to next required item
                nextRequiredIndex++;

                // Check puzzle complete
                if (nextRequiredIndex >= puzzleOrder.Count)
                {
                    puzzleComplete = true;
                    Debug.Log("✨ Puzzle complete! All items thrown in correct order.");
                    OnCauldronPotionComplete?.Invoke();
                }
            }
            else
            {
                // Wrong item => spit out
                Debug.Log($"❌ Wrong item! Expected '{requiredItem.itemName}', got '{thrownItem.itemName}'");
                SpitOutItem(other, playSound: true);
            }
        }
        else
        {
            // Safety: If nextRequiredIndex >= puzzleOrder.Count, puzzle is done 
            // but we haven't set puzzleComplete for some reason?
            Debug.Log("We have no more required items, but puzzleComplete=false? Spitting item out.");
            SpitOutItem(other, playSound: false);
        }
    }

    private void SpitOutItem(Collider col, bool playSound)
    {
        Rigidbody rb = col.GetComponent<Rigidbody>();
        if (rb != null)
        {
            if (!puzzleComplete && playSound && spittedAudio != null)
            {
                audioSource.PlayOneShot(spittedAudio);
            }

            // random upward direction
            Vector3 dir = UnityEngine.Random.insideUnitSphere;
            if (dir.y < 0f) dir.y = -dir.y;
            dir.Normalize();

            rb.AddForce(dir * spitForce, ForceMode.Impulse);
            rb.AddTorque(UnityEngine.Random.insideUnitSphere * spitTorque, ForceMode.Impulse);
        }
    }
}
