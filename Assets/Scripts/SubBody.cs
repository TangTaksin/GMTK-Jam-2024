using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubBody : MonoBehaviour
{
    public Transform anchor;
    public Vector3 offsetFromAnchor;

    Vector3 _defaultScale;
    Vector3 _defaultOffset;

    private void OnEnable()
    {
        _defaultScale = transform.localScale;
        _defaultOffset = offsetFromAnchor;
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

    public void Resize(Side _currentSide, float _scaleAxis)
    {
        if (_scaleAxis == 0)
            return;

        var smallmovement = Vector3.zero;

        switch (_currentSide)
        {
            case Side.Top:
                transform.localScale += Vector3.up * _scaleAxis * Time.deltaTime;
                smallmovement = Vector3.up * (_scaleAxis / 2) * Time.deltaTime;
                offsetFromAnchor += smallmovement;
                break;

            case Side.Bottom:
                transform.localScale -= Vector3.down * _scaleAxis * Time.deltaTime;
                smallmovement = Vector3.down * (_scaleAxis / 2) * Time.deltaTime;
                offsetFromAnchor += smallmovement;
                break;

            case Side.Left:
                transform.localScale -= Vector3.left * _scaleAxis * Time.deltaTime;
                smallmovement = Vector3.left * (_scaleAxis / 2) * Time.deltaTime;
                offsetFromAnchor += smallmovement;
                break;

            case Side.Right:
                transform.localScale += Vector3.right * _scaleAxis * Time.deltaTime;
                smallmovement = Vector3.right * (_scaleAxis / 2) * Time.deltaTime;
                offsetFromAnchor += smallmovement;
                break;
        }

        if (!anchor)
        {
            transform.position += smallmovement;
        }
    }

    public void ResetScale()
    {
        transform.localScale = _defaultScale;
        offsetFromAnchor = _defaultOffset;
    }

    private void OnDrawGizmos()
    {
        if (!anchor)
            return;

        var rotatedVector = anchor.rotation * offsetFromAnchor;
        Gizmos.DrawCube(anchor.position + rotatedVector, Vector3.one * 0.1f);
    }
}
