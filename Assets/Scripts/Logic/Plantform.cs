using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plantform : MonoBehaviour, IDynaObj
{
    #region data
    public PlantformData data;
    #endregion

    #region represent
    public List<ArchMgr> archMgrs;
    public Tower tower;
    #endregion

    public void DynaCreate()
    {
        for(int i = 0; i < data.archMgrDatas.Count; i++)
        {
           ArchMgr archMgr = Instantiate(data.archMgr, this.transform, false).GetComponent<ArchMgr>();
            archMgr.data = data.archMgrDatas[i];
            archMgr.DynaCreate();
            archMgrs.Add(archMgr);
        }
        tower.data = data.towerData;
        tower.DynaCreate();
    }

    public void DynaDestroy()
    {
        tower.DynaDestroy();
        foreach(var arch in archMgrs)
        {
            arch.DynaDestroy();
        }
        archMgrs.Clear();
    }
}
