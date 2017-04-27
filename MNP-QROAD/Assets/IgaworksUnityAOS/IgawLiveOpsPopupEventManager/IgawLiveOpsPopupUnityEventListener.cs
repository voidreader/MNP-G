using UnityEngine;
using System;
using System.Collections;

namespace IgaworksUnityAOS.IgawLiveOpsPopupEventManager
{

	internal interface IgawLiveOpsPopupUnityEventListener{
		void onPopupClick();
		void onCancelPopupBtnClick();
	}

	
}
