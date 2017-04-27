using UnityEngine;
using System.Collections;
using PathologicalGames;

public class MailColumnCtrl : MonoBehaviour {

	[SerializeField] UISprite _spColumnHeader; // 메일 재화 정보
    [SerializeField] UISprite _spColumnNeko; // 메일 네코 정보 
    [SerializeField] UILabel _lblNekoGrade;
    [SerializeField] UISprite _spColumnBox;

	[SerializeField] int _mailtype; // 메일 타입 
	[SerializeField] string _columnClass; // 메일 보상 재화 (Heart)
	[SerializeField] string _text; // 메일 내용 
	[SerializeField] long _expiredTime; // 만료시간 

	[SerializeField] UILabel _lblText;
	[SerializeField] UILabel _lblExpiredTime;
	[SerializeField] int _maildbkey;
    [SerializeField] int _mailValue;
    string _strMailValue;

    [SerializeField]
    bool _mailCheckFlag = false; //메일 순차 읽기를 처리할때 사용

	private System.TimeSpan _tsRemainTime;
	private System.DateTime _dtExpiredTime;

    int _nekoID;
    int _nekoStar;
    string _nekoGrade;
    

    #region 무지개 티켓 교환 정보 
    [SerializeField]
    int _exchangeNekoID;

    [SerializeField]
    int _exchangeNekoGrade;
    #endregion

    #region Properties


    public int Mailtype {
        get {
            return _mailtype;
        }

        set {
            _mailtype = value;
        }
    }

    public int Maildbkey {
        get {
            return _maildbkey;
        }

        set {
            _maildbkey = value;
        }
    }

    public bool MailCheckFlag {
        get {
            return _mailCheckFlag;
        }

        set {
            _mailCheckFlag = value;
        }
    }

    public string ColumnClass {
        get {
            return _columnClass;
        }

        set {
            _columnClass = value;
        }
    }

    public long ExpiredTime {
        get {
            return _expiredTime;
        }

        set {
            _expiredTime = value;
        }
    }

    public int ExchangeNekoID {
        get {
            return _exchangeNekoID;
        }

        set {
            _exchangeNekoID = value;
        }
    }

    public int ExchangeNekoGrade {
        get {
            return _exchangeNekoGrade;
        }

        set {
            _exchangeNekoGrade = value;
        }
    }

    #endregion

    /// <summary>
    /// Sets the mail column.
    /// </summary>
    /// <param name="pIndex">P index.</param>
    public void SetMailColumn(int pIndex) {
		this.transform.localScale = GameSystem.Instance.BaseScale;

        ExchangeNekoID = -1;
        ExchangeNekoGrade = -1;

        // maildatas 정보를 가지고 세팅한다. 
        ColumnClass = GameSystem.Instance.MailData ["data"] ["maildatas"] [pIndex] ["rewardtype"];
		Maildbkey = GameSystem.Instance.MailData ["data"] ["maildatas"] [pIndex] ["dbkey"].AsInt;
		Mailtype = GameSystem.Instance.MailData ["data"] ["maildatas"] [pIndex] ["mailtype"].AsInt;

		//mailtype에 따른 메세지 설정
		switch (Mailtype) {
            case 1:
                //_text = "레벨업 보상으로\n";

                _text = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3448);
                break;
            case 2:
                //_text = "네코의 보은 보상으로\n";
                //_text = "네코의 보은 보상으로\n";
                break;
            case 3:
                //_text = "주간 랭킹 보상으로\n";

                _text = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3450);
                break;
            case 4:
                //_text = "SNS 업로드 보상으로\n";

