using UnityEngine;

public class FanSpin : MonoBehaviour
{

    [Header("Rotation Settings")]
    public Vector3 rotationAxis = Vector3.up; // Axis to spin around
    public float rotationSpeed = 180f; // Degrees per second

    void Update()
    {
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);
    }
}

