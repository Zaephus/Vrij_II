using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState {

    public AttackState(EnemyController _e) : base(_e)  {}

    public override void OnStart() {
        enemyController.StartCoroutine(Attack());
    }

    public override void OnUpdate() {}

    public override void OnEnd() {}

    // TODO: Add rotation to attack.
    private IEnumerator Attack() {

        Vector3 targetDir = (enemyController.target.position - enemyController.transform.position).normalized;

        float distLeft = enemyController.attackRange;

        yield return new WaitForSeconds(enemyController.beforeAttackDelay);

        while(distLeft > 0.0f) {
            enemyController.agent.Move(targetDir * enemyController.attackSpeed * Time.deltaTime);
            distLeft -= (targetDir * enemyController.attackSpeed * Time.deltaTime).magnitude;
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(enemyController.afterAttackDelay);

        enemyController.SwitchState("ChaseState");

    }

}