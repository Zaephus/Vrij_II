using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PatrolState : BaseState {

    private Transform[] waypoints;

    private Transform currentWaypoint;

    public PatrolState(EnemyController _e, Transform[] _waypoints) : base(_e) {
        waypoints = _waypoints;
        currentWaypoint = waypoints[0];
    }

    public override void OnStart() {
        enemyController.agent.SetDestination(currentWaypoint.position);
        enemyController.agent.isStopped = false;
    }

    public override void OnUpdate() {
        Navigate();
        CheckTarget();
    }

    public override void OnEnd() {
        enemyController.agent.isStopped = true;
    }

    private void Navigate() {

        Vector3 comparePos = enemyController.transform.position;
        comparePos.y = currentWaypoint.position.y;

        if(Vector3.Distance(comparePos, currentWaypoint.position) <= enemyController.agent.stoppingDistance) {
            int index = (System.Array.IndexOf(waypoints, currentWaypoint)  + 1) % waypoints.Length;
            currentWaypoint = waypoints[index];
            enemyController.agent.SetDestination(currentWaypoint.position);
        }

    }

    private void CheckTarget() {
        
        if(Vector3.Distance(enemyController.transform.position, enemyController.target.position) <= enemyController.viewDistance) {

            Vector3 targetDir = enemyController.target.position - enemyController.transform.position;

            Ray ray = new Ray(enemyController.transform.position, targetDir.normalized);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, enemyController.viewDistance)) {
                if(hit.collider.GetComponent<PlayerMovement>() != null) {
                    enemyController.SwitchState("ChaseState");
                }
            }

        }
        
    }

}