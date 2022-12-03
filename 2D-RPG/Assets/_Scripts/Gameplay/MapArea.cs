using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapArea : MonoBehaviour
{
    [SerializeField] List<BattleUnit> wildUnits;


    public BattleUnit GetRandomWildUnit() {
        var wild = wildUnits[Random.Range(0, wildUnits.Count)];
        wild.Init();
        return wild;
    }
}
