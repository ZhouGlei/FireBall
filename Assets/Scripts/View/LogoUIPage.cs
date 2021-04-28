using System;
using System.Collections;
using System.Collections.Generic;
using TinyTeam.UI;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class LogoUIPage : TTUIPage
{
    protected override string uiPath => "UIPage/LogoUI";

    public LogoUIPage()
        : base(UIType.Normal, UIMode.DoNothing, UICollider.None) { }

    public override void Awake(GameObject go)
    {
        TTUIRoot.Instance.StartCoroutine(ShowLogo());
    }

    private IEnumerator ShowLogo()
    {
        var logoText =this.transform.Find("LogoText").GetComponent<CanvasGroup>();

        var logoImg = this.transform.Find("LogoImg").GetComponent<CanvasGroup>();

        while (logoText.alpha < 0.95f)
        {
            logoText.alpha = Mathf.Lerp(logoText.alpha, 1, 1 / 1.5f * Time.deltaTime);
            yield return null;
        }
        while (logoImg.alpha < 0.95f)
        {
            logoImg.alpha = Mathf.Lerp(logoImg.alpha, 1, 1 / 1.5f * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(1);
        Hide();
        TTUIPage.ShowPage<MainUIPage>();
    }
}
