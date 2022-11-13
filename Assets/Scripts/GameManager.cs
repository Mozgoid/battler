using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private BattleEvents _battleEvents;
    [SerializeField] private Unit _unitPrefab;

    int _seed;
    List<Unit> _units = new List<Unit>();

    public void Init(int seed)
    {
        _seed = seed;
        Random.InitState(seed);
        ClearAndGenerateNewBattle();
        _battleEvents.OnBattleStarted();
    }

    void ClearAndGenerateNewBattle()
    {
        foreach (var u in _units)
        {
            Destroy(u.gameObject);
        }
        _units.Clear();

        //add new units

    }

    public void Restart()
    {
        Init(_seed);
    }

    void Tick()
    {
        DoMove();
        DoAttack();
        TryFinishGame();
    }


    void DoMove()
    {

    }

    void DoAttack()
    {

    }

    void TryFinishGame()
    {
        // _battleEvents.OnBattleEnded(-1);
    }
}
