using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Scarab;

public class ScarabController : MonoBehaviour {

    private BaseState<ScarabController> currentState;
    private ScarabState state;

    public NavMeshAgent agent;

    public Transform target;

    public float viewDistance;
    public float attackRange;

    private void Start() {
        SwitchState(ScarabState.Patrolling);
        if(target == null) {
            target = FindAnyObjectByType<PlayerManager>().transform;
        }
    }

    private void Update() {
        currentState.OnUpdate();
    }

    public void SwitchState(ScarabState _state) {
        if(currentState != null) {
            currentState.OnEnd();
        }

        switch(_state) {
            case ScarabState.Patrolling:
                currentState = GetComponent<PatrolState>();
                break;
            case ScarabState.Chasing:
                currentState = GetComponent<ChaseState>();
                break;
            case ScarabState.Attacking:
                currentState = GetComponent<AttackState>();
                break;
            default:
                return;
        }

        state = _state;

#if UNITY_EDITOR
        Debug.Log("Switched State to " + _state);
#endif

        currentState.OnStart();
    }

    public void OnTriggerEnter(Collider _other) {
        if(state == ScarabState.Attacking) {
            if(_other.GetComponent<IDamageable>() != null) {
                _other.GetComponent<IDamageable>().Hit();
            }
        }
    }

}