using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TinyTeam.UI;
using UnityEngine;
using UnityEngine.UI;


public class MainUIPage : TTUIPage,ViewBase
{

    #region data
    bool isSettingOpened = false;
    #endregion

    #region pres
    private Level lvl;
    #endregion
    #region ViewBase

    public string Name => this.GetType().Name;
    public ISet<string> interestEvents
    {
        get => new HashSet<string>() { Consts.OnDiamondUpdated,Consts.OnLevelUp, Consts.OnLevelFailed,Consts.Finshed };
    }

    #endregion

    #region TTUI
    protected override string uiPath => "UIPage/MainUI";

    public MainUIPage()
        : base(UIType.Normal, UIMode.DoNothing, UICollider.None) { }

    private IEnumerator LoadLevel()
    {
        yield return null;
        lvl.data = DataMgr.instance.lvls[DataMgr.instance.curLvl];
        lvl.DynaCreate();
    }
    // 关卡初始化
    private void init()
    {
        GameObject lvlPrefab = Resources.Load<GameObject>("Level/Level");
        lvl = GameObject.Instantiate(lvlPrefab).GetComponent<Level>();
        MVC.instance.RegisterView(lvl);
    }
    public override void Awake(GameObject go)
    {
        init();
        MVC.instance.RegisterView(this);
        
        transform.Find("Bottom/StartButton").GetComponent<Button>().onClick.AddListener(() =>
        {
            Hide();
            if (lvl)
            {
                //lvl.DynaDestroy();
            }
            TTUIRoot.Instance.StartCoroutine(LoadLevel());
        });

        #region 设置按钮特效
        Transform tfSetting = transform.Find("LeftTop/Setting");
        Transform tfMusic = transform.Find("LeftTop/Music");
        Transform tfShake = transform.Find("LeftTop/Shake");

        tfSetting.GetComponent<Button>().onClick.AddListener(() =>
        {
            var btnSetting = tfSetting.GetComponent<Button>();
            btnSetting.enabled = false;
            if (!isSettingOpened)
            {
                tfMusic.DOLocalMoveY(-200, .5f).SetRelative(); 
                tfShake.DOLocalMoveY(-400, 1f).SetRelative().OnComplete(()=> {
                    btnSetting.enabled = true;
                    isSettingOpened = true;
                });
                
            }
            else
            {
                tfMusic.position = tfSetting.position;
                tfShake.position = tfSetting.position;
                btnSetting.enabled = true;
                isSettingOpened = false;
            }
        });
        #endregion

        #region 促销特效
        transform.Find("RightTop/Promotion").GetComponent<Button>().onClick.AddListener(() =>
        {
            ShowPage<PromotionPopupPage>();
        });
        #endregion
    }

    
    // 处理事件
    public void HandlerEvents(string eventName, object eventParams)
    {

        switch (eventName)
        {
            case Consts.OnDiamondUpdated:
                {
                    OnDiamondUpdated();
                }
                break;
            case Consts.OnLevelUp:
                {
                    DataMgr.instance.curLvl++;
                    if(DataMgr.instance.curLvl == DataMgr.instance.lvls.Length)
                    {
                        MVC.instance.SendEvents(Consts.Finshed, "");
                        break;
                    }
                    DealLevel();
                }
                break ;
            case Consts.OnLevelFailed:
                {
                    DealLevel();
                }
                break;
            case Consts.Finshed:
                {

                    ShowPage<FinishedUIPage>();
                    Finished();
                }
                break;
        }
    }
    // 关卡升级
    private void DealLevel()
    {
        lvl.DynaDestroy();
        TTUIRoot.Instance.StartCoroutine(LoadLevel());
    }
    // 通关
    private void Finished()
    {
        lvl.DynaDestroy();
        init();
    }
    // 钻石变化
    private void OnDiamondUpdated()
    {
        transform.Find("RightTop/Diamond/Text").GetComponent<Text>().DOText(DataMgr.instance.diamondNum.ToString(), 1f);
    }
    #endregion

    
}
