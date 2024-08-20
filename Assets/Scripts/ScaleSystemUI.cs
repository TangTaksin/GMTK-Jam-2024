using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleSystemUI : MonoBehaviour
{
    Camera _camera;

    public GameObject scaleUIPanel;
    public GameObject pointer;

    [Header("Prompt")]
    public GameObject up_prompt;
    public GameObject down_prompt;
    public GameObject left_prompt;
    public GameObject right_prompt;

    [Header("Guide Panel")]
    public Image toggleImage;
    public Sprite toggleOffSpr;
    public Sprite toggleOnSpr;

    SubBody _target;
    Side _markedSide;

    Vector2 _direction;
    float _angle;

    private void OnEnable()
    {
        _camera = Camera.main;

        scaleUIPanel.SetActive(false);

        ScaleSystem.OnToggle += OnToggle;
        ScaleSystem.OnSwitch += OnSwitch;
    }

    private void OnDisable()
    {
        ScaleSystem.OnToggle -= OnToggle;
        ScaleSystem.OnSwitch -= OnSwitch;
    }

    private void Update()
    {
        if (_target)
        {
            var _offseted = new Vector3(_direction.x * _target.transform.lossyScale.x / 2, _direction.y * _target.transform.lossyScale.y / 2);
            var _rotated = _target.transform.rotation * _offseted;
            var _sum = _target.transform.position + _rotated;
            pointer.transform.position = _camera.WorldToScreenPoint(_sum);

            var _angleOff = Quaternion.Euler(0, 0, _angle + _target.transform.rotation.eulerAngles.z);
            pointer.transform.rotation = _angleOff;

            up_prompt.transform.position = UpdatePromptPosition(Vector2.up);
            down_prompt.transform.position = UpdatePromptPosition(Vector2.down);
            left_prompt.transform.position = UpdatePromptPosition(Vector2.left);
            right_prompt.transform.position = UpdatePromptPosition(Vector2.right);
        }
    }

    void OnToggle(bool _isEdit)
    {
        print("toggled | isEdit : " + _isEdit);
        scaleUIPanel.SetActive(_isEdit);

        if (toggleImage)
        toggleImage.sprite = (_isEdit) ? toggleOnSpr : toggleOffSpr;
    }

    void OnSwitch(SubBody _sub, Side _side)
    {
        _target = _sub;
        _markedSide = _side;

        UpdateSide(_markedSide);
    }

    void UpdateSide(Side _side)
    {
        switch(_side)
        {
            case Side.Top:
                _direction = Vector2.up;
                _angle = 0;
                break;

            case Side.Bottom:
                _direction = Vector2.down;
                _angle = 0;
                break;

            case Side.Left:
                _direction = Vector2.left;
                _angle = 90;
                break;

            case Side.Right:
                _direction = Vector2.right;
                _angle = 90;
                break;
        }
    }



    Vector3 UpdatePromptPosition(Vector3 _dir)
    {
        
        var _offseted = new Vector3(_dir.x * (_target.transform.lossyScale.x / 2 + .5f), _dir.y * (_target.transform.lossyScale.y / 2 + .5f));
        var _rotated = _target.transform.rotation * _offseted;
        var _sum = _target.transform.position + _rotated;

        return _camera.WorldToScreenPoint(_sum);

    }
}
