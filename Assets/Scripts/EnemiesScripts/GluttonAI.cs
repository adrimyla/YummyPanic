using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GluttonAI : MonoBehaviour
{
    private NavMeshAgent thisAgent = null;
    public Transform target = null; 
    [SerializeField] private string _targetedFoodTag;
    [SerializeField] private float _distanceForEating;
    [SerializeField] private float _speed;
    private GameObject currentTarget = null;

    private void Awake()
    {
        thisAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        thisAgent.SetDestination(target.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(_targetedFoodTag))
        {
            currentTarget = collision.gameObject;
            Debug.Log("Nourriture détectée");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(_targetedFoodTag) && currentTarget != null)
        {
            currentTarget = null;
        }
    }
}
