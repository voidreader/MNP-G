using UnityEngine;
using System.Collections;

public class NekoAppearCtrl : MonoBehaviour {

    public UISprite spNeko;
    

    public Transform nekoTarget;
    public Camera worldCam = null;
    public Camera guiCam = null;

    private bool isInit = false;
    private Vector3 pos = Vector3.zero;  // hp가 위치할 좌표
    private Vector3 targetPos = Vector3.zero; // 타겟의 월드좌표 

    string namePrefix;

    [SerializeField] UISprite _cageBack;
    [SerializeField] UISprite _cageFront;

    [SerializeField] GameObject _drop; // 땀방울



    [SerializeField] bool _isFiveGradePossible = false;

    public NekoAppearCtrl InitNekoAppear(Transform neko, int nekoID, int grade, bool isPlay) {
        this.gameObject.SetActive(true);

        nekoTarget = neko;
        worldCam = NGUITools.FindCameraForLayer(nekoTarget.gameObject.layer);
        guiCam = NGUITools.FindCameraForLayer(gameObject.layer);



        isInit = true;

        //spNeko.atlas
        spNeko.gameObject.SetActive(true);
        GameSystem.Instance.SetNekoSprite(spNeko, nekoID, grade);



        return this;
    }




    /// <summary>
    /// 인게임 네코 설정 
    /// </summary>
    /// <param name="neko"></param>
    /// <param name="nekoID"></param>
    /// <param name="grade"></param>
    /// <param name="isPlay"></param>
    /// <returns></returns>
    public NekoAppearCtrl InitRescueNekoCloneAppear(Transform neko, int nekoID) {
        this.gameObject.SetActive(true);

        nekoTarget = neko;
        worldCam = NGUITools.FindCameraForLayer(nekoTarget.gameObject.layer);
        guiCam = NGUITools.FindCameraForLayer(gameObject.layer);



        isInit = true;

        //spNeko.atlas
        spNeko.gameObject.SetActive(true);


        GameSystem.Instance.SetNekoSpriteByID(spNeko, nekoID);

        spNeko.width = 100;
        spNeko.height = 100;

        return this;
    }

    /// <summary>
    /// 인게임 네코 설정 
    /// </summary>
    /// <param name="neko"></param>
    /// <param name="nekoID"></param>
    /// <param name="grade"></param>
    /// <param name="isPlay"></param>
    /// <returns></returns>
    public NekoAppearCtrl InitRescueNekoAppear(Transform neko, int nekoID) {
        this.gameObject.SetActive(true);

        nekoTarget = neko;
        worldCam = NGUITools.FindCameraForLayer(nekoTarget.gameObject.layer);
        guiCam = NGUITools.FindCameraForLayer(gameObject.layer);



        isInit = true;

        //spNeko.atlas
        spNeko.gameObject.SetActive(true);

        
        GameSystem.Instance.SetNekoSpriteByID(spNeko, nekoID);

        spNeko.width = 100;
        spNeko.height = 100;

        SetOriginCage();

        return this;
    }



    public NekoAppearCtrl InitBossNekoAppear(Transform neko, int nekoID) {
        this.gameObject.SetActive(true);

        nekoTarget = neko;
        worldCam = NGUITools.FindCameraForLayer(nekoTarget.gameObject.layer);
        guiCam = NGUITools.FindCameraForLayer(gameObject.layer);



        isInit = true;

        //spNeko.atlas
        spNeko.gameObject.SetActive(true);
        
        GameSystem.Instance.SetNekoSpriteByID(spNeko, nekoID);

        spNeko.GetComponent<UISprite>().height = 220;
        spNeko.GetComponent<UISprite>().width = 220;

        //spNeko.transform.localScale = new Vector3(1.2f, 1.2f, 1);

        SetHideCage();
        return this;
    }

    public void SetOriginCage() {
        _cageBack.gameObject.SetActive(true);
        _cageFront.gameObject.SetActive(true);

        _cageBack.spriteName = PuzzleConstBox.spriteCageBackFull;
        _cageFront.spriteName = PuzzleConstBox.spriteCageFrontFull;
    }

    public void SetHalfCage() {
        _cageBack.gameObject.SetActive(true);
        _cageFront.gameObject.SetActive(true);

        _cageBack.spriteName = PuzzleConstBox.spriteCageBackHalf;
        _cageFront.spriteName = PuzzleConstBox.spriteCageFrontHalf;
    }

    public void SetHideCage() {
        _cageBack.gameObject.SetActive(false);
        _cageFront.gameObject.SetActive(false);
    }


    /// <summary>
    /// 사이즈 세팅 
    /// </summary>
    /// <param name="pSize"></param>
    public void SetSize(NekoAppearSize pSize) {
        
        if (pSize == NekoAppearSize.Small) {
            this.transform.localScale = PuzzleConstBox.smallScale;
        }
        else if (pSize == NekoAppearSize.Mini) {

        }

    }

