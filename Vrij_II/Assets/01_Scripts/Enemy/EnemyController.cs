using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    private PatrolState patrolState;
    private ChaseState chaseState;
    private AttackState attackState;

    private BaseState currentState;

    public Transform target;

    public NavMeshAgent agent;

    [SerializeField]
    private Transform[] waypoints;

    public float viewDistance;
    public float attackRange;

    public float beforeAttackDelay;
    public float afterAttackDelay;
    
    public float attackSpeed;

    private void Start() {
        BaseState.ExitState += SwitchState;

        patrolState = new PatrolState(this, waypoints);
        chaseState = new ChaseState(this);
        attackState = new AttackState(this);

        SwitchState("PatrolState");
    }

    private void Update() {
        currentState.OnUpdate();
    }

    public void SwitchState(string _stateType) {
        if(currentState != null) {
            currentState.OnEnd();
        }

        switch(_stateType) {
            case "PatrolState":
                currentState = patrolState;
                break;
            case "ChaseState":
                currentState = chaseState;
                break;
            case "AttackState":
                currentState = attackState;
                break;
            default:
                return;
        }

        Debug.Log("Switched State to " + _stateType);

        currentState.OnStart();
    }

}