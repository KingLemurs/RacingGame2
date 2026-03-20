using System;
using Source;
using Unity.VisualScripting;
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
    private BoxCollider _col;
    private Gamemode _mode;

    private Vector3 _LastCheckpoint;
    private float turnForce = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _actions = new Default();
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<BoxCollider>();
        _LastCheckpoint = transform.position;
        _mode = GameObject.FindGameObjectWithTag("GameController").GetComponent<Gamemode>();
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

        // check if we are touching ground
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

    // died
    private void OnTriggerEnter(Collider other)
    {
        _LastCheckpoint = transform.position;
        _mode.OnPlayerDeath.Invoke();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bad"))
        {
            _rb.linearVelocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
            this.transform.position = _LastCheckpoint;
        }
    }

    private void OnCollisionStay(Collision other)
    {
        var dir = _move.ReadValue<Vector2>();
        if (dir.y != 0)
        {
            _rb.linearVelocity += transform.forward * moveSpeed;
        }
    }
}
