using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RedBjorn.ProtoTiles;
using RedBjorn.ProtoTiles.Example;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] BattleEvents _battleEvents;
    [SerializeField] TeamConfig[] _teams;
    [SerializeField] ExampleStart _mapHolder;
    [SerializeField] float _tickTime = 0.1f;

    HashSet<TeamConfig> _aliveTeams = new HashSet<TeamConfig>();
    int _seed;
    List<Unit> _units = new List<Unit>();
    public IReadOnlyList<Unit> AllUnits => _units;

    public void Start()
    {
        _battleEvents.BattleStartRequested += Init;
    }

    public void Init(int seed)
    {
        _seed = seed;
        Random.InitState(seed);
        ClearAndGenerateNewBattle();
        _battleEvents.OnBattleStarted();
        UpdateAliveTeams();

        StopAllCoroutines();
        StartCoroutine(Ticks());
    }

    void ClearAndGenerateNewBattle()
    {
        foreach (var u in _units)
        {
            Destroy(u.gameObject);
        }
        _units.Clear();

        var allMapTiles = _mapHolder.MapEntity.TileKeys.ToList();
        allMapTiles.Shuffle();

        foreach (var t in _teams)
        {
            foreach (var unitConf in t.Units)
            {
                var newUnit = Instantiate(unitConf.Prefab, this.transform);
                newUnit.Init(unitConf, t, _mapHolder.MapEntity, allMapTiles[_units.Count], this);


                _units.Add(newUnit);
            }
        }

        _units.Shuffle();
    }

    public void Restart()
    {
        Init(_seed);
    }

    IEnumerator Ticks()
    {
        while (!TryFinishGame())
        {
            DoMove();
            DoAttack();
            yield return new WaitForSeconds(_tickTime);
        }
    }


    void DoMove()
    {
        foreach (var u in _units)
        {
            u.TryMove();
        }
    }

    void DoAttack()
    {
        foreach (var u in _units)
        {
            u.TryAttack();
        }
    }

    void UpdateAliveTeams()
    {
        _aliveTeams.Clear();
        foreach (var u in _units)
        {
            if (u.IsAlive)
            {
                _aliveTeams.Add(u.Team);
            }
        }
    }

    bool TryFinishGame()
    {
        UpdateAliveTeams();
        if (_aliveTeams.Count > 1)
            return false;
        
        foreach (var team in _teams)
        {
            if (_aliveTeams.Contains(team))
            {
                _battleEvents.OnBattleEnded(team);
                return true;
            }
        }

        _battleEvents.OnBattleEnded(null);
        return true;
    }
}
