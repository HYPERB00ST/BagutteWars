using System;
using System.Collections;
using System.Collections.Generic;
using Globals;
using UnityEngine;

public class NPCAnimController : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private float TimeToCatch = 5f;
    private float TimePassed = 0f;
    void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleJumpingAnim();
    }

    private void HandleJumpingAnim()
    {
        if (_animator.GetBool("isJumping")) {
            _animator.speed = 0.5f;
            TimePassed += Time.deltaTime;
            
            if (TimePassed >= TimeToCatch) {
                _animator.speed = 0f;
            }
        }
    }
}
