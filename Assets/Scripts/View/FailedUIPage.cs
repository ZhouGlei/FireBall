using System.Collections;
using System.Collections.Generic;
using TinyTeam.UI;
using UnityEngine;
using UnityEngine.UI;

public class FailedUIPage : TTUIPage,ViewBase
{
    protected override string uiPath =>"UIPage/FailedUI";

    public string Name => this.GetType().Name;

    public ISet<string> interestEvents => new HashSet<string>() {
        Consts.OnRewardAdsResulted
    };

    public FailedUIPage()
        : base(UIType.Normal, UIMode.DoNothing, UICollider.None) { }

    public override void Awake(GameObject go)
    {
        
        transform.Find("ReStart").GetComponent<Button>().onClick.AddListener(() =>
        {
            MVC.instance.SendEvents(Consts.OnLevelFailed,"");
            Hide();
        });

        transform.Find("Resurgence").gameObject.AddComponent<UnityAdsButton>();

        MVC.instance.RegisterView(this);
    }

    public void HandlerEvents(string eventName, object eventParams)
    {
        switch (eventName)
        {
            case Consts.OnRewardAdsResulted:
                {
                    Hide();
                    // 切换失败页面时传入的Player对象
                    Player player = this.data as Player;
                    player.GetComponent<Player>().isGameOver = false;
                }
                break;
        }
    }
}
