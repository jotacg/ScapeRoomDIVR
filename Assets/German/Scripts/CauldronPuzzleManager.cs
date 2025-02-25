using UnityEngine;
using System.Collections.Generic;
using System;

public class CauldronPuzzleManager : MonoBehaviour
{
    // Puzzle items in the exact required sequence (0-based).
    // e.g., puzzleOrder.Count = 3 for: [0=Red, 1=Green, 2=Book].
    [Tooltip("List puzzle items in the correct sequence.")]
    public List<PuzzleItem> puzzleOrder;

    // Which item we expect next
    private int currentSequenceIndex = 0;

    // Event: Fired when all items are thrown in the correct order
    public static event Action OnCauldronPotionComplete;

    [Header("Audio")]
    [Tooltip("Assign a WAV or audio file for the correct item 'ingestion' sound.")]
    public AudioClip ingestionAudio;    // played when correct item is thrown
    [Tooltip("Assign a WAV or audio file for the wrong/spitted item sound.")]
    public AudioClip spittedAudio;      // played when item is spat out

    private AudioSource audioSource;

    [Header("Spit-Out Settings (Wrong Order)")]
    [Tooltip("Force applied to wrong item to fling it out of the cauldron.")]
    public float spitForce = 5f;

    [Tooltip("Torque (rotational force) applied to wrong item.")]
    public float spitTorque = 3f;

    private void Start()
    {
        // Debug how many items are in puzzleOrder
        if (puzzleOrder == null)
        {
            Debug.LogError("No puzzleOrder assigned! The puzzle won't work properly.");
        }
        else
        {
            Debug.Log($"[CauldronPuzzleManager] puzzleOrder has {puzzleOrder.Count} item(s).");
        }

        // Ensure there's an AudioSource on this GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // If there's no AudioSource, create one
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the thrown object has a PuzzleItem script
        PuzzleItem item = other.GetComponent<PuzzleItem>();
        if (item == null)
        {
            // It's an alien / non-puzzle object => spit it out anyway
            Debug.Log($"[CauldronPuzzleManager] Spitting out NON-puzzle item: {other.name}");
            SpitOutItem(other.GetComponent<Rigidbody>());
            return;
        }

        // If it's a puzzle item, check sequence
        if (item.sequenceIndex == currentSequenceIndex)
        {
            // ✅ Correct item
            Debug.Log($"✅ Correct item in sequence! (Index {currentSequenceIndex})");

            // Play ingestion sound if assigned
            if (ingestionAudio != null)
            {
                audioSource.PlayOneShot(ingestionAudio);
            }

            // Destroy the puzzle item (it disappears in the cauldron)
            Destroy(other.gameObject);

            // Move to the next required item
            currentSequenceIndex++;

            // If we've hit the end of the puzzle order, puzzle is complete
            if (puzzleOrder != null && currentSequenceIndex >= puzzleOrder.Count)
            {
                Debug.Log("✨ All items in correct order! Puzzle complete.");
                OnCauldronPotionComplete?.Invoke(); // Fire event
            }
        }
        else
        {
            // ❌ Wrong item order
            Debug.Log($"❌ Wrong item order! Spitting the item out... (Expected sequenceIndex={currentSequenceIndex}, got {item.sequenceIndex})");
            SpitOutItem(other.GetComponent<Rigidbody>());
        }
    }

    private void SpitOutItem(Rigidbody rb)
    {
        if (rb == null) return;

        // Pick a random direction (biased upwards)
        Vector3 randomDir = UnityEngine.Random.insideUnitSphere;
        if (randomDir.y < 0f)
        {
            randomDir.y = -randomDir.y; // ensure some upward force
        }
        randomDir.Normalize();

        // Play spitted sound if assigned
        if (spittedAudio != null)
        {
            audioSource.PlayOneShot(spittedAudio);
        }

        // Apply force & torque
        rb.AddForce(randomDir * spitForce, ForceMode.Impulse);
        rb.AddTorque(UnityEngine.Random.insideUnitSphere * spitTorque, ForceMode.Impulse);
    }
}
