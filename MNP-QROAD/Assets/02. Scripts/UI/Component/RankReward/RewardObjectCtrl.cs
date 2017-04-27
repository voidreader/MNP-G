using UnityEngine;
using System.Collections;
using SimpleJSON;

public class RewardObjectCtrl : MonoBehaviour {


    [SerializeField] string _debugNode;

    [SerializeField] string type;
    [SerializeField] int value1;
    [SerializeField] int value2;
    [SerializeField] int value3;

    
    [SerializeField] UISprite _boxSprite;
    [SerializeField] UISprite _nekoSprite;
    [SerializeField] UISprite _iconSprite;
    [SerializeField] UILabel _lblValue;
    [SerializeField] UILabel _lblNekoGrade;
    


    string _star;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pNode"></param>
	public void SetRewardInfo(JSONNode pNode) {

        this.gameObject.SetActive(true);

        _debugNode = pNode.ToString();


        type = pNode["type"].Value;
        value1 = pNode["value1"].AsInt;
        value2 = pNode["value2"].AsInt;
        value3 = pNode["value3"].AsInt;

        if (type == "neko") { // 네코 타입 
            _nekoSprite.gameObject.SetActive(true);
            _iconSprite.gameObject.SetActive(false);

            _boxSprite.spriteName = PuzzleConstBox.spriteBoxBlue;

            // 네코 정보 세팅 
            GameSystem.Instance.SetNekoSprite(_nekoSprite, value1, value2);

            _star = "";
            for (int i=0; i<value2; i++) {
                _star += "*";
            }

            _lblNekoGrade.text = _star;
            //_lblValue.text = GameSystem.Instance.GetNekoName(value1, value2);
            _lblValue.text = "";
        }
        else {

            _nekoSprite.gameObject.SetActive(false);
            _iconSprite.gameObject.SetActive(true);

            _boxSprite.spriteName = PuzzleConstBox.spriteBoxGreen;

            // 아이콘 처리 
            if (type == "gem")
                _iconSprite.spriteName = PuzzleConstBox.spriteUIDiaMark;
            else if (type == "coin")
                _iconSprite.spriteName = PuzzleConstBox.spriteUIGoldMark;
            else if (type == "chub") {
                _boxSprite.spriteName = PuzzleConstBox.spriteBoxYellow;
                _iconSprite.spriteName = PuzzleConstBox.spriteUIChubMark;
            }
            else if (type == "tuna") {
                _boxSprite.spriteName = PuzzleConstBox.spriteBoxYellow;
                _iconSprite.spriteName = PuzzleConstBox.spriteUITunaMark;
            }
            else if (type == "salmon") {
                _boxSprite.spriteName = PuzzleConstBox.spriteBoxYellow;
                _iconSprite.spriteName = PuzzleConstBox.spriteUISalmonMark;
            }
            else if (type == "freeticket") {
                _boxSprite.spriteName = PuzzleConstBox.spriteBoxBlue;
                _iconSprite.spriteName = PuzzleConstBox.spriteUIFreeTicket;
            }
            else if (type == "rareticket") {
                _boxSprite.spriteName = PuzzleConstBox.spriteBoxBlue;
                _iconSprite.spriteName = PuzzleConstBox.spriteUIRareTicket;
            }
			else if (type == "rainbowticket") {
				_boxSprite.spriteName = PuzzleConstBox.spriteBoxBlue;
				_iconSprite.spriteName = PuzzleConstBox.spriteUIRainbowTicket;
			}


            _lblValue.text = GameSystem.Instance.GetNumberToString(value1);

        }

    }
}
