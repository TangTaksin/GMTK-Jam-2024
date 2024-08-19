using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubBody : MonoBehaviour
{
    public Transform anchor;
    public Vector3 offsetFromAnchor;

    [Header("Scale Settings")]
    private Vector3 minScale = new Vector3(0.1f, 0.1f, 0.1f);
    private Vector3 maxScale = new Vector3(5f, 5f, 5f);
    public float scaleSpeed = 5f; // Speed of the scaling

    private Vector3 _defaultScale;
    private Vector3 _defaultOffset;
    private Vector3 targetScale;

    private void OnEnable()
    {
        _defaultScale = transform.localScale;
        _defaultOffset = offsetFromAnchor;
        targetScale = transform.localScale; // Initialize targetScale with the current scale
    }

    private void Update()
    {
        if (anchor)
        {
            var rotatedVector = anchor.rotation * offsetFromAnchor;
            transform.position = anchor.position + rotatedVector;
        }

        // Smoothly scale the object to the target scale
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, scaleSpeed * Time.deltaTime);
    }

    public void Resize(Side _currentSide, float _scaleAxis)
    {
        if (_scaleAxis == 0)
            return;

        var smallmovement = Vector3.zero;
        Vector3 newScale = targetScale;

        switch (_currentSide)
        {
            case Side.Top:
                newScale += Vector3.up * _scaleAxis * Time.deltaTime;
                smallmovement = Vector3.up * (_scaleAxis / 2) * Time.deltaTime;
                break;

            case Side.Bottom:
                newScale -= Vector3.down * _scaleAxis * Time.deltaTime;
                smallmovement = Vector3.down * (_scaleAxis / 2) * Time.deltaTime;
                break;

            case Side.Left:
                newScale -= Vector3.left * _scaleAxis * Time.deltaTime;
                smallmovement = Vector3.left * (_scaleAxis / 2) * Time.deltaTime;
                break;

            case Side.Right:
                newScale += Vector3.right * _scaleAxis * Time.deltaTime;
                smallmovement = Vector3.right * (_scaleAxis / 2) * Time.deltaTime;
                break;
        }

        // Clamp the target scale between minScale and maxScale
        newScale.x = Mathf.Clamp(newScale.x, minScale.x, maxScale.x);
        newScale.y = Mathf.Clamp(newScale.y, minScale.y, maxScale.y);
        newScale.z = Mathf.Clamp(newScale.z, minScale.z, maxScale.z);

        targetScale = newScale; // Set the target scale

        // Adjust the offsetFromAnchor based on smallmovement
        offsetFromAnchor += smallmovement;

        // Clamp the position to ensure it stays within bounds
        if (anchor)
        {
            Vector3 clampedOffset = offsetFromAnchor;
            // Adjust the offset based on the new scale and its limits
            clampedOffset.x = Mathf.Clamp(clampedOffset.x, -targetScale.x / 2f, targetScale.x / 2f);
            clampedOffset.y = Mathf.Clamp(clampedOffset.y, -targetScale.y / 2f, targetScale.y / 2f);
            clampedOffset.z = Mathf.Clamp(clampedOffset.z, -targetScale.z / 2f, targetScale.z / 2f);

            offsetFromAnchor = clampedOffset;
            transform.position = anchor.position + (anchor.rotation * offsetFromAnchor);
        }
        else
        {
            transform.position += smallmovement;
        }
    }

    public void ResetScale()
    {
        targetScale = _defaultScale;
        offsetFromAnchor = _defaultOffset;
    }

    private void OnDrawGizmos()
    {
        if (!anchor)
            return;

        var rotatedVector = anchor.rotation * offsetFromAnchor;
        Gizmos.DrawCube(anchor.position + rotatedVector, Vector3.one * 0.1f);
    }

    public void SetColor(Color color)
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = color;
        }
    }
}
