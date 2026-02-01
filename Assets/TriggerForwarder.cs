using System;
using UnityEngine;
using UnityEngine.Events;

public class TriggerForwarder : MonoBehaviour
{
    [SerializeField]
    private string requiredTag;

    public event Action<Rigidbody> OnThingEntered;
    public event Action<Rigidbody> OnThingExited;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == requiredTag)
        {
            OnThingEntered?.Invoke(other.attachedRigidbody);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == requiredTag)
        {
            OnThingExited?.Invoke(other.attachedRigidbody);
        }
    }


}
