using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using Google2u;

public partial class GameSystem : MonoBehaviour {

    private JSONNode _missionDayInitJSON; // 미션 기초 정보 
    private JSONNode _missionWeekInitJSON; // 미션 기초 정보 
    private JSONNode _bingoInitJSON; // 빙고 기초 정보 
    private JSONNode _bingoGroupJSON = null; // 빙고 관련 고양이 그룹 정보 
    List<int> _listBingoGroup1 = new List<int>();
    List<int> _listBingoGroup2 = new List<int>();
    List<int> _listBingoGroup3 = new List<int>();
    List<int> _listBingoGroup4 = new List<int>();
    List<int> _listBingoGroup5 = new List<int>();
    List<int> _listBingoGroup6 = new List<int>();
    List<int> _listBingoGroup7 = new List<int>();
    List<int> _listBingoGroup8 = new List<int>();
    List<int> _listBingoGroup9 = new List<int>();
    List<int> _listBingoGroup10 = new List<int>();
    List<int> _listBingoGroup11 = new List<int>();
    List<int> _listBingoGroup12 = new List<int>();
    List<int> _listBingoGroup13 = new List<int>();
    List<int> _listBingoGroup14 = new List<int>();
    List<int> _listBingoGroup15 = new List<int>();
    List<int> _listBingoGroup16 = new List<int>();
    List<int> _listBingoGroup17 = new List<int>();
    [SerializeField] List<int> _listBingoGroup18 = new List<int>();
    [SerializeField] List<int> _listBingoGroup19 = new List<int>();
    [SerializeField] List<int> _listBingoGroup20 = new List<int>();
    [SerializeField] List<int> _listBingoGroup21 = new List<int>();
    [SerializeField] List<int> _listBingoGroup22 = new List<int>();
    [SerializeField] List<int> _listBingoGroup23 = new List<int>();
    [SerializeField] List<int> _listBingoGroup24 = new List<int>();
    [SerializeField] List<int> _listBingoGroup25 = new List<int>();
    [SerializeField] List<int> _listBingoGroup26 = new List<int>();
    [SerializeField] List<int> _listBingoGroup27 = new List<int>();
    [SerializeField] List<int> _listBingoGroup28 = new List<int>();
    [SerializeField] List<int> _listBingoGroup29 = new List<int>();
    [SerializeField] List<int> _listBingoGroup30 = new List<int>();
    [SerializeField] List<int> _listBingoGroup31 = new List<int>();
    [SerializeField] List<int> _listBingoGroup32 = new List<int>();


    // 미션 정보
    JSONNode _missionDayJSON = null;
    JSONNode _missionWeekJSON = null;
    JSONNode _bingoJSON = null;

    JSONNode _cloneUserStageJSON = null;

    int tempi;
    string temps;
    JSONNode tempNode;


    #region Downloaded Texture

    /* 2016.06 배너의 모든 이미지는 small 하나만 사용하기로 함 */

    // 로컬 파일 명 
    string _fileFishSmallBanner;
    string _fileFreeSmallBanner;
    string _fileSpecialSmallBanner;
    string _filePackageSmallBanner;
    string _fileNoticeSmallBanner;

    [SerializeField] Texture2D _fishSmallBanner = null;
    [SerializeField] Texture2D _freeSmallBanner = null;
    [SerializeField] Texture2D _specialSmallBanner = null;
    [SerializeField] Texture2D[] _arrPackageSmallTextures;
    [SerializeField] Texture2D[] _arrNoticeSmallTextures;


    #endregion

    #region readonly game data version
    readonly string ENV_DATA = "env"; // 환경설정 및 가격설정 표 
    readonly string COINSHOP_DATA = "coin_shop"; // 코인 샵 설정 표 
    readonly string ATTENDANCE_DATA = "attendance"; // 출석체크 
    readonly string NEKOSKILL_DATA = "neko_skill";
    readonly string RANKREWARD_DATA = "rank";
    readonly string NEKOBEAD_DATA = "neko_bead";
    readonly string MISSION_DAILY_DATA = "mission_daily";
    readonly string MISSION_WEEKLY_DATA = "mission_weekly";
    readonly string GATCHA_BANNER_DATA = "banner_gacha";
    readonly string PACKAGE_BANNER_DATA = "banner_package";
    readonly string NOTICE_BANNER_DATA = "banner_notice";
    readonly string BINGO_DATA = "bingo";
    readonly string BINGO_GROUP_DATA = "neko_group";
    readonly string USER_PASSIVE_DATA = "user_passive"; // 유저 패시브 스킬 가격정보
    readonly string STAGE_DETAIL_DATA = "stage_base";
    readonly string STAGE_MASTER_DATA = "stage_master";


    readonly string JSON_DATA = "-JSON"; // JSON 데이터 

    readonly string MISSION_DAY_PROGESS = "mission_day_progress";
    readonly string MISSION_WEEK_PROGESS = "mission_week_progress";
    readonly string BINGO_PROGRESS = "bingo_progress";


    [SerializeField] int ENV_VERSION = -1;
    [SerializeField] int COINSHOP_VERSION = -1;
    [SerializeField] int ATTENDANCE_VERSION = -1;
    [SerializeField] int NEKOSKILL_VERSION = -1;
    [SerializeField] int RANKREWARD_VERSION = -1;
    [SerializeField] int NEKOBEAD_VERSION = -1;
    [SerializeField] int MISSION_DAILY_VERSION = -1;
    [SerializeField] int MISSION_WEEKLY_VERSION = -1;
    [SerializeField] int GATCHA_BANNER_VERSION = -1;
    [SerializeField] int PACKAGE_BANNER_VERSION = -1;
    [SerializeField] int NOTICE_BANNER_VERSION = -1;
    [SerializeField] int BINGO_VERSION = -1;
    [SerializeField] int BINGO_GROUP_VERSION = -1;
    [SerializeField] int USER_PASSIVE_VERSION = -1;

    [SerializeField] int STAGE_DETAIL_VERSION = -1;
    [SerializeField] int STAGE_MASTER_VERSION = -1;

    [SerializeField] List<string> _requestGameData = new List<string>();

    [SerializeField]
    int _mission_day;

    [SerializeField]
    int _weeklymission_refreshDay;


    JSONNode _currentMissionJSON;

    #endregion


    #region 빙고 로직

    [SerializeField] int _currentBingoID = 0;
    [SerializeField] List<int> _listClearedBingoCols = new List<int>();
    [SerializeField] List<int> _listClearedBingoLines = new List<int>();

    int _tempLineIndex;
    readonly string FillState = "fill";



    /// <summary>
    /// 대상 빙고의 클리어 여부 판단 
    /// </summary>
    public bool CheckBingoClear(int pBingoID) {

        JSONNode bingodata;

        for(int i=0; i<BingoJSON.Count; i++) {

            if (pBingoID != BingoJSON[i]["bingoid"].AsInt)
                continue;

            bingodata = BingoJSON[i]["bingodata"];

            for(int j=0; j<bingodata.Count; j++) {
                if(bingodata[j]["state"].Value.Equals("empty")) { // 하나라도 empty 상태가 있으면 return false
                    return false;
                }
            }

        }

        return true;
    }


    /// <summary>
    /// 빙고 기준 정보 갱신처리 (GameData)
    /// </summary>
    public void UpdateBingoBaseInfo() {

        JSONNode initdataNode = null;
        JSONNode userdataNode = null;


        if (BingoJSON == null) {
            Debug.Log("▶▶ UpdateBingoBaseInfo No BingoJSON");

            BingoJSON = BingoInitJSON;
            SaveBingoProgress();
            return;
        }

        if (string.IsNullOrEmpty(BingoJSON[0]["color"].Value)) {

            Debug.Log("▶▶ UpdateBingoBaseInfo No BingoJSON #2");

            BingoJSON = BingoInitJSON;
            SaveBingoProgress();
            return;
        }


        // BingoInitJSON의 bingoID, id, type, questid 가 같은 것을 찾아서 current와 state, fill을 복사 
        for (int i=0; i<BingoInitJSON.Count; i++) {

            for (int j=0; j< BingoJSON.Count; j++) {

                if (BingoInitJSON[i]["bingoid"].AsInt != BingoJSON[j]["bingoid"].AsInt) {
                    continue;
                }

                // 같은 Bingo ID에서 Version이 다른 경우는, Reset 처리를 해야한다. (복사하지 않음)
                // DB는 수동 리셋이다!
                if (BingoInitJSON[i]["version"].AsInt != BingoJSON[j]["version"].AsInt) {
                    Debug.Log("★★★★Bingo Version Update! :: " + BingoInitJSON[i]["bingoid"].AsInt);
                    continue;
                }


                initdataNode = BingoInitJSON[i]["bingodata"];
                userdataNode = BingoJSON[j]["bingodata"];

                for(int k=0; k<initdataNode.Count; k++) {

                    for(int l=0; l<userdataNode.Count; l++) {

                        // id, type, questid가 동일할 때에, 
                        if (initdataNode[k]["id"].AsInt == userdataNode[l]["id"].AsInt
                            && initdataNode[k]["type"].Value.Equals("column")
                            && initdataNode[k]["type"].Value == userdataNode[l]["type"].Value
                            && initdataNode[k]["questid"].AsInt == userdataNode[l]["questid"].AsInt) {

                            // 사용자 정보를 기준 정보 node로 복사 
                            initdataNode[k]["current"].AsInt = userdataNode[l]["current"].AsInt;
                            initdataNode[k]["state"].Value = userdataNode[l]["state"].Value;
                            initdataNode[k]["checked"].AsBool = userdataNode[l]["checked"].AsBool;

                        }
                    }

                } 
                // end of k, l for 

            }
        } // end of i, j for 


        Debug.Log("▶▶ UpdateBingoBaseInfo Complete");
        BingoJSON = BingoInitJSON; // 처리 종료후 덮어쓰기.

        SaveBingoProgress();

    }


