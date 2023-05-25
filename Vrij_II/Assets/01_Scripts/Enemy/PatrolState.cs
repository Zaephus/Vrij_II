
using UnityEngine;

public class PatrolState : BaseState<EnemyController> {

    [SerializeField]
    private Transform[] waypoints;

    private Transform currentWaypoint = null;

    public override void OnStart() {
        if(waypoints.Length > 0) {
            currentWaypoint = waypoints[FindClosestWaypoint()];
        }
        if(currentWaypoint != null) {
            runner.agent.isStopped = false;
            runner.agent.SetDestination(currentWaypoint.position);
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

            Vector3 targetDir = (runner.target.position - runner.transform.position).normalized;
            Debug.DrawLine(transform.position, transform.position + targetDir * 3);

            Ray ray = new Ray(runner.transform.position, targetDir.normalized);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, runner.viewDistance)) {
                if(hit.collider.GetComponent<IDamageable>() != null) {
                    runner.SwitchState("ChaseState");
                }
            }

        }
        
    }

    private int FindClosestWaypoint() {
        Transform closest = waypoints[0];
        int k = 0;
        for(int i = 0; i < waypoints.Length; i++) {
            if(Vector3.Distance(transform.position, closest.position) > Vector3.Distance(transform.position, waypoints[i].position)) {
                closest = waypoints[i];
                k = i;
            }
        }
        return k;
    }

}