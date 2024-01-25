using UnityEngine;

public class NPCTargetMove : MonoBehaviour
{

    [SerializeField] private float MAX_HALF_RANGE = 17f;
    [SerializeField] private float speed = 5f;
    private bool hasDestination = false;
    private Vector3 _destination;
    private float originalX;
    private const float POSITION_OFFSET = 0.02f;
    private bool direction; // Right true / left false

    void Start() {
        originalX = transform.position.x;
    }

    void Update()
    {
        if (!hasDestination) {
            SetDestination();
        }
        MoveTarget();
    }

    private void SetDestination()
    {
        _destination = new Vector3(Random.Range(-MAX_HALF_RANGE, MAX_HALF_RANGE) + originalX, 
            transform.position.y, transform.position.z);
        hasDestination = true;

        SetDirection();
    }

    private void SetDirection()
    {
        direction = CheckDirection();
    }

    private void MoveTarget()
    {
        if (CheckActualPosition()) {
            hasDestination = false;
            return;
        }
        if (CheckDirection()) {
            transform.Translate(transform.right * speed * Time.deltaTime);
            return;
        }
        transform.Translate(-(transform.right * speed * Time.deltaTime));
    }

    private bool CheckActualPosition()
    {     
        if (direction && (transform.position.x > _destination.x)) {
            return true;
        }
        else if (!direction && (transform.position.x < _destination.x)) {
            return true;
        }
        return (_destination.x - transform.position.x) < POSITION_OFFSET && 
            (_destination.x - transform.position.x) > 0f;
    }

    private bool CheckDirection()
    {
        return _destination.x > transform.position.x;
    }
}
