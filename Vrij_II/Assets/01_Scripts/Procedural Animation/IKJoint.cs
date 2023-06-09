using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKJoint : MonoBehaviour {

    // How many times to run the algorithm
    private int iterations = 10;

    // How close the affector needs to be to the target for us to stop doing iterations
    private float accuracy = 0.001f;

    // The control object the arm will bend towards
    [SerializeField]
    private Transform control;

    // Number of bones in the arm
    [SerializeField]
    private int armLength;

    // Target the affector will attempt to move to
    [SerializeField]
    private IKTarget ikTarget;

    private Vector3 target;

    // List of bones
    public Transform[] bones;

    // List of the position offsets of the bones
    private Vector3[] positions;

    // List of the length of the bones
    private float[] boneLengths;

    // Total length of the bones
    private float fullLength;

    // Rotation information
    private Vector3[] startDirections;
    private Quaternion[] startRotations;
    private Quaternion endEffectorStartRotation;

    [HideInInspector]
    public bool isMoving = false;

    private void Start() {
        target = Vector3.zero;
        InitializeLeg();
    }

    public void TryMove(float _threshold) {

        if (isMoving) {
            return;
        }
        
        //check if target is further than threshold
        if (Vector3.Distance(target, ikTarget.transform.position) > _threshold) {

            StartCoroutine(MoveToTarget(ikTarget));
        }
        //ResolveIK();
    }

    private IEnumerator MoveToTarget(IKTarget _target) {

        isMoving = true;

        float elapsedTime = 0;
        float waitTime = 0.1f;
        Vector3 currentPos = target;

        do {
            target = Vector3.Lerp(currentPos, _target.transform.position, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;

            yield return null;
        }
        while (elapsedTime < waitTime);


        isMoving = false;
    }

    public void ResolveIK() {
        //call init if length of leg has changed
        if (boneLengths.Length != armLength) {
            InitializeLeg();
        }

        //get current bone positions for computations
        for (int i = 0; i < bones.Length; i++) {
            positions[i] = bones[i].position;
        }

            //IK computation
            // Can we reach the target?
            float sqrDistanceToTarget = (target - bones[0].position).sqrMagnitude;
            if (sqrDistanceToTarget >= fullLength * fullLength) {
                // Get the direction towards the target
                Vector3 dir = (target - positions[0]).normalized;

                // Distribute bones along the direction towards the target
                for (int i = 1; i < positions.Length; i++)
                    positions[i] = positions[i - 1] + dir * boneLengths[i - 1];
            }
            else {
                for (int iteration = 0; iteration < iterations; iteration++) {
                    // TODO: Backwards Propagation
                    for (int i = positions.Length - 1; i > 0; i--) {
                        if (i == positions.Length - 1) {
                            // Just set the effector to the target position
                            positions[i] = target;
                        }
                        else {
                            // Move the current bone to its new position on the line based on its length and the position of the next bone
                            positions[i] = positions[i + 1] + (positions[i] - positions[i + 1]).normalized * boneLengths[i];
                        }
                    }
                    // TODO: Forwards Propagation
                    for (int i = 1; i < positions.Length; i++) {
                        // This time set the current bone's position to the position on the line between itself and the previous bone, taking its length into consideration
                        positions[i] = positions[i - 1] + (positions[i] - positions[i - 1]).normalized * boneLengths[i - 1];
                    }


                    // Stop iterating if we are close enough according to the accuracy value
                    float sqdistance = (positions[positions.Length - 1] - target).sqrMagnitude;
                    if (sqdistance < accuracy * accuracy)
                        break;
                }
        }



        // Move bone positions towards control object
        if (control != null) {
            // We are only interested in the bones between the first and last one
            for (int i = 1; i < positions.Length - 1; i++) {
                Plane projectionPlane = new Plane(positions[i + 1] - positions[i - 1], positions[i - 1]);
                Vector3 projectedBonePosition = projectionPlane.ClosestPointOnPlane(positions[i]);
                Vector3 projectedControl = projectionPlane.ClosestPointOnPlane(control.position);
                float angleOnPlane = Vector3.SignedAngle(projectedBonePosition - positions[i - 1], projectedControl - positions[i - 1], projectionPlane.normal);
                positions[i] = Quaternion.AngleAxis(angleOnPlane, projectionPlane.normal) * (positions[i] - positions[i - 1]) + positions[i - 1];
            }
        }

        //set transform positions to the computed new positions
        for (int i = 0; i < positions.Length; i++) {
            bones[i].position = positions[i];

            if (i == positions.Length - 1) {
            //bones[i].rotation = target.rotation * Quaternion.Inverse(endEffectorStartRotation) * startRotations[i];
            }
            else
                bones[i].rotation = Quaternion.FromToRotation(startDirections[i], positions[i + 1] - positions[i]) * startRotations[i];
        }

    }

    private void InitializeLeg() {
        //set current target position
        target = transform.position;

        //initialize arrays as leg length + effector
        bones = new Transform[armLength + 1];
        positions = new Vector3[armLength + 1];

        // Init rotation information
        startDirections = new Vector3[armLength + 1]; //initialize bone start directions array
        startRotations = new Quaternion[armLength + 1]; //initialize bone start rotations array

        endEffectorStartRotation = transform.rotation; //save target start rotation

        //last bone is the effector, so it has no lenght
        boneLengths = new float[armLength];

        fullLength = 0;

        Transform cur = transform;
        //initialize array data
        for (int i = bones.Length - 1; i >= 0; i--) {
            bones[i] = cur;
            startRotations[i] = cur.rotation; // NEW Store current bone's initial rotation
            if (i == bones.Length - 1) {
                // Affector bone
                startDirections[i] = target - cur.position; // store the vector from the target position to the effector position
            }
            else {
                // Set the length of this bone equal to the difference of the position of this and the previous bone
                boneLengths[i] = (bones[i + 1].position - cur.position).magnitude;
                // Add current bone's length to the total length of the arm
                fullLength += boneLengths[i];

                startDirections[i] = bones[i + 1].position - cur.position; // store the vector pointing from the current bone to its child
            }

            cur = cur.parent;

        }

    }

    private void OnDrawGizmos() {
        Transform current = this.transform;
        for (int i = 0; i < armLength && current != null && current.parent != null; i++) {
            Debug.DrawLine(current.position, current.parent.position, Color.green);
            current = current.parent;
        }

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, ikTarget.transform.position);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(control.position, 0.1f);

    }

}
