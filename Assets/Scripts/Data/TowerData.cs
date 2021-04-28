using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Tower",menuName ="Tower")]
public class TowerData : ScriptableObject
{
    public GameObject brickPrefab; // 砖块模型
    public int height = 15; // 塔高
    public int eachRot = 10; // 每层扭转角度
    public Color[] colors = new Color[2];  // 砖块颜色
}
