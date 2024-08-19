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
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.buildIndex + 1);
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
}
