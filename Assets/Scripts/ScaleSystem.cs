using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



enum ControlState
{
    Movement,
    EditScale
}

public enum Side
{
    Top,
    Bottom,
    Left,
    Right,
}

public class ScaleSystem : MonoBehaviour
{
    ControlState _currentControl;

    PlayerInput _playerInput;

    public SubBody[] sub_Body;
    int _currentSubbody = 0;
    Side _currentSide = Side.Top;

    public float scaleAmount;
    float scaleAxis;

    public delegate void SwitchEvent(SubBody _sub, Side _side);
    public static SwitchEvent OnSwitch;

    public delegate void ModeEvent(bool _isEdit);
    public static ModeEvent OnToggle;

    private void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        sub_Body[_currentSubbody].Resize(_currentSide, scaleAxis);
    }

    #region PartSelect Listener

    public void OnToggleMode()
    {
        switch (_currentControl)
        {
            case ControlState.Movement:
                ToEditScale();

                break;

            case ControlState.EditScale:
                ToMovement();
                break;
        }

        OnToggle?.Invoke(_currentControl == ControlState.EditScale);
        OnSwitch?.Invoke(sub_Body[_currentSubbody], _currentSide);

    }

    public void OnSwitchPart(InputValue _value)
    {
        var shift = _value.Get<float>();
        var lastSub = _currentSubbody;

        _currentSubbody += (int)shift;

        if (_currentSubbody < 0)
            _currentSubbody = sub_Body.Length - 1;
        if (_currentSubbody > sub_Body.Length - 1)
            _currentSubbody = 0;

        sub_Body[lastSub].SetImage(false);
        sub_Body[_currentSubbody].SetImage(true);

        OnSwitch?.Invoke(sub_Body[_currentSubbody], _currentSide);
    }

    public void OnSwitchSide(InputValue _value)
    {
        var dir = _value.Get<Vector2>();

        switch (dir.x, dir.y)
        {
            case (0, 1):
                _currentSide = Side.Top;
                break;

            case (0, -1):
                _currentSide = Side.Bottom;
                break;

            case (1, 0):
                _currentSide = Side.Right;
                break;

            case (-1, 0):
                _currentSide = Side.Left;
                break;
        }

        OnSwitch?.Invoke(sub_Body[_currentSubbody], _currentSide);
    }

    public void OnResize(InputValue _value)
    {
        scaleAxis = _value.Get<float>();
    }

    public void OnResetAllSize()
    {
        foreach (var _sb in sub_Body)
        {
            _sb.ResetScale();
        }
    }

    public void OnResetPartSize()
    {
        sub_Body[_currentSubbody].ResetScale();
    }

    #endregion

    void ToMovement()
    {
        _currentControl = ControlState.Movement;
        sub_Body[_currentSubbody].SetImage(false);

        _playerInput.SwitchCurrentActionMap("PlayerMoveMent");
    }

    void ToEditScale()
    {
        _currentControl = ControlState.EditScale;
        sub_Body[_currentSubbody].SetImage(true);

        _playerInput.SwitchCurrentActionMap("ScaleMode");
    }
}
