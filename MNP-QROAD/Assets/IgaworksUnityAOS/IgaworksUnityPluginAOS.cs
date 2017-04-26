using UnityEngine;
using System;
using System.Collections.Generic;
using IgaworksUnityAOS.IgawLiveOpsPopupEventManager;

namespace IgaworksUnityAOS
{
    public class IgaworksUnityPluginAOS : MonoBehaviour
    {

        public class Gender
        {
            public static int FEMALE = 1;
            public static int MALE = 2;
        }

        public class OfferwallThemeStyle
        {
            public static string RED_THEME = "ff9d261c";
            public static string BLUE_THEME = "ff3d7caf";
            public static string YELLOW_THEME = "ffffba03";
        }

        public enum CohortVariable
        {

            COHORT_1, COHORT_2, COHORT_3

        }

        public class AndroidNotificationPriority
        {
            public static int PRIORITY_DEFAULT = 0;
            public static int PRIORITY_HIGH = 1;
            public static int PRIORITY_LOW = -1;
            public static int PRIORITY_MAX = 2;
            public static int PRIORITY_MIN = -2;
        }

        public class AndroidNotificationVisibility
        {
            public static int VISIBILITY_PRIVATE = 0;
            public static int VISIBILITY_PUBLIC = 1;
            public static int VISIBILITY_SECRET = -1;
        }

        public class APPermissionConst
        {
            public static int DEFAULT = 0;
            public static int GET_ACCOUNT = 1;
            public static int READ_PHONE_STATE = 2;
            public static int READ_EXTERNAL_STORAGE = 4;
            public static int WRITE_EXTERNAL_STORAGE = 8;
        }

        private static int numOfObject = 0;
        private int currentObjectIndex;

        void Awake()
        {
            currentObjectIndex = numOfObject;
            numOfObject++;
            Debug.Log("igaw awake, " + gameObject.name + ", index is " + currentObjectIndex);

            if (currentObjectIndex == 0)
                DontDestroyOnLoad(gameObject);
            else
                Destroy(gameObject);
        }

        /** IGAWorks Delegate
		 */
        public delegate void onReceiveDeferredLink(string deferredLink); // Facebook Ads install
        public static onReceiveDeferredLink OnReceiveDeferredLink;

        public delegate void onClosedOfferwallPage();
        public static onClosedOfferwallPage OnClosedOfferwallPage;

        public delegate void onGetRewardInfo(string campaignkey, string campaignname, string quantity, string cv, string rewardkey);
        public static onGetRewardInfo OnGetRewardInfo;
        public delegate void onDidGiveRewardItemRequestResult(bool isSuccess, string rewardkey);
        public static onDidGiveRewardItemRequestResult OnDidGiveRewardItemRequestResult;

        public delegate void onPlayBtnClickListener();
        public static onPlayBtnClickListener OnPlayBtnClickListener;
        public delegate void onOpenDialogListener();
        public static onOpenDialogListener OnOpenDialogListener;
        public delegate void onNoADAvailableListener();
        public static onNoADAvailableListener OnNoADAvailableListener;
        public delegate void onHideDialogListener();
        public static onHideDialogListener OnHideDialogListener;

        public delegate void onSendCouponSucceed(string msg, int itemKey, string itemName, long quantity);
        public static onSendCouponSucceed OnSendCouponSucceed;

        public delegate void onSendCouponFailed(string msg);
        public static onSendCouponFailed OnSendCouponFailed;

        public delegate void onOpenNanooFanPage(string url);
        public static onOpenNanooFanPage OnOpenNanooFanPage;

        public delegate void onGetTrackingParameter(int ck, string sub_ck);
        public static onGetTrackingParameter OnGetTrackingParameter;

        public delegate void onRequestPopupResource(bool isSuccess);
        public static onRequestPopupResource OnRequestPopupResource;

        public delegate void onEnableService(bool isSuccess);
        public static onEnableService OnEnableService;

        public delegate void onReceiveDeeplinkData(string deeplink);
        public static onReceiveDeeplinkData OnReceiveDeeplinkData;

        public delegate void onReceiveRegistrationId(string regId);
        public static onReceiveRegistrationId OnReceiveRegistrationId;

        public delegate void onLiveOpsPopupClick();
        public static onLiveOpsPopupClick OnLiveOpsPopupClick;

        public delegate void onLiveOpsCancelPopupBtnClick();
        public static onLiveOpsCancelPopupBtnClick OnLiveOpsCancelPopupBtnClick;

        private static LiveOpsPopupEventManager mLiveOpsPopupEventManager;

