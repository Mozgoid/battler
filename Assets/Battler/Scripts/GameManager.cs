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

    int _seed;
    List<Unit> _units = new List<Unit>();

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
                newUnit.Init(unitConf, t, _mapHolder.MapEntity);

                var tile = allMapTiles[_units.Count];
                newUnit.transform.position = _mapHolder.MapEntity.WorldPosition(tile);

                _units.Add(newUnit);
            }
        }

        _units.Shuffle();
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
