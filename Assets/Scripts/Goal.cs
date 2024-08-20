using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Fader.OnFadeIn.Invoke();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        // Get the collider attached to this GameObject
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        if (collider != null)
        {
            // Draw a wireframe box representing the collider
            Gizmos.DrawWireCube(collider.bounds.center, collider.bounds.size);
        }
    }

    public void LoadNextScene(String sceneName)
    {
        if (sceneName == "Fade_In")
        {
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.buildIndex + 1);
        }
    }

    private void OnEnable()
    {
        Fader.FadeFinished += LoadNextScene;
    }

    private void OnDisable()
    {
        Fader.FadeFinished -= LoadNextScene;

    }
}
