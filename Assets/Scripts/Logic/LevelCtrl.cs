using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCtrl : MonoBehaviour
{
    #region Data
    public LevelData[] data;
    #endregion

    #region represent
    private AudioSource bgm;
    public int curLvl = 0;
    public Level lvl; // 关卡对象
    #endregion

    /// <summary>
    /// 游戏对象创建的起点，由UIRoot调用
    /// </summary>
    void OnStart()
    {
        lvl.data = data[curLvl];
        lvl.DynaCreate();
        bgm = transform.GetComponent<AudioSource>();
        bgm.Play();
    }
    private void OnDestroy()
    {
        lvl.DynaDestroy();
    }

    /// <summary>
    /// 通关UI点击触发
    /// </summary>
    /// <param name="panel"></param>
    public void Passed(RectTransform panel)
    {
        panel.gameObject.SetActive(false);
        lvl.DynaDestroy();
        StartCoroutine(_Passed(panel));
    }

    private IEnumerator _Passed(RectTransform panel)
    {
        yield return null;
        curLvl++;
        if(curLvl == data.Length)
        {
            yield break;
        }

        lvl.data = data[curLvl];
        lvl.DynaCreate();
    }

    public void Failed(RectTransform panel)
    {
        panel.gameObject.SetActive(false);
        lvl.DynaDestroy();

        StartCoroutine(_Failed(panel));
    }

    private IEnumerator _Failed(RectTransform panel)
    {
        yield return null;
        lvl.DynaCreate();
    }
}
