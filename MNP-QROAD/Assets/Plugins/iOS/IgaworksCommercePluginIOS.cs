using UnityEngine;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;
using System.Collections;
using System.ComponentModel;

public class IgaworksCommercePluginIOS : MonoBehaviour {

	// for commerceCurrency defines
	public const int IgaworksCommerceCurrencyKRW = 1;
	public const int IgaworksCommerceCurrencyUSD = 2;
	public const int IgaworksCommerceCurrencyJPY = 3;
	public const int IgaworksCommerceCurrencyEUR = 4;
	public const int IgaworksCommerceCurrencyGBP = 5;
	public const int IgaworksCommerceCurrencyCHY = 6;
	public const int IgaworksCommerceCurrencyTWD = 7;
	public const int IgaworksCommerceCurrencyHKD = 8;

	[DllImport("__Internal")]
	extern public static void _IgaworksCommercePurchase (string orderId, string productId, string productName, double price, int quantity, string currencyString, string category);

	[DllImport("__Internal")]
	extern public static void _IgaworksCommercePurchaseWithJson (string jsonString);

	[DllImport("__Internal")]
	extern public static void _IgaworksCommercePurchaseList (string[] pArr, int arrCnt);

	[DllImport("__Internal")]
	extern public static string _currencyName (int currency);

	/// <summary>
	/// 구매 발생 시 호출한다.
	/// </summary>
	/// <param name="productId">
	/// A <see cref="System.string"/> 
	/// </param>
	/// <param name="price">
	/// A <see cref="System.double"/> 
	/// </param>
	/// <param name="currencyString">
	/// A <see cref="System.string"/> 
	/// </param>
	/// <param name="category1">
	/// A <see cref="System.string"/> 
	/// </param>
	/// <param name="category2">
	/// A <see cref="System.string"/> 
	/// </param>
	/// <param name="category3">
	/// A <see cref="System.string"/> 
	/// </param>
	/// <param name="category4">
	/// A <see cref="System.string"/> 
	/// </param>
	/// <param name="category5">
	/// A <see cref="System.string"/> 
	/// </param>
	/// <param name="quantity">
	/// A <see cref="System.int"/> 
	/// </param>
	/// <param name="productName">
	/// A <see cref="System.string"/> 
	/// </param>

	public static void IgaworksCommercePurchase(string orderId, string productId, string productName, double price, int quantity, string currencyString, string category)
	{
		#if UNITY_IPHONE
		_IgaworksCommercePurchase(orderId, productId, productName, price, quantity, currencyString, category);
		#endif
	}

	public static void IgaworksCommercePurchaseWithJson(string jsonString)
	{
		#if UNITY_IPHONE
		_IgaworksCommercePurchaseWithJson(jsonString);
		#endif
	}
		
	public static void IgaworksCommercePurchaseList(ArrayList pArr)
	{
		
		#if UNITY_IPHONE
		string[] myArray =  new string[pArr.Count];
		for (int i = 0; pArr.Count > i; i++)
		{
			IgawCommerceItemModel pObject = (IgawCommerceItemModel)pArr [i];
			myArray[i] = pObject.OrderId +"&"+ pObject.ProductId +"&"+ pObject.ProductName +"&"+ pObject.Price +"&"+ pObject.Quantity +"&"+ pObject.CurrencyString +"&"+ pObject.Category;
		}
		if(pArr.Count > 0)
		_IgaworksCommercePurchaseList(myArray, pArr.Count);
		#endif
	}

	public static string currencyName(int currency) 
	{
		string res = null;
		#if UNITY_IPHONE
		res = _currencyName (currency);
		#endif
		return res;
	}


}
	
public class IgawCommerceItemModel
{
	public string OrderId { get; set; }
	public string ProductId { get; set; }
	public double Price { get; set; } 
	public string CurrencyString{ get; set; } 
	public string Category{ get; set; }
	public int Quantity{ get; set; } 
	public string ProductName{ get; set; }

	public IgawCommerceItemModel(string orderId, string productId, string productName, double price, int quantity, string currencyString, string category)
	{
		this.OrderId = orderId;
		this.ProductId = productId;
		this.Price = price; 
		this.CurrencyString = currencyString; 
		this.Category = category;
		this.Quantity = quantity;
		this.ProductName = productName;
	}

	public static IgawCommerceItemModel create(string orderID, string productID, string productName, double price, int quantity, string currencyString, string category){

		return new IgawCommerceItemModel(orderID, productID, productName, price, quantity, currencyString, category);

	}
}