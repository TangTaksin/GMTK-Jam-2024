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

    public float scaleAmount = 1.0f; // Set a default scale amount
    float scaleAxis;

    private void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        sub_Body[_currentSubbody].Resize(_currentSide, scaleAxis * scaleAmount); // Apply scaleAmount here
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

        if (_currentControl == ControlState.EditScale)
        {
            // Reset the color of the currently selected sub body
            sub_Body[_currentSubbody].SetColor(Color.white); // Assuming default color is white

            _currentSubbody += (int)shift;

            if (_currentSubbody < 0)
                _currentSubbody = sub_Body.Length - 1;
            if (_currentSubbody >= sub_Body.Length)
                _currentSubbody = 0;

            // Highlight the newly selected sub body
            HighlightCurrentSubBody();
        }
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

        // Re-highlight the current sub-body after reset
        if (_currentControl == ControlState.EditScale)
        {
            HighlightCurrentSubBody();
        }
    }

    public void OnResetCurrentSubbody()
    {
        sub_Body[_currentSubbody].ResetScale();
        if (_currentControl == ControlState.EditScale)
        {
            HighlightCurrentSubBody(); // Reapply the highlight after resetting the scale
        }
    }

    #endregion

    void ToMovement()
    {
        _currentControl = ControlState.Movement;
        _playerInput.SwitchCurrentActionMap("PlayerMoveMent");

        // Remove highlight when switching to Movement mode
        RemoveHighlightCurrentSubBody();
    }

    void ToEditScale()
    {
        _currentControl = ControlState.EditScale;
        _playerInput.SwitchCurrentActionMap("ScaleMode");

        // Highlight the current sub-body when switching to EditScale mode
        HighlightCurrentSubBody();
    }

    void HighlightCurrentSubBody()
    {
        sub_Body[_currentSubbody].SetColor(Color.red);
    }

    void RemoveHighlightCurrentSubBody()
    {
        sub_Body[_currentSubbody].SetColor(Color.white); // Reset to default color
    }
}
