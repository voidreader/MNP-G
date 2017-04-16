using UnityEngine;
using System.Collections;

public class RankColumnCtrl : MonoBehaviour {

	[SerializeField] UISprite _spMainNeko;
	[SerializeField] UILabel _lblScore;
	[SerializeField] UILabel _lblName;
	[SerializeField] UILabel _lblRank;
	[SerializeField] UISprite _spRankNumber;
    [SerializeField] UISprite _spRankBar;
    [SerializeField] int _score;

    [SerializeField] UITexture spPic;
    [SerializeField] UISprite _spNekoBox;

	int _mainNekoID;

    [SerializeField]
    int _userdbkey;


    readonly string _topBar = "top-bar";
    readonly string _myBar = "my-bar";

    readonly string _1stMark = "med-1";
    readonly string _2ndMark = "med-2";
    readonly string _3rdMark = "med-3";


    readonly string _pinkBoxSprite = "frm_profile_box_pink";
    readonly string _blueBoxSprite = "frm_profile_box_blue";
    readonly string _greenBoxSprite = "frm_profile_box_green";
    readonly string _yellowBoxSprite = "frm_profile_box_yellow";

    // Use this for initialization
    void Start () {
	
	}


    #region FB

    public void SetFBRankInfo(FB_Score fbScore, int pRank) {

        //_fbUser = friend;
        spPic.gameObject.SetActive(true);
        _spNekoBox.gameObject.SetActive(false);
        _spRankBar.gameObject.SetActive(false);
        _spRankNumber.gameObject.SetActive(false);
       

  		if (fbScore.GetProfileImage(FB_ProfileImageSize.square) == null) {
            fbScore.OnProfileImageLoaded += OnProfileImageLoaded;
            fbScore.LoadProfileImage(FB_ProfileImageSize.square);
		}

        _score = fbScore.value;
        _lblScore.text = GameSystem.Instance.GetNumberToString(fbScore.value) + GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L4334);
        _lblRank.text = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3215).Replace("[n]", pRank.ToString());

        _lblName.text = fbScore.UserName;

        if (_score == 0)
            return;


        // 1,2,3등은 마커가 붙는다.
        if (pRank == 1) {
            _spRankNumber.gameObject.SetActive(true);
            _spRankNumber.spriteName = _1stMark;

            _spRankBar.gameObject.SetActive(true);
            _spRankBar.spriteName = _topBar;
            _spNekoBox.spriteName = _pinkBoxSprite;
        }
        else if (pRank == 2) {
            _spRankNumber.gameObject.SetActive(true);
            _spRankNumber.spriteName = _2ndMark;

            _spRankBar.gameObject.SetActive(true);
            _spRankBar.spriteName = _topBar;
            _spNekoBox.spriteName = _blueBoxSprite;
        }
        else if (pRank == 3) {
            _spRankNumber.gameObject.SetActive(true);
            _spRankNumber.spriteName = _3rdMark;

            _spRankBar.gameObject.SetActive(true);
            _spRankBar.spriteName = _topBar;
            _spNekoBox.spriteName = _greenBoxSprite;
        }
        else {
            _spRankNumber.gameObject.SetActive(false);
            _spRankNumber.gameObject.SetActive(false);
            _spNekoBox.spriteName = _yellowBoxSprite;
        }

    }

    private void OnProfileImageLoaded(FB_Score fbScore) {
        spPic.mainTexture = fbScore.GetProfileImage(FB_ProfileImageSize.square);
        fbScore.OnProfileImageLoaded -= OnProfileImageLoaded;
    }


    #endregion


    /// <summary>
    /// Sets the rank info.
    /// </summary>
    /// <param name="pNekoID">P neko I.</param>
    /// <param name="pScore">P score.</param>
    /// <param name="pRank">P rank.</param>
    /// <param name="pName">P name.</param>
    public void SetRankInfo(int pNekoID, int pNekoGrade, int pScore, int pRank, string pName, int pUserDBKey) {

        spPic.gameObject.SetActive(false);
        _spNekoBox.gameObject.SetActive(true);
        _spRankBar.gameObject.SetActive(false);

        _mainNekoID = pNekoID;
        _userdbkey = pUserDBKey;


        if (_mainNekoID < 0) {
			_mainNekoID = Random.Range(0, 80);
		}



        //_spMainNeko.spriteName = GameSystem.Instance.NekoBaseInfo.get<string> (_mainNekoID.ToString (), "rect_sprite");
        //_spMainNeko.atlas = GameSystem.Instance.GetNekoUIAtlas(GameSystem.Instance.NekoBaseInfo.get<string>(_mainNekoID.ToString(), "collection_name"));
        //_spMainNeko.spriteName = "neko" + (_mainNekoID + 1).ToString() + "-1";
        GameSystem.Instance.SetNekoSprite(_spMainNeko, pNekoID, pNekoGrade);



        _lblScore.text = GameSystem.Instance.GetNumberToString(pScore) + GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L4334);
        _lblRank.text = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3215).Replace("[n]", pRank.ToString());

        _lblName.text = pName;

		// 1,2,3등은 마커가 붙는다.
		if (pRank == 1) {
            _spRankNumber.gameObject.SetActive(true);
            _spRankNumber.spriteName = _1stMark;

            _spRankBar.gameObject.SetActive (true);
            _spRankBar.spriteName = _topBar;
            _spNekoBox.spriteName = _pinkBoxSprite;
		} else if (pRank == 2) {
            _spRankNumber.gameObject.SetActive(true);
            _spRankNumber.spriteName = _2ndMark;

            _spRankBar.gameObject.SetActive (true);
            _spRankBar.spriteName = _topBar;
            _spNekoBox.spriteName = _blueBoxSprite;
        } else if (pRank == 3) {
            _spRankNumber.gameObject.SetActive(true);
            _spRankNumber.spriteName = _3rdMark;

            _spRankBar.gameObject.SetActive (true);
            _spRankBar.spriteName = _topBar;
            _spNekoBox.spriteName = _greenBoxSprite;
        } else {
            _spRankNumber.gameObject.SetActive(false);
            _spRankNumber.gameObject.SetActive(false);
            _spNekoBox.spriteName = _yellowBoxSprite;
        }

	}


    public void SetMyAroundRank() {
        _spRankBar.gameObject.SetActive(true);
        _spRankBar.spriteName = _topBar;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="pNekoID"></param>
    /// <param name="pNekoGrade"></param>
    /// <param name="pScore"></param>
    /// <param name="pRank"></param>
    /// <param name="pName"></param>
    /// <param name="pMe"></param>
    public void SetAroundRankInfo(int pNekoID, int pNekoGrade, int pScore, int pRank, string pName, int pUserDBKey, bool pMe = false) {

        //Debug.Log("★SetAroundRankInfo")

        spPic.gameObject.SetActive(false);
        _spRankNumber.gameObject.SetActive(false);
        _spNekoBox.gameObject.SetActive(true);
        _spNekoBox.spriteName = _yellowBoxSprite;
        _spRankBar.gameObject.SetActive(false);

        _mainNekoID = pNekoID;
        _userdbkey = pUserDBKey;

        if (_mainNekoID < 0) {
            _mainNekoID = Random.Range(0, 80);
        }


        GameSystem.Instance.SetNekoSprite(_spMainNeko, pNekoID, pNekoGrade);



        _lblScore.text = GameSystem.Instance.GetNumberToString(pScore) + GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L4334);
        _lblRank.text = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3215).Replace("[n]", pRank.ToString());
        _lblName.text = pName;

    
        if(pMe) {
            _spRankBar.gameObject.SetActive(true);
            _spRankBar.spriteName = _topBar;
        }

    }

    public void OpenRecord() {

        
        // Debug.Log("★Open Rank Record");
        // GameSystem.Instance.Post2UserRecord(_userdbkey);
    }
}
