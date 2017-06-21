
using UnityEngine;
using System.Collections;


public class InSoundManager : MonoBehaviour {


	static InSoundManager _instance = null;

	[SerializeField] private AudioClip bombBoard;

	// 얼음 블록 깨짐
	[SerializeField] private AudioClip breakIce;


	[SerializeField] AudioClip clipBombDrop; // 폭탄 드랍
	[SerializeField] AudioClip clipBombAfter; // 폭탄이펙트 후 발생 효과금 

	[SerializeField] AudioClip[] clipEnemyHitVoice;
	[SerializeField] AudioClip clipBlockAraise; // 블록 등장음 

    [SerializeField]
    AudioClip clipBonusTime;

    [SerializeField] AudioClip _clipStageMissionPopUp;
    [SerializeField] AudioClip _clipStageMissionCol;
    [SerializeField] AudioClip _clipStageMissionClose;

    [SerializeField] AudioClip _clipClear;

    [SerializeField] AudioClip _clipVoiceClear;
    [SerializeField] AudioClip _clipVoiceGreat;
    [SerializeField] AudioClip _clipVoicePefect;
    [SerializeField] AudioClip _clipPerfectBonusTime;

    [SerializeField] AudioClip _clipFishGrilling;
    [SerializeField] AudioClip _clipFishStick;

    [SerializeField] AudioClip _clipMovingJump;
    [SerializeField] AudioClip _clipMovingJumpLast;

    [SerializeField] AudioClip _clipHitFirework;
    [SerializeField] AudioClip _clipShootFirework;
    [SerializeField] AudioClip _clipFireworkTail;

    [SerializeField] AudioClip _clipInGameStageClear;


    [SerializeField]
    AudioClip _clipStageFail;



    public AudioSource inGameBGM = null;
	public AudioSource inGameMatch = null;
	public AudioSource inGameAlert = null;
	public AudioSource srcNekoSmall = null;

	public AudioSource srcEnemyNekoClips = null;
	public AudioSource srcBlockEffectClips = null;
	public AudioSource srcEffectClips = null;
	public AudioSource srcCoinEffect = null;

    public AudioSource srcVoiceEffect = null;

    public AudioClip[] matchSound;

    [SerializeField] AudioClip clip10SecAdd = null;
    [SerializeField] AudioClip clipAbsorbTime = null;


	public static InSoundManager Instance {
		get {
			if(_instance == null) {
				_instance = FindObjectOfType(typeof(InSoundManager)) as InSoundManager;
				
				if(_instance == null) {
					Debug.Log("InSoundManager Init Error");
					return null;
				}
			}
			
			return _instance;
		}
	}

	void Start() {
		GameSystem.Instance.SetSoundVolumn (); // 사운드 옵션 처리 
	}

	public void StopBGM() {
		inGameBGM.Stop ();
	}


	public void PlayBGM(bool pPlayFlag) {

		inGameBGM.Stop ();
        inGameBGM.clip = SoundConstBox.acIngameBGM;
		


		inGameBGM.loop = true; 


		if (pPlayFlag) {
			inGameBGM.Play ();
		} else {
			inGameBGM.Stop ();
		}
	}

	public void PlayFeverBGM(bool pPlayFlag) {

		inGameBGM.Stop ();

		inGameBGM.clip = SoundConstBox.acFeverBGM;
		//inGameBGM.loop = false;
		
		if (pPlayFlag) {
			inGameBGM.Play ();

			// Voice 플레이 추가 
			srcEffectClips.PlayOneShot(SoundConstBox.acFeverVocie);

		} else {
			inGameBGM.Stop ();
		}
	}

	public void PlayFeverVoice() {
		srcEffectClips.PlayOneShot(SoundConstBox.acFeverVocie);
	}


	



	IEnumerator IncreasePitch(AudioSource pSource) {

		while (pSource.pitch <= 1.1f) {
			pSource.pitch += 0.01f;
			yield return null;
			yield return null;
			yield return null;
		}

	}

	IEnumerator DecreasePitch(AudioSource pSource) {



		while (pSource.pitch >= 1f) {
			pSource.pitch -= 0.01f;
			yield return null;
			yield return null;
			yield return null;
		}
		
	}


	public void PlayBlockAraise() {
		srcBlockEffectClips.PlayOneShot (clipBlockAraise);
		
	}

	/// <summary>
	/// 적 캐릭터 Hit Voice 
	/// </summary>
	public void PlayEnemyHitVoice() {

		srcEnemyNekoClips.PlayOneShot (clipEnemyHitVoice [Random.Range (0, clipEnemyHitVoice.Length)]);
	}

	/// <summary>
	/// 플레이어 네코 싱글어택 효과음 
	/// </summary>
	public void PlayPlayerNekoSingleAttackVoice() {
		//acPlayerSingleAttackVoice
		srcEffectClips.PlayOneShot (SoundConstBox.acPlayerSingleAttackVoice);
	}


	/// <summary>
	/// 매치 사운드 재생 
	/// </summary>
	/// <param name="indx">Indx.</param>
	public void PlayMatchSound(int indx) {


		inGameMatch.clip = matchSound [indx];
		inGameMatch.PlayOneShot (matchSound [indx]);
	}


    public void PlayBonusTime() {
        srcEffectClips.PlayOneShot(clipBonusTime);
    }

