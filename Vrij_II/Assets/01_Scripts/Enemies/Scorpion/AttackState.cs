using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scorpion {

    public class AttackState : BaseState<ScorpionController> {

        [SerializeField]
        private float maxFollowTime;

        [SerializeField]
        private float rotateSpeed;

        [SerializeField]
        private ParticleSystem particle;

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
            yield return null;
            // particle.Play();
        }

    }

}