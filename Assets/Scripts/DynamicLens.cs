using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicLens : MonoBehaviour
{
    public ScaleSystem _targetScaler;
    public float orthoLensBuffer = 5;
    Cinemachine.CinemachineVirtualCamera _cinemachine;

    Transform _target;
    Bounds _tbounds;
    float _lensOrthoTarget;

    private void OnEnable()
    {
        _target = new GameObject().transform;

        _cinemachine = GetComponent<Cinemachine.CinemachineVirtualCamera>();
        _cinemachine.Follow = _target;
        _cinemachine.LookAt = _target;
    }

    private void Update()
    {
        UpdateTargetBound();
    }

    void UpdateTargetBound()
    {
        _tbounds = _targetScaler.GetBound();
        _target.position = _tbounds.center;

        _lensOrthoTarget = _tbounds.size.magnitude + orthoLensBuffer;

        // Reduce the interpolation factor for a smoother transition
        float interpolationFactor = 0.05f; // Smaller value for smoother transition

        _lensOrthoTarget = _tbounds.size.magnitude + orthoLensBuffer;
        _cinemachine.m_Lens.OrthographicSize = Mathf.Lerp(_cinemachine.m_Lens.OrthographicSize, _lensOrthoTarget, Time.deltaTime * interpolationFactor);
    }
}
