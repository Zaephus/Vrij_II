
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearHeld : MonoBehaviour {

    [SerializeField]
    private GameObject spearOne;
    [SerializeField]
    private GameObject spearTwo;
    [SerializeField]
    private Collider hitbox;

    public void SwitchSpear(SpearType _spearType) {

        if (_spearType == SpearType.One) {
            spearOne.SetActive(true);
            spearTwo.SetActive(false);
        }

        if (_spearType == SpearType.Two) {
            spearOne.SetActive(false);
            spearTwo.SetActive(true);
        }
    }

    public IEnumerator Stab() {
        hitbox.enabled = true;
        yield return new WaitForSeconds(1f);

        hitbox.enabled = false;
        yield return false;
    }

    private void OnTriggerEnter(Collider other) {
        IDamageable target;
        if (other.TryGetComponent<IDamageable>(out target)) {
            target.Hit(1f);
        }
    }

}