    /// <summary>
    /// 
    /// </summary>
    public void SaveBingoProgress() {
        ES2.Save<string>(BingoJSON.ToString(), BINGO_PROGRESS);
    }




    /// <summary>
    /// 사용자 빙고 달성상황 처리 (request_userdata)
    /// </summary>
    private void SetUserBingoProgress() {
        JSONNode currentBingoNode = null;
        JSONNode userBingoData = null;

        Debug.Log("★★★ SetUserBingoProgress BingoJSON Count :: " + BingoJSON.Count);
        Debug.Log("★★★ SetUserBingoProgress UserBingo Count :: " + UserJSON["allbingo"].Count);



        if(BingoJSON.Count != UserJSON["allbingo"].Count) {
            Debug.Log("▶▶ Count Not Match!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! ");
            return;
        }

        

        // 빙고 마스터의 개수만틈 진행한다. 
        for (int i = 0; i < UserJSON["allbingo"].Count; i++) {

            int indx = -1;

            for(int j = 0; j< BingoJSON.Count; j++) {
                if (BingoJSON[j]["bingoid"].AsInt != UserJSON["allbingo"][i]["bingoid"].AsInt) {
                    continue;
                }
                else { // indx 설정 (일치 아이디 
                    indx = j;
                }
            } // end of of j for . finding indx.



            // 대상 빙고마스터의 bingodata를 추출 
            currentBingoNode = BingoJSON[indx]["bingodata"];
            userBingoData = UserJSON["allbingo"][i]["bingodata"];

            // 두 Node의 bingodata를 비교해서 설정한다. 
            for(int j=0; j<currentBingoNode.Count; j++) {


				// 28, 47 Mission Quest Value 유동적으로 처리해줘야 한다. 
				if (currentBingoNode[j]["questid"].AsInt == 54) {
					currentBingoNode [j] ["questvalue"].AsInt = MissionDayInitJSON.Count;
					
				} else if (currentBingoNode[j]["questid"].AsInt == 55) {
					currentBingoNode [j] ["questvalue"].AsInt = MissionWeekInitJSON.Count;
				}


                // 빙고 Progress 복사 
				for(int k=0; k<userBingoData.Count; k++) {

                    // 컬럼 처리 .
                    if (currentBingoNode[j]["type"].Value.Equals("column") && userBingoData[k]["type"].Value.Equals("column")
                        && (currentBingoNode[j]["id"].AsInt == userBingoData[k]["id"].AsInt)) {

                        currentBingoNode[j]["state"] = userBingoData[k]["state"];

                        // DB 의 progress 값이 0보다 크거나 같을 경우에 복사한다. (초기값은 -1)
                        if(userBingoData[k]["progress"].AsInt >= 0 && userBingoData[k]["state"].Value.Equals("empty")) {
                            currentBingoNode[j]["current"].AsInt = userBingoData[k]["progress"].AsInt ;
                        }

                        if (userBingoData[k]["state"].Value.Equals("empty")) {
                            currentBingoNode[j]["checked"].AsBool = false;
                        }
                        else {
                            currentBingoNode[j]["checked"].AsBool = true;
                        }
                        break;
                    } // 컬럼 처리 종료 

                    // 라인 처리 
                    // 25를 더해서 체크 
                    if (currentBingoNode[j]["type"].Value.Equals("line") && userBingoData[k]["type"].Value.Equals("line")
                        && (currentBingoNode[j]["id"].AsInt == userBingoData[k]["id"].AsInt + 25)) {

                        currentBingoNode[j]["state"] = userBingoData[k]["state"];

                        if (userBingoData[k]["state"].Value.Equals("empty")) {
                            currentBingoNode[j]["checked"].AsBool = false;
                        }
                        else {
                            currentBingoNode[j]["checked"].AsBool = true;
                        }

                        break;
                    }

                }

            } // end of 비교 


            //Debug.Log("★★★ SetUserBingoProgress currentBingoNode :: " + currentBingoNode.ToString());

        } // end of i for 


        
        Debug.Log("★★★ SetUserBingoProgress :: " + BingoJSON.ToString());

    }


    /// <summary>
    /// 빙고 미션 진행 처리 
    /// </summary>
    /// <param name="pQuestID">개별 questid</param>
    /// <param name="pQuestValue">진행도 value </param>
    /// <param name="pGroup">neko group</param>
    /// <param name="pOveride">진척도 덮어 쓰기 여부</param>

    public void SetBingoQuestProgress(int pQuestID, int pQuestValue, int pGroup, bool pOveride = false) {

        if (!UserJSON["bingouse"].AsBool)
            return;

        // 튜토리얼 사용자는 제외
        if (TutorialComplete == 0)
            return;

        int bingoIndex = CheckBingoQuestExists(pQuestID);
        bool hasFill = false;

        if (bingoIndex < 0) {
            //Debug.Log("▶▶▶ SetBingoQuestProgress No BingoIndex!!!! :: " + pQuestID);
            return;
        }


        // 현재 도전중인 빙고 ID의 index를 Get
        for (int i = 0; i < BingoJSON.Count; i++) {

            if (BingoJSON[i]["bingoid"].AsInt == UserJSON["currentbingoid"].AsInt) {
                tempi = i;
                tempNode = BingoJSON[i]["bingodata"];
                break;
            }
        }

        //Debug.Log("▶▶▶ BingoJSON Check[" + bingoIndex + "] !! :: " + BingoJSON[bingoIndex]["checked"].AsBool);
        //Debug.Log("▶▶▶ BingoJSON Check[" + bingoIndex + "] !! :: " + BingoJSON[bingoIndex]["state"].Value);

        // 이미 완료된 경우 리턴
        /*
        if (BingoJSON[bingoIndex]["checked"].AsBool 
            || BingoJSON[bingoIndex]["state"] == "fill") {
            Debug.Log("▶▶▶ SetBingoQuestProgress Alread Fill");
            return;
        }
        */

        if (tempNode[bingoIndex] ["state"].Value.Equals("fill")) {
			//Debug.Log ("▶ It's already Fill :: " + pQuestID);
			return;
		}


        // 그룹이 달라도 리턴 
        if (tempNode[bingoIndex]["questgroup"].AsInt != pGroup) {

            //Debug.Log("▶ group different  :: " + tempNode[bingoIndex]["questgroup"].AsInt + " / " + pGroup);

            return;
        }

        // current
        if(!pOveride) // 일반적으로는 이어지지만, 
            tempNode[bingoIndex]["current"].AsInt = tempNode[bingoIndex]["current"].AsInt + pQuestValue;
        else  // 퍼즐 1회에 ~하는 경우에는 덮어쓰는것으로 한다. 
            tempNode[bingoIndex]["current"].AsInt = pQuestValue;

        //Debug.Log("▶▶▶ Update Progress :: " + tempNode[bingoIndex].ToString());

        // 완료 
        if (tempNode[bingoIndex]["current"].AsInt >= tempNode[bingoIndex]["questvalue"].AsInt) {
            tempNode[bingoIndex]["state"] = "fill";
            tempNode[bingoIndex]["checked"].AsBool = false; // 아직 연출 전이니깐 false로 처리 

            ListClearedBingoCols.Add(tempNode[bingoIndex]["id"].AsInt); // Add
            hasFill = true;
        }

        // 갱신된 정보가 있으면 서버와 통신
        GameSystem.Instance.Post2UpdateBingoProgress(tempNode[bingoIndex]);


        // 라인 처리 (완료된 컬럼이 있을때만)
        if (hasFill) {

            for (int i = 25; i < tempNode.Count; i++) {

                if (!tempNode[i]["type"].Value.Equals("line"))
                    continue;

				if (tempNode[i]["checked"].AsBool || tempNode[i]["state"].Value.Equals("fill"))
                    continue;

                if (CheckBingoLine(i) && !ListClearedBingoLines.Contains(tempNode[i]["id"].AsInt)) {
                    Debug.Log("▶▶ Adding ListClearedBingoLines :: " + i);
                    ListClearedBingoLines.Add(tempNode[i]["id"].AsInt); // Add
                }

            }
        }


        // _listClearedBingoCols는 서버로 전송시 사용 

        SaveBingoProgress(); // 세이브 
    }


