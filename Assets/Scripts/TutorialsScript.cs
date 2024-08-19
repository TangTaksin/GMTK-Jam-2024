using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialsScript : MonoBehaviour
{
    [SerializeField] private GameObject textObject;
    [SerializeField] private Color gizmoColor = Color.green; // Color of the gizmo in the Editor
    [SerializeField] private SpriteRenderer spriteRenderer;

    void Start()
    {
        textObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            textObject.SetActive(true);

            // Check if spriteRenderer is assigned before using it
            if (spriteRenderer != null)
            {
                spriteRenderer.enabled = false;
            }
            else
            {
                Debug.LogWarning("spriteRenderer is not assigned, ignoring the operation.");
            }
        }
    }

    // Draws gizmos in the scene view
    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;

        // Get the collider attached to this GameObject
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        if (collider != null)
        {
            // Draw a wireframe box representing the collider
            Gizmos.DrawWireCube(collider.bounds.center, collider.bounds.size);
        }
    }
}
