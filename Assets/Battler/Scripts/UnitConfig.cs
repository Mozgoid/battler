using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class UnitConfig : ScriptableObject
{
    public Unit Prefab;
    public int AttackPower = 3;
    public int AttackDistance = 5;
    public float TicksToMove = 1;
    public float TicksToAttack = 10;
    public int MinHp = 2, MaxHp = 5;
}
