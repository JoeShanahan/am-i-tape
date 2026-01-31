using UnityEngine;

public class AutoRespawn : MonoBehaviour
{

    private Vector3 _startPos;
    private Quaternion _startRot;

    [SerializeField]
    private float _zKill = -10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _startPos = transform.position;
        _startRot = transform.rotation;    
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < _zKill)
        {
            transform.position = _startPos;
            transform.rotation = _startRot;

            if (TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                
            }
        }
    }
}
