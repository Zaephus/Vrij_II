
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverState : BaseState<GameManager> {

    [SerializeField]
    private GameObject gameOverMenu;

    [SerializeField]
    private GameObject deathEffectCanvas;
    [SerializeField]
    private Material deathMaterial;
    [SerializeField]
    private float deathEffectSpeed = 2.0f;

    public override void OnStart() {
        StartCoroutine(DeathEffect());
    }

    public override void OnUpdate() {}

    public override void OnEnd() {
        gameOverMenu.SetActive(false);
    }

    private IEnumerator DeathEffect() {
        
        float completion = 0.0f;

        deathEffectCanvas.SetActive(true);
        
        while(completion < 1.0f) {
            deathMaterial.SetFloat("_Desolve", completion);
            completion += (1 / deathEffectSpeed) * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        
        deathEffectCanvas.SetActive(false);
        Destroy(GetComponent<RunningState>().level);
        gameOverMenu.SetActive(true);

    }

}