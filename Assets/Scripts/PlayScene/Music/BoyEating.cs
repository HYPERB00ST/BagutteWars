using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoyEating : MonoBehaviour
{
    private AudioSource _audio;
    void Start()
    {
        _audio = gameObject.GetComponent<AudioSource>();
    }
    
    void OnTriggerEnter() {
        _audio.Play();
    }
}
