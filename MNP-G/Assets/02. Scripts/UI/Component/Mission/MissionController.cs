using UnityEngine;
using System.Collections;
using PathologicalGames;

public class MissionController : MonoBehaviour {
    [SerializeField]
    UIButton btnDay;
    [SerializeField]
    UIButton btnWeek;

    [SerializeField]
    UIPanel pnlMissionScrollView;

    [SerializeField]
    UILabel _lblExpireInfo;


    [SerializeField] bool _isDaily = true;

    public bool IsDaily {
        get {
            return _isDaily;
        }

        set {
            _isDaily = value;
        }
    }

    void OnEnable() {
        
    }

    void OnDisable() {

    }

    public void SetTabButton() {
        Debug.Log(">>>> MissionControl SetTabButton ");
        // True
        if (IsDaily) {
            btnDay.normalSprite = "common_btn_tap_red";
            btnWeek.normalSprite = "common_btn_tap_gray";
            LobbyCtrl.Instance.SpawnDailyMissions();

            _lblExpireInfo.text = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3248);
        }
        else {
            btnWeek.normalSprite = "common_btn_tap_red";
            btnDay.normalSprite = "common_btn_tap_gray";
            LobbyCtrl.Instance.SpawnWeeklyMissions();

            _lblExpireInfo.text = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3250);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void InitMissionController() {
        if (IsDaily) {
            btnDay.normalSprite = "common_btn_tap_red";
            btnWeek.normalSprite = "common_btn_tap_gray";

            _lblExpireInfo.text = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3248);
        }
        else {
            btnWeek.normalSprite = "common_btn_tap_red";
            btnDay.normalSprite = "common_btn_tap_gray";

            _lblExpireInfo.text = GameSystem.Instance.GetLocalizeText(Google2u.MNP_Localize.rowIds.L3250);
        }
    }

    public void OnClickDay() {
        IsDaily = true;
        SetTabButton();

    }

    public void OnClickWeek() {
        IsDaily = false;
        SetTabButton();
    }




    public void DespawnAll() {
        PoolManager.Pools["MissionPool"].DespawnAll();
    }
}
