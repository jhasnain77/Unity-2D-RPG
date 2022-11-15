using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BattleUnitBase", menuName = "2D-RPG/BattleUnitBase", order = 0)]
public class BattleUnitBase : ScriptableObject {
    
    [SerializeField] string name;

    [TextArea]
    [SerializeField] string description;

    [SerializeField] Sprite partySprite;
    [SerializeField] Sprite enemySprite;

    [SerializeField] Element elementType;

    [SerializeField] int hp;
    [SerializeField] int attack;
    [SerializeField] int defense;
    [SerializeField] int mAttack;
    [SerializeField] int mDefense;
    [SerializeField] int speed;
    [SerializeField] int mp;  

    [SerializeField] List<LearnableAbility> learnableAbilities;

    public string Name {
        get { return name; }
    }

    public string Description {
        get { return description; }
    }

    public Sprite PartySprite {
        get { return partySprite; }
    }

    public Sprite EnemySprite {
        get { return enemySprite; }
    }

    public Element ElementType {
        get { return elementType; }
    }

    public int HP {
        get { return hp; }
    }
    public int Attack {
        get { return attack; }
    }
    public int Defense {
        get { return defense; }
    }
    public int MAttack {
        get { return mAttack; }
    }
    public int MDefense {
        get { return mDefense; }
    }
    public int Speed {
        get { return speed; }
    }
    public int MP {
        get { return mp; }
    }

    public List<LearnableAbility> LearnableAbilities {
        get { return learnableAbilities; }
    }
}

[System.Serializable]

public class LearnableAbility {
    [SerializeField] AbilityBase abilityBase;
    [SerializeField] int level;

    public AbilityBase Base {
        get { return abilityBase; }
    }

    public int Level {
        get { return level; }
    }
}

public enum Element {
    None,
    Flame,
    Aqua,
    Nature,
    Lightning,
    Earth,
    Air,
    Dark,
    Light
}

public class ElementChart {
    // [Attacker][Defender]
    static float [][] chart = {
        //                             NON   FLA   AQU   NAT   THU   EAR   AIR   DAR   LIG
        /*NONE*/        new float[] {   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f },
        /*FLAME*/       new float[] {   1f, 0.5f, 0.5f,   2f,   1f, 0.5f,   1f,   2f,   1f },
        /*AQUA*/        new float[] {   1f,   2f, 0.5f,   1f, 0.5f,   2f,   1f,   1f,   1f },
        /*NATURE*/      new float[] {   1f, 0.5f,   2f,   1f,   2f,   2f, 0.5f,   1f, 0.5f },
        /*THUNDER*/     new float[] {   1f,   1f,   2f, 0.5f, 0.5f, 0.5f,   2f,   2f, 0.5f },
        /*EARTH*/       new float[] {   1f,   2f,   1f,   1f,   2f,   2f, 0.5f,   1f,   1f },
        /*AIR*/         new float[] {   1f,   2f,   2f, 0.5f, 0.5f,   1f,   1f, 0.5f,   2f },
        /*DARK*/        new float[] {   1f,   1f,   1f,   1f,   2f,   1f,   2f, 0.5f,   2f },
        /*LIGHT*/       new float[] {   1f, 0.5f,   2f,   1f,   1f,   2f, 0.5f,   2f, 0.5f }
    };

    public static float GetEffectiveness(Element atkElem, Element defElem) {
        int row = (int) atkElem;
        int col = (int) defElem;
        return chart[row][col];
    }
}

