using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SandwichPress : MonoBehaviour
{
    [SerializeField]
    float _requiredTime = 0.5f;

    [SerializeField]
    float _shakeTime = 0.5f;

    [SerializeField]
    float _shakeAmount = 5f;

    [SerializeField]
    private float _flipTime = 0.2f;

    [SerializeField]
    private float _flipAngle = -45;

    [SerializeField]
    private Rigidbody _flipper;

    [SerializeField]
    private Ease _ease = Ease.OutElastic;

    [SerializeField]
    private float _currentTime;

    [SerializeField]
    private Transform _explosionCenter;

    [SerializeField]
    private float _explosionForce = 100;

    [SerializeField]
    private float _explosionDistance = 10;

    [SerializeField]
    private TriggerForwarder _triggers;


    private bool _hasBeenSequenced;

    private List<Rigidbody> _bodies = new();

    public void PlayerEnteredZone(Rigidbody rb)
    {
        _bodies.Add(rb);       
    }

    public void PlayerExitedZone(Rigidbody rb)
    {
        _bodies.Remove(rb);
    }

    private void Start()
    {
        _triggers.OnThingEntered += PlayerEnteredZone;
        _triggers.OnThingExited += PlayerExitedZone;
    }

    private void Update()
    {
        if (_hasBeenSequenced)
            return;

        if (_bodies.Count == 0)
        {
            _currentTime = 0;
            return;
        }
        _currentTime += Time.deltaTime;

        if (_currentTime >= _requiredTime)
        {
            StartCoroutine(FlipRoutine());
            _hasBeenSequenced = true;
        }
    }

    private IEnumerator FlipRoutine()
    {
        float t = _shakeTime;

        Quaternion baseLocalRot = _flipper.transform.localRotation;

        while (t > 0)
        {
            t -= Time.deltaTime;

            float shake = Mathf.PerlinNoise(t  *20, (t + 1.23f) * 20) * _shakeAmount;

            // Build the shake in LOCAL space
            Quaternion localShakeRot = Quaternion.Euler(0, 0, shake);

            // Convert local → world for Rigidbody.MoveRotation
            Quaternion worldRot = _flipper.transform.parent.rotation * (baseLocalRot * localShakeRot);

            _flipper.MoveRotation(worldRot);

            yield return null;
        }

        foreach (Rigidbody rb in _bodies)
        {
            rb.AddExplosionForce(_explosionForce, _explosionCenter.position, _explosionDistance);
        }

        // Reset to base local rotation
        _flipper.MoveRotation(_flipper.transform.parent.rotation * baseLocalRot);

        // Now your DOTween rotations should also be local:
        _flipper.transform.DOLocalRotate(new Vector3(_flipAngle, 0, 0), _flipTime).SetEase(_ease);

        yield return new WaitForSeconds(_flipTime * 3);

        _flipper.transform.DOLocalRotate(Vector3.zero, _flipTime * 3).SetEase(Ease.Linear);

        yield return new WaitForSeconds(_flipTime * 3);

        _hasBeenSequenced = false;
        _currentTime = 0;

    }
}
