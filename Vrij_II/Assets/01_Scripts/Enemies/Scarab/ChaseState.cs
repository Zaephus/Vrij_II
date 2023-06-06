using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scarab {

    public class ChaseState : BaseState<ScarabController> {

        public override void OnStart() {
            runner.agent.SetDestination(runner.target.position);
            runner.agent.isStopped = false;
        }

        public override void OnUpdate() {
            Chase();
            CheckAttack();
        }

        public override void OnEnd() {
            runner.agent.isStopped = true;
        }

        private void Chase() {

            if(Vector3.Distance(runner.agent.destination, runner.target.position) > 1.0f) {
                runner.agent.SetDestination(runner.target.position);
            }

            if(Vector3.Distance(runner.transform.position, runner.target.position) >= runner.viewDistance) {
                runner.SwitchState(ScarabState.Patrolling);
            }

        }

        private void CheckAttack() {

            if(Vector3.Distance(runner.transform.position, runner.target.position) <= runner.attackRange) {
                runner.SwitchState(ScarabState.Attacking);
            }

        }   

    }

}