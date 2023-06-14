using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scorpion;

public class ScorpionController : MonoBehaviour, IDamageable {

    private BaseState<ScorpionController> currentState;
    [ReadOnly]
    public ScorpionState state;

    public Transform target = null;

    public float viewDistance;
    public float defendRange;

    [SerializeField, Range(0, 1)]
    private float defendChance;

    private bool targetInDefendRange = false;

    [SerializeField]
    private float health;

    private void Start() {
        SwitchState(ScorpionState.Idle);
        if(target == null) {
            target = FindAnyObjectByType<PlayerManager>().transform;
        }
    }

    private void Update() {
        currentState.OnUpdate();

        if(Vector3.Distance(target.position, transform.position) <= defendRange) {
            if(!targetInDefendRange) {
                PlayerManager.ThrowAction += Defend;
                PlayerManager.StabAction += Defend;
            }
            targetInDefendRange = true;
        }
        else {
            if(targetInDefendRange) {
                PlayerManager.ThrowAction -= Defend;
                PlayerManager.StabAction -= Defend;
            }
            targetInDefendRange = false;
        }

    }

    public void SwitchState(ScorpionState _state) {
        if(currentState != null) {
            currentState.OnEnd();
        }

        switch(_state) {
            case ScorpionState.Idle:
                currentState = GetComponent<IdleState>();
                break;
            case ScorpionState.Defending:
                currentState = GetComponent<DefendState>();
                break;
            case ScorpionState.Attacking:
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

    private void Defend() {
        if(Random.value <= defendChance) {
            SwitchState(ScorpionState.Defending);
        }
    }

}