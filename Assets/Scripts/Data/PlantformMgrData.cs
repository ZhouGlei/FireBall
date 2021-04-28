using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New PlantformMgr",menuName ="PlantformMgr")]
public class PlantformMgrData : ScriptableObject
{
    public GameObject plantformPrefab; // 平台模型
    public List<PlantformData> plantformDatas; // 平台配置
}
