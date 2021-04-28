using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;
using Random = UnityEngine.Random;

public class ArchMgr : MonoBehaviour,IDynaObj
{
    #region data
    public ArchMgrData data;
    #endregion
    #region represent
    private List<ProBuilderMesh> archs = new List<ProBuilderMesh>(); // 保存圆环方便删除
    public bool isFade = false; // 是否下沉
    #endregion
    
    // Start is called before the first frame update
    public void SetFadeOut()
    {
        this.isFade = true;
    }
   public  void DynaCreate()
    {
        int avgDeg = 360 / data.archNum;
        for (int i = 0; i < data.archNum; i++)
        {
            CreateArch(Random.Range(20, 80), data.radius, i * avgDeg);
        }
        StartCoroutine(ChangeSpeed());
    }

    private IEnumerator ChangeSpeed()
    {
        while (true)
        {
            yield return new WaitForSeconds(4);
            data.speed = Random.Range(-120, 120);
            
        }
    }

    /// <summary>
    /// 创建圆环
    /// </summary>
    /// <param name="deg">圆环角度</param>
    /// <param name="radius">圆环半径</param>
    /// <param name="dot">圆环初始位置角度</param>
    private void CreateArch(int deg,int radius,int dot)
    {
        ProBuilderMesh pbMesh = ShapeGenerator.GenerateArch(PivotLocation.FirstVertex, deg, radius, 1, 1, 20, true, true, true, true, true);
        pbMesh.gameObject.tag = "Archs";
        pbMesh.gameObject.AddComponent<MeshCollider>();
        pbMesh.transform.Rotate(-90, 0, 0);
        pbMesh.GetComponent<MeshRenderer>().material = data.mat;

        Transform yPos = new GameObject("yPos").transform;
        yPos.Rotate(0, dot, 0);
        pbMesh.transform.SetParent(yPos, false);
        yPos.SetParent(this.transform, false);
        archs.Add(pbMesh);
    }

    // Update is called once per frame
    void Update()
    {
        DiscRotate();
        if (isFade)
        {
            FadeOut();
        }
    }

    private void FadeOut()
    {
        transform.Translate(Vector3.down * Time.deltaTime );
    }

    private void DiscRotate()
    {
        
        this.transform.Rotate(0, data.speed * Time.deltaTime, 0);


    }



    public void DynaDestroy()
    {
        foreach(var arch in archs)
        {
            Destroy(arch.gameObject);
        }
        archs.Clear();
    }
}
