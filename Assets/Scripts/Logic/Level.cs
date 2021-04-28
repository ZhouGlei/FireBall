using System;
using System.Collections;
using System.Collections.Generic;
using TinyTeam.UI;
using UnityEngine;

public class Level : MonoBehaviour, IDynaObj, ViewBase
{
    #region data
    public LevelData data;
    #endregion

    #region represent
    public Player ply; // 玩家对象组件
    public PlantformMgr PlantformMgr; // 平台管理器对象组件
    public float moveTime = 2f; // 移动到下一个平台的运动的时间
    public float rotTime = .2f; // 移动到下一个平台的旋转的时间
    private int curPlantform = 0;

    public string Name => this.GetType().Name;

    public ISet<string> interestEvents => new HashSet<string>() { Consts.OnPlatformCleared};
    #endregion
    public void DynaCreate()
    {
        ply.data = data.PlayerData;
        ply.DynaCreate();

        PlantformMgr.data = data.PlantformMgrData;
        PlantformMgr.DynaCreate();

        // 设置玩家位置
        ply.transform.position = PlantformMgr.plantforms[0].transform.Find("PlayerPos").position;
        ply.transform.rotation = PlantformMgr.plantforms[0].transform.Find("PlayerPos").rotation;

    }

    public void DynaDestroy()
    {
        PlantformMgr.DynaDestroy();
        ply.DynaDestroy();
        curPlantform = 0;
    }

    /// <summary>
    /// 侦听并移动到下一个平台
    /// </summary>

    public void HandlerEvents(string eventName, object eventParams)
    {
        switch (eventName)
        {
            case Consts.OnPlatformCleared:
                {
                    ply.isMoveing = true;
                    StartCoroutine(MoveToNextPlantform());
                }
                break;
        }
    }

    private IEnumerator MoveToNextPlantform()
    {
        Plantform p = PlantformMgr.plantforms[curPlantform];
        foreach(var arch in p.archMgrs)
        {
            arch.SetFadeOut();
        }
        yield return MoveToTarget("PlantformPos");
        curPlantform++;
        if(curPlantform == PlantformMgr.plantforms.Count)
        {
            TTUIPage.ShowPage<PassedUIPage>();
            yield break;
        }
        yield return MoveToTarget("PlayerPos");
        ply.isMoveing = false;
    }

    IEnumerator MoveToTarget(string target)
    {
        Plantform p = PlantformMgr.plantforms[curPlantform];
        Transform tar = p.transform.Find(target);
        Vector3 startPos = ply.transform.position;
        Quaternion startRot = ply.transform.rotation;
        Vector3 endPos = tar.position;
        Quaternion endRot = tar.rotation;

        float tMove = 0;
        float tRot = 0;
        while (tMove < 0.95f)
        {
            tMove += 1 / moveTime * Time.deltaTime;
            tRot += 1 / rotTime * Time.deltaTime;
            ply.transform.position = Vector3.Lerp(startPos, endPos, tMove);
            ply.transform.rotation = Quaternion.Lerp(startRot, endRot, tRot);
            yield return null;
        }
        
        yield break;
    }

    
}
