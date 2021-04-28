using System.Collections;
using System.Collections.Generic;
using TinyTeam.UI;
using UnityEngine;
using UnityEngine.UI;

public class PromotionPopupPage : TTUIPage
{
    protected override string uiPath => "UIPage/PromotionPopup";

    public PromotionPopupPage()
        : base(UIType.PopUp, UIMode.DoNothing, UICollider.None) { }
    public override void Awake(GameObject go)
    {
        transform.Find("Mask").GetComponent<Button>().onClick.AddListener(() =>
        {
            Hide();
            ShowPage<MainUIPage>();
        });
    }
}
