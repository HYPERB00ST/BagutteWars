using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToastSound : MonoBehaviour
{
    [SerializeField] private AudioClip Clip1, Clip2;
    private Animator _animator;
    private AudioSource _audio;
    private bool soundPlayed = false;
    void Start()
    {
        _audio = gameObject.GetComponent<AudioSource>();
        _animator = gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_animator.GetBool("isJumping") && !soundPlayed) {
            if (Random.Range(0f, 1f) < 0.5f) {
                _audio.PlayOneShot(Clip1);
            }
            else {
                _audio.PlayOneShot(Clip2);
            }
            soundPlayed = true;
        }
    }
}
