using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRoot : MonoBehaviour
{
    public RectTransform root;
    public RectTransform logo;
    public RectTransform levelPassed;
    public RectTransform levalFailed;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShowLogo());
    }

    void OnLevelPassed(int lvlId)
    {
        print($"passed {lvlId}");
        levelPassed.gameObject.SetActive(true);
    }

    void OnLevelFailed()
    {
        print("failed");
    }

    private IEnumerator ShowLogo()
    {
        /*CanvasGroup logoImg = logo.Find("LogoImg").GetComponent<CanvasGroup>();
        CanvasGroup logoText = logo.Find("LogoText").GetComponent<CanvasGroup>();
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

        yield return new WaitForSeconds(2);
        logo.gameObject.SetActive(false);*/
        yield return null;


        transform.root.BroadcastMessage("OnStart");
    }
}
