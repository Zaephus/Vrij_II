
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YellowScarab {

    public class ChaseState : BaseState<ScarabController> {

        [SerializeField]
        private float chaseSpeed;

        public override void OnStart() {
            runner.agent.SetDestination(runner.target.position);
            runner.agent.isStopped = false;
            runner.agent.speed = chaseSpeed;
        }

        public override void OnUpdate() {
            Chase();
            CheckAttack();
        }

        public override void OnEnd() {
            runner.agent.isStopped = true;
        }

        private void Chase() {

            CheckTarget();

            if(Vector3.Distance(runner.agent.destination, runner.target.position) > 1.0f) {
                runner.agent.SetDestination(runner.target.position);
            }

            if(Vector3.Distance(transform.position, runner.target.position) >= runner.viewDistance) {
                runner.SwitchState(ScarabState.Patrolling);
            }

            if(Vector3.Distance(transform.position, runner.manager.transform.position) >= runner.manager.globalRange) {
                runner.SwitchState(ScarabState.Patrolling);
            }

        }

        private void CheckTarget() {
            
            Vector3 targetPos = new Vector3(runner.target.position.x, transform.position.y, runner.target.position.z);
            Vector3 targetDir = (targetPos - runner.transform.position).normalized;
            Debug.DrawLine(transform.position, transform.position + targetDir * runner.viewDistance);

            Ray ray = new Ray(runner.transform.position, targetDir.normalized);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, runner.viewDistance)) {
                if(hit.collider.GetComponent<PlayerManager>() != null) {
                    return;
                }
            }

            runner.SwitchState(ScarabState.Patrolling);
            
        }

        private void CheckAttack() {

            if(Vector3.Distance(runner.transform.position, runner.target.position) <= runner.attackRange) {
                runner.SwitchState(ScarabState.Attacking);
            }

        }   

    }

}