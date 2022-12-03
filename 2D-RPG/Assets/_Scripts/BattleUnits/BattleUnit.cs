using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class BattleUnit
{

    [SerializeField] BattleUnitBase _base;
    [SerializeField] int level;

    public BattleUnitBase Base {
        get {
            return _base;
        }
    }

    public int Level {
        get {
            return level;
        }
    }

    public int CurrentHP { get; set; }
    public int CurrentMP { get; set; }

    public List<Ability> Abilities { get; set; }

    public void Init()
    {
        CurrentHP = HP;
        CurrentMP = MP;

        Abilities = new List<Ability>();
        foreach (var ability in Base.LearnableAbilities)
        {
            if (ability.Level <= Level)
                Abilities.Add(new Ability(ability.Base));

            if (Abilities.Count >= 4)
                break;
        }
    }

    public int HP {
        get { return Mathf.FloorToInt(((2 * Base.HP) * Level) / 100f) + Level + 10; }
    }

    public int Attack {
        get { return Mathf.FloorToInt(((2 * Base.Attack) * Level) / 100f) + 5; }
    }

    public int Defense {
        get { return Mathf.FloorToInt(((2 * Base.Defense) * Level) / 100f) + 5; }
    }

    public int MAttack {
        get { return Mathf.FloorToInt(((2 * Base.MAttack) * Level) / 100f) + 5; }
    }

    public int MDefense {
        get { return Mathf.FloorToInt(((2 * Base.MDefense) * Level) / 100f) + 5; }
    }

    public int Speed {
        get { return Mathf.FloorToInt(((2 * Base.Speed) * Level) / 100f) + 5; }
    }
    
    public int MP {
        get { return Mathf.FloorToInt(((2 * Base.MP) * Level) / 100f) + Level + 20; }
    }

    public DamageDetails TakeDamage(Ability ability, BattleUnit attacker) {
        float critical = 1f;
        if (Random.value * 100f <= 6.25) {
            critical = 2f;
        }

        float element = ElementChart.GetEffectiveness(ability.Base.ElementType, this.Base.ElementType);

        var damageDetails = new DamageDetails() {
            Element = element,
            Critical = critical,
            Fainted = false
        };

        float modifiers = element * critical;

        float attack;
        float defense;

        switch (ability.Base.Category) {
            case AbilityCategory.Physical:
                attack = attacker.Attack;
                defense = Defense;
                break;
            case AbilityCategory.Magic:
                attack = attacker.MAttack;
                defense = MDefense;
                break;
            case AbilityCategory.Status:
                modifiers = 0f;
                attack = 0f;
                defense = 1f;
                break;
            default:
                attack = attacker.Attack;
                defense = Defense;
                break;
        }

        float a = ((2 * attacker.Level) + 10) / 250f;
        float d = a * ability.Base.Power * ((float)attack / defense) + 2;
        int damage = Mathf.FloorToInt(d * modifiers);

        CurrentHP -= damage;
        if (CurrentHP <= 0) {
            CurrentHP = 0;
            damageDetails.Fainted = true;
        }

        return damageDetails;
    }

    public Ability GetRandomAbility() {
        int r = Random.Range(0, Abilities.Count);
        return Abilities[r];
    }

}

public class DamageDetails {
    public bool Fainted { get; set; }

    public float Critical { get; set; }

    public float Element { get; set; }
}