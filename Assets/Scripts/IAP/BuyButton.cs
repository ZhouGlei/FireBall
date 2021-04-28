using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{
    public enum ProductType { 
        heart10,
        noAds,
        promotion
    }
    public ProductType productType;
    public Text showText;
    public string defaultText="0.0$";
    private void Start()
    {
        showText.text=defaultText;
        StartCoroutine(LoadPrice());
    }

    private IEnumerator LoadPrice()
    {
        while (!IAPMgr.instance.IsInitialized())
        {
            yield return null;
        }

        string loadedPrice = "";
        switch (productType)
        {
            case ProductType.heart10:
                loadedPrice = IAPMgr.instance.GetProductPriceFromStore(IAPMgr.instance.heart_10);
                break;
            case ProductType.noAds:
                loadedPrice = IAPMgr.instance.GetProductPriceFromStore(IAPMgr.instance.no_ads);
                break;
            case ProductType.promotion:
                loadedPrice = IAPMgr.instance.GetProductPriceFromStore(IAPMgr.instance.promotion);
                break;
        }
        print(loadedPrice);
        showText.text = loadedPrice;
    }

    public void ClickBuy()
    {
        switch (productType)
        {
            case ProductType.heart10:
                IAPMgr.instance.BuyHeart10();
                break;
            case ProductType.noAds:
                IAPMgr.instance.BuyNoAds();
                break;
            case ProductType.promotion:
                IAPMgr.instance.BuyPromotion();
                break;
        }
    }
}
