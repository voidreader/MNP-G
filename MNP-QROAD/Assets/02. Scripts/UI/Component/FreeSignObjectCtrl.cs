using UnityEngine;

public class FreeSignObjectCtrl : MonoBehaviour {

    public GameObject _freeSignSprite;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pFlag"></param>
    public void OnFreeSign(bool pFlag) {
        _freeSignSprite.SetActive(pFlag);
    }



}