        // Add IgawUnityPlugin_aos_connector_v1.0.23.jar : AdPopcorn v4.0.9a Delegate
        public delegate void onLoadVideoAdFailure(string apErrorMessage);
        public static onLoadVideoAdFailure OnLoadVideoAdFailure;

        public delegate void onLoadVideoAdSuccess();
        public static onLoadVideoAdSuccess OnLoadVideoAdSuccess;

        public delegate void onShowVideoAdFailure(string apErrorMessage);
        public static onShowVideoAdFailure OnShowVideoAdFailure;

        public delegate void onShowVideoAdSuccess();
        public static onShowVideoAdSuccess OnShowVideoAdSuccess;

        public delegate void onVideoAdClose();
        public static onVideoAdClose OnVideoAdClose;

#if UNITY_EDITOR
#elif UNITY_ANDROID
	
	static IgaworksUnityPluginAOS _igaworksUnityPluginAosInstance = null;
	static AndroidJavaClass _igaworksUnityPluginAosClass = null;
#endif

        public static void InitPlugin()
        {
#if UNITY_EDITOR
            Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
		
		if(_igaworksUnityPluginAosInstance == null)
		{
			Debug.Log ("#########################################");
			Debug.Log ("IGAWorksAdbrixUnityPluginAOS GameObject Created!!!");
			_igaworksUnityPluginAosInstance = new GameObject("IgaworksUnityPluginAOS").AddComponent<IgaworksUnityPluginAOS>();
			
		}		
		
		_igaworksUnityPluginAosClass = new AndroidJavaClass("com.igaworks.unity.plugin.IgaworksUnityPluginAos");
		if(_igaworksUnityPluginAosClass != null){
			Debug.Log ("#########################################");
			Debug.Log ("IGAWorksAdbrixUnityPluginAOS Connected!!!");
			Debug.Log ("#########################################");
		}else{
			Debug.Log ("#########################################");
			Debug.Log ("IGAWorksAdbrixUnityPluginAOS Connect FAIL!!!");
			Debug.Log ("#########################################");
		}
#endif
        }

        public static class Common
        {

            public static void startApplication()
            {
#if UNITY_EDITOR
                Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
			//InitPlugin();
			_igaworksUnityPluginAosClass.CallStatic ("startApplication");
			Debug.Log ("IGAWorksAdbrixUnityPlugin Call StartApplication!!!");
			
#endif
            }

            public static void startApplication(string IMEI)
            {
#if UNITY_EDITOR
                Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
			//InitPlugin();
			_igaworksUnityPluginAosClass.CallStatic ("startApplication", IMEI);
			Debug.Log ("IGAWorksAdbrixUnityPlugin Call StartApplication with puid!!!");
			
#endif
            }

            public static void setDeferredLinkListener()
            {
#if UNITY_EDITOR
                Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
			_igaworksUnityPluginAosClass.CallStatic ("setDeferredLinkListener");
#endif

            }
            // setDeferredLinkListener should be called instead of FB.Mobile.FetchDeferredAppLinkData in Facebook Unity SDK
            // If FB.Mobile.FetchDeferredAppLinkData is used. You have to call setReferralUrl for Igaworks to collect tracking parameters
            public static void setReferralUrl(string native_deeplink_Uri)
            {
#if UNITY_EDITOR
                Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
			    _igaworksUnityPluginAosClass.CallStatic ("setReferralUrl", native_deeplink_Uri);
                Debug.Log ("Igaworks Unity >> setReferralUrl: " + native_deeplink_Uri);
#endif
            }
            public static void setReferralUrlForFacebook(string native_deeplink_Uri)
            {
#if UNITY_EDITOR
                Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
			    _igaworksUnityPluginAosClass.CallStatic ("setReferralUrl", native_deeplink_Uri);
                Debug.Log ("Igaworks Unity >> setReferralUrl: " + native_deeplink_Uri);
#endif
            }

            public static void startSession()
            {
#if UNITY_EDITOR
                Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
			_igaworksUnityPluginAosClass.CallStatic ("startSession");
#endif
            }

            public static void endSession()
            {
#if UNITY_EDITOR
                Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
			_igaworksUnityPluginAosClass.CallStatic ("endSession");
#endif

            }

            public static void setUserId(string userId)
            {
#if UNITY_EDITOR
                Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
			_igaworksUnityPluginAosClass.CallStatic ("setUserId", userId);
#endif
            }

            public static void setClientRewardEventListener()
            {
                //setIgawRewardServerReceiver
#if UNITY_EDITOR
                Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
			Debug.Log ("igaworks:RewardEventListener Setted!!");
			_igaworksUnityPluginAosClass.CallStatic ("setUnityPlatform");
			_igaworksUnityPluginAosClass.CallStatic ("setIgawRewardServerReceiver");
#endif
            }

