using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class UnitConfig : ScriptableObject
{
    public Unit Prefab;
    public int AttackPower = 3;
    public int AttackDistance = 5;
    public float MovePerTick = 1;
    public int MinHp = 2, MaxHp = 5;
}