    /// <summary>
    /// 로비상의 빙고 미션 체크 (출석체크, 물고기, 낚시, 레벨업, 네코 서비스 등)
    /// </summary>
    /// <param name="pQuestID"></param>
    public void CheckLobbyBingoQuest(int pQuestID, int pValue = 1,  bool pOverride = false) {

        if (!UserJSON["bingouse"].AsBool)
            return;

        if (UserJSON["currentbingoid"].AsInt < 0)
            return;

        // 튜토리얼 사용자는 제외
        if (TutorialComplete == 0)
            return;


        if (UserCurrentStage < 14)
            return;

        GameSystem.Instance.ListClearedBingoCols.Clear();
        GameSystem.Instance.ListClearedBingoLines.Clear();

        SetBingoQuestProgress(pQuestID, pValue, 0, pOverride);

        if (GameSystem.Instance.ListClearedBingoCols.Count > 0) {
            // Packet 전송 
            Post2ClearBingo();
        }
    }

    /// <summary>
    /// 빙고 미션 존재 여부 
    /// </summary>
    /// <param name="pQuestID"></param>
    /// <returns></returns>
    private int CheckBingoQuestExists(int pQuestID) {
        tempNode = null;
        tempi = -1;

        if (UserJSON["currentbingoid"].AsInt < 0) {
            Debug.Log("currentbingoid is < 0");
            return -1;
        }

        // 현재 도전중인 빙고 ID의 index를 Get
        for(int i=0; i<BingoJSON.Count; i++) {

            if (BingoJSON[i]["bingoid"].AsInt == UserJSON["currentbingoid"].AsInt) {
                tempi = i;
                tempNode = BingoJSON[i]["bingodata"];
                break;
            }
        }

        if (tempNode == null)
            return -1;

        for(int i=0; i< tempNode.Count; i++) {
            if (!tempNode[i]["type"].Value.Equals("column"))
                continue;

            if (tempNode[i]["questid"].AsInt == pQuestID) {
                return i;
            }
        }

        return -1;

    }


    /// <summary>
    /// 퀘스트 목표값 조회 
    /// </summary>
    /// <param name="pQuestID"></param>
    /// <returns></returns>
    public int GetQuestValue(int pQuestID) {

        // 현재 도전중인 빙고 ID의 index를 Get
        for (int i = 0; i < BingoJSON.Count; i++) {

            if (BingoJSON[i]["bingoid"].AsInt == UserJSON["currentbingoid"].AsInt) {
                tempi = i;
                tempNode = BingoJSON[i]["bingodata"];
                break;
            }
        }

        for (int i = 0; i < tempNode.Count; i++) {

            if (tempNode[i]["questid"].AsInt == pQuestID) {
                return tempNode[i]["questvalue"].AsInt;
            }

        }

        return -1;

    }


    /// <summary>
    /// 각 Line별 Column 의 클리어 여부를 체크 
    /// </summary>
    public bool CheckBingoLine(int pLineIndex) {


        // 현재 도전중인 빙고 ID의 index를 Get
        for (int i = 0; i < BingoJSON.Count; i++) {

            if (BingoJSON[i]["bingoid"].AsInt == UserJSON["currentbingoid"].AsInt) {
                tempi = i;
                tempNode = BingoJSON[i]["bingodata"];
                break;
            }
        }

        int _calLineIndex = pLineIndex - 25; // 25를 뺀다. 
        

        //Debug.Log("★★ CheckLine :: " + _calLineIndex);


        if (_calLineIndex < 5) { // 가로줄 라인 체크 
            _tempLineIndex = _calLineIndex;
            _tempLineIndex *= 5;

            if (_tempLineIndex == 0) {
                /*
                Debug.Log("state _tempLineIndex[" + _tempLineIndex + "] state :: " + BingoJSON[_tempLineIndex]["state"]);
                Debug.Log("state _tempLineIndex[" + _tempLineIndex + 1 + "] state :: " + BingoJSON[_tempLineIndex + 1]["state"]);
                Debug.Log("state _tempLineIndex[" + _tempLineIndex + 2 + "] state :: " + BingoJSON[_tempLineIndex + 2]["state"]);
                Debug.Log("state _tempLineIndex[" + _tempLineIndex + 3 + "] state :: " + BingoJSON[_tempLineIndex + 3]["state"]);
                Debug.Log("state _tempLineIndex[" + _tempLineIndex + 4 + "] state :: " + BingoJSON[_tempLineIndex + 4]["state"]);


                Debug.Log("state _tempLineIndex[" + _tempLineIndex + "] checked :: " + BingoJSON[_tempLineIndex]["checked"]);
                Debug.Log("state _tempLineIndex[" + _tempLineIndex + 1 + "] checked :: " + BingoJSON[_tempLineIndex + 1]["checked"]);
                Debug.Log("state _tempLineIndex[" + _tempLineIndex + 2 + "] checked :: " + BingoJSON[_tempLineIndex + 2]["checked"]);
                Debug.Log("state _tempLineIndex[" + _tempLineIndex + 3 + "] checked :: " + BingoJSON[_tempLineIndex + 3]["checked"]);
                Debug.Log("state _tempLineIndex[" + _tempLineIndex + 4 + "] checked :: " + BingoJSON[_tempLineIndex + 4]["checked"]);
                */
            }





            if (tempNode[_tempLineIndex]["state"].Value.Equals(FillState)
                && tempNode[_tempLineIndex + 1]["state"].Value.Equals(FillState)
                && tempNode[_tempLineIndex + 2]["state"].Value.Equals(FillState)
                && tempNode[_tempLineIndex + 3]["state"].Value.Equals(FillState)
                && tempNode[_tempLineIndex + 4]["state"].Value.Equals(FillState)) {

                return true;
            }



        }
        else if (_calLineIndex >= 5 && _calLineIndex < 10) { // 세로줄 라인 
            _tempLineIndex = _calLineIndex - 5;

            if (tempNode[_tempLineIndex]["state"].Value.Equals(FillState)
                && tempNode[_tempLineIndex + 5]["state"].Value.Equals(FillState)
                && tempNode[_tempLineIndex + 10]["state"].Value.Equals(FillState)
                && tempNode[_tempLineIndex + 15]["state"].Value.Equals(FillState)
                && tempNode[_tempLineIndex + 20]["state"].Value.Equals(FillState)) {

                return true;

            }
        }
        else if (_calLineIndex == 10) { // 왼쪽 대각선 라인 

            if (tempNode[0]["state"].Value.Equals(FillState)
                && tempNode[6]["state"].Value.Equals(FillState)
                && tempNode[12]["state"].Value.Equals(FillState)
                && tempNode[18]["state"].Value.Equals(FillState)
                && tempNode[24]["state"].Value.Equals(FillState)) {

                return true;

            }
        }
        else if (_calLineIndex == 11) { // 오른쪽 대각선 라인 

            if (tempNode[4]["state"].Value.Equals(FillState)
                && tempNode[8]["state"].Value.Equals(FillState)
                && tempNode[12]["state"].Value.Equals(FillState)
                && tempNode[16]["state"].Value.Equals(FillState)
                && tempNode[20]["state"].Value.Equals(FillState)) {

                return true;

            }
        }


        return false;
    }

    #endregion


    #region 미션 로직 

    /// <summary>
    /// 일일, 주간미션의 날짜 정보 저장 
    /// </summary>
    private void SaveMissionDay() {

        Debug.Log("SaveMissionDay DayOfYear :: " + _dtSyncTime.DayOfYear);

        _mission_day = _dtSyncTime.DayOfYear;
        
        

        // Monday = 1
        ES2.Save<int>(_mission_day, "mission_day");
        ES2.Save<int>(_weeklymission_refreshDay, "weeklymission_refreshday");

        ES2.Save<string>(_missionDayJSON.ToString(), "mission_day_progress");
        ES2.Save<string>(_missionWeekJSON.ToString(), "mission_week_progress");
    }

