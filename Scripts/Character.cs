using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharaterData", menuName = "Character/CharacterData")]
public class Character : ScriptableObject
{
    [Header("Stats")]
    public float MaxHealth;
   // public float CurrentHealth;
    public float Damage;

    [Header("Movement Data")]
    public float MoveSpeed;
    public float RotationSpeed;

    [Header("Rewards")]
    public int MinReward;
    public int MaxReward;
}
