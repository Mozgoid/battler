using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private BattleEvents _battleEvents;
    [SerializeField] private TeamConfig[] _teams;

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

        foreach (var t in _teams)
        {
            foreach (var unitConf in t.Units)
            {
                var newUnit = Instantiate(unitConf.Prefab, this.transform);
                newUnit.Init(unitConf, t);
                //TODO: set position
                _units.Add(newUnit);
            }
        }

        //shuffle
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
