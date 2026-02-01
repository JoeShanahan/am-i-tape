using System;
using UnityEngine;


public class TapePlayerInput : MonoBehaviour
{

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
    private bool _rotateRequested;
    private float _rotateYModifier = 2f;
    private float _rotateXModifier = 0.8f;
    private bool _isGrounded = false;
    // private int _groundedContactPoints = 0;

    void Start()
    {
        _input = new();
        _input.Enable();
        _input.Player.Jump.performed += JumpPressed;
        _input.Player.Jump.canceled += JumpPressed;
        _input.Player.Rotate.performed += RotatePressed;
        _input.Player.Rotate.canceled += RotatePressed;
    }

    private void OnDestroy()
    {
        if (_input != null)
        {
            _input.Player.Jump.performed -= JumpPressed;
            _input.Player.Jump.canceled -= JumpPressed;
            _input.Player.Rotate.performed -= RotatePressed;
            _input.Player.Rotate.canceled -= RotatePressed;
            _input.Disable();
        }
    }

    private bool _isLocalPlayer;

    public void Init(Transform forward, bool isLocalPlayer)
    {
        _forwardTransform = forward;
        _isLocalPlayer = isLocalPlayer;

        if (isLocalPlayer)
        {
            _input = new();
            _input.Enable();
            _input.Player.Jump.performed += JumpPressed;
            _input.Player.Jump.canceled += JumpPressed;
        }
        else
        {
            Destroy(GetComponent<TapeRaycaster>());
            Destroy(GetComponent<TapePlacer>());
            Destroy(GetComponent<AutoRespawn>());
            Destroy(this);
        }
    }

    private void JumpPressed(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            _jumpRequested = true;

        if (ctx.canceled)
            _jumpRequested = false;

    }

    private void RotatePressed(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            _rotateRequested = true;
        if (ctx.canceled)
            _rotateRequested = false;
    }

    void FixedUpdate()
    {
        if (!_isLocalPlayer)
            return;


        Vector3 angular = _rb.angularVelocity;
        angular.x = Mathf.Clamp(angular.x, -_maxAngular, _maxAngular);
        angular.y = Mathf.Clamp(angular.y, -_maxAngular, _maxAngular);
        angular.z = Mathf.Clamp(angular.z, -_maxAngular, _maxAngular);

        var move = _input.Player.Move.ReadValue<Vector2>();


        // Forward roll axis
        Vector3 forwardAxis = Vector3.Cross(-_forwardTransform.forward, Vector3.up);

        // Side roll axis (left/right)
        Vector3 sideAxis = Vector3.Cross(_forwardTransform.right, Vector3.up);

        // Debug.Log(_groundedContactPoints);
        // _isGrounded = _groundedContactPoints > 0;

        if (_jumpRequested) // && _isGrounded)
        {
            // _groundedContactPoints = 0;
            _jumpRequested = false;
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            _isGrounded = false;
        }

        if (_rotateRequested)
        {
            float rotateInput = _input.Player.Rotate.ReadValue<float>();
            float rotateAmountY = rotateInput * _rotateYModifier;
            float newRotationY = _rb.rotation.eulerAngles.y + rotateAmountY;
            float rotateAmountX = rotateInput * _rotateXModifier;
            float newRotationX = _rb.rotation.eulerAngles.x + rotateAmountX;
            _rb.rotation = Quaternion.Euler(newRotationX, newRotationY, _rb.rotation.eulerAngles.z);
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
            sideAxis * (_tiltStrength * -move.x);

        _rb.AddTorque(torque, ForceMode.Acceleration);

    }

    void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.CompareTag("Bounce"))
            _jumpRequested = true;
    }

    void OnCollisionStay(Collision hit)
    {
        // if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Ground"))
        //     _groundedContactPoints = hit.contactCount;
        // else _groundedContactPoints = 0;
    }

}
