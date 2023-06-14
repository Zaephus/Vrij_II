using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using YellowScarab;
using RedScarab;

public class ScarabController : MonoBehaviour, IDamageable {

    public enum ScarabType {
        Yellow,
        Red
    }

    [SerializeField]
    private ScarabType type;

    private BaseState<ScarabController> currentState;
    [HideInInspector]
    public ScarabState state;

    public ScarabManager manager;

    public NavMeshAgent agent;

    public Transform target = null;

    public float viewDistance;
    public float attackRange;

    [SerializeField]
    private float health;

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

        if(type == ScarabType.Yellow) {
            switch(_state) {
                case ScarabState.Patrolling:
                    currentState = GetComponent<YellowScarab.PatrolState>();
                    break;
                case ScarabState.Chasing:
                    currentState = GetComponent<YellowScarab.ChaseState>();
                    break;
                case ScarabState.Attacking:
                    currentState = GetComponent<YellowScarab.AttackState>();
                    break;
                default:
                    return;
            }
        }
        else if(type == ScarabType.Red) {
            switch(_state) {
                case ScarabState.Patrolling:
                    currentState = GetComponent<RedScarab.PatrolState>();
                    break;
                case ScarabState.Chasing:
                    currentState = GetComponent<RedScarab.ChaseState>();
                    break;
                case ScarabState.Attacking:
                    currentState = GetComponent<RedScarab.AttackState>();
                    break;
                default:
                    return;
            }
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