    /// <summary>
    /// 미션 진행사항 저장 
    /// </summary>
    private void SaveMissionProgress() {
        ES2.Save<string>(_missionDayJSON.ToString(), "mission_day_progress");
        ES2.Save<string>(_missionWeekJSON.ToString(), "mission_week_progress");
    }


    /// <summary>
    /// 미션의 일자를 체크해서 미션 정보를 할당 
    /// </summary>
    public void CheckMissionDay() {

        if (ES2.Exists("mission_day"))
            _mission_day = ES2.Load<int>("mission_day");
        else
            _mission_day = -1;



        // 주간미션 갱신일자. 
        if (ES2.Exists("weeklymission_refreshday")) {
            _weeklymission_refreshDay = ES2.Load<int>("weeklymission_refreshday");
        }
        else {
            _weeklymission_refreshDay = -1;
        }



        // 일일 미션의 갱신
        if (_mission_day != _dtSyncTime.DayOfYear) {
            Debug.Log("It's Next Day");
            MissionDayJSON = _missionDayInitJSON;

            // 진척도 초기화 
            MissionDayJSON = InitMissionProgress(MissionDayJSON);

        }
        else {

            if (ES2.Exists("mission_day_progress")) {
                MissionDayJSON = JSON.Parse(ES2.Load<string>("mission_day_progress"));
            }
            else {
                MissionDayJSON = _missionDayInitJSON;
                MissionDayJSON = InitMissionProgress(MissionDayJSON);
            }
        }

        // 주간 미션의 갱신 체크
        if(_weeklymission_refreshDay < 0) {
            // 주간시면 갱신일자가 음수 이면, 무조건 갱신.
            Debug.Log("First Weekly Mission");
            MissionWeekJSON = _missionWeekInitJSON;
            MissionWeekJSON = InitMissionProgress(MissionWeekJSON);
        }
        else {
            
            // 주간미션 갱신일자 이전이면 이전 진행정보 로드 
            if(_dtSyncTime.DayOfYear < _weeklymission_refreshDay) {
                if (ES2.Exists("mission_week_progress")) {
                    MissionWeekJSON = JSON.Parse(ES2.Load<string>("mission_week_progress"));
                }
                else {
                    MissionWeekJSON = _missionWeekInitJSON;
                    MissionWeekJSON = InitMissionProgress(MissionWeekJSON);
                }
            }
            else { // 갱신 
                MissionWeekJSON = _missionWeekInitJSON;
                MissionWeekJSON = InitMissionProgress(MissionWeekJSON);

            }

        }




        // Check 를 하고 Save를 한다. 
        // 저장된 일자와 현재 시간이 같다면, 프로그레스 정보를 불러오고 
        // 날짜가 다른 경우에는 기초 정보에서 가져온다. 
        //Debug.Log("Call SaveMissionDay");
        SaveMissionDay();

		// Bingo Check
		CheckLobbyBingoQuest(54, GetCompletedMissionForBingo(MissionType.Day), true);
		CheckLobbyBingoQuest(55, GetCompletedMissionForBingo(MissionType.Week), true);
		//CheckLobbyBingoQuest (40, UserLevel, true); // Player Level 
        //CheckLobbyBingoQuest(84, UserLevel, true); // Player Level
        //CheckLobbyBingoQuest(89, UserLevel, true); // Player Level 30
        //CheckLobbyBingoQuest(95, UserLevel, true); // Player Level 40

        LobbyCtrl.Instance.SpecialBingoCheck();


    }


    private void SetNextWeeklyMissionRefreshDay() {
        if (_dtSyncTime.DayOfWeek == DayOfWeek.Monday) {
            _weeklymission_refreshDay = _dtSyncTime.DayOfYear + 7;
        }
        else if (_dtSyncTime.DayOfWeek == DayOfWeek.Tuesday) {
            _weeklymission_refreshDay = _dtSyncTime.DayOfYear + 6;
        }
        else if (_dtSyncTime.DayOfWeek == DayOfWeek.Wednesday) {
            _weeklymission_refreshDay = _dtSyncTime.DayOfYear + 5;
        }
        else if (_dtSyncTime.DayOfWeek == DayOfWeek.Thursday) {
            _weeklymission_refreshDay = _dtSyncTime.DayOfYear + 4;
        }
        else if (_dtSyncTime.DayOfWeek == DayOfWeek.Friday) {
            _weeklymission_refreshDay = _dtSyncTime.DayOfYear + 3;
        }
        else if (_dtSyncTime.DayOfWeek == DayOfWeek.Saturday) {
            _weeklymission_refreshDay = _dtSyncTime.DayOfYear + 2;
        }
        else if (_dtSyncTime.DayOfWeek == DayOfWeek.Sunday) {
            _weeklymission_refreshDay = _dtSyncTime.DayOfYear + 1;
        }
    }


    /// <summary>
    /// 미션 진행사항 처리 
    /// </summary>
    /// <param name="pMissionType">미션 타입 (일일, 주간)</param>
    /// <param name="pMissionID">미션 아이디</param>
    /// <param name="pValue">미션 진행 값 </param>
    public void CheckMissionProgress(MissionType pMissionType,  int pMissionID, int pValue) {

        //Debug.Log("CheckMissionProgress #1 :: " + pMissionType.ToString() + "/" + pMissionID.ToString() + "/" + pValue.ToString());

        int missionIndex = CheckMissionExists(pMissionType, pMissionID);

        // 미션 존재 체크 
        if (missionIndex < 0) 
            return;


        if (pMissionType == MissionType.Day)
            _currentMissionJSON = MissionDayJSON[missionIndex];
        else
            _currentMissionJSON = MissionWeekJSON[missionIndex];

        // 완료 체크 (0:미완료, 1:완료 , 2:보상받음)
        if(_currentMissionJSON["progress"].AsInt > 0) {
            return;
        }

        // 값 대입 
        _currentMissionJSON["current"].AsInt = _currentMissionJSON["current"].AsInt + pValue;

        // 값 대입 후 완료 체크
        if(_currentMissionJSON["missionCount"].AsInt <= _currentMissionJSON["current"].AsInt) {
            _currentMissionJSON["progress"].AsInt = 1; // 완료로 설정 

            // 완료 설정이 늘어날때마다 체크 
            CheckClearEveryMission(pMissionType);
        }



        // 서버와 동기화 로직을 추가 
        if (pMissionType == MissionType.Day)
            _currentMissionJSON["missiontype"] = "daily";
        else
            _currentMissionJSON["missiontype"] = "weekly";

        Post2UpdateMissionProgress(_currentMissionJSON);

        SaveMissionProgress();


        UpdateLobbyMissionButton();
    }

    /// <summary>
    /// 마지막 단계 미션 (주간/일일 미션을 모두 완료)를 체크하기 위한 메소드 
    /// </summary>
    private void CheckClearEveryMission(MissionType pType) {

        // 하나라도 미완료값이 있으면 종료 
        // 11번 tid가 완료된 상태여도 종료 
        if(pType == MissionType.Day) {

            int index = CheckMissionExists(MissionType.Day, 11);

            if (index < 0)
                return;


            for (int i = 0; i < _missionDayJSON.Count; i++) {

                if (_missionDayJSON[i]["tid"].AsInt == 11)
                    continue;

                // 하나라도 미완료가 있다면 종료 
                if (_missionDayJSON[i]["progress"].AsInt == 0)
                    return;
            }


            //_missionDayJSON[index]["current"].AsInt = GetCompletedMissionCount(pType);

            // 11번이 미완료 상태일때만. 
            if (_missionDayJSON[index]["progress"].AsInt == 0) {
                _missionDayJSON[index]["progress"].AsInt = 1;
            }

        } // 일일미션 체크 종료 



        if (pType == MissionType.Week) {

            int index = CheckMissionExists(MissionType.Week, 11);

            if (index < 0)
                return;


            for (int i = 0; i < _missionWeekJSON.Count; i++) {

                if (_missionWeekJSON[i]["tid"].AsInt == 11)
                    continue;

                // 하나라도 미완료가 있다면 종료 
                if (_missionWeekJSON[i]["progress"].AsInt == 0)
                    return;
            }

            // 11번이 미완료 상태일때만. 
            if (_missionWeekJSON[index]["progress"].AsInt == 0) {
                _missionWeekJSON[index]["progress"].AsInt = 1;
            }

        } // 일일미션 체크 종료 

        SaveMissionProgress();
        UpdateLobbyMissionButton();
    }


