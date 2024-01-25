using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuyFeed : MonoBehaviour
{
    [SerializeField] private int EnemyLayer = 6;
    void OnTriggerEnter(Collider other) {
        
        if (other.gameObject.layer == EnemyLayer) {
            other.GetComponentInChildren<Animator>().SetBool("isJumping", true);
        } 
    }
}
