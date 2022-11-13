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

    int _seed;
    List<Unit> _units = new List<Unit>();
    public IReadOnlyList<Unit> AllUnits => _units;

    public void Start()
    {
        Init(0);
    }

    public void Init(int seed)
    {
        _seed = seed;
        Random.InitState(seed);
        ClearAndGenerateNewBattle();
        _battleEvents.OnBattleStarted();

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

    bool TryFinishGame()
    {
        return false;
        // _battleEvents.OnBattleEnded(-1);
    }
}
