using UnityEngine;
using System.Collections;

public class NekoFollowHP : MonoBehaviour {

	//public UIProgressBar nekoHP = null;
	public GameObject nekoTarget;
	public Camera worldCam = null;
	public Camera guiCam = null;
	
	private bool isInit = false;
	private Vector3 pos = Vector3.zero;  // hp가 위치할 좌표
	private Vector3 targetPos = Vector3.zero; // 타겟의 월드좌표 
	
	public float maxHP; // 최대 HP 
	public float currentHP; // 현 HP
	public UISlider nekoHPBar = null;

    [SerializeField] UISprite _spriteBase;
    [SerializeField] UISprite _spriteFill;

    bool _isBossNeko = false;
    bool _isTopPlayerCat = false;

	/// <summary>
	/// Inits the neko follow H.
	/// </summary>
	/// <param name="neko">Neko.</param>
	public void InitNekoFollowHP(GameObject neko, bool pBoss) {

        _isBossNeko = pBoss;

        nekoTarget = neko;
		worldCam = NGUITools.FindCameraForLayer (nekoTarget.layer);
		guiCam = NGUITools.FindCameraForLayer (gameObject.layer);
		
		gameObject.SetActive (true);
		isInit = true;
	}

    /// <summary>
    /// 고양이를 따라다니는 상단 바 (스킬바)
    /// </summary>
    /// <param name="neko"></param>
    public void InitTopPlayerCatFollowBar(GameObject neko) {
        nekoTarget = neko;
        worldCam = NGUITools.FindCameraForLayer(nekoTarget.layer);
        guiCam = NGUITools.FindCameraForLayer(gameObject.layer);

        

        gameObject.SetActive(true);

        isInit = true;
        _isTopPlayerCat = true;
    }

	/// <summary>
	/// Neko HP 값 조정 
	/// </summary>
	/// <param name="pMaxHP">P max H.</param>
	/// <param name="pCurHP">P current H.</param>
	public void SetNekoHP(float pMaxHP, float pCurHP) {
		maxHP = pMaxHP;
		currentHP = pCurHP;
		nekoHPBar.value = currentHP / maxHP;
	}


    /// <summary>
    /// 고양이 일자 스킬바 
    /// </summary>
    /// <param name="pMax"></param>
    /// <param name="pCurrent"></param>
    /// <param name="pIndex"></param>
    public void SetNekoSkillBar(float pMax, float pCurrent) {
        maxHP = pMax;
        currentHP = pCurrent;
        nekoHPBar.value = currentHP / maxHP;


    }


    /// <summary>
    /// 고양이 일자 스킬바 
    /// </summary>
    /// <param name="pMax"></param>
    /// <param name="pCurrent"></param>
    /// <param name="pIndex"></param>
    public void SetNekoSkillBar(float pMax, float pCurrent, int pIndex) {
        maxHP = pMax;
        currentHP = pCurrent;
        nekoHPBar.value = currentHP / maxHP;


        switch(pIndex) {
            case 0:
                _spriteBase.spriteName = "blue-cat-g-base";
                _spriteFill.spriteName = "blue-cat-g";
                break;
            case 1:
                _spriteBase.spriteName = "yellow-cat-g-base";
                _spriteFill.spriteName = "yellow-cat-g";
                break;
            case 2:
                _spriteBase.spriteName = "red-cat-g-base";
                _spriteFill.spriteName = "red-cat-g";
                break;

        }

        _spriteFill.width = 90;
        _spriteFill.height = 12;

        _spriteBase.width = 90;
        _spriteBase.height = 12;
    }


    void FollowTopPlayerCat() {
        targetPos = nekoTarget.transform.position;

        targetPos.x = targetPos.x - 0.5f;
        targetPos.y = targetPos.y + 0.8f;

        //타겟의 포지션을 월드좌표에서 ViewPort좌표로 변환하고 다시 ViewPort좌표를 NGUI월드좌표로 변환합니다.
        //pos = guiCam.ViewportToWorldPoint (worldCam.WorldToViewportPoint (nekoTarget.transform.position));
        pos = guiCam.ViewportToWorldPoint(worldCam.WorldToViewportPoint(targetPos));
        pos.z = 0;

        transform.position = pos;
    }

	// Update is called once per frame
	void Update () {
		if (!isInit)
			return;

        if(_isTopPlayerCat ) {

            FollowTopPlayerCat();
            return;
        }
		
		targetPos = nekoTarget.transform.position;

        if(_isBossNeko) {
            // targetPos.x = targetPos.x  0.4f;
            targetPos.x = targetPos.x - 0.5f;
            targetPos.y = targetPos.y + 1f;
        }
        else {
            targetPos.x = targetPos.x - 0.5f;
            targetPos.y = targetPos.y - 0.8f;
        }

		
		
		
		//타겟의 포지션을 월드좌표에서 ViewPort좌표로 변환하고 다시 ViewPort좌표를 NGUI월드좌표로 변환합니다.
		//pos = guiCam.ViewportToWorldPoint (worldCam.WorldToViewportPoint (nekoTarget.transform.position));
		pos = guiCam.ViewportToWorldPoint (worldCam.WorldToViewportPoint (targetPos));
		pos.z = 0;
		
		transform.position = pos;

	}

	public void HideNekoHP() {
		isInit = false;
		gameObject.SetActive (false);
	}
	
	

}
