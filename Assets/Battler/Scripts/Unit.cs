using System.Collections;
using System.Collections.Generic;
using RedBjorn.ProtoTiles;
using RedBjorn.ProtoTiles.Example;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public TeamConfig Team { get; private set; }
    public UnitConfig UnitConfig { get; private set; }

    private int _hp;
    public int Hp 
    {
        get => _hp;
        private set
        {
            if (value < _hp)
            {
                _animator.SetTrigger("TakeDamage");
            }
            _hp = value;
            _animator.SetInteger("Hp", _hp);
        } 
    }

    public bool IsAlive => Hp > 0;
    private int _ticksWithoutMove = 0;
    private int _ticksWithoutAttack = 0;
    private GameManager _gameManager;


    TileEntity _currentTile = null;
    public TileEntity CurrentTile
    {
        get => _currentTile;
        set
        {
            if (_currentTile != value)
            {
                if (_currentTile != null)
                {
                    _currentTile.ObstacleCount--;
                }
                _currentTile = value;
                if (_currentTile != null)
                {
                    _currentTile.ObstacleCount++;
                }
            }
            else
            {
                Debug.LogWarning("Asdasd");
            }
        }
    }

    private MapEntity Map { get; set; }
    [SerializeField] Renderer _body;
    [SerializeField] Animator _animator;

    public void Init(UnitConfig unitConfig, TeamConfig teamConfig, MapEntity map, Vector3Int tilePos, GameManager gameManager)
    {
        _gameManager = gameManager;
        UnitConfig = unitConfig;
        Map = map;
        Team = teamConfig;
        _body.material.color = Team.Color;

        CurrentTile = map.Tile(tilePos);
        this.transform.position = map.WorldPosition(CurrentTile);

        Hp = Random.Range(UnitConfig.MinHp, UnitConfig.MaxHp + 1);
    }

    public Unit ClosestEnemyUnit()
    {
        var closesDistance = float.MaxValue;
        Unit closest = null;

        foreach (var other in _gameManager.AllUnits)
        {
            if (other.IsAlive && other.Team != this.Team)
            {
                var distance = Map.Distance(CurrentTile, other.CurrentTile);
                if (distance < closesDistance)
                {
                    closest = other;
                }
            }
        }
        return closest;
    }

    public bool CanAttack(Unit other)
    {
        if (other == null || !other.IsAlive || !this.IsAlive)
            return false;

        var distance = Map.Distance(CurrentTile, other.CurrentTile);
        return UnitConfig.AttackDistance >= distance;
    }

    public void TryMove()
    {
        if (!IsAlive)
            return;

        _ticksWithoutMove++;

        if (_ticksWithoutMove >= UnitConfig.TicksToMove)
        {
            var closestEnemy = ClosestEnemyUnit();
            if (closestEnemy != null)
            {
                var distance = Map.Distance(CurrentTile, closestEnemy.CurrentTile);
                if (distance > UnitConfig.AttackDistance)
                {
                    _ticksWithoutMove = 0;
                    closestEnemy.CurrentTile.ObstacleCount--; // we need it to set pass
                    var path = Map.PathTiles(Map.WorldPosition(CurrentTile), Map.WorldPosition(closestEnemy.CurrentTile), float.MaxValue);
                    closestEnemy.CurrentTile.ObstacleCount++;

                    if (path.Count < 2)
                    {
                        Debug.LogError("can't go to enemy");
                        return;
                    }

                    var nextTile = path[1];
                    if (nextTile != closestEnemy.CurrentTile)
                    {
                        CurrentTile = nextTile;
                        // TODO: move animation
                        this.transform.position = Map.WorldPosition(CurrentTile);
                    }
                }
            }
        }
    }


    public void TryAttack()
    {
        if (!IsAlive)
            return;

        _ticksWithoutAttack++;
        if (_ticksWithoutAttack >= UnitConfig.TicksToAttack)
        {
            var closestEnemy = ClosestEnemyUnit();
            if (closestEnemy != null)
            {
                var distance = Map.Distance(CurrentTile, closestEnemy.CurrentTile);
                if (distance <= UnitConfig.AttackDistance)
                {
                    _animator.SetTrigger("Attack");
                    _ticksWithoutAttack = 0;
                    closestEnemy.Hp -= UnitConfig.AttackPower;
                }
            }
        }
    }
}
