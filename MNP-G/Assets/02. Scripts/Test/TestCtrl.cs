using UnityEngine;
using System.Collections;
using DG.Tweening;

using BestHTTP;
using System.Text;

public class TestCtrl : MonoBehaviour {

    public Transform _obj;

    

    void Awake() {

    }

    // Use this for initialization
    void Start() {

        PuzzleConstBox.Initialize();

    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.A)) {
            //_lblText.text = System.Math.Round(8.4000001f, 3).ToString();
            //_obj.DOLocalJump(new Vector3(0, -3, 0), -1, 1, 3).OnComplete(OnCompleteFirstJump);
            //_leaf.SpawnLeaf();
        }

    }

    private void OnCompleteFirstJump() {
        _obj.DOLocalJump(new Vector3(2, -4, 0), 1, 1, 3);
    }


    
}
