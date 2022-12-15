using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] private float _homeDistanceOffset = 0.2f;
    [SerializeField] private float _speed;
    public GameObject currentTarget = null;
    [HideInInspector] public bool isHome = true;

    [Header("Roaming parameters")]
    [SerializeField] private float _roamingRadius;
    [SerializeField] private int _randomPositionsCount;
    [SerializeField] private float _timerBeforeNextPosition;
    private Vector3 _currentRoamingPosition = Vector3.zero;
    private float _timer;

    // List of burrows on the field
    private List<GameObject> _burrows = new List<GameObject>();

    // The personal foods "inventory"
    [HideInInspector] public GluttonFoodInventory inventory;

    private void Awake()
    {
        thisAgent = GetComponent<NavMeshAgent>();
        inventory = GetComponent<GluttonFoodInventory>();
        // Prevent the Glutton from being impacted by the Z axis
        thisAgent.updateRotation = false;
        thisAgent.updateUpAxis = false;
        // Set timer to start the random roaming
        _timer = _timerBeforeNextPosition;
        // Get all the burrows in the area
        GetAllBurrows();
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
    }

    // Search for all burrows at the beginning. They'll be used later whenever the gluttons want to go home
    public void GetAllBurrows()
    {
        foreach (Transform burrow in GameManager.Instance.burrowsContainerGO.transform)
        {
            _burrows.Add(burrow.gameObject);
        }
        homeLocation = _burrows[0].transform;
    }

    // Find nearest burrow after the glutton is full
    public void FindNearestBurrow()
    {
        if(_burrows.Count > 1)
        {
            for(int i = 1; i < _burrows.Count; i++)
            {
                float distanceToCheck = Vector2.Distance(gameObject.transform.position, _burrows[i].transform.position);
                // We update the current home's location depending on the distance between the Glutton and the burrow we're currently checking
                if (distanceToCheck < Vector2.Distance(gameObject.transform.position, homeLocation.position))
                    homeLocation = _burrows[i].transform;
            }
        }
    }

    /// <summary>
    ///  Check if the glutton is back to its burrow
    /// </summary>
    public void CheckIfHome()
    {
        if (Vector3.Distance(transform.position, homeLocation.position) <= _homeDistanceOffset)
        {
            isHome = true;
            if(!inventory.isEmpty)
                inventory.FreeInventory();
        }
        else
            isHome = false;
    }

    public void MoveTowardTarget(Vector3 destination)
    {
        thisAgent.SetDestination(destination);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(targetedFoodTag) && currentTarget == null && !inventory.isFull)
        {
            currentTarget = collision.gameObject;
            Debug.Log("Food found");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(targetedFoodTag) && currentTarget != null && !inventory.isFull && Vector3.Distance(collision.transform.position, this.transform.position) <= _distanceForEating)
        {
            Debug.Log("Food eaten");
            inventory.Add(collision.gameObject.GetComponent<ItemPickup>().item);
            Destroy(collision.gameObject);
            currentTarget = null;
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
