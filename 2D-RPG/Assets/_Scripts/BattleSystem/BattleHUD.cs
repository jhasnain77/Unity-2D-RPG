using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    [SerializeField] Text nameText;
    [SerializeField] Text levelText;
    [SerializeField] HPBar hpBar;
    [SerializeField] Text hpValue;
    [SerializeField] MPBar mpBar;
    [SerializeField] Text mpValue;

    BattleUnit _unit;

    public void SetData(BattleUnit unit)
    {
        _unit = unit;

        nameText.text = unit.Base.Name;
        levelText.text = "Lv. " + unit.Level;
        hpValue.text = $"{unit.CurrentHP} / {unit.HP}";
        hpBar.SetHP((float)unit.CurrentHP / unit.HP);
        mpValue.text = $"{unit.CurrentMP} / {unit.MP}";
        mpBar.SetMP((float)unit.CurrentMP / unit.MP);
    }

    public IEnumerator UpdateHP()
    {
        hpValue.text = $"{_unit.CurrentHP} / {_unit.HP}";
        yield return hpBar.SetHPSmooth((float)_unit.CurrentHP / _unit.HP);
    }

    public IEnumerator UpdateMP()
    {
        mpValue.text = $"{_unit.CurrentMP} / {_unit.MP}";
        yield return mpBar.SetMPSmooth((float)_unit.CurrentMP / _unit.MP);
    }


}
