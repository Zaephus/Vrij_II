using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttackState : BaseState<EnemyController> {

    [SerializeField]
    private float beforeAttackDelay;
    [SerializeField]
    private float afterAttackDelay;
    
    [SerializeField]
    private float attackSpeed;

    public override void OnStart() {
        runner.StartCoroutine(Attack());
    }

    public override void OnUpdate() {}

    public override void OnEnd() {}

    // TODO: Add rotation to attack.
    private IEnumerator Attack() {

        Vector3 targetDir = (runner.target.position - runner.transform.position).normalized;

        float distLeft = runner.attackRange;

        yield return new WaitForSeconds(beforeAttackDelay);

        while(distLeft > 0.0f) {
            runner.agent.Move(targetDir * attackSpeed * Time.deltaTime);
            distLeft -= (targetDir * attackSpeed * Time.deltaTime).magnitude;
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(afterAttackDelay);

        runner.SwitchState("ChaseState");

    }

}