using UnityEngine;
using System;
using System.Collections;
using SimpleJSON;

public class SimplePopupCtrl : MonoBehaviour {

    

	[SerializeField] UILabel _lblMessage;
	[SerializeField] UILabel _lblConfirmMessage;
	[SerializeField] string _message;
	[SerializeField] PopMessageType _messageType;

	[SerializeField] UISprite _messageIcon;
	[SerializeField] UISprite _fishIcon;

    [SerializeField] UISprite _nekoSprite;
    [SerializeField] UISprite _nekoFishIcon;
    [SerializeField] UILabel _nekoFishValue;

    [SerializeField] GameObject _groupYesNoButton;


	[SerializeField] GameObject _btnConfirm;
	[SerializeField] UILabel _lblbtnConfirm;

	private string _heartMark = "main_ico_heart";
	private string _diaMark = "main_ico_dia";
	private string _goldMark = "main_ico_coin";
	private string _starMark = "main_top_star_ico";

    readonly string _baseTitleSprtie = "img_title_notice";
    readonly string _bingoRetryTitle = "retry-r";
    readonly string _bingoChallengeTitle = "challenge-r";
   

    JSONNode _nekoNode;

    [SerializeField]
    string _confirmValue = string.Empty;

    // Actions
    event Action OnCompleteClose = delegate { };


    /*
    public void SetAction(Action func) {
        OnCompleteClose += func;
    }
    */

    #region 텍스트 처리 

    /// <summary>
    /// 텍스트 (메세지 설정)
    /// </summary>
    /// <param name="pType"></param>
    private void SetText(PopMessageType pType) {

        // 가챠 진입 확인 
		if (pType == PopMessageType.GatchaConfirm) {
            _message = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3033);
            _message = _message.Replace ("[n]", GameSystem.Instance.GetNumberToString (GameSystem.Instance.SpecialSingleGatchaPrice)); // 가격 정보 연결  
		} else if (pType == PopMessageType.ConfirmSpecialGatchaTen) {
            _message = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3035);
            
