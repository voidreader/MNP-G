using UnityEngine;
using System.Collections;
using PathologicalGames;

public class StageFallingObjectCtrl : MonoBehaviour {

    void OnTriggerEnter(Collider pCol) {
        if (pCol.CompareTag("StageFence")) {
            this.gameObject.SetActive(false);
        }
    }

    void OnBecameInvisible() {
        this.gameObject.SetActive(false);
    }
}
