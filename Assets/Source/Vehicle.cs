using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Vehicle : MonoBehaviour
{
    public float moveSpeed;
    public float turnSpeed;
    public float brakeForce = 0.95f;
    public GameObject Pivot;

    private Default _actions;
    private InputAction _move;
    private InputAction _brake;
    private Rigidbody _rb;

    private float turnForce = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _actions = new Default();
        _rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _move = _actions.Player.Move;
        _move.Enable();
        _brake = _actions.Player.Brake;
        _brake.Enable();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var dir = _move.ReadValue<Vector2>();
        var brake = _brake.IsPressed();

        if (dir.y != 0)
        {
            _rb.linearVelocity += transform.forward * moveSpeed;
        }
        if (dir.x != 0)
        {
            turnForce += turnSpeed * dir.x;
        }
        transform.RotateAround(Pivot.transform.position, Vector3.up, turnForce);
        turnForce *= 0.9f;
        
        if (brake)
        {
            _rb.linearVelocity *= brakeForce;
        }
    }
}
