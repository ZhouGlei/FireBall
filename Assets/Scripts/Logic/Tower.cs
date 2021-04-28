using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour,IDynaObj
{
    #region data
    public TowerData data;
    #endregion
/*    public GameObject brickPrefab;  // 砖块预制体
    public Color[] colors = new Color[2]; // 砖块颜色
    public int height = 30; // 塔高*/

    private List<GameObject> bricks = new List<GameObject>(); // 砖块数组
    // Start is called before the first frame update


    public void DynaCreate()
    {
        CreateBricks();
    }

    /// <summary>
    /// 创建塔
    /// </summary>
    private void CreateBricks()
    {
        
        for (int i = 0; i < data.height; i++)
        {
            Transform brick = Instantiate(data.brickPrefab).transform;
            brick.Translate(0, i, 0);
            brick.GetComponent<MeshRenderer>().material.color =  data.colors[i % 2];
            brick.Rotate(0, i * 10, 0);
            brick.SetParent(this.transform, false);
            bricks.Add(brick.gameObject);
        }
        StartCoroutine(RaiseUp());
    }

    private IEnumerator RaiseUp()
    {
        this.transform.Translate(0, -data.height * this.transform.lossyScale.y, 0);
        while (this.transform.localPosition.y < 0)
        {
            this.transform.Translate(0, 5*Time.deltaTime, 0);
            yield return null;
        }
    }

    /// <summary>
    ///  砖块击中处理
    /// </summary>
    internal void DropAndDown()
    {
        StartCoroutine(DroDown());
    }

    private IEnumerator DroDown()
    {
        if (bricks.Count == 0)
        {
            //transform.root.BroadcastMessage("OnPlantformCleared");
            MVC.instance.SendEvents(Consts.OnPlatformCleared, "");
            yield break;
        }
        // 找到下层砖块
        GameObject brick = bricks[0];
        bricks.RemoveAt(0);
        
        // 播放淡出动画
        brick.GetComponent<Brick>().FadeOut();
        // 砖块向下移动一层
        for (int i = 0; i < bricks.Count; i++)
        {
            Vector3 p = bricks[i].transform.localPosition;
            p.y = p.y - 1;
            bricks[i].transform.localPosition = p;
        }
        // 销毁底下砖块
        Destroy(brick, 1f);
        yield break;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(0, 150*Time.deltaTime, 0);
    }



    public void DynaDestroy()
    {
        foreach(var brick in bricks)
        {
            Destroy(brick);

        }
        bricks.Clear();
    }
}
