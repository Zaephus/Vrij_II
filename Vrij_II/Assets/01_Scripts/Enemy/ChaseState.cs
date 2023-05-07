using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : BaseState {

    public ChaseState(EnemyController _e) : base(_e)  {}

    public override void OnStart() {
        enemyController.agent.SetDestination(enemyController.target.position);
        enemyController.agent.isStopped = false;
    }

    public override void OnUpdate() {
        Chase();
        CheckAttack();
    }

    public override void OnEnd() {
        enemyController.agent.isStopped = true;
    }

    private void Chase() {

        if(Vector3.Distance(enemyController.agent.destination, enemyController.target.position) > 1.0f) {
            enemyController.agent.SetDestination(enemyController.target.position);
        }

        if(Vector3.Distance(enemyController.transform.position, enemyController.target.position) >= enemyController.viewDistance) {
            enemyController.SwitchState("PatrolState");
        }

    }

    private void CheckAttack() {

        if(Vector3.Distance(enemyController.transform.position, enemyController.target.position) <= enemyController.attackRange) {
            enemyController.SwitchState("AttackState");
        }

    }   

}