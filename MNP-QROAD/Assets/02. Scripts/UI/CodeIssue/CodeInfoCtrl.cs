using UnityEngine;
using System.Collections;

public class CodeInfoCtrl : MonoBehaviour {

    [SerializeField] UILabel _lblCode;
    [SerializeField] UILabel _lblTitle;
    [SerializeField] UILabel _lblExpiredTime;


    long _expiredTick;

    // Use this for initialization
	void Start () {
	
	}
	
    /// <summary>
    /// 
    /// </summary>
    /// <param name="pCode"></param>
    /// <param name="pExpiredTime"></param>
    /// <param name="pHasCode"></param>
    public void  SetCodeInfo(string pCode, long pExpiredTime, bool pHasCode) {
        _lblCode.text = pCode;

        _expiredTick = GameSystem.Instance.ConvertServerTimeTick(pExpiredTime);

        System.DateTime expiredTime = new System.DateTime(_expiredTick);


        _lblExpiredTime.text = GameSystem.Instance.GetLocalizeText(3097).Replace("[n]", expiredTime.Year + " / " + string.Format("{0:D2}", expiredTime.Month) + "/" + string.Format("{0:D2}", expiredTime.Day) + " " + string.Format("{0:D2}", expiredTime.Hour) + ":" + string.Format("{0:D2}", expiredTime.Minute));


        if (pHasCode)
            _lblTitle.text = GameSystem.Instance.GetLocalizeText(3096);
        else
            _lblTitle.text = GameSystem.Instance.GetLocalizeText(4216);

        this.gameObject.SetActive(true);

    }

}
