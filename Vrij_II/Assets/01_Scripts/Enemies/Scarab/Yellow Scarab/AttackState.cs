using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YellowScarab {

    public class AttackState : BaseState<ScarabController> {

        [SerializeField]
        private float maxRotateTime;
        [SerializeField]
        private float beforeAttackDelay;
        [SerializeField]
        private float afterAttackDelay;

        [SerializeField]
        private float rotateSpeed;

        [SerializeField]
        private Animator animator;
        
        [SerializeField]
        private float attackSpeed;

        public override void OnStart()
        {
            StartCoroutine(RotateTowards());
        }

        public override void OnUpdate() { }

        public override void OnEnd() { }

        public void StartAttack()
        {
            StartCoroutine(Attack());
        }

        private IEnumerator RotateTowards()
        {

            float timeLeft = maxRotateTime;

            while (timeLeft > 0.0f && runner.state == ScarabState.Attacking)
            {
                transform.forward = Vector3.RotateTowards(transform.forward, (runner.target.transform.position - transform.position).normalized, rotateSpeed * Time.deltaTime, 0.0f);

                timeLeft -= Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            animator.SetTrigger("Attack Start");

        }

        private IEnumerator Attack() {;

            float distLeft = runner.attackRange;

            yield return new WaitForSeconds(beforeAttackDelay);

            while(distLeft > 0.0f) {
                runner.agent.Move(transform.forward * attackSpeed * Time.deltaTime);
                distLeft -= (transform.forward * attackSpeed * Time.deltaTime).magnitude;
                yield return new WaitForEndOfFrame();
            }

            yield return new WaitForSeconds(afterAttackDelay);

            runner.SwitchState(ScarabState.Chasing);

        }

    }

}