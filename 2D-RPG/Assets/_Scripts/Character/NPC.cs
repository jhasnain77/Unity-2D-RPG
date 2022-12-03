using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NPC : MonoBehaviour, Interactable
{

    [SerializeField] string name;
    [SerializeField] BattleUnit battleUnit;
    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] List<DialogueContainer> dialogueContainers;
    private int interactions = 0;

    public event Action OnStartDialogue;

    public void Interact() {
        dialogueManager.Setup(dialogueContainers[interactions]);
        Debug.Log("Interacting with NPC");
        if (interactions < dialogueContainers.Count - 1) {
            interactions++;
        }
        OnStartDialogue();
    }
}
