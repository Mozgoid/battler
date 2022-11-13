using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class BattleEvents : ScriptableObject
{
    public UnityAction OnBattleStarted = () => Debug.Log("battle started");
    public UnityAction<int> OnBattleEnded = (winnerTeam) => Debug.Log($"battle has ended. Winner: {winnerTeam}");
}
