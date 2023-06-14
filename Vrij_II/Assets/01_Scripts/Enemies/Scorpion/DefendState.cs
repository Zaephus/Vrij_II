
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scorpion {

    public class DefendState : BaseState<ScorpionController> {

        [SerializeField]
        private Animator animator;

        [SerializeField]
        private float defendDelay;

        public override void OnStart() {
            StartCoroutine(Defend());
        }

        public override void OnUpdate() {}

        public override void OnEnd() {}

        public void EndDefendState() {
            runner.SwitchState(ScorpionState.Idle);
        }

        private IEnumerator Defend() {
            yield return new WaitForSeconds(defendDelay);
            animator.SetTrigger("DefendTrigger");
        }

    }

}