using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GluttonStateManager : MonoBehaviour
{
    public State currentState;
    [SerializeField] private GluttonAI _AIMovement;

    // Start is called before the first frame update
    void Start()
    {
        currentState = State.ROAMING;
    }

    // Update is called once per frame
    void Update()
    {
        SwitchState();
        AdaptBehaviour();
    }

    private void SwitchState()
    {
        //// Random roaming
        //if (_AIMovement.currentTarget == null && currentState != State.ROAMING && currentState != State.TARGETING_FOOD)
        //    currentState = State.ROAMING;
        //// Movement toward an entity/a place
        //else
        //{
        //    if (_AIMovement.currentTarget != null && _AIMovement.currentTarget.CompareTag(_AIMovement.targetedFoodTag) && currentState != State.TARGETING_FOOD && currentState != State.GOING_HOME)
        //        currentState = State.TARGETING_FOOD;
        //    else if (_AIMovement.currentTarget != null && _AIMovement.currentTarget.CompareTag("Player") && currentState != State.TARGETING_PLAYER)
        //        currentState = State.TARGETING_PLAYER;
        //    else if(currentState == State.TARGETING_FOOD && _AIMovement.currentTarget == null)
        //        currentState = State.GOING_HOME;
        //}
        if (_AIMovement.currentTarget != null && _AIMovement.currentTarget.CompareTag(_AIMovement.targetedFoodTag) && currentState != State.TARGETING_FOOD && currentState != State.GOING_HOME)
            currentState = State.TARGETING_FOOD;
        else if (_AIMovement.currentTarget != null && _AIMovement.currentTarget.CompareTag("Player") && currentState != State.TARGETING_PLAYER)
            currentState = State.TARGETING_PLAYER;
        else if (currentState == State.TARGETING_FOOD && _AIMovement.currentTarget == null)
            currentState = State.GOING_HOME;
        else
            currentState = State.ROAMING;
    }

    private void AdaptBehaviour()
    {
        switch(currentState)
        {
            case State.TARGETING_FOOD:
                Debug.Log("Bouffe");
                _AIMovement.MoveTowardTarget(_AIMovement.currentTarget.transform.position);
                break;
            case State.TARGETING_PLAYER:
                Debug.Log("Joueur");
                _AIMovement.MoveTowardTarget(_AIMovement.currentTarget.transform.position);
                break;
            case State.GOING_HOME:
                Debug.Log("Base");
                _AIMovement.MoveTowardTarget(_AIMovement.homeLocation.position);
                break;
            default:
                Debug.Log("Mouvement aléatoire");
                _AIMovement.MoveRandomly();
                break;
        }
    }

    public enum State
    {
        ROAMING,
        TARGETING_FOOD,
        TARGETING_PLAYER,
        GOING_HOME
    }
}
