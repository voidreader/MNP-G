using UnityEngine;
using System.Collections;

public static class SoundConstBox {

    // 결과화면 
	public static AudioClip acBestScore; // 최고점수 기록시 효과음 
    public static AudioClip acScoring;
	public static AudioClip acResultFormBGM; // 결과창 BGM
	public static AudioClip acResultBottle; // 결과창 병
    public static AudioClip acUserLevelUp; // 유저 레벨업

	// Ready, Go, Timeout
	public static AudioClip acReady; // 레디
	public static AudioClip acGo; // 고
	public static AudioClip acTimeout; // 타임아웃
	public static AudioClip acTick; // 5초 경고 

	// Enemy 
	public static AudioClip acEnemyNekoKill; // 적 킬 
	public static AudioClip acEnemyHit;  // Enemy Small Hit
	public static AudioClip acEnemySingleBigHit; // Enmey Single Big Hit

	// Player Neko 
	public static AudioClip acPlayerSingleAttackVoice; // 단일 공격 음성 
	public static AudioClip acPlayerTripleAttackVoice; // 트리플 공격 음성 
	public static AudioClip acPlayerSkillSingleFull; // 싱글 게이지 풀 
	public static AudioClip acPlayerSkillTripleFull; // 트리플 게이지 풀 
	public static AudioClip acPlayerTripleAttackWhip; // 바람소리 
	public static AudioClip acPlayerSkillAttackHit; // 단일 공격 효과음 

	// Title
	public static AudioClip acTitleBGM;

	// BGM
	public static AudioClip acIngameBGM;
	
	
	public static AudioClip acFeverBGM;
	public static AudioClip acFeverVocie;
    public static AudioClip acFishGatchaBGM;

    

    // 고양이 모일때
    public static AudioClip acNekoAssemble;
	// 결과창 뚜겅
	public static AudioClip acCap;

	// UI
	public static AudioClip acUIPossitive;
	public static AudioClip acUINegative;


    // 퓨전
    /*
	public static AudioClip acVoiceCinematic1;
	public static AudioClip acVoiceCinematic2;
	public static AudioClip acAppearPic;
	public static AudioClip acVoiceFusion;
	public static AudioClip acFusionFirst;
	public static AudioClip acFusionSecond;
	public static AudioClip acFusionThird;
    */

    // 성장
    public static AudioClip acNekoFeed;
    public static AudioClip acNekoLevelUp;

    // 진화
    public static AudioClip acNekoEvolBGM;
    public static AudioClip acNekoJump;
    public static AudioClip acShrinkNeko;
    public static AudioClip acStarJump;


    // InGame
    public static AudioClip acCoinAbsorb;
	public static AudioClip acEquipItemPing;


    public static AudioClip acGatchaResult;

	public static AudioClip[] acPing;



}
