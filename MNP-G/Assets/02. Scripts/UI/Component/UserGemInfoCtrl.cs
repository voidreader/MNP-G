using UnityEngine;
using System.Collections;
using SimpleJSON;

public class UserGemInfoCtrl : MonoBehaviour {

	[SerializeField] UILabel _purchaseValue;
    [SerializeField] UILabel _freeValue;
      


    public void SetValue(JSONNode pNode) {

        this.gameObject.SetActive(true);

        _purchaseValue.text = GameSystem.Instance.GetNumberToString(pNode["cash"].AsInt);
        _freeValue.text = GameSystem.Instance.GetNumberToString(pNode["free"].AsInt);
    }
}
