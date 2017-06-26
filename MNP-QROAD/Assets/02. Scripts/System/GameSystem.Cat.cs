using UnityEngine;
using System.Collections;
using SimpleJSON;

public partial class GameSystem : MonoBehaviour {





    /// <summary>
    /// 고양이 정렬
    /// </summary>
    private void InitSortUserNeko() {
        _listUserNeko.Clear();

        //Debug.Log("InitSortUserNeko Count :: " + _userNekoData[_jData]["nekodatas"].Count);

        for (int i = 0; i < UserNeko.Count; i++) {
            _listUserNeko.Add(UserNeko[i]);
        }
    }


    /// <summary>
    /// 등급 순으로 정렬 
    /// </summary>
    public void SortUserNekoByBead() {
        InitSortUserNeko();
        _listUserNeko.Sort(delegate (JSONNode node1, JSONNode node2) { return node2["bead"].AsInt.CompareTo(node1["bead"].AsInt); });
    }

    /// <summary>
    /// 획득 순서대로 정렬
    /// </summary>
    public void SortUserNekoByGet() {
        InitSortUserNeko();
        _listUserNeko.Sort(delegate (JSONNode node1, JSONNode node2) { return node1["bead"].AsInt.CompareTo(node2["bead"].AsInt); });
    }

    /// <summary>
    /// 고양이 * 등급 텍스트 
    /// </summary>
    /// <param name="pStar"></param>
    /// <returns></returns>
    public string GetNekoGradeText(int pStar) {

        string returnValue = string.Empty;

        for (int i = 0; i < pStar; i++) {
            returnValue += "*";
        }

        return returnValue;
    }


    /// <summary>
    /// 고양이 순수 파워 조회 
    /// </summary>
    /// <param name="pGrade">등급</param>
    /// <param name="pLevel">레벨</param>
    /// <returns></returns>
    public int GetNekoPurePower(int pGrade, int pLevel) {
        int power = 0;

        switch(pGrade) {
            case 1:
                power = 100;
                break;
            case 2:
                power = 150;
                break;
            case 3:
                power = 250;
                break;
            case 4:
                power = 350;
                break;
            case 5:
                power = 500;
                break;

        }


        power += (pLevel - 1) * 30;

        return power;

    }

    /// <summary>
    /// 인게임상의 네코 파워 조회 
    /// </summary>
    /// <returns></returns>
    public float GetNekoInGamePower(int pNekoID) {

        float power = 100;
        JSONNode neko = GetNekoNodeByID(pNekoID);

        if (neko == null)
            return 0;



        switch (neko["star"].AsInt) {
            case 1:
                power = 100;
                break;
            case 2:
                power = 150;
                break;
            case 3:
                power = 250;
                break;
            case 4:
                power = 350;
                break;
            case 5:
                power = 500;
                break;

        }

        power += (neko["level"].AsInt - 1) * 30;


        // 패시브 능력 처리 
        if (GameSystem.Instance.NekoPowerPlus > 0) {
            power = power + (power * GameSystem.Instance.NekoPowerPlus / 100);
        }

        return power;
    }



    /// <summary>
    /// 고양이 그룹 이름 조회 
    /// </summary>
    /// <param name="pGroupID"></param>
    /// <returns></returns>
    public string GetCatGroupName(int pGroupID) {

        switch(pGroupID) {
            case 1:
                return GetLocalizeText(Google2u.MNP_Localize.rowIds.L6600);
            case 2:
                return GetLocalizeText(Google2u.MNP_Localize.rowIds.L6601);
            case 3:
                return GetLocalizeText(Google2u.MNP_Localize.rowIds.L6602);
            case 4:
                return GetLocalizeText(Google2u.MNP_Localize.rowIds.L6603);
            case 5:
                return GetLocalizeText(Google2u.MNP_Localize.rowIds.L6604);
            case 6:
                return GetLocalizeText(Google2u.MNP_Localize.rowIds.L6605);
            case 7:
                return GetLocalizeText(Google2u.MNP_Localize.rowIds.L6606);
            case 8:
                return GetLocalizeText(Google2u.MNP_Localize.rowIds.L6607);
            case 9:
                return GetLocalizeText(Google2u.MNP_Localize.rowIds.L6608);
            case 10:
                return GetLocalizeText(Google2u.MNP_Localize.rowIds.L6609);
            case 11:
                return GetLocalizeText(Google2u.MNP_Localize.rowIds.L6610);
            default:
                return string.Empty;
        }


    }

