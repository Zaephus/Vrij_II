
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    [SerializeField]
    [NamedArrayAttribute(new string[] { "frontLeftJoint", "frontRightJoint", "backLeftJoint", "backRightJoint" })]
    private List<IKJoint> joints = new List<IKJoint>();

    private IKJoint frontLeftJoint;
    private IKJoint frontRightJoint;
    private IKJoint backLeftJoint;
    private IKJoint backRightJoint;

    [SerializeField]
    private IKJoint tail;

    [SerializeField]
    private float threshold = 8;

    private void Start() {
        frontLeftJoint = joints[0];
        frontRightJoint = joints[1];
        backLeftJoint = joints[2];
        backRightJoint = joints[3];

        StartCoroutine(LegUpdateCoroutine());
    }

    private void FixedUpdate() {

        Vector3 dir = new Vector3(Input.GetAxisRaw("Vertical"), 0, Input.GetAxis("Horizontal"));

        transform.position += dir * Time.deltaTime * 10;
    }

    private void Update() {

        foreach (IKJoint joint in joints) {
            joint.ResolveIK();
        }

        tail?.TryMove(0);
        tail?.ResolveIK();
    }

    private IEnumerator IdleCoroutine() {


        return null;

    }

    private IEnumerator LegUpdateCoroutine() {
        // Run continuously
        while (true) {
            
            // Try moving one diagonal pair of legs
            do {
                frontLeftJoint?.TryMove(threshold);
                backRightJoint?.TryMove(threshold);
                // Wait a frame
                yield return new WaitForSeconds(0.1f);

                // Stay in this loop while either leg is moving.
                // If only one leg in the pair is moving, the calls to TryMove() will let
                // the other leg move if it wants to.
            } while (backLeftJoint.isMoving || frontRightJoint.isMoving);

            // Do the same thing for the other diagonal pair
            do {
                frontRightJoint?.TryMove(threshold);
                backLeftJoint?.TryMove(threshold);
                yield return new WaitForSeconds(0.1f);
            } while (backRightJoint.isMoving || frontLeftJoint.isMoving);

        }
    }

}