                _text = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3451);
                break;
            case 5:
                //_text = "동영상 시청 보상으로\n";
                _text = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3452);
                break;
            case 6:
                //_text = "신규 네코 승리 보상으로\n";
                
                _text = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3453);
                break;
            case 7:
                //_text = "퓨전 불가 보상으로\n";
                
                _text = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3454);
                break;

            case 8:
                //_text = "쿠폰 보상으로\n";
                
                _text = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3455);
                break;

            case 9:
                //_text = "페이스북 친구에게 받은\n";
                
                _text = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3456);
                break;

            case 10:
                //_text = "집사의 보은으로\n";
                _text = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3457);
                break;

            case 11: // 출석체크
                _text = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3458);

                break;

            case 12:
                //_text = "이벤트 광고 참여 보상으로\n";
                _text = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3459);
                
                break;

            case 13:
                //_text = "SNS 보상으로\n";
                _text = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3460);

                break;

            case 14:
                //_text = "평가하기 보상으로\n";
                _text = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3461);
                break;

            case 15: // 메세지 전달 메일 
                _text = GameSystem.Instance.MailData["data"]["maildatas"][pIndex]["message"].Value;
                // 로컬라이징 필요 
                break;

            case 16: // 로컬 메세지 전달 메일 
                _text = GameSystem.Instance.GetLocalizeText(GameSystem.Instance.MailData["data"]["maildatas"][pIndex]["message"].Value);
                break;

            case 17: // 푸시 이벤트 
                _text = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L4165);
                break;


            case 50:

                //_text = "이벤트 보상으로\n";
                _text = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3462);
                
                break;

            case 51:
                //_text = "오류발생 보상으로\n";
                _text = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3463);
                
                break;

            default:
                _text = "보상으로\n";
                break;

        }

        this._spColumnHeader.gameObject.SetActive(true);
        this._spColumnNeko.gameObject.SetActive(false);
        _spColumnBox.spriteName = PuzzleConstBox.spriteBoxBlue;


        // 메일 값 
        _mailValue = GameSystem.Instance.MailData["data"]["maildatas"][pIndex]["value"].AsInt;
        _strMailValue = string.Format("{0:n0}", _mailValue);

        // 헤더, text 처리 
        if ("heart".Equals(ColumnClass)) {
            _spColumnHeader.spriteName = "i-heart";

            _text += "\n"
                + GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L4100).Replace("[n1]", GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3224))
                .Replace("[n2]", _strMailValue);

        }
        else if ("gold".Equals(ColumnClass) || "coin".Equals(ColumnClass)) {
            _spColumnHeader.spriteName = PuzzleConstBox.spriteUIGoldMark;

            _text += "\n"
                + GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L4100).Replace("[n1]", GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3225))
                .Replace("[n2]", _strMailValue);

        }
        else if ("gem".Equals(ColumnClass)) {
            _spColumnHeader.spriteName = PuzzleConstBox.spriteUIGemMark;

            _text += "\n"
                + GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L4100).Replace("[n1]", GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3226))
                .Replace("[n2]", _strMailValue);


        }
        else if ("chub".Equals(ColumnClass)) {
            _spColumnHeader.spriteName = PuzzleConstBox.spriteUIChubMark;

            _text += "\n"
                + GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L4100).Replace("[n1]", GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L4102))
                .Replace("[n2]", _strMailValue);


        }
        else if ("tuna".Equals(ColumnClass)) {
            _spColumnHeader.spriteName = PuzzleConstBox.spriteUITunaMark;

            _text += "\n"
                  + GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L4100).Replace("[n1]", GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L4103))
                    .Replace("[n2]", _strMailValue);

        }
        else if ("salmon".Equals(ColumnClass)) {
            _spColumnHeader.spriteName = PuzzleConstBox.spriteUISalmonMark;

            _text += "\n"
                + GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L4100).Replace("[n1]", GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L4104))
                .Replace("[n2]", _strMailValue);

        }
        else if ("neko".Equals(ColumnClass)) {
            _spColumnHeader.gameObject.SetActive(false);
            _spColumnNeko.gameObject.SetActive(true);

            _nekoID = GameSystem.Instance.MailData["data"]["maildatas"][pIndex]["value"].AsInt;
            _nekoStar = GameSystem.Instance.MailData["data"]["maildatas"][pIndex]["value2"].AsInt;
            _nekoGrade = "";
            for (int i = 0; i < _nekoStar; i++) {
                _nekoGrade += "*";
            }

            _lblNekoGrade.text = _nekoGrade;
            GameSystem.Instance.SetNekoSprite(_spColumnNeko, _nekoID, _nekoStar);
        }
        else if ("freeticket".Equals(ColumnClass)) {

            _spColumnHeader.spriteName = PuzzleConstBox.spriteUIFreeTicket;


            _text += "\n"
                + GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L4100).Replace("[n1]", GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3217))
                .Replace("[n2]", _strMailValue);

            _spColumnBox.spriteName = PuzzleConstBox.spriteBoxGreen;

        }
        else if ("rareticket".Equals(ColumnClass)) {

            _spColumnHeader.spriteName = PuzzleConstBox.spriteUIRareTicket;

            _text += "\n"
                + GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L4100).Replace("[n1]", GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3218))
                .Replace("[n2]", _strMailValue);


            _spColumnBox.spriteName = PuzzleConstBox.spriteBoxGreen;

        }
        else if ("rainbowticket".Equals(ColumnClass)) {

            _spColumnHeader.spriteName = PuzzleConstBox.spriteUIRainbowTicket;

            _text += "\n"
                + GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L4100).Replace("[n1]", GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3219))
                .Replace("[n2]", _strMailValue);

            _spColumnBox.spriteName = PuzzleConstBox.spriteBoxGreen;

        }
        


        // expired Time
        ExpiredTime = GameSystem.Instance.ConvertServerTimeTick (GameSystem.Instance.MailData ["data"] ["maildatas"] [pIndex] ["expiredtime"].AsLong);
		_dtExpiredTime = new System.DateTime (ExpiredTime);

        // 서버와 시간 시간 동기화 
		_tsRemainTime = _dtExpiredTime - new System.DateTime (GameSystem.Instance.SyncTime);

		// 레이블 처리 
		_lblText.text = _text;
		_lblExpiredTime.text = string.Format(GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3214) + " "
            + "{0:D2}"
            + GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3234)
            + "{1:D2}"
            + GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3235)
            , _tsRemainTime.Days, _tsRemainTime.Hours);
             

		LobbyCtrl.Instance.ListMailColumn.Add (this);

	}




	/// <summary>
	/// 단일 우편 수신 
	/// </summary>
	public void ReceiveMail() {

        // 무지개 티켓은 커밍 순 
        if (ColumnClass.Contains("rainbowticket")) {
            //LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.NotImplemented);
            GameSystem.Instance.Post2NekoTicketList(this.Maildbkey, OnCompleteReadNekoTicketList);
            return;
        }


        // 프리 레어 티켓은 확인 메일을 띄운다.
        if (ColumnClass.Contains("freeticket") || ColumnClass.Contains("rareticket")) {
            LobbyCtrl.Instance.OpenInfoPopUp(PopMessageType.UseTicket, ReceiveMailForce);
            return;
        }

        GameSystem.Instance.Post2MailRead (Maildbkey, this, OnCompleteMailRead);
	}


    /// <summary>
    /// 티켓교환 리스트 오픈 
    /// </summary>
    /// <param name="pNode"></param>
    private void OnCompleteReadNekoTicketList(SimpleJSON.JSONNode pNode) {
        LobbyCtrl.Instance.OpenNekoTicketList(pNode, Maildbkey, this, OnCompleteMailRead);
    }


    private void ReceiveMailForce() {
        GameSystem.Instance.Post2MailRead(Maildbkey, this, OnCompleteMailRead);
    }


    /// <summary>
    /// 메일 수신 완료 후 Callback
    /// </summary>
    private void OnCompleteMailRead() {
        
        PoolManager.Pools["MailBoxPool"].Despawn(this.transform);

        LobbyCtrl.Instance.ArrangeMailBox(this);

        GameSystem.Instance.OnOffWaitingRequestInLobby(false);

    }


}
