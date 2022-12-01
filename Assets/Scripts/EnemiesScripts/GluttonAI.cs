using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GluttonAI : MonoBehaviour
{
    private NavMeshAgent thisAgent = null;
    public Transform target = null;
    public Transform homeLocation;
    public string targetedFoodTag;
    [SerializeField] private float _distanceForEating;
    [SerializeField] private float _speed;
    public GameObject currentTarget = null;

    [Header("Roaming parameters")]
    [SerializeField] private float _roamingRadius;
    [SerializeField] private int _randomPositionsCount;
    [SerializeField] private float _timerBeforeNextPosition;
    private Vector3 _currentRoamingPosition = Vector3.zero;
    private float _timer;

    private void Awake()
    {
        thisAgent = GetComponent<NavMeshAgent>();
        // Prevent the Glutton from being impacted by the Z axis
        thisAgent.updateRotation = false;
        thisAgent.updateUpAxis = false;
        // Set timer to start the random roaming
        _timer = _timerBeforeNextPosition;
    }

    private void Update()
    {
        //if(currentTarget != null)
        //    thisAgent.SetDestination(target.position);
    }

    public void MoveRandomly() 
    {
        _timer += Time.deltaTime;
        if(_timer >= _timerBeforeNextPosition || transform.position == _currentRoamingPosition)
        {
            _currentRoamingPosition = (Vector2)transform.position + Random.insideUnitCircle * _roamingRadius;
            MoveTowardTarget(_currentRoamingPosition);
            _timer = 0f;
        }
        else
        {
            Debug.Log("Nope");
        }
    }

    public void MoveTowardTarget(Vector3 destination)
    {
        thisAgent.SetDestination(destination);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(targetedFoodTag) && currentTarget == null)
        {
            currentTarget = collision.gameObject;
            Debug.Log("Nourriture détectée");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(targetedFoodTag) && currentTarget != null && Vector3.Distance(collision.transform.position, this.transform.position) <= _distanceForEating)
        {
            Destroy(collision.gameObject);
            currentTarget = null;
            Debug.Log("Nourriture récupérée");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(targetedFoodTag) && currentTarget != null)
        {
            currentTarget = null;
        }
    }
}