            public static void getClientPendingRewardItems()
            {
#if UNITY_EDITOR
                Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
			_igaworksUnityPluginAosClass.CallStatic ("getClientPendingRewardItems");
#endif
            }

            public static void didGiveRewardItem(string cv, string rewardkey)
            {
#if UNITY_EDITOR
                Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
			_igaworksUnityPluginAosClass.CallStatic("didGiveRewardItem",cv,rewardkey);
#endif
            }

        }

        public static class Adbrix
        {

            [System.Serializable]
            public class PurchaseItemModel
            {
                public string orderId = "unknown";
                public string productId = "unknown";
                public string productName = "unknown";
                public double price = 0;
                public int quantity = 1;
                public string currency = "unknown";
                public string category = "unknown";

                public PurchaseItemModel(string orderID, string productID,
                    string productName, double price, int quantity, string currency,
                    string category)
                {
                    if (orderID != null && orderID.Length > 0) this.orderId = orderID;
                    if (productID != null && productID.Length > 0) this.productId = productID;
                    if (productName != null && productName.Length > 0) this.productName = productName;
                    this.price = price;
                    this.quantity = quantity;
                    if (currency != null && currency.Length > 0) this.currency = currency;
                    if (category != null && category.Length > 0) this.category = category;
                }
            }

            public class Currency
            {
                public static string KR_KRW = "KRW";
                public static string US_USD = "USD";
                public static string JP_JPY = "JPY";
                public static string EU_EUR = "EUR";
                public static string UK_GBP = "GBP";
                public static string CH_CHY = "CHY";
                public static string TW_TWD = "TWD";
                public static string HK_HKD = "HKD";
            }

            // For commerce API:
            public static string stringifyCommerceItem(PurchaseItemModel item)
            {
                string jsonstring = "";
                if (item != null)
                {
                    string orderId = item.orderId;
                    string productId = item.productId;
                    string productName = item.productName;
                    double price = item.price;
                    string currency = item.currency;
                    int quantity = item.quantity;
                    string category = item.category;

                    jsonstring = "{ \"orderId\": " + orderId + ", " +
                                    "\"productId\": " + productId + ", " +
                                    "\"productName\": " + productName + ", " +
                                    "\"price\": " + price + ", " +
                                    "\"currency\": " + currency + ", " +
                                    "\"quantity\": " + quantity + ", " +
                                    "\"category\": " + currency + " }";

                }
                return jsonstring;
            }


            public static void setCustomCohort(CohortVariable cohortVariable, string cohort)
            {
#if UNITY_EDITOR
                Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
			_igaworksUnityPluginAosClass.CallStatic ("setCustomCohort", cohortVariable.ToString(), cohort);
#endif
            }

            public static void setAge(int age)
            {
#if UNITY_EDITOR
                Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
			_igaworksUnityPluginAosClass.CallStatic ("setAge", age);
#endif
            }

            public static void setGender(int gender)
            {
#if UNITY_EDITOR
                Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
			_igaworksUnityPluginAosClass.CallStatic ("setGender", gender);
#endif
            }

            public static void firstTimeExperience(string name)
            {
#if UNITY_EDITOR
                Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
			_igaworksUnityPluginAosClass.CallStatic ("firstTimeExperience", name);
#endif
            }
            public static void firstTimeExperience(string name, string param)
            {
#if UNITY_EDITOR
                Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
			_igaworksUnityPluginAosClass.CallStatic ("firstTimeExperience", name, param);
#endif
            }

            public static void retention(string name)
            {
#if UNITY_EDITOR
                Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
			_igaworksUnityPluginAosClass.CallStatic ("retention", name);
#endif
            }

            public static void retention(string name, string param)
            {
#if UNITY_EDITOR
                Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
			_igaworksUnityPluginAosClass.CallStatic ("retention", name, param);		
#endif
            }

            public static void buy(string name)
            {
#if UNITY_EDITOR
                Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
			_igaworksUnityPluginAosClass.CallStatic ("buy", name);
#endif
            }

            public static void buy(string name, string param)
            {
#if UNITY_EDITOR
                Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
			_igaworksUnityPluginAosClass.CallStatic ("buy", name, param);
#endif
            }
            public static void getTrackingParameter()
            {
#if UNITY_EDITOR
                Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
			_igaworksUnityPluginAosClass.CallStatic ("getTrackingParameter");
#endif
            }

