using UnityEngine;
using System.Collections;

public partial class LobbyCtrl : MonoBehaviour {

    [SerializeField] AudioClip _acUnlock;
	[SerializeField] AudioSource effectSrc;
	[SerializeField] AudioSource bgmSrc;
	[SerializeField] AudioSource assembleSource;
	[SerializeField] AudioSource effectSource;

    [SerializeField] AudioClip _acFirework;

	[SerializeField] AudioClip[] acLobbyNekoTouch;


    public void OffMuteSound() {
        effectSrc.volume = 1;
        bgmSrc.volume = 1;
    }

    public void MuteLobbySound() {
        effectSrc.volume = 0;
        bgmSrc.volume = 0;
    }


    public void PlayFirework() {
        effectSrc.PlayOneShot(_acFirework);
    }

    private void PlayUnlock() {
        effectSrc.PlayOneShot(_acUnlock);
    }


	/// <summary>
	/// 로비 네코 터치 사운드 플레이 
	/// </summary>
	public void PlayLobbyNekoTouchSound() {
		effectSrc.PlayOneShot(acLobbyNekoTouch[Random.Range(0, acLobbyNekoTouch.Length)]);
	}


    private void PlaySingleAssemble() {

    }

	public void PlayAssemble() {

		if (assembleSource.isPlaying)
			return;

		assembleSource.Play ();
	}

	public void StopAssemble() {
		assembleSource.Stop ();
	}

    public void PlayFishingBGM() {
        bgmSrc.clip = SoundConstBox.acFishGatchaBGM;
        bgmSrc.Play();
    }

	public void PlayGatchaBGM() {

		//return;

		bgmSrc.clip = GameSystem.Instance.GatchaBGM;
		bgmSrc.Play ();

	}

	public void PlayLobbyBGM() {

		//return;

		bgmSrc.clip = GameSystem.Instance.LobbyBGM;
		bgmSrc.Play ();
	}

	public void StopBGM() {
		bgmSrc.Stop ();
	}

    private void PauseBGM() {
        bgmSrc.Pause();
    }

    private void ResumeBGM() {
        bgmSrc.UnPause();
    }

    private void StopEffect() {
        effectSource.Stop();
    }

	/// <summary>
	/// 네코의 보은 Get 효과음 
	/// </summary>
	public void PlayNekoRewardGet() {
		effectSource.PlayOneShot (SoundConstBox.acCoinAbsorb);
	}

	/// <summary>
	/// 결과네코 튕기는 소리 플레이
	/// </summary>
	public void PlayPing() {
		
		effectSource.PlayOneShot(SoundConstBox.acPing[Random.Range(0, SoundConstBox.acPing.Length)]);
	}

	public void PlayBestScore() {
		effectSource.PlayOneShot (SoundConstBox.acBestScore);
	}

	public void PlayPossitive() {
		effectSource.PlayOneShot (SoundConstBox.acUIPossitive);
	}

	public void PlayNegative() {
		effectSource.PlayOneShot (SoundConstBox.acUINegative);
	}

    public void PlayEffect(AudioClip pAC) {
        effectSource.PlayOneShot(pAC);
    }

    public void CheckBGM() {
        if(!bgmSrc.isPlaying) {
            bgmSrc.Play();
        }
    }

    public void SoundStageClearMark() {
        effectSource.PlayOneShot(SoundConstBox.acStarJump);
    }
}
