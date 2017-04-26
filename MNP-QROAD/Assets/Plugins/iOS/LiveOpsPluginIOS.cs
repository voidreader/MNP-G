using UnityEngine;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;
using System.ComponentModel;


public class LiveOpsPluginIOS : MonoBehaviour {

	// LiveOpsPopup
	public static event Action liveOpsPopupGetPopupsResponded;
	public static event Action <string> liveOpsPopupSetPopupLinkListenerCalled;
	public static event Action <string> liveOpsPopupSetPopupCloseListenerCalled;

	[DllImport("__Internal")]
	extern public static void _LiveOpsSetCallbackHandler(string handlerName);

	[DllImport("__Internal")]
	extern public static void _LiveOpsInitPush();
	
	[DllImport("__Internal")]
	extern public static void _LiveOpsSetRemotePushEnable(bool isEnabled);
	
	[DllImport("__Internal")]
	extern public static void _LiveOpsRegisterLocalPushNotification(int intId, string date, string body, string button, string sound, int badgeNumber, string customPayload);
	
	[DllImport("__Internal")]
	extern public static void _LiveOpsCancelLocalPush(int intId);
	
	[DllImport("__Internal")]
	extern public static void _LiveOpsSetTargetingNumberData(int obj, string key);
	
	[DllImport("__Internal")]
	extern public static void _LiveOpsSetTargetingStringData(string obj, string key);
	
	[DllImport("__Internal")]
	extern public static void _LiveOpsFlush();
	
	[DllImport("__Internal")]
	extern public static void _LiveOpsPopupGetPopups();
	
	[DllImport("__Internal")]
	extern public static void _LiveOpsPopupShowPopups(string popupSpaceKey);
	
	[DllImport("__Internal")]
	extern public static void _LiveOpsPopupDestroyPopup();
	
	[DllImport("__Internal")]
	extern public static void _LiveOpsPopupDestroyAllPopups();
	
	[DllImport("__Internal")]
	extern public static void _LiveOpsSetPopupLinkListener();

	[DllImport("__Internal")]
	extern public static void _LiveOpsSetPopupCloseListener();

	#region Declarations for non-native for LiveOps
	/// <summary>
	/// Sets the callback handler.
	/// </summary>
	/// <param name='handlerName'>
	/// Handler name. Must match a Unity GameObject name, for the native code
	/// to utilize UnitySendMessage() properly.
	/// </param>
	public static void LiveOpsSetCallbackHandler(string handlerName)
	{
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		_LiveOpsSetCallbackHandler(handlerName);
		#endif
	}


	/// <summary>
	/// LiveOpsPush 초기화.
	/// </summary>
	public static void LiveOpsInitPush()
	{
		#if UNITY_IPHONE
		_LiveOpsInitPush();
		#endif
	}
	
	
	/// <summary>
	/// remote push 설정.
	/// </summary>
	/// <param name="isEnabled">
	/// A <see cref="System.bool"/> 
	/// </param>
	public static void LiveOpsSetRemotePushEnable(bool isEnabled)
	{
		#if UNITY_IPHONE
		_LiveOpsSetRemotePushEnable(isEnabled);
		#endif
	}
	
	/// <summary>
	/// local push 등록(예약).
	/// </summary>
	/// <param name="intId">
	/// A <see cref="System.int"/> 
	/// </param>
	/// <param name="date">
	/// format : 'yyyyMMddHHmmss'
	/// A <see cref="System.string"/> 
	/// </param>
	/// <param name="body">
	/// A <see cref="System.string"/> 
	/// </param>
	/// <param name="button">
	/// A <see cref="System.string"/> 
	/// </param>
	/// <param name="sound">
	/// A <see cref="System.string"/> 
	/// </param>
	/// <param name="badgeNumber">
	/// A <see cref="System.int"/> 
	/// </param>
	/// <param name="customPayload">
	/// A <see cref="System.string"/> 
	/// </param>
	public static void LiveOpsRegisterLocalPushNotification(int intId, string date, string body, string button, string sound, int badgeNumber, string customPayload)
	{
		#if UNITY_IPHONE
		_LiveOpsRegisterLocalPushNotification(intId, date, body, button, sound, badgeNumber, customPayload);
		#endif
	}
	