    /// <summary>
    /// 현재 활성화된 미션리스트에 해당 미션이 있는지 체크 
    /// </summary>
    /// <param name="pMisstionType"></param>
    /// <param name="pMissionID"></param>
    /// <returns></returns>
    private int CheckMissionExists(MissionType pMisstionType, int pMissionID) {

        if (_missionDayJSON == null || _missionWeekJSON == null)
            return -1;

        // 일일미션 체크 
        if(pMisstionType == MissionType.Day) {
            for (int i = 0; i < _missionDayJSON.Count; i++) {
                if (_missionDayJSON[i]["tid"].AsInt == pMissionID)
                    return i;
            }
        }

        // 주간미션 체크
        if (pMisstionType == MissionType.Week) {
            for (int i = 0; i < _missionWeekJSON.Count; i++) {
                if (_missionWeekJSON[i]["tid"].AsInt == pMissionID)
                    return i;
            }
        }

        return -1;
    }


    /// <summary>
    /// 미션 완료 처리 
    /// </summary>
    /// <param name="pMissionType"></param>
    /// <param name="pMissionID"></param>
    public void SetCompleteMission(MissionType pMissionType, int pMissionID) {
        Debug.Log("SetCompleteMission :: " + pMissionType.ToString() + "/" + pMissionID.ToString() );

        int missionIndex = CheckMissionExists(pMissionType, pMissionID);

        // 미션 존재 체크 
        if (missionIndex < 0)
            return;


        if (pMissionType == MissionType.Day) {
            _currentMissionJSON = MissionDayJSON[missionIndex];
        }
        else {
            _currentMissionJSON = MissionWeekJSON[missionIndex];
        }

        // 완료 체크 (0:미완료, 1:완료 , 2:보상받음)
        _currentMissionJSON["progress"].AsInt = 2;
        Debug.Log(">>> SetCompleteMission :: " + _currentMissionJSON.ToString());
            
        SaveMissionProgress();

		CheckLobbyBingoQuest(54, GetCompletedMissionForBingo(MissionType.Day), true);
		CheckLobbyBingoQuest(55, GetCompletedMissionForBingo(MissionType.Week), true);
    }

	/// <summary>
	/// Gets the completed mission.
	/// </summary>
	/// <returns>The completed mission.</returns>
	/// <param name="pType">P type.</param>
	public int GetCompletedMissionForBingo(MissionType pType) {
		int count = 0;

		if (pType == MissionType.Day) {
			for (int i = 0; i < MissionDayJSON.Count; i++) {
				if (MissionDayJSON[i]["progress"].AsInt >= 2) {
					count++;
				}
			}
		}
		else {
			for (int i = 0; i < MissionWeekJSON.Count; i++) {
				if (MissionWeekJSON[i]["progress"].AsInt >= 2) {
					count++;
				}
			}
		}

		return count;
	}


    /// <summary>
    /// 로비 미션 버튼 업데이트 
    /// </summary>
    private void UpdateLobbyMissionButton() {

        if(LobbyCtrl.Instance != null) {
            LobbyCtrl.Instance.UpdateMissionNew();
        }

    }



    /// <summary>
    /// 미션 개수 체크 
    /// </summary>
    /// <param name="pType"></param>
    /// <returns></returns>
    public int GetMissionCount(MissionType pType) {

        int returnValue = 0;

        // 11은 빼고 카운트. 

        if (pType == MissionType.Day) {

            for (int i = 0; i < _missionDayJSON.Count; i++) {
                if (_missionDayJSON[i]["tid"].AsInt == 11)
                    continue;


                returnValue++;

            }

            return returnValue;
        }


        if (pType == MissionType.Week) {

            for (int i = 0; i < _missionWeekJSON.Count; i++) {
                if (_missionWeekJSON[i]["tid"].AsInt == 11)
                    continue;


                returnValue++;

            }

            return returnValue;
        }

        return returnValue;

    }


    /// <summary>
    /// 완료 개수 체크 
    /// </summary>
    /// <param name="pType"></param>
    /// <returns></returns>
    public int GetCompletedMissionCount(MissionType pType) {

        int returnValue = 0;

        if(pType == MissionType.Day) {

            for(int i =0; i<_missionDayJSON.Count;i++) {
                if (_missionDayJSON[i]["tid"].AsInt == 11)
                    continue;


                if (_missionDayJSON[i]["progress"].AsInt >= 1)
                    returnValue++;

            }

            return returnValue;
        }


        if (pType == MissionType.Week) {

            for (int i = 0; i < _missionWeekJSON.Count; i++) {
                if (_missionWeekJSON[i]["tid"].AsInt == 11)
                    continue;


                if (_missionWeekJSON[i]["progress"].AsInt >= 1)
                    returnValue++;

            }

            return returnValue;
        }

        return returnValue;

    }


    /// <summary>
    /// 미션 중 완료 상태의 미션 체크 
    /// </summary>
    /// <returns></returns>
    public bool CheckCompletedMission() {

        if (_missionDayJSON == null || _missionWeekJSON == null)
            return false;

        // 일일미션 
        for(int i=0; i< _missionDayJSON.Count; i++) {
            if(_missionDayJSON[i]["progress"].AsInt == 1) {
                return true;
            }
        }


        // 주간미션 
        for (int i = 0; i < _missionWeekJSON.Count; i++) {
            if (_missionWeekJSON[i]["progress"].AsInt == 1) {
                return true;
            }
        }

        return false; // 완료상태가 하나도 없음 
    }


    private void RefreshMissionDayProgress() {


        Debug.Log(">>> RefreshMissionDayProgress");

        MissionDayJSON = MissionDayInitJSON;

        // 덮어씌운 후에는 Init의 current와 progress를 초기화 한다. 
        MissionDayInitJSON = InitMissionProgress(MissionDayInitJSON);
        ES2.Save<string>(MissionDayJSON.ToString(), MISSION_DAY_PROGESS);


        /*
        if (!ES2.Exists("mission_day_progress")) {
            return;
        }


        MissionDayJSON = JSON.Parse(ES2.Load<string>("mission_day_progress"));
        Debug.Log("RefreshMissionDayProgress MissionDayJSON :: " + MissionDayJSON.ToString());
        Debug.Log("RefreshMissionDayProgress MissionDayInitJSON :: " + MissionDayInitJSON.ToString());

        Debug.Log("MissionDayJSON Count :: " + MissionDayJSON.Count);
        Debug.Log("MissionDayInitJSON Count :: " + MissionDayInitJSON.Count);


        try {
            for (int i = 0; i < MissionDayJSON.Count; i++) {

                for (int j = 0; j < MissionDayInitJSON.Count; j++) {

                    if (MissionDayJSON[i]["tid"].AsInt == MissionDayInitJSON[j]["tid"].AsInt) {

                        // InitJSON에 덮어씌운다. (current, progress)
                        MissionDayInitJSON[j]["current"].AsInt = MissionDayJSON[i]["current"].AsInt;
                        MissionDayInitJSON[j]["progress"].AsInt = MissionDayJSON[i]["progress"].AsInt;

                        // Progress 업데이트 처리 11제외
                        if (MissionDayInitJSON[j]["tid"].AsInt != 11
                            && MissionDayInitJSON[j]["progress"].AsInt == 0
                            && MissionDayInitJSON[j]["current"].AsInt >= MissionDayInitJSON[j]["missionCount"].AsInt) {
                            MissionDayInitJSON[j]["progress"].AsInt = 1;
                        }
                    }
                }

            }

            MissionDayJSON = MissionDayInitJSON;

            // 덮어씌운 후에는 Init의 current와 progress를 초기화 한다. 
            MissionDayInitJSON = InitMissionProgress(MissionDayInitJSON);

            ES2.Save<string>(MissionDayJSON.ToString(), MISSION_DAY_PROGESS);
        }
        catch(Exception ex) {

            Debug.Log(ex.StackTrace);
            MissionDayJSON = MissionDayInitJSON;
            MissionDayInitJSON = InitMissionProgress(MissionDayInitJSON);
            ES2.Save<string>(MissionDayJSON.ToString(), MISSION_DAY_PROGESS);
        }
        */

    }

