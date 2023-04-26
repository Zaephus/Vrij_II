using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    private PatrolState patrolState = new PatrolState();
    private ChaseState chaseState = new ChaseState();
    private AttackState attackState = new AttackState();

    [SerializeField]
    private Transform target;

    [SerializeField]
    private NavMeshAgent agent;

    private void Start() {
        BaseState.ExitState += SwitchState;
    }

    private void Update() {

    }

    public void SwitchState(System.Type _stateType) {

    }

}