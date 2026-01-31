using Unity.VisualScripting;
using UnityEngine;

public class TapeCollider : MonoBehaviour
{
    [SerializeField]
    private int _numColliders = 24;

    [SerializeField]
    private float _radius = 1;

    [SerializeField]
    private float _capsuleRadius = 0.2f;

    [SerializeField]
    private float _capsuleLength = 0.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float increments = (Mathf.PI * 2) / _numColliders;

        for (int i=0; i<24; i++)
        {
            float rads = increments * i;
            float x = Mathf.Cos(rads);
            float y = Mathf.Sin(rads);
            Vector3 origin = new Vector3(x * _radius, y * _radius, 0);

            var capsule = gameObject.AddComponent<CapsuleCollider>();
            capsule.radius = _capsuleRadius;
            capsule.center = origin;
            capsule.direction = 2;
            capsule.height = _capsuleLength;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
