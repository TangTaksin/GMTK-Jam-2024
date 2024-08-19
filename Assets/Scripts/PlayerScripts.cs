using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScripts : MonoBehaviour
{
    Rigidbody2D _body;

    public float rollForce;
    float _torqueDir;
    float _volume;

    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        _body.angularVelocity = -_torqueDir * rollForce;
    }

    public void OnRoll(InputValue _value)
    {
        _torqueDir = _value.Get<float>();
    }
}
