using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New ArchMgr",menuName ="ArchMgr")]
public class ArchMgrData : ScriptableObject
{
    public int radius = 8; // 圆环半径
    public int archNum = 1; // 圆环数量
    public int speed = 60; // 旋转速度
    public Material mat; // 圆环材质
}
