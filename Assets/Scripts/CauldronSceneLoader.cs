using UnityEngine;
using UnityEngine.SceneManagement;

public class CauldronSceneLoader : MonoBehaviour
{
    [SerializeField] private string nextSceneName = "Jorge/Scenes/DungeonJorge";
    private bool cauldronUnlocked = false;

    private void OnEnable()
    {
        CauldronUnlocker.OnCauldronUnlocked += UnlockCauldron;
    }

    private void OnDisable()
    {
        CauldronUnlocker.OnCauldronUnlocked -= UnlockCauldron;
    }

    private void UnlockCauldron()
    {
        cauldronUnlocked = true;
        Debug.Log("🔓 Cauldron is now interactable! Touch it to proceed.");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (cauldronUnlocked && other.CompareTag("Player"))
        {
            Debug.Log($"✨ Player touched the cauldron! Loading next scene: {nextSceneName}");

            // Ensure the scene exists before loading
            if (Application.CanStreamedLevelBeLoaded(nextSceneName))
            {
                SceneManager.LoadScene(nextSceneName);
            }
            else
            {
                Debug.LogError($"🚨 Scene '{nextSceneName}' not found! Check the path and make sure it's added to Build Settings.");
            }
        }
    }
}
