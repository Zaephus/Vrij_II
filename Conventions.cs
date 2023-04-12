using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Public enums should be placed in a separate file called Enums.
public enum ConventionType {                            // Enum values use Pascal Casing.
    TypeOne = 0,                                        // If there are more than three values or they are numbered, they should be placed below eachother.
    TypeTwo = 2
}

// Public interfaces should be placed in a separate file called Interfaces.

public class Conventions : MonoBehaviour {              // Class names with Pascal Casing;

    // Public or Private identifier required.

    // Variable names should be descriptive: itemAmount instead of hoi.
    // Do not use single letter names for variables.

    public Action HasDoneSomething;                     // Actions or Events need to be named in the past tense.

    public float WaitTime { get; set; }                 // Properties with Pascal Casing;
    public float waitTime;                              // Variables with Camel Casing;

    private bool canJump;
    private bool isJumping;                             // Names of booleans need to be closed-ended questions. They can only be true or false.

    private ConventionType conventionType;

    private BackgroundLayer[] backgroundLayers;         // Names of arrays and lists should be a multiple.

    // Unity game loop functions at the top; Awake, OnEnable, Start, Update, FixedUpdate.
    private void Start() {

    } 

    private void Update() {

    }

    // Function names should be descriptive of what they do.
    // Functions should do only a single thing.

    private void DoTheFirstThing() {
        float timeToWait = Random.Range(0, 2);          // Local variables also with Camel Casing.

        if(timeToWait < 1) {                            // If statements always use curly brackets, and the body is on the next line.
            DoTheSecondThing(timeToWait);
        }
        else if(timeToWait >= 1) {
            DoTheThirdThing()
        }
        
    }

    private void DoTheSecondThing(float _waitTime) {    // Parameters with Camel Casing with Underscore.

        switch(conventionType) {

            case ConventionType.TypeOne:                // Bodies of cases in switch statements should be indented.
                WaitTime = _waitTime;
                break;

            case ConventionType.TypeTwo:
                WaitTime = -_waitTime;
                break;

        }

    }

    private void DoTheThirdThing(float _waitTime) {

        for(int i = 0; i < _waitTime; i++) {
            for(int j = 0; j < _waitTime; j++) {
                // Use i and j for normal for loops.
            }
        }

        for(int x = 0; x < _waitTime; x++) {
            for(int y = 0; y < _waitTime; y++) {
                // Use x and y for for loops that handle position.
            }
        }

    }

}