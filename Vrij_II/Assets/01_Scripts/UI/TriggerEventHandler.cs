using UnityEngine;
using UnityEngine.Events;

public class TriggerEventHandler : MonoBehaviour
{
    public UnityEvent OnEnterTrigger;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the other GameObject has a specific tag or meets certain criteria, if needed
        // Example: if (other.CompareTag("Player"))
        OnEnterTrigger.Invoke();
    }
}

