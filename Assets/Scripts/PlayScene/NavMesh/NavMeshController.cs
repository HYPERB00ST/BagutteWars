using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class NavMeshManager : MonoBehaviour
{
    private NavMeshSurface surface;

    void Start()
    {
        gameObject.TryGetComponent<NavMeshSurface>(out surface);
        if (surface == null) {
            Debug.LogError("The nav mesh doesnt exist");
        }
        surface.BuildNavMesh();
    }
}