    /// <summary>
    /// 주간 미션 진행상황 이어받기 
    /// </summary>
    private void RefreshMissionWeekProgress() {

        Debug.Log(">>> RefreshMissionWeekProgress");


        // 로직을 버전이 바뀌면 로컬에 저장된 정보를 무조껀 초기화 하도록 변경 
        MissionWeekJSON = MissionWeekInitJSON;

        // 덮어씌운 후에는 Init의 current와 progress를 초기화 한다. 
        MissionWeekInitJSON = InitMissionProgress(MissionWeekInitJSON);
        ES2.Save<string>(MissionWeekJSON.ToString(), MISSION_WEEK_PROGESS);


        /*
        if (!ES2.Exists(MISSION_WEEK_PROGESS)) {
            return;
        }


        MissionWeekJSON = JSON.Parse(ES2.Load<string>(MISSION_WEEK_PROGESS));

        try {
            for (int i = 0; i < MissionWeekJSON.Count; i++) {

                for (int j = 0; j < MissionWeekInitJSON.Count; j++) {

                    if (MissionWeekJSON[i]["tid"].AsInt == MissionWeekInitJSON[j]["tid"].AsInt) {

                        // InitJSON에 덮어씌운다. (current, progress)
                        MissionWeekInitJSON[j]["current"].AsInt = MissionWeekJSON[i]["current"].AsInt;
                        MissionWeekInitJSON[j]["progress"].AsInt = MissionWeekJSON[i]["progress"].AsInt;

                        // Progress 업데이트 처리 missionCount(11제외)
                        if (MissionWeekInitJSON[j]["tid"].AsInt != 11
                            && MissionWeekInitJSON[j]["progress"].AsInt == 0
                            && MissionWeekInitJSON[j]["current"].AsInt >= MissionWeekInitJSON[j]["missionCount"].AsInt) {

                            MissionWeekInitJSON[j]["progress"].AsInt = 1;
                        }

                    }
                }

            }

            MissionWeekJSON = MissionWeekInitJSON;

            // 덮어씌운 후에는 Init의 current와 progress를 초기화 한다. 
            MissionWeekInitJSON = InitMissionProgress(MissionWeekInitJSON);
            ES2.Save<string>(MissionWeekJSON.ToString(), MISSION_WEEK_PROGESS);
        }
        catch(Exception ex) {

            Debug.Log(ex.StackTrace);
            MissionWeekJSON = MissionWeekInitJSON;
            MissionWeekInitJSON = InitMissionProgress(MissionWeekInitJSON);
            ES2.Save<string>(MissionWeekJSON.ToString(), MISSION_WEEK_PROGESS);
        }
        */
    }

    /// <summary>
    /// 미션의 progress , current 초기화 
    /// </summary>
    /// <param name="pNode"></param>
    private JSONNode InitMissionProgress(JSONNode pNode) {

        for(int i=0; i<pNode.Count; i++) {
            pNode[i]["current"].AsInt = 0;
            pNode[i]["progress"].AsInt = 0;
        }

        return pNode;

    }



    #endregion

    #region Unlock & Tips (Unlock은 잠금 상태를 체크하는 메소드와, 잠금 해제의 진행여부를 체크하는 메소드로 구분한다.)

    /// <summary>
    /// 도감 잠금 상태 체크 
    /// </summary>
    /// <returns>true : 미션 풀림 , false : 미션 잠금</returns>
    public bool CheckStateWantedUnlock() {


        // 튜토리얼이 미완료된 경우는 무조건 lock 
        if (_tutorialComplete == 0)
            return false;




        return LoadESvalueBool(PuzzleConstBox.ES_UnlockWanted);

    }

    /// <summary>
    /// 도감 Unlock 진행 여부를 체크 
    /// </summary>
    /// <returns>true : Unlock이 필요하다.</returns>
    public bool CheckWantedUnlockProceed() {



        // 스테이지 3부터 Unlock 된다.
        if (UserCurrentStage <4)
            return false;

        if (LoadESvalueBool(PuzzleConstBox.ES_UnlockWanted))
            return false;


        // true return시 Unlock 해제가 동작한다. 
        return true;
    }




    /// <summary>
    /// 랭킹 Unlock 진행 여부를 체크 
    /// </summary>
    /// <returns>true : Unlock이 필요하다.</returns>
    public bool CheckRankingUnlockProceed() {



        // 스테이지 5미만이면 X 
        if (UserCurrentStage < 5)
            return false;

        if (LoadESvalueBool(PuzzleConstBox.ES_UnlockRanking))
            return false;


        // true return시 Unlock 해제가 동작한다. 
        return true;
    }




    /// <summary>
    /// 패시브 잠금 상태 체크 
    /// </summary>
    /// <returns>true : 미션 풀림 , false : 미션 잠금</returns>
    public bool CheckStatePassiveUnlock() {
        // 튜토리얼이 미완료된 경우는 무조건 lock 
        if (_tutorialComplete == 0)
            return false;




        return LoadESvalueBool(PuzzleConstBox.ES_UnlockPassive);

    }

    /// <summary>
    /// 패시브 Unlock 진행 여부를 체크 
    /// </summary>
    /// <returns>true : Unlock이 필요하다.</returns>
    public bool CheckPassiveUnlockProceed() {


        // 레벨이 4미만이면 X 
        if (UserCurrentStage < 5)
            return false;

        if (LoadESvalueBool(PuzzleConstBox.ES_UnlockPassive))
            return false;


        // true return시 Unlock 해제가 동작한다. 
        return true;
    }


    /// <summary>
    /// 네코 레벨업 Unlock 진행여부 체크 
    /// </summary>
    /// <returns></returns>
    public bool CheckNekoLevelUpUnlockProceed() {
        if (LoadESvalueBool(PuzzleConstBox.ES_UnlockNekoLevelup))
            return false;

        if (LoadESvalueInt(PuzzleConstBox.ES_NotRenewBestScore) < 2)
            return false;

        return true;
    }


    /// <summary>
    /// 아이템 잠금 상태 여부 체크 
    /// </summary>
    /// <returns> true : 미션 풀림 , false : 미션 잠금</returns>
    public bool CheckStateItemUnlock() {


        // 튜토리얼이 미완료된 경우는 무조건 lock 
        if (_tutorialComplete == 0)
            return false;




        // 위 조건들 종료 후 기존에 Unlock을 했는지 체크. 
        return LoadESvalueBool(PuzzleConstBox.ES_UnlockItem);

    }


    /// <summary>
    /// 미션 Unlock 진행 여부를 체크 
    /// </summary>
    /// <returns>true : Unlock이 필요하다.</returns>
    public bool CheckItemUnlockProceed() {



        // 레벨이 4미만이면 X 
        if (UserCurrentStage < 4)
            return false;

        if (LoadESvalueBool(PuzzleConstBox.ES_UnlockItem))
            return false; 


        // true return시 Unlock 해제가 동작한다. 
        return true;
    }



    


    /// <summary>
    /// 미션의 잠금 상태 여부를 체크 (반드시 진행여부 체크 전에 실행한다.)
    /// </summary>
    /// <returns> true : 미션 풀림 , false : 미션 잠금 </returns>
    public bool CheckStateMissionUnlock() {

        // 튜토리얼이 미완료된 경우는 무조건 lock 
        if (_tutorialComplete == 0)
            return false;


        // 위 조건들 종료 후 기존에 Unlock을 했는지 체크. 
        return LoadESvalueBool(PuzzleConstBox.ES_UnlockMission);
       
    }

    /// <summary>
    /// 미션 Unlock 진행 여부를 체크 
    /// </summary>
    /// <returns>true : Unlock이 필요하다.</returns>
    public bool CheckMissionUnlockProceed() {



        // Daily 미션이 1개라도 달성된 상태일때 처리
        if (!CheckCompletedMission())
            return false;

        // 퍼즐을 3회이상 플레이. 
        if (LoadESvalueInt(PuzzleConstBox.ES_GameStartCount) < 3)
            return false;

        if (LoadESvalueBool(PuzzleConstBox.ES_UnlockMission))
            return false;
            


        // true return시 Unlock 해제가 동작한다. 
        return true;
    }

    /// <summary>
    /// 네코 서비스 잠금해제 진행 여부 체크 
    /// </summary>
    /// <returns></returns>
    public bool CheckNekoServiceUnlockProceed() {
        
        // 퍼즐을 1회이상 플레이. 
        if (LoadESvalueInt(PuzzleConstBox.ES_GameStartCount) < 1)
            return false;

        if (LoadESvalueBool(PuzzleConstBox.ES_UnlockNekoService))
            return false;

        // true return시 Unlock 해제가 동작한다. 
        return true;
    }


    /// <summary>
    /// 빙고 팁 잠금 해제 
    /// </summary>
    /// <returns></returns>
    public bool CheckBingoUnlockProceed() {

        // 스테이지 80까지는 Lock 상태
        if(UserCurrentStage < 200) {
            return false;
        }

        /*
        if (LoadESvalueBool(PuzzleConstBox.ES_UnlockBingoTip)) {
            return false;
        }
        */

        return true;
    }

