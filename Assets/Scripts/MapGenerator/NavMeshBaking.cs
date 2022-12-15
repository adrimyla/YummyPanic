using NavMeshComponents.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// This class is used to configure the NavMesh, after the dungeon has been generated.
/// </summary>
public class NavMeshBaking : MonoBehaviour
{
    [SerializeField] private NavMeshSurface _navMeshSurface;
    [SerializeField] public NavMeshCollectSources2d _navMeshSourcesCollector;

    // Start is called before the first frame update
    void Start()
    {
        _navMeshSourcesCollector.enabled = false;
    }

    public void ConfigureNavMesh()
    {
        _navMeshSourcesCollector.enabled = true;
        _navMeshSurface.BuildNavMesh();
    }

}
