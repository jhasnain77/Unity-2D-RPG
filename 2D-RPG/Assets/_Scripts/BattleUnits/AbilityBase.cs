using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilityBase", menuName = "2D-RPG/AbilityBase", order = 0)]
public class AbilityBase : ScriptableObject {
    
    [SerializeField] string name;

    [TextArea]
    [SerializeField] string description;

    [SerializeField] AbilityCategory category;
    [SerializeField] Element elementType;
    [SerializeField] int power;
    [SerializeField] int accuracy;
    [SerializeField] int mp;

    public string Name {
        get { return name; }
    }

    public string Description {
        get { return description; }
    }

    public AbilityCategory Category {
        get { return category; }
    }

    public Element ElementType {
        get { return elementType; }
    }

    public int Power {
        get { return power; }
    }

    public int Accuracy {
        get { return accuracy; }
    }

    public int MP {
        get { return mp; }
    }

}

public enum AbilityCategory {
    Physical,
    Magic,
    Status
}