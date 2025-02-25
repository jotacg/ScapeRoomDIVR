using UnityEngine;
using System.Collections.Generic;
using System;

public class CauldronPuzzleManager : MonoBehaviour
{
    [Tooltip("List puzzle items in the correct sequence (0-based).")]
    public List<PuzzleItem> puzzleOrder;

    private int currentSequenceIndex = 0;

    public static event Action OnCauldronPotionComplete;

    [Header("Audio")]
    [Tooltip("Played when correct item is thrown.")]
    public AudioClip ingestionAudio;
    [Tooltip("Played when item is spat out (wrong order / non-puzzle). Disabled when puzzleComplete=true.")]
    public AudioClip spittedAudio;

    private AudioSource audioSource;

    [Header("Spit-Out Settings (Wrong Order)")]
    [Tooltip("Force applied to fling item out of the cauldron.")]
    public float spitForce = 5f;
    [Tooltip("Torque (rotational force) for the spit out.")]
    public float spitTorque = 3f;

    // Whether the puzzle is finished
    private bool puzzleComplete = false;

    private void Start()
    {
        if (puzzleOrder == null)
        {
            Debug.LogError("No puzzleOrder assigned! Puzzle won't work correctly.");
        }
        else
        {
            Debug.Log($"[CauldronPuzzleManager] puzzleOrder has {puzzleOrder.Count} items.");
        }

        // Ensure there's an AudioSource on this GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if we have a PuzzleItem
        PuzzleItem item = other.GetComponent<PuzzleItem>();
        Rigidbody rb = other.GetComponent<Rigidbody>();

        if (item == null)
        {
            // Non-puzzle object => spit it out (force only, no ingestion)
            Debug.Log($"Spitting out NON-puzzle item: {other.name}");
            SpitOutItem(rb);
            return;
        }

        // If it's a puzzle item, check if puzzle is done or not
        if (!puzzleComplete && item.sequenceIndex == currentSequenceIndex)
        {
            // Correct item
            Debug.Log($"✅ Correct item in sequence! (Index {currentSequenceIndex})");
            if (ingestionAudio != null)
            {
                audioSource.PlayOneShot(ingestionAudio);
            }
            Destroy(other.gameObject);
            currentSequenceIndex++;

            // Check if puzzle is now complete
            if (puzzleOrder != null && currentSequenceIndex >= puzzleOrder.Count)
            {
                puzzleComplete = true;
                Debug.Log("✨ All items in correct order! Puzzle complete.");
                OnCauldronPotionComplete?.Invoke();
            }
        }
        else
        {
            // Wrong item OR puzzleComplete. Spit it out.
            Debug.Log($"❌ Spitting item out... [puzzleComplete={puzzleComplete}] (Got sequenceIndex={item.sequenceIndex}, expected={currentSequenceIndex})");
            SpitOutItem(rb);
        }
    }

    private void SpitOutItem(Rigidbody rb)
    {
        if (rb == null) return;

        // Only play spit sound if puzzle not complete
        if (!puzzleComplete && spittedAudio != null)
        {
            audioSource.PlayOneShot(spittedAudio);
        }

        // Random direction biased upwards
        Vector3 randomDir = UnityEngine.Random.insideUnitSphere;
        if (randomDir.y < 0f)
        {
            randomDir.y = -randomDir.y;
        }
        randomDir.Normalize();

        rb.AddForce(randomDir * spitForce, ForceMode.Impulse);
        rb.AddTorque(UnityEngine.Random.insideUnitSphere * spitTorque, ForceMode.Impulse);
    }
}
