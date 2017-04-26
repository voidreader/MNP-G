using UnityEngine;
using System.Collections;

public class StageThunderObjectCtrl : MonoBehaviour {

    [SerializeField]
    GameObject _thunder;

	public void OnThunder() {
        this.gameObject.SetActive(true);
        StartCoroutine(DoingThunder());
    }

    IEnumerator DoingThunder() {

        
        for(int i=0; i<3; i++) {
            _thunder.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            _thunder.SetActive(false);
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(5);
    }

}
