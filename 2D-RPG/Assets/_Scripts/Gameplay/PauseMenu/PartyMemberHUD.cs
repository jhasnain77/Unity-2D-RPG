using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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
    Vector3 originalPos;

    public void Init() {
        originalPos = transform.localPosition;
    }

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

    public void PlaySelectedAnimation() {
        this.transform.DOLocalMoveX(originalPos.x + 50f, 0.25f);
    }

    public void PlayDeselectAnimation() {
        this.transform.DOLocalMoveX(originalPos.x, 0.25f);
    }
}
