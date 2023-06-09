
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    [SerializeField]
    [NamedArrayAttribute(new string[] { "frontLeftJoint", "frontRightJoint", "middleLeftJoint", "middleRightJoint", "backLeftJoint", "backRightJoint" })]
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
                joints[0].TryMove(threshold);
                joints[3].TryMove(threshold);
                joints[5].TryMove(threshold);
                // Wait a frame
                yield return new WaitForSeconds(0.1f);

                // Stay in this loop while either leg is moving.
                // If only one leg in the pair is moving, the calls to TryMove() will let
                // the other leg move if it wants to.
            } while (joints[0].isMoving || joints[3].isMoving || joints[5].isMoving);

            // Do the same thing for the other diagonal pair
            do {
                joints[1].TryMove(threshold);
                joints[2].TryMove(threshold);
                joints[4].TryMove(threshold);
               
                yield return new WaitForSeconds(0.1f);
            } while (joints[1].isMoving || joints[2].isMoving || joints[4].isMoving);

        }
    }

}