    /// <summary>
    /// 빙고 첫번째 미션 클리어 팁 
    /// </summary>
    /// <returns></returns>
    public bool CheckFirstBingoMissionUnlockProceed() {

        Debug.Log("▶CheckFirstBingoMissionUnlockProceed:: " + UserJSON["bingo_mission_tip"].AsInt);

        if (UserJSON["bingo_mission_tip"].AsInt == 0)
            return false;

        // true에서 잠금해제 
        return true;
    }



    #endregion


    #region 스테이지 미션 

    /// <summary>
    /// 스테이지 기준 정보의 스테이지 정보 조회 
    /// </summary>
    /// <param name="pStage"></param>
    /// <returns></returns>
    public JSONNode GetStageNode(int pStage) {
        return StageDetailJSON[pStage - 1];
    }

    public JSONNode GetUserStageNode(int pStage) {
        return UserStageJSON["stagelist"][pStage - 1];
    }


    /// <summary>
    /// 현재 스테이지의 try 보너스 수치 조회
    /// </summary>
    /// <returns></returns>
    public float GetTryBonusCurrentStageInGame() {
        return UserStageJSON["stagelist"][GameSystem.Instance.PlayStage - 1]["trycount"].AsInt * 0.02f;
    }
       

    public JSONNode GetCurrentStageNode() {
        return StageDetailJSON[GameSystem.Instance.PlayStage - 1];
    }

    /// <summary>
    /// 스테이지 그룹의 로컬 아이디 조회 
    /// </summary>
    /// <param name="pGroupID"></param>
    /// <returns></returns>
    public MNP_Localize.rowIds GetStageGroupLocalID(int pGroupID) {

        switch (pGroupID) {

            case 1: return MNP_Localize.rowIds.L6500;
            case 2: return MNP_Localize.rowIds.L6501;
            case 3: return MNP_Localize.rowIds.L6502;
            case 4: return MNP_Localize.rowIds.L6503;
            case 5: return MNP_Localize.rowIds.L6504;
            case 6: return MNP_Localize.rowIds.L6505;
            case 7: return MNP_Localize.rowIds.L6506;
            case 8: return MNP_Localize.rowIds.L6507;
            case 9: return MNP_Localize.rowIds.L6508;
            case 10: return MNP_Localize.rowIds.L6509;
            case 11: return MNP_Localize.rowIds.L6510;
            case 12: return MNP_Localize.rowIds.L6511;
            case 13: return MNP_Localize.rowIds.L6512;
            case 14: return MNP_Localize.rowIds.L6513;
            case 15: return MNP_Localize.rowIds.L6514;
            case 16: return MNP_Localize.rowIds.L6515;
            case 17: return MNP_Localize.rowIds.L6516;
            case 18: return MNP_Localize.rowIds.L6517;
            case 19: return MNP_Localize.rowIds.L6518;
            case 20: return MNP_Localize.rowIds.L6519;
            case 21: return MNP_Localize.rowIds.L6520;
            /*
            case 22: return MNP_Localize.rowIds.L6521;
            case 23: return MNP_Localize.rowIds.L6522;
            case 24: return MNP_Localize.rowIds.L6523;
            case 25: return MNP_Localize.rowIds.L6524;
            */

            default:
                return MNP_Localize.rowIds.L6500;
        }
    }

    /// <summary>
    /// 스테이지 미션의 로컬라이징 번호 조회 
    /// </summary>
    /// <param name="pMissionID"></param>
    /// <returns></returns>
    public MNP_Localize.rowIds GetStageMissionLocalID(int pMissionID) {
        
        switch(pMissionID) {

            case 1: return MNP_Localize.rowIds.L6000;
            case 2: return MNP_Localize.rowIds.L6001;
            case 3: return MNP_Localize.rowIds.L6002;
            case 4: return MNP_Localize.rowIds.L6003;
            case 5: return MNP_Localize.rowIds.L6004;
            case 6: return MNP_Localize.rowIds.L6005;
            case 7: return MNP_Localize.rowIds.L6006;
            case 8: return MNP_Localize.rowIds.L6007;
            case 9: return MNP_Localize.rowIds.L6008;
            case 10: return MNP_Localize.rowIds.L6009;
            case 11: return MNP_Localize.rowIds.L6010;
            case 12: return MNP_Localize.rowIds.L6011;
            case 13: return MNP_Localize.rowIds.L6012;
            case 14: return MNP_Localize.rowIds.L6013;
            case 15: return MNP_Localize.rowIds.L6014;
            case 16: return MNP_Localize.rowIds.L6015;
            case 17: return MNP_Localize.rowIds.L6016;
            case 18: return MNP_Localize.rowIds.L6017;
            case 19: return MNP_Localize.rowIds.L6018;
            case 20: return MNP_Localize.rowIds.L6019;
            case 21: return MNP_Localize.rowIds.L6020;
            case 22: return MNP_Localize.rowIds.L6021;
            case 23: return MNP_Localize.rowIds.L6022;
            case 24: return MNP_Localize.rowIds.L6023;
            case 25: return MNP_Localize.rowIds.L6024;
            case 26: return MNP_Localize.rowIds.L6025;
            case 27: return MNP_Localize.rowIds.L6026;
            case 28: return MNP_Localize.rowIds.L6027;
            case 29: return MNP_Localize.rowIds.L6028;
            case 30: return MNP_Localize.rowIds.L6029;
            case 31: return MNP_Localize.rowIds.L6030;
            case 32: return MNP_Localize.rowIds.L6031;
            case 33: return MNP_Localize.rowIds.L6032;
            case 34: return MNP_Localize.rowIds.L6033;
            case 35: return MNP_Localize.rowIds.L6034;
            case 36: return MNP_Localize.rowIds.L6035;
            case 37: return MNP_Localize.rowIds.L6036;
            case 38: return MNP_Localize.rowIds.L6037;
            case 39: return MNP_Localize.rowIds.L6038;
            case 40: return MNP_Localize.rowIds.L6039;
            case 41: return MNP_Localize.rowIds.L6040;
            /*
            case 42: return MNP_Localize.rowIds.L6041;
            case 43: return MNP_Localize.rowIds.L6042;
            case 44: return MNP_Localize.rowIds.L6043;
            case 45: return MNP_Localize.rowIds.L6044;
            case 46: return MNP_Localize.rowIds.L6045;
            case 47: return MNP_Localize.rowIds.L6046;
            case 48: return MNP_Localize.rowIds.L6047;
            case 49: return MNP_Localize.rowIds.L6048;
            case 50: return MNP_Localize.rowIds.L6049;
            */

            default:
                return MNP_Localize.rowIds.L6000;
        }
    }

    #endregion


    #region Properties


    public JSONNode MissionWeekInitJSON {
        get {
            return _missionWeekInitJSON;
        }

        set {
            _missionWeekInitJSON = value;
            //_debugMissionWeekInitJSON = _missionWeekInitJSON.ToString();
        }
    }

    public JSONNode MissionDayInitJSON {
        get {
            return _missionDayInitJSON;
        }

        set {
            _missionDayInitJSON = value;
            //_debugMissionDayInitJSON = _missionDayInitJSON.ToString();
        }
    }

    public JSONNode MissionDayJSON {
        get {
            return _missionDayJSON;
        }

        set {
            _missionDayJSON = value;
            //_debugMissionDayJSON = _missionDayJSON.ToString();
        }
    }

    public JSONNode MissionWeekJSON {
        get {
            return _missionWeekJSON;
        }

        set {
            _missionWeekJSON = value;
            //_debugMissionWeekJSON = _missionWeekJSON.ToString();
            SetNextWeeklyMissionRefreshDay();
        }
    }

    public Texture2D FishSmallBanner {
        get {
            return _fishSmallBanner;
        }

        set {
            _fishSmallBanner = value;
        }
    }



    public Texture2D FreeSmallBanner {
        get {
            return _freeSmallBanner;
        }

        set {
            _freeSmallBanner = value;
        }
    }



    public Texture2D SpecialSmallBanner {
        get {
            return _specialSmallBanner;
        }

        set {
            _specialSmallBanner = value;
        }
    }


    public Texture2D[] ArrPackageSmallTextures {
        get {
            return _arrPackageSmallTextures;
        }

        set {
            _arrPackageSmallTextures = value;
        }
    }


    public Texture2D[] ArrNoticeSmallTextures {
        get {
            return _arrNoticeSmallTextures;
        }

        set {
            _arrNoticeSmallTextures = value;
        }
    }

    public JSONNode BingoInitJSON {
        get {
            return _bingoInitJSON;
        }

        set {
            _bingoInitJSON = value;
            //_debugBingoInitJSON = _bingoInitJSON.ToString();
        }
    }

