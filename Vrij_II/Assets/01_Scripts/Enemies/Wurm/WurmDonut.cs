
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class WurmDonut : MonoBehaviour {

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float upHeight;
    [SerializeField]
    private float downHeight;

    [SerializeField]
    private VisualEffect effect;

    private bool isMoving;

    private void OnTriggerEnter(Collider _other) {
        if(_other.GetComponent<PlayerManager>() != null) {
            StartCoroutine(MoveUp());
        }
    }

    private void OnTriggerExit(Collider _other) {
        if(_other.GetComponent<PlayerManager>() != null) {
            StartCoroutine(MoveDown());
        }
    }

    private IEnumerator MoveUp() {

        isMoving = false;
        yield return new WaitForEndOfFrame();
        isMoving = true;

        effect.Play();

        while(isMoving && transform.localPosition.y < upHeight) {
            Debug.Log("Moving up");
            transform.position += new Vector3(0.0f, moveSpeed * Time.deltaTime, 0.0f);
            yield return new WaitForEndOfFrame();
        }

        effect.Stop();

    }

    private IEnumerator MoveDown() {

        isMoving = false;
        yield return new WaitForEndOfFrame();
        isMoving = true;

        effect.Play();

        while(isMoving && transform.localPosition.y > downHeight) {
            transform.position -= new Vector3(0.0f, moveSpeed * Time.deltaTime, 0.0f);
            yield return new WaitForEndOfFrame();
        }

        effect.Stop();

    }
    
}