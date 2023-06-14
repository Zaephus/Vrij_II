
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Wurm;

public class WurmController : MonoBehaviour {

    private BaseState<WurmController> currentState;
    [ReadOnly]
    public WurmState state;

    public NavMeshAgent agent;

    public Transform target;

    [HideInInspector]
    public float range;

    public float health;

    public void Start() {
        SwitchState(WurmState.Idle);
        if(target == null) {
            target = FindAnyObjectByType<PlayerManager>().transform;
        }
        range = GetComponent<SphereCollider>().radius;

        AttackState attackState = GetComponent<AttackState>();
        attackState.attackEffect.Reinit();

        MovingState movingState = GetComponent<MovingState>();
        movingState.trailEffect.Reinit();
    }

    public void Update() {}

    public void SwitchState(WurmState _state) {
        if(currentState != null) {
            currentState.OnEnd();
        }

        switch(_state) {
            case WurmState.Idle:
                currentState = GetComponent<IdleState>();
                break;
            case WurmState.Moving:
                currentState = GetComponent<MovingState>();
                break;
            case WurmState.Attacking:
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

    private void OnTriggerEnter(Collider _other) {
        Debug.Log("collided");
        if(_other.GetComponent<PlayerManager>() != null) {
            SwitchState(WurmState.Moving);
        }
    }

    private void OnTriggerExit(Collider _other) {
        if(_other.GetComponent<PlayerManager>() != null) {
            SwitchState(WurmState.Idle);
        }
    }

}