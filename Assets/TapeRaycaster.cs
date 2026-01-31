using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class TapeRaycaster : MonoBehaviour
{
    private UprightDetector _upright;
    private TapeCollider _collider;

    [SerializeField]
    private float _minUpright = 0.8f;

    [SerializeField]
    private LayerMask _mask;

    [SerializeField]
    private float _maxPointDistance = 0.8f;

    private Vector3 _lastLeftHit;
    private Vector3 _lastRightHit;

    public bool DidHitLeft { get; private set; }
    public bool DidHitRight { get; private set; }

    public event Action<Vector3, Vector3> OnNewPointReached;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _upright = GetComponent<UprightDetector>();    
        _collider = GetComponent<TapeCollider>();

        if (_upright == null)
            Debug.LogError("Tape Raycaster requires UprightDetector!"); 
    
        if (_collider == null)
            Debug.LogError("Tape Raycaster requires TapeCollider!");
    }

    public Vector3 BottomPoint => _collider.transform.position - new Vector3(0, _collider.Radius, 0);

    public Vector3 LeftRaycastPoint => BottomPoint - (_collider.transform.forward * _collider.Width * 0.5f);
    public Vector3 RightRaycastPoint => BottomPoint + (_collider.transform.forward * _collider.Width * 0.5f);

    public Vector3 RaycastLeft()
    {
       Ray leftRay = new Ray(LeftRaycastPoint, Vector3.down);

        DidHitLeft = Physics.Raycast(leftRay, out RaycastHit leftHit, 100f, _mask);

        if (DidHitLeft)
        {
            return leftHit.point;
        }
        
        return LeftRaycastPoint;
    }

    public Vector3 RaycastRight()
    {
       Ray rightRay = new Ray(RightRaycastPoint, Vector3.down);

        DidHitRight = Physics.Raycast(rightRay, out RaycastHit rightHit, 100f, _mask);

        if (DidHitRight)
        {
            return rightHit.point;
        }
        
        return RightRaycastPoint;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        DidHitLeft = DidHitRight = false;

        if (_upright == null || _collider == null)
            return;

        if (_upright.UprightPercent() < _minUpright)
            return;

        Ray leftRay = new Ray(LeftRaycastPoint, Vector3.down);
        Ray rightRay = new Ray(RightRaycastPoint, Vector3.down);

        DidHitLeft = Physics.Raycast(leftRay, out RaycastHit leftHit, 0.5f, _mask);
        DidHitRight = Physics.Raycast(rightRay, out RaycastHit rightHit, 0.5f, _mask);

        if (DidHitLeft && DidHitRight)
        {
            float leftDist = Vector3.Distance(leftHit.point, _lastLeftHit);
            float rightDist = Vector3.Distance(rightHit.point, _lastRightHit);

            if (leftDist > _maxPointDistance || rightDist > _maxPointDistance)
            {
                _lastLeftHit = leftHit.point;
                _lastRightHit = rightHit.point;
                OnNewPointReached?.Invoke(_lastLeftHit, _lastRightHit);
            }
        }
    }
}
