using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Monetization;
using UnityEngine.UI;

[RequireComponent(typeof(Button))] //需先引入Button组件
public class UnityAdsButton : MonoBehaviour
{

    public string placementId = "ad_resurgence";
    private Button adButton;

#if UNITY_IOS
    private string gameId = "4093234";
#elif UNITY_ANDROID
    private string gameId = "4093235";
#endif
    // Start is called before the first frame update
    void Start()
    {
        adButton = GetComponent<Button>();
        if (adButton)
        {
            adButton.onClick.AddListener(ShowAd);
        }
        // 判断是否开启Monetization
        if (Monetization.isSupported)
        {
            Monetization.Initialize(gameId, true);
        }
    }

    private void ShowAd()
    {
        // 广告回调函数
        ShowAdCallbacks options = new ShowAdCallbacks();
        options.finishCallback = HandleResult;
        // 广告对象
        ShowAdPlacementContent ad = Monetization.GetPlacementContent(placementId) as ShowAdPlacementContent;
        ad.Show(options);
    }

    private void HandleResult(ShowResult finishState)
    {
        if(finishState == ShowResult.Finished)
        {
            Debug.Log("ad play Finished");
            MVC.instance.SendEvents(Consts.OnRewardAdsResulted,"");
        }else if(finishState == ShowResult.Skipped)
        {
            Debug.Log("ad play skipped");
        }else if(finishState == ShowResult.Failed)
        {
            Debug.Log("ad play failed");
        }

        
    }

}
