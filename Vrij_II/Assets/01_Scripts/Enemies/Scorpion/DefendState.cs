
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace Scorpion {

    public class DefendState : BaseState<ScorpionController> {

        [SerializeField]
        private Animator animator;
        [SerializeField]
        private VisualEffect defendEffect;

        [SerializeField]
        private Collider defendCollider;

        [SerializeField]
        private float defendDelay;
        [SerializeField, Range(0.9f, 2.0f)]
        private float afterDefendDelay;

        public override void OnStart() {
            StartCoroutine(Defend());
        }

        public override void OnUpdate() {}

        public override void OnEnd() {}

        public void EndDefendState() {
            StartCoroutine(EndDefend());
        }

        private IEnumerator Defend() {
            defendEffect.Play();
            yield return new WaitForSeconds(defendDelay);
            animator.SetTrigger("DefendTrigger");
            defendCollider.enabled = true;
        }

        private IEnumerator EndDefend() {

            defendEffect.SendEvent("OnDestroy");
            defendCollider.enabled = false;
            yield return new WaitForSeconds(afterDefendDelay);
            runner.SwitchState(ScorpionState.Idle);

        }

    }

}