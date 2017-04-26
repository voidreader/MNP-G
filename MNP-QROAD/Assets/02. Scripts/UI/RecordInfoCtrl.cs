using UnityEngine;
using System.Collections;
using SimpleJSON;

public class RecordInfoCtrl : MonoBehaviour {


    #region 상단 정보

    [SerializeField]
    UISprite _spMainNeko;

    [SerializeField]
    UISprite _spBlueNeko;

    [SerializeField]
    UISprite _spYellowNeko;

    [SerializeField]
    UISprite _spRedNeko;

    [SerializeField]
    UILabel _lblUserName;

    [SerializeField]
    UILabel _lblComment;

    [SerializeField] UILabel _lblBlueNekoLevel;
    [SerializeField] UILabel _lblBlueNekoGrade;
    [SerializeField] UILabel _lblYellowNekoLevel;
    [SerializeField] UILabel _lblYellowNekoGrade;
    [SerializeField] UILabel _lblRedNekoLevel;
    [SerializeField] UILabel _lblRedNekoGrade;
    #endregion

    #region 하단 정보

    [SerializeField] UILabel _lblLevel;
    [SerializeField] UILabel _lblLoginCount;
    [SerializeField] UILabel _lblPuzzleCount;
    [SerializeField] UILabel _lblBestScore;
    [SerializeField] UILabel _lblBestStage;
    [SerializeField] UILabel _lblBestCombo;
    [SerializeField] UILabel _lblBestBlocks;

    [SerializeField] UILabel _lblGoldBadge;
    [SerializeField] UILabel _lblSilverBadge;
    [SerializeField] UILabel _lblBronzeBadge;

    [SerializeField] UILabel _lblCollection;
    [SerializeField] UILabel _lblWanted;

    readonly string _constLevel = "Lv. ";

    #endregion


    private void InitRecord() {


        // 메인 네코 
        _spMainNeko.gameObject.SetActive(false);
        _spBlueNeko.gameObject.SetActive(false);
        _spYellowNeko.gameObject.SetActive(false);
        _spRedNeko.gameObject.SetActive(false);

        // 이름, 코멘트 
        _lblUserName.text = string.Empty;
        _lblComment.text = string.Empty;


        // Blue,Yellow,Red Neko
        _lblBlueNekoGrade.text = string.Empty;
        _lblBlueNekoLevel.text = string.Empty;

        
        _lblYellowNekoGrade.text = string.Empty;
        _lblYellowNekoLevel.text = string.Empty;

        
        _lblRedNekoGrade.text = string.Empty;
        _lblRedNekoLevel.text = string.Empty;


        // 실적
        _lblLevel.text = string.Empty;
        _lblLoginCount.text = string.Empty;
        _lblPuzzleCount.text = string.Empty;
        _lblBestScore.text = string.Empty;
        _lblBestStage.text = string.Empty;
        _lblBestCombo.text = string.Empty;
        _lblBestBlocks.text = string.Empty;

        // badge
        _lblGoldBadge.text = string.Empty;
        _lblSilverBadge.text = string.Empty;
        _lblBronzeBadge.text = string.Empty;

        // Collection
        _lblCollection.text = string.Empty;
        _lblWanted.text = string.Empty;
    }

    /// <summary>
    /// 레코드 정보 세팅 
    /// </summary>
    /// <param name="pNode">정보</param>
    /// <param name="pMe">본인 여부 </param>
    public void SetRecord(JSONNode pNode, bool pMe) {

        this.gameObject.SetActive(true);
        InitRecord();

        // 메인 네코 
        if (pNode["mainnekoid"].AsInt >= 0) {
            _spMainNeko.gameObject.SetActive(true);
            GameSystem.Instance.SetNekoSprite(_spMainNeko, pNode["mainnekoid"].AsInt, pNode["mainnekograde"].AsInt);
        }

        // 이름, 코멘트 
        _lblUserName.text = pNode["username"].Value;
        _lblComment.text = pNode["comment"].Value;


        // Blue,Yellow,Red Neko
        if (pNode["bluenekoid"].AsInt >= 0) {
            _spBlueNeko.gameObject.SetActive(true);
            GameSystem.Instance.SetNekoSprite(_spBlueNeko, pNode["bluenekoid"].AsInt, pNode["bluenekograde"].AsInt);
            _lblBlueNekoGrade.text = GameSystem.Instance.GetNekoGradeText(pNode["bluenekograde"].AsInt);
            _lblBlueNekoLevel.text = _constLevel + pNode["bluenekolevel"].AsInt.ToString();
        }
        

        if (pNode["yellownekoid"].AsInt >= 0) {
            _spYellowNeko.gameObject.SetActive(true);
            GameSystem.Instance.SetNekoSprite(_spYellowNeko, pNode["yellownekoid"].AsInt, pNode["yellownekograde"].AsInt);
            _lblYellowNekoGrade.text = GameSystem.Instance.GetNekoGradeText(pNode["yellownekograde"].AsInt);
            _lblYellowNekoLevel.text = _constLevel + pNode["yellownekolevel"].AsInt.ToString();
        }
        

        if (pNode["rednekoid"].AsInt >= 0) {
            _spRedNeko.gameObject.SetActive(true);
            GameSystem.Instance.SetNekoSprite(_spRedNeko, pNode["rednekoid"].AsInt, pNode["rednekograde"].AsInt);
            _lblRedNekoGrade.text = GameSystem.Instance.GetNekoGradeText(pNode["rednekograde"].AsInt);
            _lblRedNekoLevel.text = _constLevel + pNode["rednekolevel"].AsInt.ToString();
            
        }

        // 실적
        _lblLevel.text = pNode["level"].AsInt.ToString();
        _lblLoginCount.text = GameSystem.Instance.GetNumberToString(pNode["logincount"].AsInt);
        _lblPuzzleCount.text = GameSystem.Instance.GetNumberToString(pNode["puzzlecount"].AsInt);
        _lblBestScore.text = GameSystem.Instance.GetNumberToString(pNode["bestscore"].AsInt);
        _lblBestStage.text = GameSystem.Instance.GetNumberToString(pNode["beststage"].AsInt);
        _lblBestCombo.text = GameSystem.Instance.GetNumberToString(pNode["bestcombo"].AsInt);
        _lblBestBlocks.text = GameSystem.Instance.GetNumberToString(pNode["bestblocks"].AsInt);

        // badge
        _lblGoldBadge.text =  "X " + GameSystem.Instance.GetNumberToString(pNode["goldbadge"].AsInt);
        _lblSilverBadge.text = "X " + GameSystem.Instance.GetNumberToString(pNode["silverbadge"].AsInt);
        _lblBronzeBadge.text = "X " + GameSystem.Instance.GetNumberToString(pNode["bronzebadge"].AsInt);

        // Collection
        _lblCollection.text = pNode["collectioninfo"].Value;
        _lblWanted.text = pNode["wantedinfo"].Value;

    }

}
