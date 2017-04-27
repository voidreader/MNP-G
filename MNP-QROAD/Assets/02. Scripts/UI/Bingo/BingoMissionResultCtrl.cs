using UnityEngine;
using System.Collections;
using SimpleJSON;
using DG.Tweening;

public class BingoMissionResultCtrl : MonoBehaviour {

	[SerializeField] Transform _popup;
	JSONNode currentNode;
	[SerializeField] string debugNode;

	[SerializeField] UISprite _rewardIcon;
	[SerializeField] UILabel _rewardValueLabel;

	[SerializeField] string _rewardtype;
	[SerializeField] int _rewardvalue1;
	[SerializeField] int _rewardvalue2;
	[SerializeField] int _rewardvalue3;

    Vector3 _firstPos = new Vector3(0, -100, 0);

    [SerializeField]
    UITweener _bgAlphaTweener;

    [SerializeField] AudioClip _acOpen;

    /// <summary>
    /// Sets the result info.
    /// </summary>
    public void SetResultInfo(JSONNode pNode) {

		this.gameObject.SetActive (true);

        // 알파 효과 
        _bgAlphaTweener.ResetToBeginning();
        _bgAlphaTweener.PlayForward();

       
        _popup.gameObject.SetActive (true);
        _popup.transform.DOKill();
        _popup.transform.localPosition = _firstPos;
        _popup.transform.DOLocalMoveY(0, 0.5f);

        BingoMasterCtrl.Instance.PlayAudio(_acOpen);
            

        BingoMasterCtrl.Instance.SetBingoScreenLock(false); // 락 해제

        currentNode = pNode;
		debugNode = currentNode.ToString ();

		_rewardIcon.atlas = GameSystem.Instance.comAtlas;
		_rewardtype = pNode ["rewardtype"].Value;
		_rewardvalue1 = pNode ["rewardvalue1"].AsInt;
		_rewardvalue2 = pNode ["rewardvalue2"].AsInt;
		_rewardvalue3 = pNode ["rewardvalue3"].AsInt;

		_rewardValueLabel.text = "x" + GameSystem.Instance.GetNumberToString(_rewardvalue1);

		switch (pNode ["rewardtype"].Value) {
		case "gem":
			_rewardIcon.spriteName = PuzzleConstBox.spriteUIGemMark;
			break;
		case "coin":
			_rewardIcon.spriteName = PuzzleConstBox.spriteUIGoldMark;
			break;
		case "chub":
			_rewardIcon.spriteName = PuzzleConstBox.spriteUIChubMark;
			break;
		case "tuna":
			_rewardIcon.spriteName = PuzzleConstBox.spriteUITunaMark;
			break;
		case "salmon":
			_rewardIcon.spriteName = PuzzleConstBox.spriteUISalmonMark;
			break;

		case "freeticket":
			_rewardIcon.spriteName = PuzzleConstBox.spriteUIFreeTicket;
			break;

		case "rareticket":
			_rewardIcon.spriteName = PuzzleConstBox.spriteUIRareTicket;
			break;
		case "rainbowticket":
			_rewardIcon.spriteName = PuzzleConstBox.spriteUIRainbowTicket;
			break;

		}


	}

	public void CloseMissionResult() {
		this.gameObject.SetActive (false);
        BingoMasterCtrl.Instance.SetBingoScreenLock(true); 
    }

}