    /// <summary>
    /// 네코 종합 데이터 조회 
    /// </summary>
    /// <param name="pNeko"></param>
    /// <returns></returns>
    public NekoData GetNekoData(JSONNode pNeko) {
        NekoData nekodata = new NekoData();


        // tid, level.grade, bead
        nekodata.skillCount = 0;

        nekodata.skillid1 = pNeko["skillid1"].AsInt;
        if (nekodata.skillid1 > 0)
            nekodata.skillCount++;

        nekodata.skillid2 = pNeko["skillid2"].AsInt;
        if (nekodata.skillid2 > 0)
            nekodata.skillCount++;

        nekodata.skillid3 = pNeko["skillid3"].AsInt;
        if (nekodata.skillid3 > 0)
            nekodata.skillCount++;

        nekodata.skillid4 = pNeko["skillid4"].AsInt;
        if (nekodata.skillid4 > 0)
            nekodata.skillCount++;

        nekodata.skillvalue1 = pNeko["skillvalue1"].AsInt;
        nekodata.skillvalue2 = pNeko["skillvalue2"].AsInt;
        nekodata.skillvalue3 = pNeko["skillvalue3"].AsInt;
        nekodata.skillvalue4 = pNeko["skillvalue4"].AsInt;

        nekodata.skillname1 = GetNekoSkillName(nekodata.skillid1, nekodata.skillvalue1.ToString());
        nekodata.skillname2 = GetNekoSkillName(nekodata.skillid2, nekodata.skillvalue2.ToString());
        nekodata.skillname3 = GetNekoSkillName(nekodata.skillid3, nekodata.skillvalue3.ToString());
        nekodata.skillname4 = GetNekoSkillName(nekodata.skillid4, nekodata.skillvalue4.ToString());

        nekodata.listSkillInfo = new System.Collections.Generic.List<string>();
        nekodata.listSkillInfo.Clear();

        if (nekodata.skillCount > 0)
            nekodata.listSkillInfo.Add(nekodata.skillname1);

        if (nekodata.skillCount > 1)
            nekodata.listSkillInfo.Add(nekodata.skillname2);

        if (nekodata.skillCount > 2)
            nekodata.listSkillInfo.Add(nekodata.skillname3);

        if (nekodata.skillCount > 3)
            nekodata.listSkillInfo.Add(nekodata.skillname4);

        return nekodata;
    }


    /// <summary>
    /// 네코 스킬 명 가져오기 
    /// </summary>
    /// <param name="pSkillID"></param>
    /// <returns></returns>
    string GetNekoSkillName(int pSkillID, string pSkillValue) {

        switch(pSkillID){

            case 1:
                return GetLocalizeText(Google2u.MNP_Localize.rowIds.L3950).Replace("[n]", pSkillValue);
            case 2:
                return GetLocalizeText(Google2u.MNP_Localize.rowIds.L3951).Replace("[n]", pSkillValue);
            case 3:
                return GetLocalizeText(Google2u.MNP_Localize.rowIds.L3952).Replace("[n]", pSkillValue);
            case 4:
                return GetLocalizeText(Google2u.MNP_Localize.rowIds.L3953).Replace("[n]", pSkillValue);
            case 5:
                return GetLocalizeText(Google2u.MNP_Localize.rowIds.L3954).Replace("[n]", pSkillValue);
            case 6:
                return GetLocalizeText(Google2u.MNP_Localize.rowIds.L3955).Replace("[n]", pSkillValue);
            case 7:
                return GetLocalizeText(Google2u.MNP_Localize.rowIds.L3956).Replace("[n]", pSkillValue);
            case 8:
                return GetLocalizeText(Google2u.MNP_Localize.rowIds.L3957).Replace("[n]", pSkillValue);
            case 9:
                return GetLocalizeText(Google2u.MNP_Localize.rowIds.L3958).Replace("[n]", pSkillValue);
            case 10:
                return GetLocalizeText(Google2u.MNP_Localize.rowIds.L3959).Replace("[n]", pSkillValue);
            case 11:
                return GetLocalizeText(Google2u.MNP_Localize.rowIds.L3960).Replace("[n]", pSkillValue);
            case 12:
                return GetLocalizeText(Google2u.MNP_Localize.rowIds.L3961).Replace("[n]", pSkillValue);
            case 13:
                return GetLocalizeText(Google2u.MNP_Localize.rowIds.L3962).Replace("[n]", pSkillValue);
            case 14:
                return GetLocalizeText(Google2u.MNP_Localize.rowIds.L3963).Replace("[n]", pSkillValue);
            case 15:
                return GetLocalizeText(Google2u.MNP_Localize.rowIds.L3964).Replace("[n]", pSkillValue);
            case 16:
                return GetLocalizeText(Google2u.MNP_Localize.rowIds.L3965).Replace("[n]", pSkillValue);

            default:
                return string.Empty;

        }
    }
    
 
}
