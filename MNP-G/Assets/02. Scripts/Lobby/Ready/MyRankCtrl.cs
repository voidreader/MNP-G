using UnityEngine;
using System.Collections;

public class MyRankCtrl : MonoBehaviour {

	[SerializeField] UISprite _spMainNeko;
	[SerializeField] UILabel _lblScore;
	[SerializeField] UILabel _lblName;
	[SerializeField] UILabel _lblRank;
	
    [SerializeField] int _score;

    [SerializeField] UITexture spPic;
    [SerializeField] UISprite _spNekoBox;

	int _mainNekoID; 
	
	// Use this for initialization
	void Start () {
		
	}



    /// <summary>
    /// 페이스북용 랭킹 설정 
    /// </summary>
    /// <param name="fbScore"></param>
    /// <param name="pRank"></param>
    public void SetFBRankInfo(FB_Score fbScore, int pRank) {

        //_fbUser = friend;
        spPic.gameObject.SetActive(true);
        _spNekoBox.gameObject.SetActive(false);

        if (fbScore.GetProfileImage(FB_ProfileImageSize.square) == null) {
            fbScore.OnProfileImageLoaded += OnProfileImageLoaded;
            fbScore.LoadProfileImage(FB_ProfileImageSize.square);
        }

        _score = fbScore.value;
        _lblScore.text = GameSystem.Instance.GetNumberToString(fbScore.value) + GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L4334);
        _lblRank.text = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3215).Replace("[n]", pRank.ToString());
        _lblName.text = fbScore.UserName;

    }

    private void OnProfileImageLoaded(FB_Score fbScore) {
        spPic.mainTexture = fbScore.GetProfileImage(FB_ProfileImageSize.square);
        fbScore.OnProfileImageLoaded -= OnProfileImageLoaded;
    }


    /// <summary>
    /// Sets the rank info.
    /// </summary>
    /// <param name="pNekoID">P neko I.</param>
    /// <param name="pScore">P score.</param>
    /// <param name="pRank">P rank.</param>
    /// <param name="pName">P name.</param>
    public void SetRankInfo(int pNekoID, int pGrade, int pScore, int pRank, string pName) {

        spPic.gameObject.SetActive(false);
        _spNekoBox.gameObject.SetActive(true);

        _mainNekoID = pNekoID;
		
		if (_mainNekoID < 0) {
			_mainNekoID = Random.Range(0, 80);
		}

        
        GameSystem.Instance.SetNekoSprite(_spMainNeko, pNekoID, pGrade);

        _lblScore.text = GameSystem.Instance.GetNumberToString(pScore) + GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L4334);
        _lblRank.text = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3215).Replace("[n]", pRank.ToString());

        _lblName.text = pName;
	}
}
