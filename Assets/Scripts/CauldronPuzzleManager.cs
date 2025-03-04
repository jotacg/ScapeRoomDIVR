using UnityEngine;
using System;
using System.Collections.Generic;

public class CauldronPuzzleManager : MonoBehaviour
{
    [Tooltip("Puzzle items in the correct sequence (0-based).")]
    public List<PuzzleItem> puzzleOrder;

    private int nextRequiredIndex = 0;
    public static event Action OnCauldronPotionComplete;

    [Header("Audio")]
    public AudioClip ingestionAudio; // For correct item
    public AudioClip spittedAudio;   // For wrong / leftover item
    private AudioSource audioSource;

    [Header("Spit-Out Settings")]
    [Tooltip("Force applied to fling item out of the cauldron.")]
    public float spitForce = 5f;
    [Tooltip("Torque (rotational force) for the spit out.")]
    public float spitTorque = 3f;
    [Tooltip("Minimum time (seconds) between repeated spit-outs of the same object.")]
    public float spitCooldown = 0.5f; 

    private bool puzzleComplete = false;

    // Tracks when we're allowed to spit a given collider again
    private Dictionary<Collider, float> nextSpitTime = new Dictionary<Collider, float>();

    private void Start()
    {
        if (puzzleOrder == null || puzzleOrder.Count == 0)
        {
            Debug.LogError("puzzleOrder is empty! Puzzle might complete on first correct item or do nothing.");
        }
        else
        {
            Debug.Log($"[CauldronPuzzleManager] puzzleOrder has {puzzleOrder.Count} items.");
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Immediately handle new arrivals (same logic as before).
        HandleObjectInCauldron(other);
    }

    private void OnTriggerStay(Collider other)
    {
        // Some objects might be placed directly in the collider center
        // or remain after bouncing around. We periodically push them out.
        HandleObjectInCauldron(other);
    }

    private void HandleObjectInCauldron(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        PuzzleItem thrownItem = other.GetComponent<PuzzleItem>();

        // If we've already completed the puzzle, we still spit out anything new or leftover
        // but skip the spit sound.
        if (puzzleComplete)
        {
            // If puzzle is done, forcibly spit everything except the items we used/destroyed
            SpitOutItem(rb, playSound: false);
            return;
        }

        // Non-puzzle item => spit out
        if (thrownItem == null)
        {
            Debug.Log($"Spitting out NON-puzzle item: {other.name}");
            SpitOutItem(rb, playSound: true);
            return;
        }

        // The puzzle item we expect next:
        if (nextRequiredIndex < puzzleOrder.Count)
        {
            PuzzleItem requiredItem = puzzleOrder[nextRequiredIndex];
            bool isCorrect = (thrownItem.itemName == requiredItem.itemName);

            if (isCorrect)
            {
                Debug.Log($"✅ Correct item! (Needed '{requiredItem.itemName}', got '{thrownItem.itemName}')");

                if (ingestionAudio != null)
                    audioSource.PlayOneShot(ingestionAudio);

                Destroy(thrownItem.gameObject);
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
                Debug.Log($"❌ Wrong item! Expected '{requiredItem.itemName}', got '{thrownItem.itemName}'");
                SpitOutItem(rb, playSound: true);
            }
        }
        else
        {
            // If nextRequiredIndex >= puzzleOrder.Count but puzzleComplete not set? 
            // Just spit it out
            SpitOutItem(rb, playSound: false);
        }
    }

    private void SpitOutItem(Rigidbody rb, bool playSound)
    {
        if (rb == null) return;

        // Check cooldown
        Collider col = rb.GetComponent<Collider>();
        if (col == null) return;

        float now = Time.time;
        if (!nextSpitTime.ContainsKey(col)) nextSpitTime[col] = 0f;

        if (now < nextSpitTime[col])
        {
            // Not time to spit again yet, do nothing
            return;
        }

        // Update next allowed spit time
        nextSpitTime[col] = now + spitCooldown;

        // Play spit sound if puzzle not complete & allowed
        if (!puzzleComplete && playSound && spittedAudio != null)
        {
            audioSource.PlayOneShot(spittedAudio);
        }

        // Random direction biased upwards
        Vector3 dir = UnityEngine.Random.insideUnitSphere;
        if (dir.y < 0f) dir.y = -dir.y;
        dir.Normalize();

        rb.AddForce(dir * spitForce, ForceMode.Impulse);
        rb.AddTorque(UnityEngine.Random.insideUnitSphere * spitTorque, ForceMode.Impulse);
    }
}
