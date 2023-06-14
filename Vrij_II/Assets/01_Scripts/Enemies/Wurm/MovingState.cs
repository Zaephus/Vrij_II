
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace Wurm {

    public class MovingState : BaseState<WurmController> {

        [SerializeField, Tooltip("The time before the Wurm will lock in its target position.")]
        private float followTime = 10.0f;

        public VisualEffect trailEffect;

        [SerializeField]
        private float beforeAttackWaitTime;

        public override void OnStart() {
            StartCoroutine(MoveToTarget());
            trailEffect.enabled = true;
        }

        public override void OnUpdate() {}

        public override void OnEnd() {
            trailEffect.enabled = false;
        }

        private IEnumerator MoveToTarget() {

            float timeLeft = followTime;
            Vector3 targetPos = runner.target.position;

            while(timeLeft > 0.0f) {
                targetPos = runner.target.position;
                runner.agent.SetDestination(targetPos);

                if(runner.agent.velocity.magnitude <= Mathf.Epsilon) {
                    trailEffect.enabled = false;
                }
                else {
                    trailEffect.enabled = true;
                }

                timeLeft -= Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            if(Vector3.Distance(targetPos, transform.position) > runner.range) {
                runner.SwitchState(WurmState.Idle);
                yield break;
            }

            int stationaryCounter = 60;

            yield return new WaitForEndOfFrame();

            while(runner.agent.remainingDistance >= runner.agent.stoppingDistance) {

                if(runner.agent.velocity.magnitude <= Mathf.Epsilon) {
                    if(stationaryCounter <= 0) {
                        runner.agent.ResetPath();
                        break;
                    }
                    stationaryCounter--;
                }
                else {
                    stationaryCounter = 60;
                }

                yield return new WaitForEndOfFrame();

            }

            while(runner.agent.velocity.magnitude >= Mathf.Epsilon) {
                yield return new WaitForEndOfFrame();
            }
            
            trailEffect.enabled = false;
            
            yield return new WaitForSeconds(beforeAttackWaitTime);

            if(runner.state == WurmState.Moving) {
                runner.SwitchState(WurmState.Attacking);
            }

        }

    }
    
}