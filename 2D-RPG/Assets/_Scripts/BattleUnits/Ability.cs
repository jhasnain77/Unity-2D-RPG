using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability
{

    public AbilityBase Base { get; set; }
    public int MP { get; set; }

    public Ability(AbilityBase thisBase) {
        Base = thisBase;
        MP = thisBase.MP;
    }

}
