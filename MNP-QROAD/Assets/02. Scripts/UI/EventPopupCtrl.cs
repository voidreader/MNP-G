using UnityEngine;
using System;
using System.Collections;

public class EventPopupCtrl : MonoBehaviour {

	string _message; 
	string _title;
	string _rewardValue;
	string _rewardType;
	int _eventStep;

	[SerializeField] UILabel _lblMessage;
	[SerializeField] UILabel _lblTitle;
	[SerializeField] UILabel _lblRewardValue;
	[SerializeField] UISprite _spIcon;

	public event Action<int> ActionComplete = delegate{};

	// Use this for initialization
	void Start () {

	}

	private void DispatchAction(int eventStep) {
		ActionComplete (eventStep);
	}


	/// <summary>
	/// Sets the event message.
	/// </summary>
	/// <param name="pTitle">P title.</param>
	/// <param name="pMessage">P message.</param>
	/// <param name="pRewardValue">P reward value.</param>
	/// <param name="pRewardType">P reward type.</param>
	public void SetEventMessage(string pTitle, string pMessage, string pRewardValue, string pRewardType, int pEventStep) {
		_title = pTitle;
		_message = pMessage;
		_rewardType = pRewardType;
		_rewardValue = pRewardValue;
		_eventStep = pEventStep;

		_lblMessage.text = _message;
		_lblTitle.text = _title;
		_lblRewardValue.text = pRewardValue;

		if ("heart".Equals (_rewardType)) {
			_spIcon.spriteName = PuzzleConstBox.spriteUIHeartMark;
		} else if ("gem".Equals (_rewardType)) {
			_spIcon.spriteName = PuzzleConstBox.spriteUIDiaMark;
		} else if ("gold".Equals (_rewardType)) {
			_spIcon.spriteName = PuzzleConstBox.spriteUIGoldMark;
		}
	}

	public void OnClose() {
		DispatchAction (_eventStep);
		this.gameObject.SendMessage ("CloseSelf");
	}


}
