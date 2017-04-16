using UnityEngine;
using System.Collections;
using DG.Tweening;
    

public class NekoSkillTimerCtrl : MonoBehaviour {

    [SerializeField]
    Transform _arrow;

    [SerializeField]
    UIProgressBar _progress;

    [SerializeField]
    AudioSource _audioSource;

    float time; // 유지 시간 

    [SerializeField] WhiteLightCtrl _colorfulLight;

    Vector3 _originPos = new Vector3(150, 415, 0);

    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="pTime"></param>
    public void SetNekoSkillTimer(float pTime) {

        StopCoroutine("Timer");
        time = -99;

        this.transform.DOKill();
        _arrow.DOKill();

        this.gameObject.SetActive(true);
        
        StartCoroutine(Timer(pTime));

    }

    IEnumerator Timer(float pTime) {

        yield return null;


        time = pTime;
        this.transform.localPosition = _originPos;
        this.transform.localScale = GameSystem.Instance.BaseScale;

        this.transform.DOLocalJump(_originPos, _originPos.y + 100, 1, 0.3f);

        _arrow.localEulerAngles = Vector3.zero;
        _progress.value = 1;

        _colorfulLight.PlayLocalPos(_originPos);

        // 효과음 플레이
        _audioSource.Play();
       

        // 시계침 회전 
        _arrow.DOLocalRotate(new Vector3(0, 0, -360), pTime, RotateMode.FastBeyond360).SetEase(Ease.Linear);

        do {

            yield return null;

            if (InGameCtrl.Instance.timerLock)
                continue;

            time -= Time.deltaTime;
            _progress.value = time / pTime;

        } while (time >= 0);


        if (time == -99)
            yield break;



        // 완료되면 종료 처리 
        _audioSource.Stop();
        this.transform.DOScale(0, 0.2f).OnComplete(OnCompleteScale).SetEase(Ease.InBack);
    }

    private void OnCompleteScale() {

        //동일 스킬을 연속으로 두번 쓸 경우에 오동작을 방지한다.
        if (time == -99)
            return;

        this.gameObject.SetActive(false);
    }

}
