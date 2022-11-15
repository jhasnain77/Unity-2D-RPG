using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnit
{

    public BattleUnitBase Base { get; set; }
    public int Level { get; set; }

    public int CurrentHP { get; set; }

    public List<Ability> Abilities { get; set; }

    public BattleUnit(BattleUnitBase thisBase, int thisLevel) {
        Base = thisBase;
        Level = thisLevel;
        CurrentHP = HP;

        Abilities = new List<Ability>();
        foreach (var ability in Base.LearnableAbilities) {
            if (ability.Level <= Level)
                Abilities.Add(new Ability(ability.Base));
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



}
