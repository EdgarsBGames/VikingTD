using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveData", menuName = "Wave/WaveData")]
public class Wave : ScriptableObject
{
    public string waveName;
    public Transform[] enemy;
    public int enemyCount;
    public float spawnRate;

}