            public static void purchase(string orderID, string productID, string productName, double price, int quantity, string currency, string category)
            {
#if UNITY_EDITOR
                Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
                PurchaseItemModel item = new PurchaseItemModel(orderID, productID, productName, price, quantity, currency, category);
                List<PurchaseItemModel> list = new List<PurchaseItemModel>();
                list.Add(item);
                purchase(list);
#endif
            }

            public static void purchase(List<PurchaseItemModel> items)
            {


#if UNITY_EDITOR
                Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
				if (items == null || items.Count == 0) {					
					Debug.Log("igaworks:purchase >> Null or Empty Item List");
					return;
				}
				 List<PurchaseItemModel> filterList = new List<PurchaseItemModel>();
                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i] != null) filterList.Add(items[i]);
                } 
                if (filterList == null || filterList.Count == 0) {					
					Debug.Log("igaworks:purchase >> Filtered list is empty");
					return;
				}
                
				string jsonArray = "[";
				for(int i = 0; i < filterList.Count; i++)
				{
					PurchaseItemModel item = filterList[i];
					if(i == (filterList.Count-1))
					{
						jsonArray = jsonArray + stringifyCommerceItem(item) + "]";
					}
					else
					{
						jsonArray = jsonArray + stringifyCommerceItem(item) + ",";
					}
				  
				}                
      