			//_message = "だいや[n]個で10回\n「すぺしゃるくれーん」に\nちょうせんしますか？";
			_message = _message.Replace ("[n]", GameSystem.Instance.GetNumberToString (GameSystem.Instance.SpecialMultiGatchaPrice)); // 가격 정보 연결  
		} else if (pType == PopMessageType.ConfirmFishGatchaOne) {
            _message = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3034);
            
			_message = _message.Replace ("[n1]", GameSystem.Instance.GetNumberToString (GameSystem.Instance.SpecialSingleFishingPrice)).Replace ("[n2]", "1"); // 
		} else if (pType == PopMessageType.ConfirmFishGatchaTen) {
            _message = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3034);
            
			_message = _message.Replace ("[n1]", GameSystem.Instance.GetNumberToString (GameSystem.Instance.SpecialMultiFishingPrice)).Replace ("[n2]", "10");
		} else if (pType == PopMessageType.HeartFull) {
            //_message = "사랑이 가득해 더이상 받을 수 없어요";
            _message = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3410);

		} else if (pType == PopMessageType.HeartZero) {
            _message = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3011);
        } else if (pType == PopMessageType.AdsNotEnable) {
            // _message = "스타트 피버 아이템 사용 과정에서 오류가 발생했어요.";
            _message = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3411);

		} else if (pType == PopMessageType.GoldPurchase) {
            _message = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3200);
            

		} else if (pType == PopMessageType.NeedGoldPurchase) {
            _message = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3010);
            
		} else if (pType == PopMessageType.ShortageGoldForItem) {
            _message = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3010);
            
		} else if (pType == PopMessageType.ShortageGemForGatcha) {
            _message = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3009);
        } else if (pType == PopMessageType.GoldShortage) {
            // 3010 
            _message = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3010);
        } else if (pType == PopMessageType.NoFish) {
			
            _message = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3900);

        } else if (pType == PopMessageType.CantGrowNeko) {
			
            _message = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3901);

        } else if (pType == PopMessageType.NekoGonnaMaxGrade) {
			
            _message = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3902);

        } else if (pType == PopMessageType.CouponFail) { // 없는 쿠폰 번호

			//_message = GameSystem.Instance.DocsLocalize.get<string> ("3024", "content");
			//2015.11.14 수정
			_message = "등록되지 않은 쿠폰 번호에요!\n쿠폰 입력에 실패했어요";

		} else if (pType == PopMessageType.CouponOutOf) { // 기간지난 쿠폰번호

			//_message = GameSystem.Instance.DocsLocalize.get<string> ("3024", "content");
			//2015.11.14 수정
			_message = "쿠폰 사용기간 지났어요!\n쿠폰 입력에 실패했어요";

		} else if (pType == PopMessageType.CouponUsed) { // 이미 사용된 쿠폰 번호 

			//2015.11.14 수정
			_message = "이미 사용된 쿠폰이에요!\n쿠폰 입력에 실패했어요";

		} else if (pType == PopMessageType.CouponSucceed) { // 쿠폰 성공

			
            _message = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3023);

        } else if (pType == PopMessageType.CouponInput) {
			_message = "쿠폰 코드가 입력되지 않았어요!";
		} else if (pType == PopMessageType.HeartPurchase) {
			_message = GameSystem.Instance.GetLocalizeText ("3204"); // 하트를 충전했습니다.

		} else if (pType == PopMessageType.HeartPurChaseButFull) {
			_message = GameSystem.Instance.GetLocalizeText ("3203");

		} else if (pType == PopMessageType.NeedGradeUp) {
			_message = GameSystem.Instance.GetLocalizeText ("3418"); // 더이상 업그레이드 할 수 없어요.

		} else if (pType == PopMessageType.NeedLevelUp) {
			_message = GameSystem.Instance.GetLocalizeText("3036");

        } else if (pType == PopMessageType.AdShortage) {
			_message = GameSystem.Instance.GetLocalizeText ("3419"); // 광고가 충전중이에요.

		} else if (pType == PopMessageType.FacebookFriendNeed) {
			_message = "페이스북 친구를 불러오는 중이에요.\n잠시만 기다려주세요";
		} else if (pType == PopMessageType.AlreadySentHeart) {
			_message = GameSystem.Instance.GetLocalizeText ("3422"); // 오늘은 이친구에게 하트를 보냈어요.

		} else if (pType == PopMessageType.FacebookLinkLost) {
			_message = "Facebookの接続が切断された友人です。";

		} else if (pType == PopMessageType.Logout) {
			_message = GameSystem.Instance.GetLocalizeText("3423");

        } else if (pType == PopMessageType.EquipNekoNeed) {
            _message = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3425);
		} else if (pType == PopMessageType.AdsCool) {
			_message = GameSystem.Instance.GetLocalizeText("3048");
        } else if (pType == PopMessageType.AlreadStartPackage) {
			_message = GameSystem.Instance.GetLocalizeText("3049");
        } else if (pType == PopMessageType.RateForGem) {
			_message = "게임 평가를 해주시면\n최초 1회 보석 10개를 드려요.";
		}  else if (_messageType == PopMessageType.ApplicationQuit) {
			_message = GameSystem.Instance.GetLocalizeText("3050");
        } else if (_messageType == PopMessageType.NoFunc) {
            _message = GameSystem.Instance.GetLocalizeText("3051");
		} else if (_messageType == PopMessageType.MaxPassiveLevel) {
			_message = GameSystem.Instance.GetLocalizeText ("4121");

		} else if (_messageType == PopMessageType.GameCenterConnectInfo) {
			//_message = "안전한 게임 이용을 위해 GameCenter 연동을 권장합니다. 설정 > GameCenter 로그인 > 게임 재시작";
			_message = "安全なゲーム利用のためにGameCenter連動をお勧めします。\n設定 > GameCenterログイン > ゲーム再起動";
		} else if (_messageType == PopMessageType.NekoGiftWithAdsConfirm) {

			_groupYesNoButton.SetActive (true);
			_btnConfirm.SetActive (false);

			_message = "広告を見ると「こいん×5000」「さば×5」「だいや×100」のいずれかがもらえます。\n広告を見ますか？";
			//_message = "広告を見ると「れあねこちけっと」「ねこちけっと」のいずれかがもらえます。\n広告を見ますか？";
		} else if (_messageType == PopMessageType.ConfirmFeed) {
			_message = GameSystem.Instance.GetLocalizeText ("4116");
		} else if (_messageType == PopMessageType.NoRemainAds) {
			_message = GameSystem.Instance.GetLocalizeText("3052");
            //_message = "広告をみることができません\n午前9: 00と午後21：00に";
        } else if (_messageType == PopMessageType.NoRemainFreeGatcha) { // 프리가챠는 1회가 최대치. 
			//_message = "9：00と21：00に\n3回数がリセットされます";
            _message = GameSystem.Instance.GetLocalizeText("3053");

        } else if (_messageType == PopMessageType.NickNameChanged) {
			_message = GameSystem.Instance.GetLocalizeText ("4202");

		} else if (_messageType == PopMessageType.ReLogin) {
            _message = GameSystem.Instance.GetLocalizeText("3054");

		} else if (_messageType == PopMessageType.ChangeLang) {
            _message = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3405);

            _groupYesNoButton.SetActive(true);
            _btnConfirm.SetActive(false);

        } else if (_messageType == PopMessageType.NeedWatchGatchaMovie) { // 무료뽑기 광고 보겠습니까?

            
			
            _message = GameSystem.Instance.GetLocalizeText("3055");
            
            _groupYesNoButton.SetActive (true);
			_btnConfirm.SetActive (false);

		} else if (_messageType == PopMessageType.ReadyToFreeGatcha) { // 무료뽑기를 하시겠습니까?


			//_message = "★を3つ消費して\n「ふりーくれーん」に\nちょうせんしますか？";
			//_message = "広告を見て「ふりーくれーん」にちょうせんしますか？";
			//_message = "動画広告をチェックすると、無料で「すぺしゃるくれーん」にチャレンジできます！チェックしますか？\n※あと「n」回チャレンジできます".Replace("n", GameSystem.Instance.Remainfreegacha.ToString());
			_message = GameSystem.Instance.GetLocalizeText("3056").Replace ("[n]", GameSystem.Instance.Remainfreegacha.ToString ());

			_groupYesNoButton.SetActive (true);
			_btnConfirm.SetActive (false);

		} else if (_messageType == PopMessageType.UploadComplete) {
            _message = GameSystem.Instance.GetLocalizeText("3057");

        } else if (_messageType == PopMessageType.UploadFail) {
            _message = GameSystem.Instance.GetLocalizeText("3058");

        } else if (_messageType == PopMessageType.InviteComplete) {
            _message = GameSystem.Instance.GetLocalizeText("3059");

        } else if (_messageType == PopMessageType.AlreadyIssued) {
            _message = GameSystem.Instance.GetLocalizeText("3060");

        } else if (_messageType == PopMessageType.WrongCode) {
            _message = GameSystem.Instance.GetLocalizeText("3061");

        } else if (_messageType == PopMessageType.UsedCode) {
            _message = GameSystem.Instance.GetLocalizeText("3062");

        } else if (_messageType == PopMessageType.ExpiredCode) {

            _message = GameSystem.Instance.GetLocalizeText("3063");
			GameSystem.Instance.DataCode = null;

		} else if (_messageType == PopMessageType.SameDeviceCode) {
            _message = GameSystem.Instance.GetLocalizeText("3064");
            
		} else if (_messageType == PopMessageType.CompleteDataTranfer) {
            _message = GameSystem.Instance.GetLocalizeText("3065");

		} else if (_messageType == PopMessageType.ExistsCantReadMail) {

            _message = GameSystem.Instance.GetLocalizeText("3079");

		} else if (_messageType == PopMessageType.Lock) {
            _message = GameSystem.Instance.GetLocalizeText("3080");

		} else if (_messageType == PopMessageType.BoostItemLock) {
            _message = GameSystem.Instance.GetLocalizeText("3112").Replace("[n]", "7");
        }

        else if (_messageType == PopMessageType.UseTicket) {

            _message = GameSystem.Instance.GetLocalizeText("3081");

		} else if (_messageType == PopMessageType.CommingSoon) {

            _message = GameSystem.Instance.GetLocalizeText("3082");

		} else if (_messageType == PopMessageType.GemShortage) {
			_message = GameSystem.Instance.GetLocalizeText ("3009"); // 보석이 부족해요! 보석상점을 열어봐요 
		}
        else if (_messageType == PopMessageType.GetFacebookLinkReward) {
            _message = GameSystem.Instance.GetLocalizeText("3084");
            
		} else if (_messageType == PopMessageType.BingoSelect) { // 빙고 도전 
			
            _message = GameSystem.Instance.GetLocalizeText("3205");
            _groupYesNoButton.SetActive (true);
			_btnConfirm.SetActive (false);
		} else if (_messageType == PopMessageType.BingoRetry) { // 빙고 재도전 

			
            _message = GameSystem.Instance.GetLocalizeText("3206");
            _groupYesNoButton.SetActive (true);
			_btnConfirm.SetActive (false);
		} else if (_messageType == PopMessageType.BingoNeed) {// 빙고 도전이 필요하다
            _message = GameSystem.Instance.GetLocalizeText("3207");
            _groupYesNoButton.SetActive (true);
			_btnConfirm.SetActive (false);
		} else if (_messageType == PopMessageType.BingoStart) {
            _message = GameSystem.Instance.GetLocalizeText("3087");
            
		} else if (_messageType == PopMessageType.NotImplemented) {
            _message = GameSystem.Instance.GetLocalizeText("3085");
            
        }
        else if (_messageType == PopMessageType.LineInvited) {
            _message = GameSystem.Instance.GetLocalizeText("3086");
        }
        else if (_messageType == PopMessageType.BingoLock) {
            _message = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L4138);
        }
        else if(_messageType == PopMessageType.ThemeLock) {
            _message = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L4147);
        }
        else if (_messageType == PopMessageType.PostCompleted) {
            _message = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L4262);
        }

        _lblMessage.text = _message;
        _lblConfirmMessage.text = _message;

    }

    /// <summary>
    /// Confirm Message Text 설정 
    /// </summary>
    /// <param name="pType"></param>
    /// <param name="pValue"></param>
    private void SetConfirmText(PopMessageType pType, string pValue) {
        if (_messageType == PopMessageType.GemShortage) {
            _messageIcon.spriteName = _diaMark;
            _message = GameSystem.Instance.GetLocalizeText("3009"); // 보석이 부족해요! 보석상점을 열어봐요 
        }
        else if (_messageType == PopMessageType.GemAdd) {
            _messageIcon.spriteName = _diaMark;
            //_message = "보석 " + pValue + "개를 얻었어요";
            _message = GameSystem.Instance.GetLocalizeText("3289");
            _message = _message.Replace("[n]", pValue);
        }
        else if (_messageType == PopMessageType.GoldAdd) {
            _messageIcon.spriteName = _goldMark;
            //_message = "코인 " + pValue + "개를 얻었어요";
            _message = GameSystem.Instance.GetLocalizeText("3431");
            _message = _message.Replace("[n]", pValue);

        }
        else if (_messageType == PopMessageType.RankRewardCoinAdd) {
            _messageIcon.spriteName = _goldMark;
            //_message = "코인 " + pValue + "개를 얻었어요";
            _message = GameSystem.Instance.GetLocalizeText("4124");
            _message = _message.Replace("[n]", pValue);

        }

        else if (_messageType == PopMessageType.HeartAdd) {
            _messageIcon.spriteName = _heartMark;
            _message = GameSystem.Instance.GetLocalizeText("3432"); // 하트를 받았어요 우편함을..
        }

        else if (_messageType == PopMessageType.MailHeartAdd) {
            _messageIcon.spriteName = _heartMark;
            _message = GameSystem.Instance.GetLocalizeText("3611").Replace("[n]", pValue); // 하트를 받았어요 우편함을..
        }

        else if (pType == PopMessageType.AlreadySentHeart) {
            _messageIcon.spriteName = _heartMark;
            _message = GameSystem.Instance.GetLocalizeText("3422"); // 오늘은 이친구에게 하트를 보냈어요.
        }
        else if (pType == PopMessageType.SalmonAdd || pType == PopMessageType.BandSalmonAdd || pType == PopMessageType.RankRewardSalmonAdd) {
            _messageIcon.gameObject.SetActive(false);
            _fishIcon.gameObject.SetActive(true);
            _fishIcon.spriteName = PuzzleConstBox.spriteUISalmonMark;
            
            _message = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3300).Replace("[n]", pValue);

        }
        else if (pType == PopMessageType.TunaAdd || pType == PopMessageType.BandTunaAdd || pType == PopMessageType.RankRewardTunaAdd) {
            _messageIcon.gameObject.SetActive(false);
            _fishIcon.gameObject.SetActive(true);
            _fishIcon.spriteName = PuzzleConstBox.spriteUITunaMark;
            _message = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3299).Replace("[n]", pValue);

        }
        else if (pType == PopMessageType.ChubAdd || pType == PopMessageType.BandChubAdd || pType == PopMessageType.RankRewardChubAdd) {
            _messageIcon.gameObject.SetActive(false);
            _fishIcon.gameObject.SetActive(true);
            _fishIcon.spriteName = PuzzleConstBox.spriteUIChubMark;
            _message = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3298).Replace("[n]", pValue);

        }
        else if (pType == PopMessageType.NekoGiftGoldAdd) {
            _messageIcon.spriteName = _goldMark;
            _message = "코인 " + pValue + "개를 얻었어요.\n(広告を見ると、魚を受けることができます。)";

            _btnConfirm.SetActive(true);
            _lblbtnConfirm.text = GameSystem.Instance.GetLocalizeText("3233"); // 확인


        }
        else if (_messageType == PopMessageType.NekoGiftGemAdd) {
            _messageIcon.spriteName = _diaMark;
            _message = "보석 " + pValue + "개를 얻었어요.\n(広告を見ると、魚を受けることができます。)";

            _btnConfirm.SetActive(true);
            _lblbtnConfirm.text = GameSystem.Instance.GetLocalizeText("3233"); // 확인


        }
        else if (pType == PopMessageType.CantGrowNeko) {
            
            _message = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3901);
        }
        else if (pType == PopMessageType.NekoGonnaMaxGrade) {
            
            _message = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3902);
        }
        else if (pType == PopMessageType.NekoAdd || pType == PopMessageType.RankRewardNekoAdd) {
            //_message = "ネコを獲得しました。 "; // 임시. 
            return;
        }

        else if (pType == PopMessageType.AddFreeTicket) {
            _messageIcon.spriteName = PuzzleConstBox.spriteUIFreeTicket;
            _message = GameSystem.Instance.GetLocalizeText("3208");
        }
        else if (pType == PopMessageType.AddRareTicket) {
            _messageIcon.spriteName = PuzzleConstBox.spriteUIRareTicket;
            
            _message = GameSystem.Instance.GetLocalizeText("3209");
        }
        else if (pType == PopMessageType.PowerUpgrade) {
            _messageIcon.spriteName = PuzzleConstBox.spriteUIGoldMark;
            _message = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3428).Replace("[n]", pValue);
        }
        else if (pType == PopMessageType.PowerUpgradeWithGem) {
            _messageIcon.spriteName = PuzzleConstBox.spriteUIDiaMark;
            _message = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3429).Replace("[n]", pValue);
        }
        else if (pType == PopMessageType.RescueRewarded) {
            _messageIcon.spriteName = PuzzleConstBox.spriteUIDiaMark;
            _message = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3608);
        }
        else if (pType == PopMessageType.WakeUpCoinAdd) {
            _messageIcon.spriteName = PuzzleConstBox.spriteUICoinMark;
            _message = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L4249).Replace("[n]", pValue);
        }
        else if (pType == PopMessageType.WakeUpGemAdd) {
            _messageIcon.spriteName = PuzzleConstBox.spriteUIDiaMark;
            _message = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L4248).Replace("[n]", pValue);
        }


        _lblMessage.text = _message;
        _lblConfirmMessage.text = _message;
    }

    #endregion


    /// <summary>
    /// 일반 메세지 초기화 
    /// </summary>
    private void InitMessage() {
        OnCompleteClose = delegate { };

        _lblMessage.gameObject.SetActive(true);
        _lblConfirmMessage.gameObject.SetActive(false);
        _messageIcon.gameObject.SetActive(false);
        _fishIcon.gameObject.SetActive(false);
        _nekoSprite.gameObject.SetActive(false);

        _btnConfirm.SetActive(true);

        _groupYesNoButton.SetActive(false);


        _lblbtnConfirm.text = GameSystem.Instance.GetLocalizeText("3233"); // 확인
        _btnConfirm.transform.localPosition = new Vector3(0, -155, 0);
    }


    /// <summary>
    /// 특수 메세지 초기화
    /// </summary>
    private void InitConfirmMessage() {
        OnCompleteClose = delegate { };

        _lblMessage.gameObject.SetActive(false);
        _lblConfirmMessage.gameObject.SetActive(true);
        _messageIcon.gameObject.SetActive(true);
        _fishIcon.gameObject.SetActive(false);
        _nekoSprite.gameObject.SetActive(false);

        _btnConfirm.SetActive(true);

        _groupYesNoButton.SetActive(false);



        _lblbtnConfirm.text = GameSystem.Instance.GetLocalizeText("3233"); // 확인
        _btnConfirm.transform.localPosition = new Vector3(0, -155, 0);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="pType"></param>
    public void SetMessage(PopMessageType pType) {
        _messageType = pType;

        InitMessage();
        SetText(pType);

    }


    /// <summary>
    /// 일반 안내 메세지 
    /// </summary>
    /// <param name="pType"></param>
    public void SetInfoMessage(PopMessageType pType, Action callBack) {
    
        // 메세지 창 활성화 
        this.gameObject.SetActive(true);

        _messageType = pType;

        InitMessage(); // 초기화 

        SetText(pType); // 텍스트 처리 

        OnCompleteClose += callBack; // 콜백 처리 

    }


    /// <summary>
    /// 아이콘이 포함된 확인 메세지 
    /// </summary>
    /// <param name="pType"></param>
    /// <param name="pValue"></param>
    /// <param name="callBack"></param>
    public void SetConfirmMessage(PopMessageType pType, string pValue, Action callBack) {


        this.gameObject.SetActive(true);

        _confirmValue = pValue;

        _messageType = pType;

        InitConfirmMessage();

        if(_messageType == PopMessageType.NekoAdd || _messageType == PopMessageType.RankRewardNekoAdd) {
            SetNekoInfo(pValue);
        }
        else if(_messageType == PopMessageType.GetUserEventReward) {
            SetUserEventNekoInfo(pValue);
        }

        SetConfirmText(pType, pValue);

        OnCompleteClose += callBack;


    }


    /// <summary>
    /// 특정 유저 달성 목표 완료시 획득하는 네코 정보 표기 
    /// </summary>
    /// <param name="pValue"></param>
    private void SetUserEventNekoInfo(string pValue) {

        Debug.Log("SetUserEventNekoInfo :: " + pValue);

        int nekoID, nekoStar;

        
        _nekoSprite.gameObject.SetActive(true);
        _nekoFishIcon.gameObject.SetActive(false);
        _messageIcon.gameObject.SetActive(false);

        _nekoNode = JSON.Parse(pValue);

        nekoID = _nekoNode["nekotid"].AsInt;
        nekoStar = _nekoNode["star"].AsInt;


        // 네코 이름 + 입수 메세지 
        _message = GameSystem.Instance.GetLocalizeText(_nekoNode["message"]);
        _lblMessage.text = _message;
        _lblConfirmMessage.text = _message;

        // 네코 스프라이트 
        GameSystem.Instance.SetNekoSprite(_nekoSprite, nekoID, nekoStar);

        LobbyCtrl.Instance.SendMessage("PlayUnlock");

    }


    /// <summary>
    /// 고양이 획득 정보
    /// 
    /// </summary>
    /// <param name="pValue"></param>
    private void SetNekoInfo(string pValue) {


        int nekoID, nekoStar, fishValue;
        string fishtype;

        Debug.Log("SetNekoInfo in SimplePopup pValue :: " + pValue);


        _nekoSprite.gameObject.SetActive(true);
        _nekoFishIcon.gameObject.SetActive(false);
        _messageIcon.gameObject.SetActive(false);

        _nekoNode = JSON.Parse(pValue);

        nekoID = _nekoNode["tid"].AsInt;
        nekoStar = _nekoNode["star"].AsInt;
        fishtype = _nekoNode["fishtype"].Value;
        fishValue = _nekoNode["fishvalue"].AsInt;


        // 네코 이름 + 입수 메세지 
        _message = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3610).Replace("[n]", GameSystem.Instance.GetNekoName(nekoID, nekoStar));
        _lblMessage.text = _message;
        _lblConfirmMessage.text = _message;

        // Fusion 여부 체크
        if (_nekoNode["isFusion"].AsInt != 0) {

            _nekoFishIcon.gameObject.SetActive(true);
            _nekoFishValue.gameObject.SetActive(true);

            if (fishtype == "chub")
                _nekoFishIcon.spriteName = PuzzleConstBox.spriteUIChubMark;
            else if (fishtype == "tuna")
                _nekoFishIcon.spriteName = PuzzleConstBox.spriteUITunaMark;
            else if (fishtype == "salmon")
                _nekoFishIcon.spriteName = PuzzleConstBox.spriteUISalmonMark;


            _nekoFishValue.text = "+" + fishValue.ToString();
        }

        // 네코 스프라이트 
        GameSystem.Instance.SetNekoSprite(_nekoSprite, nekoID, nekoStar);

        // 생선값 업데이트 
        if (fishtype == "chub")
            GameSystem.Instance.UserChub += fishValue;
        else if (fishtype == "tuna")
            GameSystem.Instance.UserTuna += fishValue;
        else if (fishtype == "salmon")
            GameSystem.Instance.UserSalmon += fishValue;


        // Neko Add
        GameSystem.Instance.UpdateSingleNekoData(_nekoNode);


    }


    /// <summary>
    /// 확인 메세지 (아이콘이 포함된)
    /// </summary>
    /// <param name="pType">P type.</param>
    public void SetConfirmMessage(PopMessageType pType, string pValue) {
        SetConfirmMessage(pType, pValue, null);
	}



    public void GetNeko2ndReward() {
	
		this.SendMessage ("CloseSelf");
	}


    /// <summary>
    /// 창이 닫히기 전 실행 
    /// </summary>
    public void OnClosing() {


        // 모든 메일 읽기 중에는 동작하지 않도록 처리 (XL 요청)
        if (LobbyCtrl.Instance != null && LobbyCtrl.Instance.IsProcessingReadAllMail)
            return;

        // 재화관련된 팝업들은 그냥 닫으면 UpdateTopInformation을 호출 
        if (_messageType == PopMessageType.GoldAdd || _messageType == PopMessageType.GemAdd || _messageType == PopMessageType.HeartAdd
            || _messageType == PopMessageType.ChubAdd || _messageType == PopMessageType.TunaAdd || _messageType == PopMessageType.SalmonAdd) {

            LobbyCtrl.Instance.UpdateTopInformation();

        }
        else if(_messageType == PopMessageType.ReLogin) { // re-login
            GameSystem.Instance.LoadTitleScene();
        }

        else if (_messageType == PopMessageType.GoldPurchase) {
            OnCompleteClose();
            OnCompleteClose = delegate { };
        }


    }

    /// <summary>
    /// Confirm, YES 버튼 
    /// </summary>
    public void Confirm() {


        // 모든 메일 읽기 중에는 동작하지 않도록 처리 (XL 요청)
        if (LobbyCtrl.Instance != null && LobbyCtrl.Instance.IsProcessingReadAllMail)
            return;

        if (_messageType == PopMessageType.GatchaConfirm) {

            Debug.Log("▣▣▣ GatchaConfirm Confirm");
            this.SendMessage("CloseSelf");
            //Invoke("DelayedCallback", 0.5f);
            OnCompleteClose();
            OnCompleteClose = delegate { };
            return;
        }

        OnCompleteClose();
        OnCompleteClose = delegate { };


        this.SendMessage("CloseSelf");

	}

    public void OnClickNo() {
        if (_messageType == PopMessageType.NekoGiftWithAdsConfirm) {
            LobbyCtrl.Instance.DelayedGetNekoFreeGift();
            this.SendMessage("CloseSelf");
        }
		else if(_messageType == PopMessageType.BingoNeed) {
            LobbyCtrl.Instance.GameTip.SetGameTip(TipType.Bingo);
            this.SendMessage("CloseSelf");
        }
        else {


            OnClosing();
            this.SendMessage("CloseSelf");
        }
    }




    public void OpenMessagePopupCallback(PopMessageType pType, Action callBack) {

        OnCompleteClose = delegate { };

        _messageType = pType;
        _lblMessage.gameObject.SetActive(true);
        _lblConfirmMessage.gameObject.SetActive(false);
        _messageIcon.gameObject.SetActive(false);
        _fishIcon.gameObject.SetActive(false);
        _nekoSprite.gameObject.SetActive(false);
        _btnConfirm.SetActive(false);

        _groupYesNoButton.SetActive(true);

        _lblbtnConfirm.text = GameSystem.Instance.GetLocalizeText("3233"); // 확인
        _btnConfirm.transform.localPosition = new Vector3(0, -155, 0);


        OnCompleteClose += callBack;


        if (pType == PopMessageType.GatchaConfirm) {
            
            _message = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3033);
            _message = _message.Replace("[n]", "300"); // 300으로 수동 패치
        }
        else if (pType == PopMessageType.ConfirmSpecialGatchaTen) {
            //_message = GameSystem.Instance.DocsLocalize.get<string>("3033", "content");
            //_message = "だいや[n]個で10回\n「すぺしゃるくれーん」に\nちょうせんしますか？";
            _message = _message.Replace("[n]", GameSystem.Instance.GetNumberToString(GameSystem.Instance.SpecialMultiGatchaPrice)); // 
        }

        _lblMessage.text = _message;
        _lblConfirmMessage.text = _message;
    }
}
