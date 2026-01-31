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

    void Start()
    {
        _input = new();
        _input.Enable();
    }

    void Update()
    {
        Debug.DrawLine(_forwardTransform.position, _forwardTransform.position + _forwardTransform.forward, Color.red);
    }

void FixedUpdate()
{
    var move = _input.Player.Move.ReadValue<Vector2>();

    // Forward roll axis
    Vector3 forwardAxis = Vector3.Cross(-_forwardTransform.forward, Vector3.up);

    // Side roll axis (left/right)
    Vector3 sideAxis = Vector3.Cross(_forwardTransform.right, Vector3.up);

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
