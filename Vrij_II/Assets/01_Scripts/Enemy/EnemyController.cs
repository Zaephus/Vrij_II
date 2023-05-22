using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    private BaseState<EnemyController> currentState;
    private string state;

    public NavMeshAgent agent;

    public Transform target;

    public float viewDistance;
    public float attackRange;

    private void Start() {

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
                currentState = GetComponent<PatrolState>();
                break;
            case "ChaseState":
                currentState = GetComponent<ChaseState>();
                break;
            case "AttackState":
                currentState = GetComponent<AttackState>();
                break;
            default:
                return;
        }

        state = _stateType;

        Debug.Log("Switched State to " + _stateType);

        currentState.OnStart();
    }

    public void OnTriggerEnter(Collider _other) {
        if(state == "AttackState") {
            Debug.Log("Hit something");
            if(_other.GetComponent<IDamageable>() != null) {
                _other.GetComponent<IDamageable>().Hit();
            }
        }
    }

}