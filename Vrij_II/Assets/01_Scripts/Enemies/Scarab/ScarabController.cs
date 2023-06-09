using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Scarab;

public class ScarabController : MonoBehaviour, IDamageable {

    private BaseState<ScarabController> currentState;
    private ScarabState state;

    [HideInInspector]
    public ScarabManager manager;

    public NavMeshAgent agent;

    public Transform target = null;

    public float viewDistance;
    public float attackRange;

    [SerializeField]
    private float health;

    private void Start() {
        SwitchState(ScarabState.Patrolling);
        if(manager == null) {
            manager = FindAnyObjectByType<ScarabManager>();
        }
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

    public void Hit(float _dmg) {
        health -= _dmg;
        if(health <= 0.0f) {
            Die();
        }
    }

    public void Die() {
        Destroy(gameObject);
    }

    public void OnTriggerEnter(Collider _other) {
        if(state == ScarabState.Attacking) {
            if(_other.GetComponent<PlayerManager>() != null) {
                _other.GetComponent<PlayerManager>().Hit();
            }
        }
    }

}