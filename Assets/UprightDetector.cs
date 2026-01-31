using UnityEngine;

public class UprightDetector : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public float UprightPercent()
    {
        return 1 - Mathf.Abs(Vector3.Dot(Vector3.up, transform.forward));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
