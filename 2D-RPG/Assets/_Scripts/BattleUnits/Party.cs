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

    public void SwapPartyMembers(int index1, int index2) {
        BattleUnit temp = unitList[index1];
        unitList[index1] = unitList[index2];
        unitList[index2] = temp;
    }
}
