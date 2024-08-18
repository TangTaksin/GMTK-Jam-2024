using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubBody : MonoBehaviour
{
    public Transform anchor;
    public Vector3 offsetFromAnchor;

    private void OnEnable()
    {

    }

    private void Update()
    {
        if (anchor)
        {
            var rotatedVector = anchor.rotation * offsetFromAnchor;
            transform.position = anchor.position + rotatedVector;
        }
            
    }

    public void MoveTarget(Vector3 amount)
    {
        if (anchor)
            anchor.position += amount;
    }

    public void Resize(Side _currentSide, Vector2 _scaleAxis)
    {
        if (_scaleAxis.SqrMagnitude() == 0)
            return;

        switch (_currentSide)
        {
            case Side.Top:
                transform.localScale += Vector3.up * _scaleAxis.y * Time.deltaTime;
                offsetFromAnchor += Vector3.up * (_scaleAxis.y / 2) * Time.deltaTime;
                break;

            case Side.Bottom:
                transform.localScale -= Vector3.up * _scaleAxis.y * Time.deltaTime;
                offsetFromAnchor += Vector3.up * (_scaleAxis.y / 2) * Time.deltaTime;
                break;

            case Side.Left:
                transform.localScale -= Vector3.right * _scaleAxis.x * Time.deltaTime;
                offsetFromAnchor += Vector3.right * (_scaleAxis.x / 2) * Time.deltaTime;
                break;

            case Side.Right:
                transform.localScale += Vector3.right * _scaleAxis.x * Time.deltaTime;
                offsetFromAnchor += Vector3.right * (_scaleAxis.x / 2) * Time.deltaTime;
                break;
        }
    }

    private void OnDrawGizmos()
    {
        if (!anchor)
            return;

        var rotatedVector = anchor.rotation * offsetFromAnchor;
        Gizmos.DrawCube(anchor.position + rotatedVector, Vector3.one * 0.1f);
    }
}
