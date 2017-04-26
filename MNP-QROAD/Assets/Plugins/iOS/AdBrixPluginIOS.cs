using UnityEngine;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;
using System.Collections;
using System.ComponentModel;


public class AdBrixPluginIOS : MonoBehaviour
{
	// for cohort 분석
	public const int AdBrixCustomCohort_1 = 1;
	public const int AdBrixCustomCohort_2 = 2;
	public const int AdBrixCustomCohort_3 = 3;


	// for commerceCurrency defines
	public const int AdBrixCurrencyKRW = 1;
	public const int AdBrixCurrencyUSD = 2;
	public const int AdBrixCurrencyJPY = 3;
	public const int AdBrixCurrencyEUR = 4;
	public const int AdBrixCurrencyGBP = 5;
	public const int AdBrixCurrencyCHY = 6;
	public const int AdBrixCurrencyTWD = 7;
	public const int AdBrixCurrencyHKD = 8;

	#region Events
#if UNITY_IPHONE


#endif
	#endregion

	#region	Interface to native implementation



	[DllImport("__Internal")]
	extern public static void _FirstTimeExperience(string name);
	
	[DllImport("__Internal")]
	extern public static void _FirstTimeExperienceWithParam(string name, string param);
	
	[DllImport("__Internal")]
	extern public static void _Retention(string name);
	
	[DllImport("__Internal")]
	extern public static void _RetentionWithParam(string name, string param);
	
	[DllImport("__Internal")]
	extern public static void _Buy(string name);
	
	[DllImport("__Internal")]
	extern public static void _BuyWithParam(string name, string param);

	[DllImport("__Internal")]
	extern public static void _ShowViralCPINotice();
	
	[DllImport("__Internal")]
	extern public static void _SetCustomCohort(int customCohortType, string filterName);
	
	[DllImport("__Internal")]
	extern public static void _CrossPromotionShowAD(string activityName);


	/// <summary>
	/// Commerce
	/// </summary>
	[DllImport("__Internal")]
	extern public static void _AdBrixPurchase (string orderId, string productId, string productName, double price, int quantity, string currencyString, string category);

	[DllImport("__Internal")]
	extern public static void _AdBrixPurchaseWithJson (string jsonString);

	[DllImport("__Internal")]
	extern public static void _AdBrixPurchaseList (string[] pArr, int arrCnt);

	[DllImport("__Internal")]
	extern public static string _AdBrixCurrencyName (int currency);


	#endregion

	#region Declarations for non-native for AdBrix

	/// <summary>
	/// first time experience의 activity에 해당할때 호출한다.
	/// </summary>
	/// <param name="activityName">
	/// A <see cref="System.String"/>
	/// </param>
	public static void FirstTimeExperience(string name)
	{
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		_FirstTimeExperience(name);
		#endif
	}
	
	/// <summary>
	/// first time experience의 activity에 해당할때 호출한다.
	/// </summary>
	/// <param name="activityName">
	/// A <see cref="System.String"/>
	/// </param>
	/// <param name="param">
	/// A <see cref="System.String"/>
	/// </param>
	public static void FirstTimeExperienceWithParam(string name, string param)
	{
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		_FirstTimeExperienceWithParam(name, param);
		#endif
	}
	
	/// <summary>
	/// retention activity에 해당할때 호출한다.
	/// </summary>
	/// <param name="activityName">
	/// A <see cref="System.String"/>
	/// </param>
	public static void Retention(string name)
	{
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		_Retention(name);
		#endif
	}
	
	/// <summary>
	/// retention activity에 해당할때 호출한다.
	/// </summary>
	/// <param name="activityName">
	/// A <see cref="System.String"/>
	/// </param>
	/// <param name="param">
	/// A <see cref="System.String"/>
	/// </param>
	public static void RetentionWithParam(string name, string param)
	{
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		_RetentionWithParam(name, param);
		#endif
	}
	
	/// <summary>
	/// buy의 activity에 해당할때 호출한다.
	/// </summary>
	/// <param name="activityName">
	/// A <see cref="System.String"/>
	/// </param>
	public static void Buy(string activityName)
	{
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		_Buy(activityName);
		#endif
	}
	
	/// <summary>
	/// buy의 activity에 해당할때 호출한다.
	/// </summary>
	/// <param name="activityName">
	/// A <see cref="System.String"/>
	/// </param>
	/// <param name="param">
	/// A <see cref="System.String"/>
	/// </param>
	public static void BuyWithParam(string name, string param)
	{
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		_BuyWithParam(name, param);
		#endif
	}

	/// <summary>
	/// CPI + 친구초대 이벤트 요청시 호출한다.
	/// </summary>
	public static void ShowViralCPINotice()
	{
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;

		_ShowViralCPINotice();
		#endif
	}
	
