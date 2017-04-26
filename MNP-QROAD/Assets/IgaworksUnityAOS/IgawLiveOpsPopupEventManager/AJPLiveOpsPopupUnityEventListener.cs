using UnityEngine;
using System;
using System.Collections;

namespace IgaworksUnityAOS.IgawLiveOpsPopupEventManager
{

		internal class AJPLiveOpsPopupUnityEventListener : AndroidJavaProxy{
			public const string ANDROID_UNITY_LIVEOPS_POUPUP_CALLBACK_CLASS_NAME = "com.igaworks.unity.plugin.IgawLiveOpsPopupUnityEventListener";
			private IgawLiveOpsPopupUnityEventListener listener;
			internal AJPLiveOpsPopupUnityEventListener(IgawLiveOpsPopupUnityEventListener listener)
			: base(ANDROID_UNITY_LIVEOPS_POUPUP_CALLBACK_CLASS_NAME){
				this.listener = listener;
			}

			void onPopupClick(){
				Debug.Log("AJPLiveOpsPopupUnityEventListener : onPopupClick");
				if (listener != null) 
					listener.onPopupClick();
			}
			
			void onCancelPopupBtnClick(){
				Debug.Log("AJPLiveOpsPopupUnityEventListener : onCancelPopupBtnClick");
				if (listener != null) 
					listener.onCancelPopupBtnClick();
			}

		}


}
