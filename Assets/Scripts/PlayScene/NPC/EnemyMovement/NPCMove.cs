using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;

public class NPCMove : MonoBehaviour
{
    [SerializeField] private string KidPrefabName = "ToastDestination";
    [SerializeField] private float forwardOffset = 5f;
    private Transform DespawnerTransform;
    private Transform MovingDestination;
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private Rigidbody _rigidbody;
    private Vector3 endingJumpPosition;
    private bool havePositions = false;

    void Awake()
    {
        TryGetComponent<NavMeshAgent>(out _navMeshAgent);
        
        if (_navMeshAgent == null)
        {
            Debug.LogError("The nav mesh agent component is not attached to " + gameObject.name);
        }

        MovingDestination = GameObject.Find(KidPrefabName).transform;
        _animator = gameObject.GetComponentInChildren<Animator>();
        DespawnerTransform = GameObject.Find("GuyTrolly").transform.GetChild(2);
        _rigidbody = gameObject.GetComponent<Rigidbody>();

    }

    void Update() {
        UpdateDestination();
        HandleFinalJump();
    }

    private void HandleFinalJump()
    {
        if (_animator.GetBool("isJumping")) {
            _navMeshAgent.enabled = false;
            if (!havePositions) {
                SavePositions();
                _rigidbody.isKinematic = true;
            }
            ArcJump();
        }
    }

    private void SavePositions()
    {
        endingJumpPosition = DespawnerTransform.position;
        
        havePositions = true;
    }

    private void ArcJump()
    {   
        transform.LookAt(endingJumpPosition);
        transform.Translate(-transform.forward * forwardOffset * Time.deltaTime);
        // float radius = Vector3.Distance(startingJumpPosition, endingJumpPosition) / 2;
        // Vector3 center = new Vector3((startingJumpPosition.x + endingJumpPosition.x)/2, 
        //     (startingJumpPosition.y + endingJumpPosition.y)/2, (startingJumpPosition.z + endingJumpPosition.z)/2);

        // jumpAngle += Mathf.PI / TimeToJump * Time.deltaTime;

        // Vector3 finalMove = (transform.forward * Mathf.Cos(jumpAngle) + 
        //     transform.up * Mathf.Sin(jumpAngle)) * radius; // I dont know why -forward, but it works

        // transform.Translate(finalMove * Time.deltaTime);
    }


    private void UpdateDestination()
    {
        if (_navMeshAgent.enabled) {
            _navMeshAgent.SetDestination(MovingDestination.position);
        }
    }
}