    public JSONNode BingoJSON {
        get {
            return _bingoJSON;
        }

        set {
            _bingoJSON = value;
            //_debugBingoJSON = _bingoJSON.ToString();
        }
    }

    public JSONNode BingoGroupJSON {
        get {
            return _bingoGroupJSON;
        }

        set {
            _bingoGroupJSON = value;


            if (_bingoGroupJSON == null)
                return;

            Debug.Log("▶ Bingo Neko Group Class");

            _listBingoGroup1.Clear();
            _listBingoGroup2.Clear();
            _listBingoGroup3.Clear();
            _listBingoGroup4.Clear();
            _listBingoGroup5.Clear();
            _listBingoGroup6.Clear();
            _listBingoGroup7.Clear();
            _listBingoGroup8.Clear();
            _listBingoGroup9.Clear();
            _listBingoGroup10.Clear();
            _listBingoGroup11.Clear();
            _listBingoGroup12.Clear();
            _listBingoGroup13.Clear();
            _listBingoGroup14.Clear();
            _listBingoGroup15.Clear();
            _listBingoGroup16.Clear();
            _listBingoGroup17.Clear();
            _listBingoGroup18.Clear();
            _listBingoGroup19.Clear();
            _listBingoGroup20.Clear();
            _listBingoGroup21.Clear();
            _listBingoGroup22.Clear();
            _listBingoGroup23.Clear();
            _listBingoGroup24.Clear();
            _listBingoGroup25.Clear();
            _listBingoGroup26.Clear();

            _listBingoGroup27.Clear();
            _listBingoGroup28.Clear();
            _listBingoGroup29.Clear();
            _listBingoGroup30.Clear();
            _listBingoGroup31.Clear();
            _listBingoGroup32.Clear();

            // 그룹별 분류
            for (int i=0; i<_bingoGroupJSON.Count; i++) {
                if(_bingoGroupJSON[i]["groupid"].AsInt == 1) {
                    _listBingoGroup1.Add(_bingoGroupJSON[i]["nekoid"].AsInt);
                }
                else if (_bingoGroupJSON[i]["groupid"].AsInt == 2) {
                    _listBingoGroup2.Add(_bingoGroupJSON[i]["nekoid"].AsInt);
                }
                else if (_bingoGroupJSON[i]["groupid"].AsInt == 3) {
                    _listBingoGroup3.Add(_bingoGroupJSON[i]["nekoid"].AsInt);
                }
                else if (_bingoGroupJSON[i]["groupid"].AsInt == 4) {
                    _listBingoGroup4.Add(_bingoGroupJSON[i]["nekoid"].AsInt);
                }
                else if (_bingoGroupJSON[i]["groupid"].AsInt == 5) {
                    _listBingoGroup5.Add(_bingoGroupJSON[i]["nekoid"].AsInt);
                }
                else if (_bingoGroupJSON[i]["groupid"].AsInt == 6) {
                    _listBingoGroup6.Add(_bingoGroupJSON[i]["nekoid"].AsInt);
                }
                else if (_bingoGroupJSON[i]["groupid"].AsInt == 7) {
                    _listBingoGroup7.Add(_bingoGroupJSON[i]["nekoid"].AsInt);
                }
                else if (_bingoGroupJSON[i]["groupid"].AsInt == 8) {
                    _listBingoGroup8.Add(_bingoGroupJSON[i]["nekoid"].AsInt);
                }
                else if (_bingoGroupJSON[i]["groupid"].AsInt == 9) {
                    _listBingoGroup9.Add(_bingoGroupJSON[i]["nekoid"].AsInt);
                }
                else if (_bingoGroupJSON[i]["groupid"].AsInt == 10) {
                    _listBingoGroup10.Add(_bingoGroupJSON[i]["nekoid"].AsInt);
                }
                else if (_bingoGroupJSON[i]["groupid"].AsInt == 11) {
                    _listBingoGroup11.Add(_bingoGroupJSON[i]["nekoid"].AsInt);
                }
                else if (_bingoGroupJSON[i]["groupid"].AsInt == 12) {
                    _listBingoGroup12.Add(_bingoGroupJSON[i]["nekoid"].AsInt);
                }
                else if (_bingoGroupJSON[i]["groupid"].AsInt == 13) {
                    _listBingoGroup13.Add(_bingoGroupJSON[i]["nekoid"].AsInt);
                }
                else if (_bingoGroupJSON[i]["groupid"].AsInt == 14) {
                    _listBingoGroup14.Add(_bingoGroupJSON[i]["nekoid"].AsInt);
                }
                else if (_bingoGroupJSON[i]["groupid"].AsInt == 15) {
                    _listBingoGroup15.Add(_bingoGroupJSON[i]["nekoid"].AsInt);
                }
                else if (_bingoGroupJSON[i]["groupid"].AsInt == 16) {
                    _listBingoGroup16.Add(_bingoGroupJSON[i]["nekoid"].AsInt);
                }
                else if (_bingoGroupJSON[i]["groupid"].AsInt == 17) {
                    _listBingoGroup17.Add(_bingoGroupJSON[i]["nekoid"].AsInt);
                }
                else if (_bingoGroupJSON[i]["groupid"].AsInt == 18) {
                    _listBingoGroup18.Add(_bingoGroupJSON[i]["nekoid"].AsInt);
                }
                else if (_bingoGroupJSON[i]["groupid"].AsInt == 19) {
                    _listBingoGroup19.Add(_bingoGroupJSON[i]["nekoid"].AsInt);
                }
                else if (_bingoGroupJSON[i]["groupid"].AsInt == 20) {
                    _listBingoGroup20.Add(_bingoGroupJSON[i]["nekoid"].AsInt);
                }
                else if (_bingoGroupJSON[i]["groupid"].AsInt == 21) {
                    _listBingoGroup21.Add(_bingoGroupJSON[i]["nekoid"].AsInt);
                }
                else if (_bingoGroupJSON[i]["groupid"].AsInt == 22) {
                    _listBingoGroup22.Add(_bingoGroupJSON[i]["nekoid"].AsInt);
                }
                else if (_bingoGroupJSON[i]["groupid"].AsInt == 23) {
                    _listBingoGroup23.Add(_bingoGroupJSON[i]["nekoid"].AsInt);
                }
                else if (_bingoGroupJSON[i]["groupid"].AsInt == 24) {
                    _listBingoGroup24.Add(_bingoGroupJSON[i]["nekoid"].AsInt);
                }
                else if (_bingoGroupJSON[i]["groupid"].AsInt == 25) {
                    _listBingoGroup25.Add(_bingoGroupJSON[i]["nekoid"].AsInt);
                }
                else if (_bingoGroupJSON[i]["groupid"].AsInt == 26) {
                    _listBingoGroup26.Add(_bingoGroupJSON[i]["nekoid"].AsInt);
                }
                else if (_bingoGroupJSON[i]["groupid"].AsInt == 27) {
                    _listBingoGroup27.Add(_bingoGroupJSON[i]["nekoid"].AsInt);
                }
                else if (_bingoGroupJSON[i]["groupid"].AsInt == 28) {
                    _listBingoGroup28.Add(_bingoGroupJSON[i]["nekoid"].AsInt);
                }
                else if (_bingoGroupJSON[i]["groupid"].AsInt == 29) {
                    _listBingoGroup29.Add(_bingoGroupJSON[i]["nekoid"].AsInt);
                }
                else if (_bingoGroupJSON[i]["groupid"].AsInt == 30) {
                    _listBingoGroup30.Add(_bingoGroupJSON[i]["nekoid"].AsInt);
                }
                else if (_bingoGroupJSON[i]["groupid"].AsInt == 31) {
                    _listBingoGroup31.Add(_bingoGroupJSON[i]["nekoid"].AsInt);
                }
                else if (_bingoGroupJSON[i]["groupid"].AsInt == 32) {
                    _listBingoGroup32.Add(_bingoGroupJSON[i]["nekoid"].AsInt);
                }

            } // end of for

        }
    }

    public List<int> ListClearedBingoCols {
        get {
            return _listClearedBingoCols;
        }

        set {
            _listClearedBingoCols = value;
        }
    }

    public List<int> ListClearedBingoLines {
        get {
            return _listClearedBingoLines;
        }

        set {
            _listClearedBingoLines = value;
        }
    }

    public int CurrentBingoID {
        get {
            return _currentBingoID;
        }

        set {
            _currentBingoID = value;
        }
    }

    public JSONNode CloneUserStageJSON {
        get {
            return _cloneUserStageJSON;
        }

        set {
            _cloneUserStageJSON = value;
        }
    }



    #endregion


}
