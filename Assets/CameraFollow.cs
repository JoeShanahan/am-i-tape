using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform _toFollow;

    [SerializeField]
    private Transform _verticalTilt;

    [SerializeField]
    private float _cameraSensitivity = 4;

    [SerializeField]
    private Vector2 _cameraBounds;

    private InputSystem_Actions _input;

    void Start()
    {
        _input = new();
        _input.Enable();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = _toFollow.position;
    }

    void Update()
    {
        var look = _input.Player.Look.ReadValue<Vector2>();
        transform.Rotate(0, _cameraSensitivity * look.x, 0);
        _verticalTilt.Rotate(_cameraSensitivity * look.y, 0, 0);

        Vector3 eulerAngles = _verticalTilt.localEulerAngles;

        if (eulerAngles.x > 180)
        {
            eulerAngles.x -= 360;
        }

        eulerAngles.x = Mathf.Clamp(eulerAngles.x, _cameraBounds.x, _cameraBounds.y);
        _verticalTilt.localEulerAngles = eulerAngles;
    }
}