    /// <summary>
    /// Inits the neko follow H.
    /// </summary>
    /// <param name="neko">Neko.</param>
    public NekoAppearCtrl InitNekoAppear(Transform neko, int nekoID, int grade) {

        return InitNekoAppear(neko, nekoID, grade, true);

    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="neko"></param>
    /// <param name="nekoID"></param>
    /// <param name="grade"></param>
    /// <returns></returns>
    public NekoAppearCtrl InitSmallNekoAppear(Transform neko, int nekoID, int grade) {
        this.gameObject.SetActive(true);

        nekoTarget = neko;
        worldCam = NGUITools.FindCameraForLayer(nekoTarget.gameObject.layer);
        guiCam = NGUITools.FindCameraForLayer(gameObject.layer);

        isInit = true;

        //spNeko.atlas
        spNeko.gameObject.SetActive(true);
        GameSystem.Instance.SetNekoSprite(spNeko, nekoID, grade);

        // 크기 조정 
        spNeko.width = 100;
        spNeko.height = 100;



        return this;
    }


    /// <summary>
    /// Player My Neko 설정 
    /// </summary>
    /// <param name="neko"></param>
    /// <param name="nekoID"></param>
    /// <returns></returns>
    public NekoAppearCtrl InitMyNekoAppearByTID(Transform neko, int nekoID) {

        int star = GameSystem.Instance.FindNekoStarByNekoID(nekoID);

        // 스케일을 2배로.
        if(InGameCtrl.Instance != null)
            this.transform.localScale = new Vector3(1.5f, 1.5f, 1);


        if (star < 0) {
            Debug.Log("★★★  InitMyNekoAppearByTID Can't Find Neko Star :: " + nekoID);
            return InitNekoAppear(neko, nekoID, 3);
        }
        else
            return InitNekoAppear(neko, nekoID, star);
        
    }




    /// <summary>
    /// 결과화면 NekoAppear 세팅 
    /// </summary>
    /// <param name="neko"></param>
    /// <param name="nekoID"></param>
    /// <returns></returns>
    public NekoAppearCtrl InitResultNekoAppear(Transform neko, int nekoID) {

        nekoTarget = neko;
        worldCam = NGUITools.FindCameraForLayer(nekoTarget.gameObject.layer);
        guiCam = NGUITools.FindCameraForLayer(gameObject.layer);



        isInit = true;

        //spNeko.atlas
        spNeko.gameObject.SetActive(true);

		GameSystem.Instance.SetNekoSprite (spNeko, nekoID, 3);

        //spNeko.atlas = GameSystem.Instance.GetNekoUIAtlas(GameSystem.Instance.NekoBaseInfo.get<string>(nekoID.ToString(), "collection_name"));

        //namePrefix = "neko" + (nekoID + 1).ToString() + "-";
        //spNeko.spriteName = namePrefix + "1";
        //spNekoAnimator.namePrefix = namePrefix;

        //spNekoAnimator.Play();

        return this;
    }


    public void OnNekoDrop() {
        _drop.SetActive(true);
    }

    public void OffNekoDrop() {
        _drop.SetActive(false);
    }

    public void AnimateNekoAppear() {
        
    }

    public void ChangeDepth(int pDepth) {
        spNeko.depth = pDepth;
    }


    /*
    public void SetRotation(Vector3 pRot) {

        //Debug.Log(">>> SetRotation");

        //this.spNeko.transform.localRotation = Quaternion.Euler(pRot);
        //this.transform.DORotate(pRot, 0.1f, RotateMode.FastBeyond360);
    }

    public void SetOppositeRotation() {

        if (this.transform.localEulerAngles.y == 0) {
            this.transform.localEulerAngles = new Vector3(0, 180, 0);
        }
        else {
            this.transform.localEulerAngles = new Vector3(0, 0, 0);
        }
    }
    */


    // Update is called once per frame
    void Update() {
        if (!isInit)
            return;

        targetPos = nekoTarget.position;
        //targetPos.x = targetPos.x - 0.35f;
        //targetPos.y = targetPos.y + 0.7f;

        //타겟의 포지션을 월드좌표에서 ViewPort좌표로 변환하고 다시 ViewPort좌표를 NGUI월드좌표로 변환합니다.
        pos = guiCam.ViewportToWorldPoint(worldCam.WorldToViewportPoint(targetPos));
        pos.z = 0;

        transform.position = pos;

        //rotation = nekoTarget.rotation;
        transform.rotation = nekoTarget.rotation;
    }

    public void KillNekoAppear() {

        isInit = false;
        this.gameObject.SetActive(false);
    }

    private void OnSpawned() {
        isInit = false;
        this.transform.localScale = GameSystem.Instance.BaseScale;

        spNeko.gameObject.SetActive(false);

        if(BottleManager.Instance != null) {
            BottleManager.Instance.ListBottleNekoAppear.Add(this);
        }
        
    }

    private void OnDespawned() {
        isInit = false;
    }
}
