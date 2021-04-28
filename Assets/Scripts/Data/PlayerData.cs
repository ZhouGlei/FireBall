using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Player",menuName ="Player")]
public class PlayerData : ScriptableObject
{
    public GameObject playerPrefab; // 玩家模型
    public GameObject bulletPrefab; // 子弹模型
    public int speed = 10; // 子弹射速
}
