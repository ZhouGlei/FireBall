using System.Collections;
using System.Collections.Generic;
using TinyTeam.UI;
using UnityEngine;
using UnityEngine.UI;

public class FinishedUIPage : TTUIPage
{
    protected override string uiPath => "UIPage/FinishedUI";

    public FinishedUIPage()
        : base(UIType.Normal, UIMode.DoNothing, UICollider.None) { }

    public override void Awake(GameObject go)
    {
        transform.Find("BackHome").GetComponent<Button>().onClick.AddListener(() =>
        {
            ShowPage<MainUIPage>();
            DataMgr.instance.curLvl = 0;
            Hide();
        });
    }
}
