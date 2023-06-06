
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearHeld : MonoBehaviour {

    [SerializeField]
    private GameObject spearOne;
    [SerializeField]
    private GameObject spearTwo;

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

}
