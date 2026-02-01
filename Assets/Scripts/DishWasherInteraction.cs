using UnityEngine;
using System.Collections;


public class DishWasherInteraction : MonoBehaviour
{
    [SerializeField] private GameObject _doorObject;
    [SerializeField] private GameObject _drawObject;
    [SerializeField] private TriggerForwarder _triggers;
    [SerializeField] private float _doorOpenAngle = 90f;
    [SerializeField] private float _doorOpenSpeed = 2f;
    [SerializeField] private float _drawOpenDistance = 4.5f;
    [SerializeField] private float _drawOpenSpeed = 2f;
    private bool _hasInteracted = false;
    private Coroutine _coroutine;

    private void Start()
    {
        _triggers.OnThingEntered += OnTriggerEntered;
    }

    private void OnTriggerEntered(Rigidbody rb)
    {
        if (_hasInteracted) return;
        _hasInteracted = true;
        _coroutine = StartCoroutine(dishWasherRoutine());
    }

    private IEnumerator dishWasherRoutine()
    {
        float t = _doorOpenSpeed;
        while (t > 0f)
        {
            t -= Time.deltaTime;
            float angle = Mathf.Lerp(0f, _doorOpenAngle, 1f - (t / _doorOpenSpeed));
            _doorObject.transform.localEulerAngles = new Vector3(_doorObject.transform.localEulerAngles.x, angle, _doorObject.transform.localEulerAngles.z);
            yield return null;
        }
        float t2 = _drawOpenSpeed;
        while (t2 > 0f)
        {
            t2 -= Time.deltaTime;
            float distance = Mathf.Lerp(0f, _drawOpenDistance, 1f - (t2 / _drawOpenSpeed));
            _drawObject.transform.localPosition = new Vector3(distance, _drawObject.transform.localPosition.y, _drawObject.transform.localPosition.z);
            yield return null;
        }
    }
}
