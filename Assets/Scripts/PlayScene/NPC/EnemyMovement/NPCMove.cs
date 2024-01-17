using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMove : MonoBehaviour
{
    [SerializeField] private Vector3 _destination;

    NavMeshAgent _navMeshAgent;

    void Awake()
    {
        TryGetComponent<NavMeshAgent>(out _navMeshAgent);
        
        if (_navMeshAgent == null)
        {
            Debug.LogError("The nav mesh agent component is not attached to " + gameObject.name);
        }
        SetDestination();
    }

    private void SetDestination()
    {
        if (_destination != null)
        {
            // Vector3 targetVector = _destination.transform.position;
            _navMeshAgent.SetDestination(_destination);
        }
    }
}
