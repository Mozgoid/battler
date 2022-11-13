using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class BattleEvents : ScriptableObject
{
    public UnityAction<int> BattleStartRequested = (seed) => Debug.Log($"BattleStartRequested with seed {seed}");
    public UnityAction OnBattleStarted = () => Debug.Log("battle started");
    public UnityAction<TeamConfig> OnBattleEnded = (winnerTeam) => Debug.Log($"battle has ended. Winner: {winnerTeam}");
}
