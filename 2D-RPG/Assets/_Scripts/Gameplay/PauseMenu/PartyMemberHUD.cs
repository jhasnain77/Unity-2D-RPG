using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartyMemberHUD : MonoBehaviour
{
    [SerializeField] Text nameText;
    [SerializeField] Text levelText;
    [SerializeField] HPBar hpBar;
    [SerializeField] Text hpValue;
    [SerializeField] MPBar mpBar;
    [SerializeField] Text mpValue;
    [SerializeField] Image unitImage;

    BattleUnit _unit;

    public void SetData(BattleUnit unit)
    {
        _unit = unit;

        nameText.text = unit.Base.Name;
        unitImage.sprite = unit.Base.PartySprite;
        levelText.text = "Lv. " + unit.Level;
        hpValue.text = $"{unit.CurrentHP} / {unit.HP}";
        hpBar.SetHP((float)unit.CurrentHP / unit.HP);
        mpValue.text = $"{unit.CurrentMP} / {unit.MP}";
        mpBar.SetMP((float)unit.CurrentMP / unit.MP);
    }
}
