using TMPro;
using UnityEngine;

public class PlayerDebugUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _text;

    private TapeRaycaster _raycaster;
    private UprightDetector _upright;
    private Rigidbody _rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _upright = FindFirstObjectByType<UprightDetector>();
        _rb = _upright.GetComponent<Rigidbody>();
        _raycaster = _upright.GetComponent<TapeRaycaster>();
    }

    // Update is called once per frame
    void Update()
    {
        int upright = Mathf.RoundToInt(_upright.UprightPercent() * 100);
        Vector3 angVel = _rb.angularVelocity;
        float x = Mathf.RoundToInt(angVel.x);
        float y = Mathf.RoundToInt(angVel.y);
        float z = Mathf.RoundToInt(angVel.z);
        _text.text = $"Upright: {upright}%\nSpin: {x},{y},{z}\n{_raycaster.DidHitLeft},{_raycaster.DidHitRight}";
    }
}
