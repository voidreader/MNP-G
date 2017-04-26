using UnityEngine;
using System;
using System.Collections;

namespace IgaworksUnityAOS.IgawLiveOpsPopupEventManager
{   
	
	class LiveOpsPopupEventManager : IgawLiveOpsPopupUnityEventListener
	{
		private LiveOpsPopupEventManagerPlugin mLiveOpsPopupEventManagerPlugin;
		public event EventHandler<EventArgs> OnPopupClick = delegate { };
		public event EventHandler<EventArgs> OnCancelPopupBtnClick = delegate { };
		public LiveOpsPopupEventManager()
		{
			mLiveOpsPopupEventManagerPlugin = new LiveOpsPopupEventManagerPlugin(this);
		}
		//explicit implement IgawLiveOpsPopupUnityEventListener interface
		void IgawLiveOpsPopupUnityEventListener.onPopupClick()
		{
			#if UNITY_EDITOR
						Debug.Log("igaworks:Editor mode Connected");
			#elif UNITY_ANDROID
					Debug.Log("Igaw.Unity: LiveOpsPopupEventManager : OnPopupClick");
					if (OnPopupClick != null)
						OnPopupClick (this, EventArgs.Empty);
#endif
		}

		void IgawLiveOpsPopupUnityEventListener.onCancelPopupBtnClick()
		{
			#if UNITY_EDITOR
						Debug.Log("igaworks:Editor mode Connected");
			#elif UNITY_ANDROID
					Debug.Log("Igaw.Unity: LiveOpsPopupEventManager : OnCancelPopupBtnClick");
					if (OnCancelPopupBtnClick != null)
						OnCancelPopupBtnClick (this, EventArgs.Empty);
#endif
		}
	}

}
