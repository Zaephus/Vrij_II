using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace Wurm {

    public class AttackState : BaseState<WurmController> {

        [SerializeField]
        private Animator animator;

        [SerializeField]
        private VisualEffect attackEffect;

        [SerializeField]
        private float beforeAttackWaitTime;

        public override void OnStart() {
            StartCoroutine(Attack());
        }

        public override void OnUpdate() {}

        public override void OnEnd() {}

        public void EndAttack() {
            attackEffect.enabled = false;
            runner.SwitchState(WurmState.Moving);
        }

        private IEnumerator Attack() {
            yield return new WaitForSeconds(beforeAttackWaitTime);
            animator.SetTrigger("Attack");
            attackEffect.enabled = true;
        }

    }
    
}