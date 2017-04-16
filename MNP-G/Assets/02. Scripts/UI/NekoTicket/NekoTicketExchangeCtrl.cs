using UnityEngine;
using System.Collections;
using SimpleJSON;
using Google2u;

public class NekoTicketExchangeCtrl : MonoBehaviour {

    [SerializeField] UISprite _spNekoSprite;
    [SerializeField] UILabel _lblNekoGrade;
    [SerializeField] UILabel _lblNekoName;


    NekoTicketWindowCtrl _baseView;

    MNP_NekoSkill _NekoSkill = MNP_NekoSkill.Instance; // 네코 스킬 정보 
    MNP_NekoSkillValue _SkillValue = MNP_NekoSkillValue.Instance; // 네코 스킬 값


    JSONNode nodeNeko;
    JSONNode _nekoNode;
    NekoData _nekoStructData;

    [SerializeField]
    int _nekoID;
    [SerializeField]
    int _nekoGrade;

    string _gradeStar;
    string _nekoName;


    [SerializeField]
    UILabel _lblNekoSkillInfo;

    

    void OnSpawned() {
        this.transform.localScale = PuzzleConstBox.baseScale;
    }
    



    /// <summary>
    /// 티켓 교환 네코 정보 설정 
    /// </summary>
    /// <param name="pNode"></param>
    public void SetNekoInfo(NekoTicketWindowCtrl pBase, JSONNode pNode, int pIndex) {

        _baseView = pBase;

        // 위치 설정 
        this.transform.localPosition = new Vector3(0, pIndex * -190, 0);


        _nekoID = pNode["nekoid"].AsInt;
        _nekoGrade = pNode["grade"].AsInt;
        
           
        _gradeStar = "";

        for(int i =0; i< _nekoGrade; i++) {
            _gradeStar += "*";
        }

        _lblNekoGrade.text = _gradeStar;

        _nekoName = GameSystem.Instance.GetNekoName(_nekoID, _nekoGrade);
        _lblNekoName.text  = _nekoName;

        // Sprite 세팅 
        GameSystem.Instance.SetNekoSprite(_spNekoSprite, _nekoID, _nekoGrade);

        SetNoneSkill();
        SetSkill(pNode);

    }

    void SetSkill(JSONNode pNode) {
        //_nekoNode = GameSystem.Instance.GetNekoNodeByID(_nekoID);

        //Debug.Log("Check Skill _nekoNode ::" + _nekoNode.ToString());
        _nekoStructData = GameSystem.Instance.GetNekoData(pNode);


        _lblNekoSkillInfo.text = string.Empty;
        string nekoSkill = string.Empty;


        // Debug.Log("Check Skill SkillCount ::" + _nekoStructData.skillCount);

        for (int i = 0; i < _nekoStructData.skillCount; i++) {

            if (i > 0)
                nekoSkill += "\n";

            nekoSkill += "●";
            nekoSkill += _nekoStructData.listSkillInfo[i];
        }

        _lblNekoSkillInfo.text = nekoSkill;
    }


    /// 스킬 초기화
    /// </summary>
    private void SetNoneSkill() {
        _lblNekoSkillInfo.text = string.Empty;

    }





    /// <summary>
    /// 
    /// </summary>
    public void OnClickExchange() {
        _baseView.OpenConfirm(_nekoID, _nekoGrade, _gradeStar, _nekoName, _spNekoSprite);
        //_baseView.ReadTicket(_nekoID, _nekoGrade);

    }

}
