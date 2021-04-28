using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Purchasing;

public class IAPMgr : MonoBehaviour,IStoreListener
{
	public static IAPMgr instance;

	void Awake()
	{
		instance = this;
	}
	private static IStoreController m_StoreController;          // The Unity Purchasing system.
	private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.

	public  string heart_10 = "heart_10";
	public  string no_ads = "no_ads";
	public  string promotion = "promotion";


	void Start()
	{
		// If we haven't set up the Unity Purchasing reference
		if (m_StoreController == null)
		{
			// Begin to configure our connection to Purchasing
			InitializePurchasing();
		}
	}

	public void InitializePurchasing()
	{
		// If we have already connected to Purchasing ...
		if (IsInitialized())
		{
			// ... we are done here.
			return;
		}

		// Create a builder, first passing in a suite of Unity provided stores.
		var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

		// Add a product to sell / restore by way of its identifier, associating the general identifier
		// with its store-specific identifiers.
		builder.AddProduct(heart_10, ProductType.Consumable);
		builder.AddProduct(promotion, ProductType.Consumable);
		// Continue adding the non-consumable product.
		builder.AddProduct(no_ads, ProductType.NonConsumable);

		// Kick off the remainder of the set-up with an asynchrounous call, passing the configuration 
		// and this class' instance. Expect a response either in OnInitialized or OnInitializeFailed.
		UnityPurchasing.Initialize(this, builder);
	}


	public  bool IsInitialized()
	{
		// Only say we are initialized if both the Purchasing references are set.
		return m_StoreController != null && m_StoreExtensionProvider != null;
	}

	/// <summary>
	/// 购买操作
	/// </summary>
	public void BuyHeart10()
	{
		// Buy the consumable product using its general identifier. Expect a response either 
		// through ProcessPurchase or OnPurchaseFailed asynchronously.
		BuyProductID(heart_10);
	}


	public void BuyPromotion()
	{
		// Buy the consumable product using its general identifier. Expect a response either 
		// through ProcessPurchase or OnPurchaseFailed asynchronously.
		BuyProductID(promotion);
	}

	public void BuyNoAds()
	{
		// Buy the non-consumable product using its general identifier. Expect a response either 
		// through ProcessPurchase or OnPurchaseFailed asynchronously.
		BuyProductID(no_ads);
	}

	
	void BuyProductID(string productId)
	{
		// If Purchasing has been initialized ...
		if (IsInitialized())
		{
			// ... look up the Product reference with the general product identifier and the Purchasing 
			// system's products collection.
			Product product = m_StoreController.products.WithID(productId);

			// If the look up found a product for this device's store and that product is ready to be sold ... 
			if (product != null && product.availableToPurchase)
			{
				Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
				// ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
				// asynchronously.
				m_StoreController.InitiatePurchase(product);
			}
			// Otherwise ...
			else
			{
				// ... report the product look-up failure situation  
				Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
			}
		}
		// Otherwise ...
		else
		{
			// ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
			// retrying initiailization.
			Debug.Log("BuyProductID FAIL. Not initialized.");
		}
	}

	//  
	// --- IStoreListener
	//

	public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
	{
		// Purchasing has succeeded initializing. Collect our Purchasing references.
		Debug.Log("OnInitialized: PASS");

		// Overall Purchasing system, configured with products for this application.
		m_StoreController = controller;
		// Store specific subsystem, for accessing device-specific store features.
		m_StoreExtensionProvider = extensions;
	}


	public void OnInitializeFailed(InitializationFailureReason error)
	{
		// Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
		Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
	}

	public string GetProductPriceFromStore(string id)
    {
		if (m_StoreController != null && m_StoreController.products != null)
			return m_StoreController.products.WithID(id).metadata.localizedPriceString;
		else
			return "";
    }

	/// <summary>
	/// 处理内购,购买成功后谷歌商店进行处理
	/// </summary>

	public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
	{
		// A consumable product has been purchased by this user.
		if (String.Equals(args.purchasedProduct.definition.id, heart_10, StringComparison.Ordinal))
		{
			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
			// The consumable item has been successfully purchased, add 100 coins to the player's in-game score.
			DataMgr.instance.AddHeart(10);
		}
		// Or ... a non-consumable product has been purchased by this user.
		else if (String.Equals(args.purchasedProduct.definition.id, no_ads, StringComparison.Ordinal))
		{
			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
			// TODO: The non-consumable item has been successfully purchased, grant this item to the player.
			DataMgr.instance.RemoveAds();
		}
		// Or ... a subscription product has been purchased by this user.
		else if (String.Equals(args.purchasedProduct.definition.id, promotion, StringComparison.Ordinal))
		{
			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
			// TODO: The subscription item has been successfully purchased, grant this to the player.
			DataMgr.instance.AddHeart(20);
			DataMgr.instance.AddDiamond(500);
			DataMgr.instance.RemoveAds();
			DataMgr.instance.AddSkin(0);
		}
		// Or ... an unknown product has been purchased by this user. Fill in additional products here....
		else
		{
			Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
		}

		// Return a flag indicating whether this product has completely been received, or if the application needs 
		// to be reminded of this purchase at next app launch. Use PurchaseProcessingResult.Pending when still 
		// saving purchased products to the cloud, and when that save is delayed. 
		return PurchaseProcessingResult.Complete;
	}


	public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
	{
		// A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing 
		// this reason with the user to guide their troubleshooting actions.
		Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
	}
}
