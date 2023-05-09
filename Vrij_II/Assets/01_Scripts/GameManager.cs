using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static System.Action PlayerDied;

    [SerializeField]
    private GameObject level;

    [SerializeField]
    private GameObject gameOverCanvas;

    private void Start() {
        PlayerDied += EndGame;
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
    }

    private void EndGame() {
        level.SetActive(false);
        gameOverCanvas.SetActive(true);
    }
}