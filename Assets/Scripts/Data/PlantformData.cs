using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Plantform",menuName ="Plantform")]
public class PlantformData : ScriptableObject
{
    public TowerData towerData; // 塔的配置
    public List<ArchMgrData> archMgrDatas; // 圆环的配置
    public GameObject archMgr; // 圆环对象
}
