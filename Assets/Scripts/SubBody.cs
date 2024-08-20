using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubBody : MonoBehaviour
{
    Animator _animator;
    BoxCollider2D _collider;

    public Transform anchor;
    public Vector3 offsetFromAnchor;
    public Vector3 min_Scale, max_Scale;
    public float scaleLerpSpeed = 5;

    Vector3 _defaultScale;
    Vector3 _defaultOffset;

    Vector3 _targetScale;

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<BoxCollider2D>();

        _defaultScale = transform.localScale;
        _defaultOffset = offsetFromAnchor;

        _targetScale = transform.localScale;
    }

    private void Update()
    {
        if (anchor)
        {
            var rotatedVector = anchor.rotation * offsetFromAnchor;
            transform.position = anchor.position + rotatedVector;
        }

        transform.localScale = Vector3.Lerp(transform.localScale, _targetScale, Time.deltaTime * scaleLerpSpeed);
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
        AudioManager.Instance.PlayScaleUpSFX();

        switch (_currentSide)
        {
            case Side.Top:
                if ((_scaleAxis > 0 && transform.localScale.y >= max_Scale.y) ||
                    (_scaleAxis < 0 && transform.localScale.y <= min_Scale.y))
                    return;

                _targetScale += Vector3.up * _scaleAxis * Time.deltaTime;
                smallmovement = Vector3.up * (_scaleAxis / 2) * Time.deltaTime;
                offsetFromAnchor += smallmovement;
                break;

            case Side.Bottom:
                if ((_scaleAxis > 0 && transform.localScale.y >= max_Scale.y) ||
                    (_scaleAxis < 0 && transform.localScale.y <= min_Scale.y))
                    return;

                _targetScale -= Vector3.down * _scaleAxis * Time.deltaTime;
                smallmovement = Vector3.down * (_scaleAxis / 2) * Time.deltaTime;
                offsetFromAnchor += smallmovement;
                break;

            case Side.Left:
                if ((_scaleAxis > 0 && transform.localScale.x >= max_Scale.x) ||
                    (_scaleAxis < 0 && transform.localScale.x <= min_Scale.x))
                    return;

                _targetScale -= Vector3.left * _scaleAxis * Time.deltaTime;
                smallmovement = Vector3.left * (_scaleAxis / 2) * Time.deltaTime;
                offsetFromAnchor += smallmovement;
                break;

            case Side.Right:
                if ((_scaleAxis > 0 && transform.localScale.x >= max_Scale.x) ||
                    (_scaleAxis < 0 && transform.localScale.x <= min_Scale.x))
                    return;

                _targetScale += Vector3.right * _scaleAxis * Time.deltaTime;
                smallmovement = Vector3.right * (_scaleAxis / 2) * Time.deltaTime;
                offsetFromAnchor += smallmovement;
                break;
        }

        if (!anchor)
        {
            transform.position += transform.rotation * smallmovement;
        }
    }

    public void ResetScale()
    {

        _targetScale = _defaultScale;
        offsetFromAnchor = _defaultOffset;
        AudioManager.Instance.PlaySFX(AudioManager.Instance.resetScale_sfx);
    }

    public void SetImage(bool _isEdit)
    {
        if (_isEdit)
            _animator?.Play("Edit_anim");
        else
            _animator?.Play("Normal_anim");
    }

    public Collider2D GetCollidor()
    {
        return _collider;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        AudioManager.Instance.PlayBoundSFX();
    }

    private void OnDrawGizmos()
    {
        if (!anchor)
            return;

        var rotatedVector = anchor.rotation * offsetFromAnchor;
        Gizmos.DrawCube(anchor.position + rotatedVector, Vector3.one * 0.1f);
    }
}
