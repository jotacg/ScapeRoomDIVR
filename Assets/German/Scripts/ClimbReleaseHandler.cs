using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ClimbReleaseHandler : MonoBehaviour
{
    private CharacterController characterController;

    void Start()
    {
        // Find the CharacterController on the XR Rig
        characterController = FindObjectOfType<CharacterController>();

        if (characterController == null)
        {
            Debug.LogError("No CharacterController found in the scene. Make sure the XR Rig has one!");
        }
    }

    public void OnClimbReleased(SelectExitEventArgs args)
    {
        if (characterController != null)
        {
            Vector3 nudge = new Vector3(0.01f, -0.01f, 0); // Tiny movement to wake physics
            characterController.Move(nudge);
        }
    }
}