	public void PlayAlertSound() {

		inGameAlert.loop = true;
		inGameAlert.clip = SoundConstBox.acTick;
		inGameAlert.Play ();
	}


	public void StopAlertSound() {

		inGameAlert.Stop ();
	}

	public void PlayNekoSmallHitSound() {
		srcNekoSmall.PlayOneShot(SoundConstBox.acEnemyHit);
	}


	public void PlayIceBlockBreak() {

		srcEffectClips.PlayOneShot (breakIce);
	}

	public void PlayBombBoard() {

		srcBlockEffectClips.PlayOneShot (bombBoard);
	}



	

	/// <summary>
	/// Enemy Neko Kill 사운드 재생
	/// </summary>
	public void PlayEnemyNekoKill() {
		srcEnemyNekoClips.PlayOneShot (SoundConstBox.acEnemyNekoKill);
	}

	

	public void PlayCoinAbsorb() {
		srcCoinEffect.PlayOneShot (SoundConstBox.acCoinAbsorb);
		//srcEnemyNekoClips.PlayOneShot (SoundConstBox.acCoinAbsorb);
	}


	

	public void PlayTripleAttackReady() {
		srcEffectClips.PlayOneShot (SoundConstBox.acPlayerSkillTripleFull);
	}


	public void PlaySingleAttackReady() {
		srcEffectClips.PlayOneShot (SoundConstBox.acPlayerSkillSingleFull);
	}

	public void PlayReady() {

		srcEffectClips.PlayOneShot (SoundConstBox.acReady);
	}

	public void PlayGo() {

		srcEffectClips.PlayOneShot (SoundConstBox.acGo);
	}

	public void PlayTimeOut() {
		srcEffectClips.PlayOneShot (SoundConstBox.acTimeout);
		
	}


	public void PlayEquipitemPing() {
		srcEffectClips.PlayOneShot (SoundConstBox.acEquipItemPing);
	}

    public void PlayPassivePing() {
        srcEffectClips.PlayOneShot(SoundConstBox.acPing[1]);
    }


    /// <summary>
    /// 플레이어 네코 공격 소리 
    /// </summary>
    public void PlayPlayerNekoHit() {
		srcEffectClips.PlayOneShot (SoundConstBox.acPlayerSkillAttackHit);
	}

	/// <summary>
	/// 플레이어 네코 공격 소리 
	/// </summary>
	public void PlayPlayerNekoWhip() {
		srcEffectClips.PlayOneShot (SoundConstBox.acPlayerTripleAttackWhip);

	}

	public void PlayPlayerTripleAttackVoice() {
		srcEffectClips.PlayOneShot (SoundConstBox.acPlayerTripleAttackVoice);
	}


	public void PlayBombDrop() {
		srcEffectClips.PlayOneShot (clipBombDrop);
	}

	public void PlayBombAfter() {
		srcEffectClips.PlayOneShot (clipBombAfter);
	}

	public void PlayClear() {
		srcEffectClips.PlayOneShot (SoundConstBox.acBestScore);
	}


    public void PlayVoiceClear() {
        srcVoiceEffect.PlayOneShot(_clipVoiceClear);
    }

    public void PlayVoiceGreat() {
        srcVoiceEffect.PlayOneShot(_clipVoiceGreat);
    }

    public void PlayVoicePerfect() {
        srcVoiceEffect.PlayOneShot(_clipVoicePefect);
    }

    public void PlayPerfectBonusTime() {
        srcEffectClips.PlayOneShot(_clipPerfectBonusTime);
    }

    public void PlayFishGrilling() {
        srcEffectClips.PlayOneShot(_clipFishGrilling);
    }

    public void PlayFishStick() {
        srcEffectClips.PlayOneShot(_clipFishStick);
    }

    public void PlayStageFail() {

        StopBGM();
        srcVoiceEffect.PlayOneShot(_clipStageFail);
    }

    public void PlayFireworkTail() {
        srcEffectClips.PlayOneShot(_clipFireworkTail);
    }

    public void PlayShootFirework() {
        srcEffectClips.PlayOneShot(_clipShootFirework);
    }
    public void PlayHitFirework() {
        srcEffectClips.PlayOneShot(_clipHitFirework);
    }

    public void PlayInGameStageClear() {
        srcEffectClips.PlayOneShot(_clipInGameStageClear);
    }


    #region 스테이지 미션 

    public void PlayStageMissionPopUp() {
        srcEffectClips.PlayOneShot(_clipStageMissionPopUp);
    }

    public void PlayStageMissionCol() {
        srcEffectClips.PlayOneShot(_clipStageMissionCol);
    }

    public void PlayStageMissionClose() {
        srcEffectClips.PlayOneShot(_clipStageMissionClose);
    }

    public void PlayMovingJump() {
        srcEffectClips.PlayOneShot(_clipMovingJump);
    }

    public void PlayMovingJumpLast() {
        srcEffectClips.PlayOneShot(_clipMovingJumpLast);
    }

    #endregion

    #region 10초 더 플레이 

    /// <summary>
    /// 10초 더 플레이!
    /// </summary>
    public void PlayTenSecAdd() {
        srcEffectClips.PlayOneShot(this.clip10SecAdd);
    }

    /// <summary>
    /// 10초 더 플레이에서 시간이 증가할때 플레이 
    /// </summary>
    public void PlayAbsorbTime() {
        srcEffectClips.PlayOneShot(this.clipAbsorbTime);

    }

    #endregion

}
