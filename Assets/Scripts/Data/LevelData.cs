using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Level",menuName ="Level")]
public class LevelData : ScriptableObject
{
    public int lvlId = 1;
    public PlantformMgrData PlantformMgrData;
    public PlayerData PlayerData;
}
