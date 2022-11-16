using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Unit : MonoBehaviour
{

    [SerializeField] BattleUnitBase _base;
    [SerializeField] int level;
    [SerializeField] bool isPlayerUnit;

    public BattleUnit BattleUnit { get; set; }

    Image image;
    Vector3 originalPos;
    Color originalColor;

    private void Awake() {
        image = GetComponent<Image>();
        originalPos = image.transform.localPosition;
        originalColor = image.color;
    }

    public void Setup() {
        BattleUnit = new BattleUnit(_base, level);
        if (isPlayerUnit)
            image.sprite = BattleUnit.Base.PartySprite;
        else
            image.sprite = BattleUnit.Base.EnemySprite;

        image.color = originalColor;

    }

}
