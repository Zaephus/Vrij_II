
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace Wurm {

    public class MovingState : BaseState<WurmController> {

        [SerializeField, Tooltip("The time before the Wurm will lock in its target position.")]
        private float followTime = 10.0f;

        [SerializeField]
        private VisualEffect trailEffect;

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

                timeLeft -= Time.deltaTime;
                yield return new WaitForEndOfFrame();

            }

            Vector3 lastPos = transform.position;
            int stationaryCounter = 60;

            yield return new WaitForEndOfFrame();

            while(runner.agent.remainingDistance >= runner.agent.stoppingDistance) {

                if(transform.position == lastPos) {
                    if(stationaryCounter <= 0) {
                        break;
                    }
                    stationaryCounter--;
                }
                else {
                    lastPos = transform.position;
                    stationaryCounter = 60;
                }

                yield return new WaitForEndOfFrame();

            }

            runner.SwitchState(WurmState.Attacking);

        }

    }
    
}