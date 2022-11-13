using System.Collections;
using System.Collections.Generic;
using RedBjorn.ProtoTiles;
using RedBjorn.ProtoTiles.Example;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] UnitMove _view;

    public void Init(UnitConfig unitConfig, TeamConfig teamConfig, MapEntity map)
    {
        _view?.Init(map);
    }
}
