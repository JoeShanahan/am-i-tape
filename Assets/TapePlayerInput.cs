using System;
using UnityEditor.Build;
using UnityEngine;

public class TapePlayerInput : MonoBehaviour
{
    [SerializeField]
    private Transform _forwardTransform;

    private InputSystem_Actions _input;
    
    [SerializeField]
    private Rigidbody _rb;

    [SerializeField]
    private float _rollStrength = 10f;

    [SerializeField]
    private float _tiltStrength = 2f;

    [SerializeField]
    private float _maxAngular;

    [SerializeField]
    private float _jumpForce;

    private bool _jumpRequested;

    void Start()
    {
        _input = new();
        _input.Enable();
        _input.Player.Jump.performed += JumpPressed;
        _input.Player.Jump.canceled += JumpPressed;
    }

    void Update()
    {
        Debug.DrawLine(_forwardTransform.position, _forwardTransform.position + _forwardTransform.forward, Color.red);
    }

    private void JumpPressed(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            _jumpRequested = true;

        if (ctx.canceled)
            _jumpRequested = false;

    }

    void FixedUpdate()
    {
            
        Vector3 angular = _rb.angularVelocity;
        angular.x = Mathf.Clamp(angular.x, -_maxAngular, _maxAngular);
        angular.y = Mathf.Clamp(angular.y, -_maxAngular, _maxAngular);
        angular.z = Mathf.Clamp(angular.z, -_maxAngular, _maxAngular);

        var move = _input.Player.Move.ReadValue<Vector2>();

    
        // Forward roll axis
        Vector3 forwardAxis = Vector3.Cross(-_forwardTransform.forward, Vector3.up);

        // Side roll axis (left/right)
        Vector3 sideAxis = Vector3.Cross(_forwardTransform.right, Vector3.up);

        
        if (_jumpRequested)
        {
            _jumpRequested = false;
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }

        // Skip if invalid
        if (forwardAxis.sqrMagnitude < 0.0001f && sideAxis.sqrMagnitude < 0.0001f)
            return;

        // Normalize axes
        forwardAxis.Normalize();
        sideAxis.Normalize();

        // Combine torques
        Vector3 torque =
            forwardAxis * (_rollStrength * move.y) +
            sideAxis   * (_tiltStrength * -move.x);

        _rb.AddTorque(torque, ForceMode.Acceleration);
    }

}
