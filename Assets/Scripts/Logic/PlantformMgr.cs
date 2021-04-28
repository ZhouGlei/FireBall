using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantformMgr : MonoBehaviour,IDynaObj
{
    #region data
    public PlantformMgrData data;
    #endregion

    #region represent
    public List<Plantform> plantforms;
    #endregion

    public void DynaCreate()
    {
        Transform lastRig = this.transform;
        // 创建平台
        for(int i = 0; i < data.plantformDatas.Count; i++)
        {
            Transform plantform = Instantiate(data.plantformPrefab).transform;
            plantform.gameObject.name = $"Plantform{i}";
            plantform.SetParent(lastRig, false); // 设置父节点，就是上一个平台
            plantform.GetComponent<Plantform>().data = data.plantformDatas[i];
            plantform.GetComponent<Plantform>().DynaCreate();

            // 设置下一个平台的偏移
            if (i > 0)
            {
                plantform.Translate((-30 - 10) * transform.lossyScale.x, 0,0 );
                float sign = Random.Range(0, 2) == 0 ? 1 : -1;// 使得方向不同
                plantform.RotateAround(lastRig.position, Vector3.up, Random.Range(15, 30) * sign);
            }
            lastRig = plantform;
        }
        // 获取所有平台
        plantforms = new List<Plantform>(GetComponentsInChildren<Plantform>());
    }

    public void DynaDestroy()
    {
        if (plantforms != null)
        {
            if (plantforms.Count > 0)
            {
                // 由于是链式存储，所以删除第一个即可
                Destroy(plantforms[0].gameObject);
            }
            plantforms.Clear();
        }
    }
}
