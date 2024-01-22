using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class NPCTargetTrack : MonoBehaviour
{
    public Transform Target;
    public float UpdateSpeed = 0.1f; // How frequently to recalculate path based on Target transform's position.

    private NavMeshAgent Agent;

    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        StartCoroutine(FollowTarget());
    }

    private IEnumerator FollowTarget()
    {
        WaitForSeconds Wait = new WaitForSeconds(UpdateSpeed);

        while (enabled)
    {
        Agent.SetDestination(Target.transform.position);

        yield return Wait;  
    }
    }
}
