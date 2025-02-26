using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageSceneAimar : MonoBehaviour
{
    [SerializeField] string newScene;
    [SerializeField] GameObject activateObject;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == activateObject)
        {
            SceneManager.LoadScene(newScene);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == activateObject)
        {
            SceneManager.LoadScene(newScene);
        }
    }
}
