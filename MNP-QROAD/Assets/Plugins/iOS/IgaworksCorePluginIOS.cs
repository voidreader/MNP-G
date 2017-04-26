using UnityEngine;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;
using System.ComponentModel;


public class IgaworksCorePluginIOS : MonoBehaviour
{
	public const int IgaworksCoreLogInfo = 0;		/*! only info logging  */
	public const int IgaworksCoreLogDebug = 1;		/*! info, debug logging  */
	public const int IgaworksCoreLogTrace = 2;		/*! all logging */

	public const int IgaworksCoreGenderMale = 2;
	public const int IgaworksCoreGenderFemale = 1;




	#region Events
#if UNITY_IPHONE
	public static event Action <string> onRewardRequestResult;
	public static event Action <string> onRewardCompleteResult;
	public static event Action <string> didSaveConversionKey;
	public static event Action <string> didReceiveDeeplink;

#endif
	#endregion

	#region	Interface to native implementation
	[DllImport("__Internal")]
	extern public static void _SetCallbackHandler(string handlerName);

	[DllImport("__Internal")]
	extern public static void _IgaworksCoreWithAppKey(string appKey, string hashKey);

	[DllImport("__Internal")]
	extern public static void _SetUseIgaworksRewardServer(bool isUseIgaworksRewardServer);
	
	[DllImport("__Internal")]
	extern public static void _SetLogLevel(int logLevel);

	[DllImport("__Internal")]
	extern public static void _SetAge(int age);

	[DllImport("__Internal")]
	extern public static void _SetGender(int gender);

	[DllImport("__Internal")]
	extern public static void _SetUserId(string userId);

	[DllImport("__Internal")]
	extern public static void _SetIgaworksCoreDelegate();
	
	[DllImport("__Internal")]
	extern public static void _SetReferralUrl(string url);

	[DllImport("__Internal")]
	extern public static void _SetReferralUrlForFacebook(string url);

	#endregion

	#region Declarations for non-native for IgaworksCore
	/// <summary>
	/// Sets the callback handler.
	/// </summary>
	/// <param name='handlerName'>
	/// Handler name. Must match a Unity GameObject name, for the native code
	/// to utilize UnitySendMessage() properly.
	/// </param>
	public static void SetCallbackHandler(string handlerName)
	{
        #if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		_SetCallbackHandler(handlerName);
        #endif
	}
		
	/// <summary>
	/// 앱 기동시 초기화한다.  App. 기동시 한번만 호출하면 된다.
	/// </summary>
	/// <param name='appKey'>
	/// A <see cref="System.String"/> 
	/// 미디어 등록 후, IGAWorks로부터 발급된 키.
	/// </param>
	/// <param name='hashKey'>
	/// A <see cref="System.String"/> 
	/// 미디어 등록 후 발급된 키.
	/// </param>
	public static void IgaworksCoreWithAppKey(string mediaKey, string hashKey)
	{
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;

		_IgaworksCoreWithAppKey(mediaKey, hashKey);
		#endif
	}

	/// <summary>
	/// Set useIgaworksRewardServer 
	/// </summary>
	/// <param name='isUseIgaworksRewardServer'>
	/// A <see cref="System.bool"/> 
	/// igaworks에서 제공하는 r	eward server를 사용할것인지 여부.
	/// </param>
	public static void SetUseIgaworksRewardServer(bool isUseIgaworksRewardServer)
	{
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;

		_SetUseIgaworksRewardServer(isUseIgaworksRewardServer);
		#endif
	}


	/// <summary>
	/// 로그를 level를 설정한다.
	/// </summary>
	/// <param name='logLevel'>
	/// A <see cref="System.Int32"/> 
	/// 보고자 하는 로그 level을 info, debug, trace으로 설정한다.
	/// </param>
	public static void SetLogLevel(int logLevel)
	{
        #if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		_SetLogLevel(logLevel);
        #endif

	}

	/// <summary>
	/// 사용자의 나이 정보를 전송하고자 할때 호출한다.
	/// </summary>
	/// <param name="age">
	/// A <see cref="System.Int32"/>
	/// </param>
	public static void SetAge(int age)
	{
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		_SetAge(age);
		#endif
	}
	
	/// <summary>
	/// 사용자의 성별 정보를 전송하고자 할때 호출한다.
	/// </summary>
	/// <param name="gender">
	/// A <see cref="System.Int32"/>
	/// </param>
	public static void SetGender(int gender)
	{
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		_SetGender(gender);
		#endif
	}
	
	/// <summary>
	/// 사용자의 user id를 전송하고자 할때 호출한다.
	/// </summary>
	/// <param name="userId">
	/// A <see cref="System.String"/>
	/// </param>
	public static void SetUserId(string userId)
	{
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		_SetUserId(userId);
		#endif
	}

	/// <summary>
	/// Set IgaworksCoreDelegate 
	/// </summary>
	public static void SetIgaworksCoreDelegate()
	{
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		_SetIgaworksCoreDelegate();
		#endif
	}
		
	/// <summary>
	/// setReferralUrl
	/// </summary>	
	public static void SetReferralUrl(string url)
	{
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;

		_SetReferralUrl(url);
		#endif
	}

	/// <summary>
	/// setReferralUrlForFacebook
	/// </summary>	
	public static void setReferralUrlForFacebook(string url)
	{
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;

		_SetReferralUrl(url);
		#endif
	}

	#endregion

	#region IgaworksCore Callback Methods
	/// <summary>
	/// igaworks reward server를 사용하는 경우, reward 요청에 대한 결과를 delegate에 노티한다.
	/// </summary>	
	public void OnRewardRequestResult(string rewardRequestResult)
	{
		#if UNITY_IPHONE
		if (onRewardRequestResult != null) 
		{
			onRewardRequestResult(rewardRequestResult);
		}
		#endif
	}
	
	/// <summary>
	/// igaworks reward server를 사용하는 경우, reward 지급 완료에 대한 요쳥의 결과를 delegate에 노티한다.
	/// </summary>	
	public void OnRewardCompleteResult(string rewardCompleteResult)
	{
		#if UNITY_IPHONE
		if (onRewardCompleteResult != null) 
		{
			onRewardCompleteResult(rewardCompleteResult);
		}
		#endif
	}

	/// <summary>
	/// conversion시 conversion key, sub referral key를 전달한다.
	/// </summary>	
	public void DidSaveConversionKey(string conversionInfo)
	{
		#if UNITY_IPHONE
		if (conversionInfo != null) 
		{
			didSaveConversionKey(conversionInfo);
		}
		#endif
	}

	/// <summary>
	/// deep link를 전달한다.
	/// </summary>	
	public void DidReceiveDeeplink(string deepLink)
	{
		#if UNITY_IPHONE
		if (deepLink != null) 
		{
			didReceiveDeeplink(deepLink);
		}
		#endif
	}

	#endregion
}
