using System.Collections;
using System.Collections.Generic;
using TinyTeam.UI;
using UnityEngine;

public class DataMgr : MonoBehaviour
{
    public static DataMgr instance;
    private void Awake()
    {
        instance = this;
    }
    [HideInInspector]
    public int heartNum = 0; // 爱心数量
    [HideInInspector]
    public int diamondNum = 0; // 钻石数量
    [HideInInspector]
    public bool noAds = false; // 无广告
    [HideInInspector]
    public HashSet<int> skinIds = new HashSet<int>(); // 皮肤

    public int curLvl = 0; // 当前关卡
    public LevelData[] lvls; // 关卡配置


    private void Start()
    {
        TTUIPage.ShowPage<LogoUIPage>();
        heartNum = PlayerPrefs.GetInt("heartNum", 0);
        diamondNum = PlayerPrefs.GetInt("diamondNum", 0);
        noAds = PlayerPrefs.GetInt("noAds") == 1;
    }
    // 以下所有操作均是购买成功后的事件处理程序

    public void AddSkin(int skinId)
    {
        skinIds.Add(skinId);
        string s = string.Join(",", skinIds);
        PlayerPrefs.SetString("skinIds", s);
        PlayerPrefs.Save();
    }

    public void AddHeart(int num)
    {
        heartNum += num;
        PlayerPrefs.SetInt("heartNum", heartNum);
        PlayerPrefs.Save();
    }

    public void RemoveAds()
    {
        noAds = true;
        PlayerPrefs.SetInt("noAds", noAds == true ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void AddDiamond(int num)
    {
        diamondNum += num;
        PlayerPrefs.SetInt("diamondNum", diamondNum);
        PlayerPrefs.Save();
    }


    public void AddPormotion()
    {
        AddSkin(0);
        AddHeart(20);
        RemoveAds();
        AddDiamond(500);
    }
    
}
