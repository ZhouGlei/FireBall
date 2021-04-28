using System.Collections;
using System.Collections.Generic;
using TinyTeam.UI;
using UnityEngine;
using UnityEngine.UI;

public class PassedUIPage : TTUIPage
{
    protected override string uiPath => "UIPage/PassedUI";

    public PassedUIPage()
        : base(UIType.Normal, UIMode.DoNothing, UICollider.None) { }

    public override void Awake(GameObject go)
    {
        transform.Find("Next").GetComponent<Button>().onClick.AddListener(() =>
        {
            MVC.instance.SendEvents("OnLevelUp","");
            Hide();
        });
    }
}