	/// <summary>
	/// Cohort 분석 요청시 호출한다.
	/// </summary>
	/// <param name="customCohortType">
	/// A <see cref="int"/>
	/// </param>
	/// <param name="filterName">
	/// A <see cref="System.String"/>
	/// </param>
	public static void SetCustomCohort(int customCohortType, string filterName)
	{
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		_SetCustomCohort(customCohortType, filterName);
		#endif
	}



	public static void AdBrixPurchase(string orderId, string productId, string productName, double price, int quantity, string currencyString, string category)
	{
		#if UNITY_IPHONE
		_AdBrixPurchase(orderId, productId, productName, price, quantity, currencyString, category);
		#endif
	}

	public static void AdBrixPurchaseWithJson(string jsonString)
	{
		#if UNITY_IPHONE
		_AdBrixPurchaseWithJson(jsonString);
		#endif
	}

	public static void AdBrixPurchaseList(ArrayList pArr)
	{

		#if UNITY_IPHONE
		string[] myArray =  new string[pArr.Count];
		for (int i = 0; pArr.Count > i; i++)
		{
			if (pArr[i] is AdBrixItemModel)
			{
				AdBrixItemModel pObject = (AdBrixItemModel)pArr [i];
				myArray[i] = pObject.OrderId +"&"+ pObject.ProductId +"&"+ pObject.ProductName +"&"+ pObject.Price +"&"+ pObject.Quantity +"&"+ pObject.CurrencyString +"&"+ pObject.Category;
			}
			if (pArr[i] is PurchaseItemModel)
			{
				PurchaseItemModel pObject = (PurchaseItemModel)pArr [i];
				myArray[i] = pObject.OrderId +"&"+ pObject.ProductId +"&"+ pObject.ProductName +"&"+ pObject.Price +"&"+ pObject.Quantity +"&"+ pObject.CurrencyString +"&"+ pObject.Category;
			}
		
		}
		if(pArr.Count > 0)
		_AdBrixPurchaseList(myArray, pArr.Count);
		#endif
	}

	public static string AdBrixCurrencyName(int currency) 
	{
		string res = null;
		#if UNITY_IPHONE
		res = _AdBrixCurrencyName (currency);
		#endif
		return res;
	}

	#endregion	
	
	
	#region Declarations for non-native for CrossPromotion
	/// <summary>
	/// Cross Promotion 광고를 노출하고자 할때 호출한다.
	/// </summary>
	/// <param name="activityName">
	/// A <see cref="System.String"/>
	/// </param>
	public static void CrossPromotionShowAD(string activityName)
	{
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		_CrossPromotionShowAD(activityName);
		#endif
	}
		

	#endregion
}


public class AdBrixItemModel
{
	public string OrderId { get; set; }
	public string ProductId { get; set; }
	public double Price { get; set; } 
	public string CurrencyString{ get; set; } 
	public string Category{ get; set; }
	public int Quantity{ get; set; } 
	public string ProductName{ get; set; }

	public AdBrixItemModel(string orderId, string productId, string productName, double price, int quantity, string currencyString, string category)
	{
		if (orderId.Equals (null))
			orderId = "";
		if (productId.Equals (null))
			productId = "";
		if (productName.Equals (null))
			productName = "";
		if(price.Equals (null))
			price = 0.0;
		if(quantity.Equals (null))
			quantity = 1;
		if(currencyString.Equals (null))
			currencyString = "";
		if(category.Equals (null))
			category = "";

		this.OrderId = orderId;
		this.ProductId = productId;
		this.Price = price; 
		this.CurrencyString = currencyString; 
		this.Category = category;
		this.Quantity = quantity;
		this.ProductName = productName;
	}

	public static AdBrixItemModel create(string orderID, string productID, string productName, double price, int quantity, string currencyString, string category){

		return new AdBrixItemModel(orderID, productID, productName, price, quantity, currencyString, category);

	}
}

public class PurchaseItemModel
{
	public string OrderId { get; set; }
	public string ProductId { get; set; }
	public double Price { get; set; } 
	public string CurrencyString{ get; set; } 
	public string Category{ get; set; }
	public int Quantity{ get; set; } 
	public string ProductName{ get; set; }

	public PurchaseItemModel(string orderId, string productId, string productName, double price, int quantity, string currencyString, string category)
	{
		if (orderId.Equals (null))
			orderId = "";
		if (productId.Equals (null))
			productId = "";
		if (productName.Equals (null))
			productName = "";
		if(price.Equals (null))
			price = 0.0;
		if(quantity.Equals (null))
			quantity = 1;
		if(currencyString.Equals (null))
			currencyString = "";
		if(category.Equals (null))
			category = "";

		this.OrderId = orderId;
		this.ProductId = productId;
		this.Price = price; 
		this.CurrencyString = currencyString; 
		this.Category = category;
		this.Quantity = quantity;
		this.ProductName = productName;
	}

	public static PurchaseItemModel create(string orderID, string productID, string productName, double price, int quantity, string currencyString, string category){

		return new PurchaseItemModel(orderID, productID, productName, price, quantity, currencyString, category);

	}
}