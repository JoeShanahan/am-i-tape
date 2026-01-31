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
        // Axis of rotation for rolling
        Vector3 rollAxis = Vector3.Cross(-_forwardTransform.forward, Vector3.up);

        // If the direction is vertical or invalid, skip
        if (rollAxis.sqrMagnitude < 0.0001f)
            return;

        var move = _input.Player.Move.ReadValue<Vector2>();

        // Torque proportional to desired rolling speed
        Vector3 torque = rollAxis.normalized * _rollStrength * move.y;

        _rb.AddTorque(torque, ForceMode.Acceleration);
    }
}
