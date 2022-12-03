using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Party : MonoBehaviour
{
    [SerializeField] public List<BattleUnit> unitList;

    private void Start() {
        foreach (var unit in unitList) {
            unit.Init();
        }
    }

    public BattleUnit GetHealthyUnits() {
        return unitList.Where(x => x.HP > 0).FirstOrDefault();
    }

    public List<BattleUnit> GetAllUnits() {
        return unitList;
    }
}
