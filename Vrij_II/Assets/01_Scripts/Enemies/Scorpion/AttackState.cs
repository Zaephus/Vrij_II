using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scorpion {

    public class AttackState : BaseState<ScorpionController> {

        [SerializeField]
        private ParticleSystem particle;

        [SerializeField]
        private float maxFollowTime;
        [SerializeField]
        private float rotateSpeed;

        [SerializeField]
        private float attackSpeed;

        [SerializeField]
        private Transform tailTargetIK;

        [SerializeField]
        private Transform[] pickupPoints;
        [SerializeField]
        private Transform pathMidPoint;
        [SerializeField]
        private Transform pathEndPoint;

        [SerializeField]
        private float preAttackDelay;
        [SerializeField]
        private float preThrowDelay;
        [SerializeField]
        private float afterThrowDelay;
        [SerializeField]
        private float afterAttackDelay;

        public override void OnStart() {
            StartCoroutine(RotateTowards());
        }

        public override void OnUpdate() {}

        public override void OnEnd() {}

        private IEnumerator RotateTowards() {

            float timeLeft = maxFollowTime;

            while(timeLeft > 0.0f && runner.state == ScorpionState.Attacking) {
                transform.forward = Vector3.RotateTowards(transform.forward, (runner.target.transform.position - transform.position).normalized, rotateSpeed * Time.deltaTime, 0.0f);

                timeLeft -= Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            StartCoroutine(Attack());

        }

        private IEnumerator Attack() {
            
            Vector3 pickupTarget = pickupPoints[Mathf.RoundToInt(Random.value * 2)].position;

            float completion = 0.0f;

            yield return new WaitForSeconds(preAttackDelay);

            while(Vector3.Distance(pickupTarget, tailTargetIK.position) > attackSpeed * Time.deltaTime) {
                
                tailTargetIK.position = Vector3.Lerp(tailTargetIK.position, pickupTarget, completion);
                completion += attackSpeed * Time.deltaTime;

                yield return new WaitForEndOfFrame();

            }

            completion = 0.0f;

            yield return new WaitForSeconds(preThrowDelay);

            while(Vector3.Distance(tailTargetIK.position, pathEndPoint.position) > attackSpeed * Time.deltaTime) {

                Vector3 lerpOne = Vector3.Lerp(pickupTarget, pathMidPoint.position, completion);
                Vector3 lerpTwo = Vector3.Lerp(pathMidPoint.position, pathEndPoint.position, completion);

                tailTargetIK.position = Vector3.Lerp(lerpOne, lerpTwo, completion);

                completion += attackSpeed * Time.deltaTime;

                yield return new WaitForEndOfFrame();

            }

            particle.Play();
            yield return new WaitForSeconds(afterThrowDelay);

            completion = 0.0f;

            while(Vector3.Distance(tailTargetIK.position, pickupPoints[1].position) > attackSpeed * Time.deltaTime) {

                Vector3 lerpOne = Vector3.Lerp(pathEndPoint.position, pathMidPoint.position, completion);
                Vector3 lerpTwo = Vector3.Lerp(pathMidPoint.position, pickupPoints[1].position, completion);

                tailTargetIK.position = Vector3.Lerp(lerpOne, lerpTwo, completion);

                completion += attackSpeed * Time.deltaTime;

                yield return new WaitForEndOfFrame();

            }

            yield return new WaitForSeconds(afterAttackDelay);

            runner.SwitchState(ScorpionState.Idle);

        }

    }

}