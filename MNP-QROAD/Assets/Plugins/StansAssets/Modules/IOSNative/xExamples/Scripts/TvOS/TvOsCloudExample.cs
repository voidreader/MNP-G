using UnityEngine;
using System.Collections;

public class TvOsCloudExample : MonoBehaviour {

	void Start() {
		Debug.Log("iCloudManager.Instance.init()");


		iCloudManager.OnCloudDataReceivedAction += OnCloudDataReceivedAction;


		iCloudManager.Instance.setString("Test", "test");



		iCloudManager.Instance.requestDataForKey ("Test");
	}



	private void OnCloudDataReceivedAction (iCloudData data) {
		Debug.Log("OnCloudDataReceivedAction");
		if(data.IsEmpty) {
			Debug.Log(data.key + " / " + "data is empty");
		} else {
			Debug.Log(data.key + " / " + data.stringValue);
		}
	}	
}
