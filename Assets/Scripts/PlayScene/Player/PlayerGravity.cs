using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGravity : MonoBehaviour
{
    [SerializeField] private CharacterController player;
    [SerializeField] private float gravity;
    private Vector3 gravityVector;

    void Start() {
        gravityVector = new(0, -gravity, 0);
    }
    void FixedUpdate()
    {
        player.Move(gravityVector * Time.fixedDeltaTime);
    }
}
