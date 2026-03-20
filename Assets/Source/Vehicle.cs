using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Vehicle : MonoBehaviour
{
    public float moveSpeed;
    public float turnSpeed;
    public float brakeForce = 0.95f;
    public GameObject Pivot;

    public float maxNitro = 5f;
    public float nitroMultiplier = 2f;
    public float explodeDelay = 3f;
    private float currentNitro;
    private float explodeTimer;
    private bool explosionTimerRunning;
    private bool nitroActive;
    private Vector3 startPosition;
    private Quaternion startRotation;

    private Default _actions;
    private InputAction _move;
    private InputAction _brake;
    private InputAction _interact; //interact(e) will serve as our nitro activation key
    private Rigidbody _rb;
    private BoxCollider _col;

    private float turnForce = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _actions = new Default();
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<BoxCollider>();
        currentNitro = maxNitro;
        explodeTimer = explodeDelay;
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    private void OnEnable()
    {
        _move = _actions.Player.Move;
        _move.Enable();
        _brake = _actions.Player.Brake;
        _brake.Enable();
        _interact = _actions.Player.Interact;
        _interact.Enable();
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

    private void OnCollisionStay(Collision other)
    {
        var dir = _move.ReadValue<Vector2>();

        float currentSpeed = moveSpeed;
        if (nitroActive)
        {
            currentSpeed *= nitroMultiplier;
        }

        if (dir.y != 0)
        {
            _rb.linearVelocity += transform.forward * currentSpeed;
        }
    }

    void Update()
    {
        bool interactHeld = _interact.IsPressed();
        bool interactReleased = _interact.WasReleasedThisFrame();

        if (interactHeld && currentNitro > 0f && nitroActive)
        {
            nitroActive = true;

            currentNitro -= Time.deltaTime;
            if (currentNitro < 0f)
            {
                currentNitro = 0f;
            }

            explodeTimer = explodeDelay;
            explosionTimerRunning = false;
        }
        else
        {
            nitroActive = false;
        }

        if (interactReleased && currentNitro > 0f)
        {
            explosionTimerRunning = true;
        }

        if (explosionTimerRunning)
        {
            explodeTimer -= Time.deltaTime;

            if (explodeTimer <= 0f)
            {
                explodeTimer = 0f;
                Explode();
            }
        }

        if (currentNitro <= 0f)
        {
            ResetVehicle();
        }
    }

    void ResetVehicle()
    {
        transform.position = startPosition;
        transform.rotation = startRotation;
        _rb.linearVelocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;

        currentNitro = maxNitro;
        explodeTimer = explodeDelay;
        explosionTimerRunning = false;
        nitroActive = false;
        turnForce = 0f;
    }

    void Explode()
    {
        Debug.Log("Vehicle exploded");
        ResetVehicle();
    }
}
