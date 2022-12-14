using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodDetection : MonoBehaviour
{
    [SerializeField] private string _targetedFoodTag;
    [SerializeField] private float _distanceForEating;
    [SerializeField] private float _speed;
    private GameObject currentTarget = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CatchFood();
    }

    void CatchFood()
    {
        float step = _speed * Time.deltaTime;
        if (currentTarget != null)
        {
            transform.parent.transform.position = Vector3.MoveTowards(transform.parent.transform.position, currentTarget.transform.position, step);
            if(Vector3.Distance(transform.parent.transform.position, currentTarget.transform.position) <= _distanceForEating)
            {
                Destroy(currentTarget);
                currentTarget = null;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag(_targetedFoodTag) && currentTarget == null)
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
