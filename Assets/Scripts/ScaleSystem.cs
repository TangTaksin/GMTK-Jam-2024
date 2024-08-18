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

    public delegate void ModeEvent();

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
    }

    public void OnSwitchPart(InputValue _value)
    {
        var shift = _value.Get<float>();

        _currentSubbody += (int)shift;

        if (_currentSubbody < 0)
            _currentSubbody = sub_Body.Length - 1;
        if (_currentSubbody > sub_Body.Length - 1)
            _currentSubbody = 0;
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
    }

    public void OnResize(InputValue _value)
    {
        scaleAxis = _value.Get<float>();
    }

    public void OnResetSize()
    {
        foreach (var _sb in sub_Body)
        {
            _sb.ResetScale();
        }
    }

    #endregion

    void ToMovement()
    {
        _currentControl = ControlState.Movement;

        _playerInput.SwitchCurrentActionMap("PlayerMoveMent");
    }

    void ToEditScale()
    {
        _currentControl = ControlState.EditScale;

        _playerInput.SwitchCurrentActionMap("ScaleMode");
    }
}
