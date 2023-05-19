using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class PatrolState : BaseState<EnemyController> {

    [SerializeField]
    private Transform[] waypoints;

    private Transform currentWaypoint;

    public override void OnStart() {
        if(waypoints.Length > 0) {
            currentWaypoint = waypoints[0];
        }
        if(currentWaypoint != null) {
            runner.agent.SetDestination(currentWaypoint.position);
            runner.agent.isStopped = false;
        }
    }

    public override void OnUpdate() {
        if(currentWaypoint != null) {
            Navigate();
        }
        CheckTarget();
    }

    public override void OnEnd() {
        runner.agent.isStopped = true;
    }

    private void Navigate() {

        Vector3 comparePos = runner.transform.position;
        comparePos.y = currentWaypoint.position.y;

        if(Vector3.Distance(comparePos, currentWaypoint.position) <= runner.agent.stoppingDistance) {
            int index = (System.Array.IndexOf(waypoints, currentWaypoint)  + 1) % waypoints.Length;
            currentWaypoint = waypoints[index];
            runner.agent.SetDestination(currentWaypoint.position);
        }

    }

    private void CheckTarget() {
        
        if(Vector3.Distance(runner.transform.position, runner.target.position) <= runner.viewDistance) {

            Vector3 targetDir = runner.target.position - runner.transform.position;

            Ray ray = new Ray(runner.transform.position, targetDir.normalized);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, runner.viewDistance)) {
                if(hit.collider.GetComponent<IDamageable>() != null) {
                    runner.SwitchState("ChaseState");
                }
            }

        }
        
    }

}