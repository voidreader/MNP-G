using UnityEngine;
using System.Collections;

public class LocalStorageUseExample : MonoBehaviour {


	void Start () {

		SA.IOSNative.Storage.AppCache.Save ("TEST_KEY", "Some String");

		string val = SA.IOSNative.Storage.AppCache.GetString ("TEST_KEY");
		Debug.Log (val);

		SA.IOSNative.Storage.AppCache.Remove ("TEST_KEY");
		Texture2D 	image = new Texture2D(1, 1);

		SA.IOSNative.Storage.AppCache.Save ("TEST_IMAGE_KEY", image);
		Texture2D cachedImage = SA.IOSNative.Storage.AppCache.GetTexture ("TEST_IMAGE_KEY");
		Debug.Log (cachedImage);

		byte[] data = null;
		SA.IOSNative.Storage.AppCache.Save ("TEST_DATA_KEY", data);
		byte[] cachedData = SA.IOSNative.Storage.AppCache.GetData ("TEST_DATA_KEY");
		Debug.Log (cachedData);

	}

}
