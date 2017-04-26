using UnityEngine;
using System;
using System.Collections;
using SimpleJSON;
using PathologicalGames;

public class NekoTicketWindowCtrl : MonoBehaviour {

    [SerializeField]
    UIPanel _scrollView;

    JSONNode _ticketNode;

    [SerializeField]
    NekoTicketExchangeCtrl _spawnedTicket;

    [SerializeField]
    NekoTicketConfirmCtrl _confirmWindow;

    [SerializeField]
    int _mailKey;

    MailColumnCtrl _targetMail;
    public static event Action OnCompleteTicketRead;

    void OnDisable() {
        PoolManager.Pools[PuzzleConstBox.nekoTicketExchangePool].DespawnAll();
    }

    /// <summary>
    /// 뷰 오픈!
    /// </summary>
    /// <param name="pNode"></param>
    public void SetNekoTicketExchangeView(JSONNode pNode, int pMailKey, MailColumnCtrl pMail, Action pCallback ) {
        this.gameObject.SetActive(true);

        _mailKey = pMailKey;
        _targetMail = pMail;

        OnCompleteTicketRead = delegate { };
        OnCompleteTicketRead += pCallback;
             

        _ticketNode = pNode;
        InitScrollView();

        SpawnTickets();


    }

    private void InitScrollView() {
        _scrollView.gameObject.GetComponent<UIScrollView>().ResetPosition();
        _scrollView.clipOffset = Vector2.zero;
        _scrollView.transform.localPosition = new Vector3(0, 260, 0);
    }

    private void SpawnTickets() {
        for(int i=0; i<_ticketNode.Count; i++) {

            _spawnedTicket = PoolManager.Pools[PuzzleConstBox.nekoTicketExchangePool].Spawn(PuzzleConstBox.prefabNekoTicketExchange).GetComponent<NekoTicketExchangeCtrl>();
            _spawnedTicket.SetNekoInfo(this, _ticketNode[i], i);

        }

        // 재배치 
        //_scrollView.GetComponent<UIGrid>().Reposition();
    }


    /// <summary>
    /// 확인창 오픈 
    /// </summary>
    /// <param name="pNekoID"></param>
    /// <param name="pNekoGrade"></param>
    /// <param name="pGradeStar"></param>
    /// <param name="pNekoName"></param>
    /// <param name="pSprite"></param>
    public void OpenConfirm(int pNekoID, int pNekoGrade, string pGradeStar, string pNekoName, UISprite pSprite) {
        // callback으로 ReadTicket
        _confirmWindow.OpenNekoTicketExchangeConfirm(pNekoID, pNekoGrade, pGradeStar, pNekoName, pSprite, ReadTicket);
    }

    /// <summary>
    /// 확인창에서 YES 터치 
    /// </summary>
    public void ReadTicket(int pNekoID, int pNekoGrade) {

        // 교환할 정보를 추가 기재 
        _targetMail.ExchangeNekoID = pNekoID;
        _targetMail.ExchangeNekoGrade = pNekoGrade;

        GameSystem.Instance.Post2MailRead(_mailKey, _targetMail, OnCompleteTicketRead);

        this.GetComponent<LobbyCommonUICtrl>().CloseSelf();

    }


}
