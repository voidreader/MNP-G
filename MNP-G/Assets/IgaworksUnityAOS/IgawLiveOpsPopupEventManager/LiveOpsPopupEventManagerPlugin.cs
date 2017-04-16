using UnityEngine;
using System;
using System.Collections;
using System.Text;

namespace IgaworksUnityAOS.IgawLiveOpsPopupEventManager
{
	internal class LiveOpsPopupEventManagerPlugin
	{

		private AndroidJavaObject popupEventMgrAndroidObject;

		public LiveOpsPopupEventManagerPlugin(IgawLiveOpsPopupUnityEventListener listener){
			AndroidJavaClass playerClass = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
			AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject> ("currentActivity");
			popupEventMgrAndroidObject = new AndroidJavaObject ("com.igaworks.unity.plugin.IgawLiveOpsPopupEventManager", activity, new AJPLiveOpsPopupUnityEventListener(listener));
		}

    }
}
