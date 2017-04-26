using UnityEngine;
using System.Collections;

public class BossHPCtrl : MonoBehaviour {


    public float maxHP; // 최대 HP 
    public float currentHP; // 현 HP
    public UISlider nekoHPBar = null;

    /// <summary>
    /// Neko HP 값 조정 
    /// </summary>
    /// <param name="pMaxHP">P max H.</param>
    /// <param name="pCurHP">P current H.</param>
    public void SetNekoHP(float pMaxHP, float pCurHP) {
        maxHP = pMaxHP;
        currentHP = pCurHP;
        nekoHPBar.value = currentHP / maxHP;
    }


    public void HideNekoHP() {
        this.gameObject.SetActive(false);
    }
}