				_igaworksUnityPluginAosClass.CallStatic("purchase", jsonArray);
#endif

            }

        }

        public static class Adpopcorn
        {

            public static void openOfferwall()
            {
#if UNITY_EDITOR
                Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
			_igaworksUnityPluginAosClass.CallStatic ("openOfferwall");
#endif
            }

            public static void openBanner(string ckey)
            {
#if UNITY_EDITOR
                Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
			_igaworksUnityPluginAosClass.CallStatic ("openBanner", ckey);
#endif
            }

            public static void openPromotion()
            {
#if UNITY_EDITOR
                Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
			_igaworksUnityPluginAosClass.CallStatic ("openPromotion");
#endif
            }

            public static void setSensorLandscapeEnable(bool enable)
            {
#if UNITY_EDITOR
                Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
			_igaworksUnityPluginAosClass.CallStatic ("setSensorLandscapeEnable", enable);
#endif
            }

            public static void setExceptionPermissionList(int exceptionPermissionList)
            {
#if UNITY_EDITOR
                Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
				_igaworksUnityPluginAosClass.CallStatic ("setExceptionPermissionList", exceptionPermissionList);
#endif
            }

            public static void loadVideoAd()
            {
#if UNITY_EDITOR
                Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
				_igaworksUnityPluginAosClass.CallStatic ("loadVideoAd");
#endif
            }

            public static void showVideoAd()
            {
#if UNITY_EDITOR
                Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
				_igaworksUnityPluginAosClass.CallStatic ("showVideoAd");
#endif
            }

            public static void setOfferwallThemeColor(string color)
            {
#if UNITY_EDITOR
                Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
			_igaworksUnityPluginAosClass.CallStatic ("setOfferwallThemeColor", color);
#endif
            }

            public static void setOfferwallRewardThemeColor(string color)
            {
#if UNITY_EDITOR
                Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
			_igaworksUnityPluginAosClass.CallStatic ("setOfferwallRewardThemeColor", color);
#endif
            }

            public static void setOfferwallRewardCheckThemeColor(string color)
            {
#if UNITY_EDITOR
                Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
			_igaworksUnityPluginAosClass.CallStatic ("setOfferwallRewardCheckThemeColor", color);
#endif
            }

            public static void setOfferwallTextThemeColor(string color)
            {
#if UNITY_EDITOR
                Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
			_igaworksUnityPluginAosClass.CallStatic ("setOfferwallTextThemeColor", color);
#endif
            }

            public static void setAdpopcornOfferwallEventListener()
            {
#if UNITY_EDITOR
                Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
			_igaworksUnityPluginAosClass.CallStatic ("setAdpopcornOfferwallEventListener");
#endif
            }
        }

        public static class Promotion
        {

            public static void showAD(string name)
            {
#if UNITY_EDITOR
                Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
			_igaworksUnityPluginAosClass.CallStatic ("showAD", name);
#endif
            }

            public static void hideAD()
            {
#if UNITY_EDITOR
                Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
			_igaworksUnityPluginAosClass.CallStatic ("hideAD");
#endif
            }

        }

        public static class Coupon
        {

            public static void showCouponDialog(bool showResultMsgToast)
            {

#if UNITY_EDITOR
                Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
			_igaworksUnityPluginAosClass.CallStatic ("showCouponDialog", showResultMsgToast);
#endif

            }

            public static void checkCoupon(string couponText)
            {

#if UNITY_EDITOR
                Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
			_igaworksUnityPluginAosClass.CallStatic ("checkCoupon", couponText);
#endif

            }

        }

        public static class LiveOps
        {

            public static void initialize()
            {
#if UNITY_EDITOR
                Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
			_igaworksUnityPluginAosClass.CallStatic ("initializeLiveOps");
#endif
            }

            public static void initialize(string senderIDs)
            {
#if UNITY_EDITOR
                Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
			_igaworksUnityPluginAosClass.CallStatic ("initializeLiveOps",senderIDs);
#endif
            }

            public static void resume()
            {
#if UNITY_EDITOR
                Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
			_igaworksUnityPluginAosClass.CallStatic ("resumeLiveOps");
#endif
            }

            public static void pause()
            {
#if UNITY_EDITOR
                Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
						_igaworksUnityPluginAosClass.CallStatic ("pauseLiveOps");
#endif
            }

            public static void setTargetingData(string userGroup, int userData)
            {
#if UNITY_EDITOR
                Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
			_igaworksUnityPluginAosClass.CallStatic ("setTargetingData", userGroup, userData);
			
#endif
            }
            public static void setTargetingData(string userGroup, long userData)
            {
#if UNITY_EDITOR
                Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
			_igaworksUnityPluginAosClass.CallStatic ("setTargetingData", userGroup, userData);
			
#endif
            }
            public static void setTargetingData(string userGroup, string userData)
            {
#if UNITY_EDITOR
                Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
			_igaworksUnityPluginAosClass.CallStatic ("setTargetingData", userGroup, userData);
			
#endif
            }
            public static void setTargetingData(string userGroup, bool userData)
            {
#if UNITY_EDITOR
                Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
			_igaworksUnityPluginAosClass.CallStatic ("setTargetingData", userGroup, userData);
			
#endif
            }
            public static void setTargetingData(string userGroup, float userData)
            {
#if UNITY_EDITOR
                Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
			_igaworksUnityPluginAosClass.CallStatic ("setTargetingData", userGroup, userData);
			
#endif
            }

            public static void cancelClientPushEvent(int eventId)
            {
#if UNITY_EDITOR
                Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
			_igaworksUnityPluginAosClass.CallStatic ("cancelClientPushEvent", eventId);
#endif
            }


            public static void setNormalClientPushEvent(long second, string contentText, int eventId, bool alwaysIsShown)
            {
#if UNITY_EDITOR
                Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
			_igaworksUnityPluginAosClass.CallStatic ("setNormalClientPushEvent", second, contentText, eventId, alwaysIsShown);
#endif
            }

            public static void setBigTextClientPushEvent(long second, string contentText, string bigContentTitle, string bigText,
                                                         string summaryText, int eventId, bool alwaysIsShown)
            {
#if UNITY_EDITOR
                Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
			_igaworksUnityPluginAosClass.CallStatic ("setBigTextClientPushEvent", second, contentText, bigContentTitle, bigText, summaryText, eventId, alwaysIsShown);
#endif
            }

            public static void enableService(bool enable)
            {
#if UNITY_EDITOR
                Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
			_igaworksUnityPluginAosClass.CallStatic ("enableService", enable);
#endif
            }

            public static void enableServiceWithDelegate(bool enable)
            {
#if UNITY_EDITOR
                Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
			_igaworksUnityPluginAosClass.CallStatic ("enableServiceWithDelegate", enable);
#endif
            }

            public static void setNotificationIconName(string notificationIconName)
            {
#if UNITY_EDITOR
                Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
			_igaworksUnityPluginAosClass.CallStatic ("setNotificationIconName", notificationIconName);
#endif
            }

            public static void setNotificationIconStyle(string smallIcon, string largeIcon, string iconbackground_argb)
            {
#if UNITY_EDITOR
                Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
			_igaworksUnityPluginAosClass.CallStatic ("setNotificationIconStyle", smallIcon, largeIcon, iconbackground_argb);
#endif
            }

            public static void setNotificationOption(int priority, int visibility)
            {
#if UNITY_EDITOR
                Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
			_igaworksUnityPluginAosClass.CallStatic ("setNotificationOption", priority, visibility);
#endif
            }

            public static void setStackingNotificationOption(bool useStacking, bool useTitleForStacking, string ContentTitle, string ContentText, string bigContentTitle, string bigContentSummaryText)
            {
#if UNITY_EDITOR
                Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
			_igaworksUnityPluginAosClass.CallStatic ("setStackingNotificationOption", useStacking, useTitleForStacking, ContentTitle, ContentText, bigContentTitle, bigContentSummaryText);
#endif
            }

            public static void requestPopupResource()
            {
#if UNITY_EDITOR
                Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
			_igaworksUnityPluginAosClass.CallStatic ("requestPopupResource");
#endif
            }
            public static void showPopUp(string spaceKey)
            {
#if UNITY_EDITOR
                Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
			_igaworksUnityPluginAosClass.CallStatic ("showPopUp", spaceKey);
#endif
            }

            public static void setRegistrationIdEventListener()
            {
#if UNITY_EDITOR
                Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
					   _igaworksUnityPluginAosClass.CallStatic("setRegistrationIdEventListener");
#endif
            }

            public static void setLiveOpsPopupEventListener()
            {

                mLiveOpsPopupEventManager = new LiveOpsPopupEventManager();
                mLiveOpsPopupEventManager.OnPopupClick += mLiveOpsPopupEventManager_OnPopupClick;
                mLiveOpsPopupEventManager.OnCancelPopupBtnClick += mLiveOpsPopupEventManager_OnCancelPopupBtnClick;
            }
            public static void destroyPopup()
            {
#if UNITY_EDITOR
                Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
						_igaworksUnityPluginAosClass.CallStatic ("destroyPopup");
#endif
            }
            public static void destroyAllPopups()
            {
#if UNITY_EDITOR
                Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
									_igaworksUnityPluginAosClass.CallStatic ("destroyAllPopups");
#endif
            }
            public static void flushTargetingData()
            {
#if UNITY_EDITOR
                Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
					_igaworksUnityPluginAosClass.CallStatic ("flush");
#endif

            }


        }

        public static class Nanoo
        {

            public static void openNanooFanPage(bool openAutomatically)
            {
#if UNITY_EDITOR
                Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
			_igaworksUnityPluginAosClass.CallStatic ("openNanooFanPage", openAutomatically);
#endif
            }

        }

        public void DeferredLinkListenerForUnity(string deferredLink)
        {

#if UNITY_EDITOR
            Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
		Debug.Log("Igaw.Unity : DeferredLinkListenerForUnity Result: " + deferredLink);		
		if (OnReceiveDeferredLink != null)
			OnReceiveDeferredLink (deferredLink);
#endif
        }

        public void OnClosedOfferwallPageForUnity(string result)
        {

#if UNITY_EDITOR
            Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
		Debug.Log("AP.Unity : OnClosedOfferwallPageForUnity : ");
		if (OnClosedOfferwallPage != null)
			OnClosedOfferwallPage ();
#endif

        }

        public void OnPlayBtnClickListenerForUnity()
        {

#if UNITY_EDITOR
            Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
		Debug.Log("AP.Unity : OnPlayBtnClickListenerForUnity : ");
		if (OnPlayBtnClickListener != null)
			OnPlayBtnClickListener ();
#endif

        }

        public void OnOpenDialogListenerForUnity()
        {

#if UNITY_EDITOR
            Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
		Debug.Log("AP.Unity : OnOpenDialogListenerForUnity : ");
		if (OnOpenDialogListener != null)
			OnOpenDialogListener ();
#endif

        }

        public void OnNoADAvailableListenerForUnity()
        {

#if UNITY_EDITOR
            Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
		Debug.Log("AP.Unity : OnNoADAvailableListenerForUnity : ");
		if (OnNoADAvailableListener != null)
			OnNoADAvailableListener ();
#endif

        }

        public void OnHideDialogListenerForUnity()
        {

#if UNITY_EDITOR
            Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
		Debug.Log("AP.Unity : OnHideDialogListenerForUnity : ");
		if (OnHideDialogListener != null)
			OnHideDialogListener ();
#endif

        }

        public void onSendCouponSucceedForUnity(string param)
        {

            string[] pList = param.Split('&');
            string message = null, itemName = null;
            int itemKey = 0;
            long quantity = 0;

            foreach (string item in pList)
            {

                string[] unit = item.Split('=');
                string key = unit[0];
                string val = Uri.UnescapeDataString(unit[1].Replace("+", " "));

                if (key.Equals("Message"))
                {
                    message = val;
                }
                else if (key.Equals("ItemKey"))
                {
                    itemKey = Convert.ToInt32(val);
                }
                else if (key.Equals("ItemName"))
                {
                    itemName = val;
                }
                else if (key.Equals("Quantity"))
                {
                    quantity = Convert.ToInt64(val);
                }

            }

#if UNITY_EDITOR
            Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
		Debug.Log("AP.Unity : onSendCouponSucceedForUnity : " + param);
		if (OnSendCouponSucceed != null){
			OnSendCouponSucceed (message, itemKey, itemName, quantity);
		}
#endif

        }

        public void onSendCouponFailedForUnity(string msg)
        {

#if UNITY_EDITOR
            Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
		Debug.Log("AP.Unity : onSendCouponFailedForUnity : ");
		if (OnSendCouponFailed != null)
			OnSendCouponFailed (msg);
#endif

        }

        /*public void onLiveOpsNotificationForUnity(string data) {

#if UNITY_EDITOR
			Debug.Log ("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
			//Debug.Log("AP.Unity : OnLiveOpsNotificationForUnity : " + data);
			if(OnLiveOpsNotification != null)
				OnLiveOpsNotification(data);
#endif
		}*/

        public void onOpenNanooFanPageForUnity(string url)
        {
            /*
			 * IGAW's reward center server will send the result message of 
			 */
#if UNITY_EDITOR
            Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
		//Debug.Log("AP.Unity : OnOpenNanooFanPageForUnity : " + url);
		if(OnOpenNanooFanPage != null)
			OnOpenNanooFanPage(url);
#endif
        }

        public void OnGetRewardInfoForUnity(string rewardInfo)
        {

            /* 
			 * You can get the reward inforamation about user completed each campaign.
			 * You have to provide proper reward to user by using information(quantity, campaign name, campaignkey, rewardkey) are included in above rewardInfo parameter.
			 * The rewardInfo value is comprised below format.
			 * 	  campaignkey=100001&campaignname=testcampaign&quantity=700&cv=testcv&rewardkey=1a2b3c4d5f6g
			 * 
			 * parameter details
			 *    campaignkey : campaign unique id
			 *    campaignname : The name of campaign
			 * 	  cv : The unique verification key.
			 *    quantity : reward's quantity
			 *    rewardkey : The transaction unique id about each campaign
			 */

#if UNITY_EDITOR
            Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
		Debug.Log("AP.Unity : Pending Reward Info : " + rewardInfo);
		string[] rewardData = rewardInfo.Split(new string[] {"&"},System.StringSplitOptions.None);
		
		string campaignkey = null;
		string campaignname = null;
		string quantity = null;
		string cv = null;
		string rewardkey = null;
		
		foreach(string rewardItems in rewardData){
			
			string[] rewardItem = rewardItems.Split(new string[] {"="}, System.StringSplitOptions.None);
			if(rewardItem[0].Contains("campaignkey"))
				campaignkey = rewardItem[1];
			
			if(rewardItem[0].Contains("campaignname"))
				campaignname = rewardItem[1];
			
			if(rewardItem[0].Contains("quantity"))
				quantity = rewardItem[1];
			
			if(rewardItem[0].Contains("rewardkey"))
				rewardkey = rewardItem[1];
			
			if(rewardItem[0].Contains("cv"))
				cv = rewardItem[1];
		}
		
		if(OnGetRewardInfo != null)
			OnGetRewardInfo(campaignkey,campaignname,quantity,cv,rewardkey);
		
#endif
        }

        public void OnDidGiveRewardItemRequestResultForUnity(string rewardInfo)
        {
            /*
			 * IGAW's reward center server will send the result message of 
			 */
#if UNITY_EDITOR
            Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
		Debug.Log("AP.Unity : OnCompleteMessage : " + rewardInfo);
		string[] rewardData = rewardInfo.Split(new string[] {"&"},System.StringSplitOptions.None);

		string isSuccess = null;
		string completedRewardKey = null;

		foreach(string rewardItems in rewardData){

			string[] rewardItem = rewardItems.Split(new string[] {"="}, System.StringSplitOptions.None);
			if(rewardItem[0].Contains("isSuccess"))
				isSuccess = rewardItem[1];

			if(rewardItem[0].Contains("completedRewardKey"))
				completedRewardKey = rewardItem[1];
		}

		if(OnDidGiveRewardItemRequestResult != null){
			if(isSuccess.Equals("true"))
				OnDidGiveRewardItemRequestResult(true, completedRewardKey);
			else
				OnDidGiveRewardItemRequestResult(false, completedRewardKey);
		}
#endif
        }
        public void OnGetTrackingParameterForUnity(string trackingParaInfo)
        {

#if UNITY_EDITOR
            Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
		Debug.Log("Igaw.Unity : OnGetTrackingParameterForUnity : " + trackingParaInfo);

		string[] pList = trackingParaInfo.Split ('&');

		int ck = -1;
		string sub_ck = null;
		
		foreach (string item in pList) {
			
			string[] unit = item.Split('=');
			string key = unit[0];
			string val = Uri.UnescapeDataString(unit[1].Replace("+", " "));
			
			if(key.Equals("ck")){
				ck = Convert.ToInt32(val);
			}else if(key.Equals("sub_ck")){
				sub_ck = val;
			}	
		}

		if (OnGetTrackingParameter != null)
			OnGetTrackingParameter (ck,sub_ck);
#endif

        }
        public void onRequestPopupResourceForUnity(string isSuccess)
        {

#if UNITY_EDITOR
            Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
				Debug.Log("Igaw.Unity : OnRequestPopupResourceForUnity Result: " + isSuccess);	
				if(OnRequestPopupResource != null){
					if(isSuccess.Equals("true"))
						OnRequestPopupResource(true);
					else
						OnRequestPopupResource(false);
				}
#endif
        }

        public void onEnableServiceForUnity(string isSuccess)
        {

#if UNITY_EDITOR
            Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
						Debug.Log("Igaw.Unity : OnEnableServiceForUnity Result: " + isSuccess);	
						if(OnEnableService != null){
							if(isSuccess.Equals("true"))
								OnEnableService(true);
							else
								OnEnableService(false);
						}
#endif
        }


        public void onReceiveDeeplinkDataForUnity(string deeplink)
        {

#if UNITY_EDITOR
            Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
		Debug.Log("Igaw.Unity : OnReceiveDeeplinkDataForUnity Result: " + deeplink);		
		if (OnReceiveDeeplinkData != null)
			OnReceiveDeeplinkData (deeplink);
#endif
        }
        public void onReceiveRegistrationIdForUnity(string regId)
        {

#if UNITY_EDITOR
            Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
		Debug.Log("Igaw.Unity : onReceiveRegistrationIdForUnity >> Registration ID: " + regId);		
		if (OnReceiveRegistrationId != null)
			OnReceiveRegistrationId (regId);
#endif
        }

        //Popup event listener for Unity
        private static void mLiveOpsPopupEventManager_OnCancelPopupBtnClick(object sender, EventArgs e)
        {
            //Debug.Log("Igaw.Unity : onCancelPopupBtnClickForUnity");
            if (OnLiveOpsCancelPopupBtnClick != null)
                OnLiveOpsCancelPopupBtnClick();
        }

        private static void mLiveOpsPopupEventManager_OnPopupClick(object sender, EventArgs e)
        {
            //Debug.Log("Igaw.Unity : onPopupClickForUnity");
            if (OnLiveOpsPopupClick != null)
                OnLiveOpsPopupClick();
        }
        void OnDestroy()
        {
            if (mLiveOpsPopupEventManager != null)
            {
                mLiveOpsPopupEventManager.OnPopupClick -= mLiveOpsPopupEventManager_OnPopupClick;
                mLiveOpsPopupEventManager.OnCancelPopupBtnClick -= mLiveOpsPopupEventManager_OnCancelPopupBtnClick;
            }
        }

        // Add IgawUnityPlugin_aos_connector_v1.0.23.jar : AdPopcorn v4.0.9a Delegate
        public void OnLoadVideoAdFailureForUnity(string apErrorMessage)
        {

#if UNITY_EDITOR
            Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
			Debug.Log("AP.Unity : OnLoadVideoAdFailureForUnity");
			if (OnLoadVideoAdFailure != null)
				OnLoadVideoAdFailure (apErrorMessage);
#endif

        }

        public void OnLoadVideoAdSuccessForUnity(string result)
        {

#if UNITY_EDITOR
            Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
			Debug.Log("AP.Unity : OnLoadVideoAdSuccessForUnity");
			if (OnLoadVideoAdSuccess != null)
				OnLoadVideoAdSuccess ();
#endif

        }

        public void OnShowVideoAdFailureForUnity(string apErrorMessage)
        {

#if UNITY_EDITOR
            Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
			Debug.Log("AP.Unity : OnShowVideoAdFailureForUnity");
			if (OnShowVideoAdFailure != null)
				OnShowVideoAdFailure (apErrorMessage);
#endif

        }

        public void OnShowVideoAdSuccessForUnity(string result)
        {

#if UNITY_EDITOR
            Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
			Debug.Log("AP.Unity : OnShowVideoAdSuccessForUnity");
			if (OnShowVideoAdSuccess != null)
				OnShowVideoAdSuccess ();
#endif

        }

        public void OnVideoAdCloseForUnity(string result)
        {

#if UNITY_EDITOR
            Debug.Log("igaworks:Editor mode Connected");
#elif UNITY_ANDROID
			Debug.Log("AP.Unity : OnVideoAdCloseForUnity");
			if (OnVideoAdClose != null)
				OnVideoAdClose ();
#endif

        }


    }

}
