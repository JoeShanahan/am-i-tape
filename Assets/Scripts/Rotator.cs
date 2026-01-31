using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Rotator : MonoBehaviour
{
    [SerializeField] private Vector3 rotationSpeed = new Vector3(0,0,0);
    private Rigidbody rb;
    void Start() => rb = GetComponent<Rigidbody>();
    void FixedUpdate() => rb.MoveRotation(rb.rotation * Quaternion.Euler(rotationSpeed * Time.fixedDeltaTime));
}