	/// <summary>
	/// local push 등록(예약) 취소.
	/// </summary>
	/// <param name="intId">
	/// A <see cref="System.int"/> 
	/// </param>
	public static void LiveOpsCancelLocalPush(int intId)
	{
		#if UNITY_IPHONE
		_LiveOpsCancelLocalPush(intId);
		#endif
	}
	
	/// <summary>
	/// targeting 정보 등록
	/// </summary>
	/// <param name="obj">
	/// A <see cref="System.int"/> 
	/// </param>
	/// <param name="key">
	/// A <see cref="System.string"/> 
	/// </param>
	public static void LiveOpsSetTargetingNumberData(int obj, string key)
	{
		#if UNITY_IPHONE
		_LiveOpsSetTargetingNumberData(obj, key);
		#endif
	}
	
	/// <summary>
	/// targeting 정보 등록
	/// </summary>
	/// <param name="obj">
	/// A <see cref="System.string"/> 
	/// </param>
	/// <param name="key">
	/// A <see cref="System.string"/> 
	/// </param>
	public static void LiveOpsSetTargetingStringData(string obj, string key)
	{
		#if UNITY_IPHONE
		_LiveOpsSetTargetingStringData(obj, key);
		#endif
	}
	
	/// <summary>
	/// live ops user 정보 flush
	/// </summary>
	public static void LiveOpsFlush()
	{
		#if UNITY_IPHONE
		_LiveOpsFlush();
		#endif
	}
	
	/// <summary>
	/// get popups
	/// </summary>
	public static void LiveOpsPopupGetPopups() 
	{
		#if UNITY_IPHONE
		_LiveOpsPopupGetPopups();
		#endif
	}
	
	/// <summary>
	/// show popups
	/// </summary>
	/// <param name="popupSpaceKey">
	/// A <see cref="System.string"/> 
	/// </param>
	public static void LiveOpsPopupShowPopups(string popupSpaceKey)
	{
		#if UNITY_IPHONE
		_LiveOpsPopupShowPopups(popupSpaceKey);
		#endif
	}
	
	/// <summary>
	/// destroy now opened top popup
	/// </summary>
	public static void LiveOpsPopupDestroyPopup()
	{
		#if UNITY_IPHONE
		_LiveOpsPopupDestroyPopup();
		#endif
	}
	
	/// <summary>
	/// destroy all popups
	/// </summary>
	public static void LiveOpsPopupDestroyAllPopups()
	{
		#if UNITY_IPHONE
		_LiveOpsPopupDestroyAllPopups();
		#endif
	}
	
	/// <summary>
	/// Set PopupLinkListener
	/// </summary>
	public static void LiveOpsSetPopupLinkListener()
	{
		#if UNITY_IPHONE
		_LiveOpsSetPopupLinkListener();
		#endif
	}

	/// <summary>
	/// Set PopupCloseListener
	/// </summary>
	public static void LiveOpsSetPopupCloseListener()
	{
		#if UNITY_IPHONE
		_LiveOpsSetPopupCloseListener();
		#endif
	}
	
	#endregion



	#region LiveOpsPopup Callback Methods
	/// <summary>
	/// LiveOpsPopup GetPopup responded.
	/// </summary>	
	public void LiveOpsPopupGetPopupsResponded()
	{
		#if UNITY_IPHONE
		if ( liveOpsPopupGetPopupsResponded != null) {
			liveOpsPopupGetPopupsResponded();
		}
		#endif
	}
	
	/// <summary>
	/// LiveOpsPopup Set PopupLink Listener Called.
	/// </summary>	
	public void LiveOpsPopupSetPopupLinkListenerCalled(string popupLinkListenerResult)
	{
		#if UNITY_IPHONE
		if ( liveOpsPopupSetPopupLinkListenerCalled != null) {
			liveOpsPopupSetPopupLinkListenerCalled(popupLinkListenerResult);
		}
		#endif
	}

	/// <summary>
	/// LiveOpsPopup Set PopupClose Listener Called.
	/// </summary>	
	public void LiveOpsPopupSetPopupCloseListenerCalled(string popupCloseListenerResult)
	{
		#if UNITY_IPHONE
		if ( liveOpsPopupSetPopupCloseListenerCalled != null) {
			liveOpsPopupSetPopupCloseListenerCalled(popupCloseListenerResult);
		}
		#endif
	}
	
	#endregion
}