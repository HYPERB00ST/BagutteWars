using UnityEngine;

public class JarRunning : MonoBehaviour
{
    [SerializeField] private AudioClip[] attackClips;
    [SerializeField] private AudioClip RunClip;
    private bool attackIsPlaying = false;
    private float attackTimer = 0f;
    private AudioSource _audio;
    private Animator _animator;
    void Start()
    {
        _audio = gameObject.GetComponent<AudioSource>();
        _animator = gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleRunSound();
        HandleAttackSound();
    }

    private void HandleAttackSound()
    {
        HandleSoundTimer();
        if (_animator.GetBool("isAttacking") && !attackIsPlaying) {
            _audio.PlayOneShot(attackClips[Random.Range(0, 3)]);
            attackIsPlaying = true;
        }
    }

    private void HandleSoundTimer()
    {
        if (attackIsPlaying) {
            attackTimer += Time.deltaTime;
            if (attackTimer >= 0.5f) {
                attackIsPlaying = !attackIsPlaying;
                attackTimer = 0f;
            }
        }
    }

    private void HandleRunSound()
    {
        if (_animator.GetInteger("moveState") != 0) {
            if (!_audio.isPlaying) {
                _audio.PlayOneShot(RunClip);
            }
        } else {
            _audio.Pause();
        }
    }
}
